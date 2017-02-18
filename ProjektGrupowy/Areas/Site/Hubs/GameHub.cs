using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjektGrupowy.Models.Core;
using ProjektGrupowy.Models.Platform;

namespace ProjektGrupowy.Areas.Game.Hubs
{
    

    public class GameHub : Hub
    {
        private enum Events {  OnJoinPLayer = 10, OnLeavePlayer = 11 };

        private void NotifyRest(int gameId, int eventId, Object eventObj)
        {
            foreach (var connection in ConnectionManager.GetConnectionsExcept(gameId, Context.ConnectionId))
            {
                Clients.Client(connection.ConnectionId).onEvent(eventId, eventObj);
            }
        }

        private void NotifyAll(int gameId, int eventId, Object eventObj)
        {
            foreach (var connection in ConnectionManager.GetConnections(gameId))
            {
                Clients.Client(connection.ConnectionId).onEvent(eventId, eventObj);
            }
        }

        public void onEvent(int gameId, int eventId, Object eventObj)
        {
            NotifyRest(gameId, eventId, eventObj);
        }

        public void addConnection(string id, int gameId, int playerId)
        {
            ConnectionManager.AddConnection(new Connection
            {
                ConnectionId = id,
                PlayerId = playerId,
                GameId = gameId
            });

            NotifyAll(gameId, (int)Events.OnJoinPLayer, new { playerId = playerId });
            Debug.WriteLine(id + " has connected");
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Debug.WriteLine(Context.ConnectionId + " has disconnected");

            ConnectionManager.RemoveConnection(Context.ConnectionId, (gameId, playerId) =>
            {
                NotifyAll(gameId, (int)Events.OnLeavePlayer, new { playerId = playerId });
            });
            
            return base.OnDisconnected(stopCalled);
        }
    }
}