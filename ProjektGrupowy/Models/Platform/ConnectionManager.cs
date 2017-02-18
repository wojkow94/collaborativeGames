using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Platform
{
    public class Connection
    {
        public string ConnectionId;
        public int GameId;
        public int PlayerId;
    }

    static public class ConnectionManager
    {
        static private Dictionary<int, List<Connection>> Connections = new Dictionary<int, List<Connection>>();

        static public void AddConnection(Connection connection)
        {
            if (Connections.ContainsKey(connection.GameId) == false)
            {
                Connections[connection.GameId] = new List<Connection>();
            }
            Connections[connection.GameId].Add(connection);
        }

        static public void RemoveConnection(string connectionId, Action<int, int> onRemove)
        {
            Connection toRemove = null;
            int gameId = -1;
            foreach (var game in Connections)
            {
                if (toRemove == null)
                {
                    foreach (var connection in game.Value)
                    {
                        if (connection.ConnectionId == connectionId)
                        {
                            gameId = game.Key;
                            toRemove = connection;
                            break;
                        }
                    }
                }
            }
            if (gameId >= 0)
            {
                onRemove(gameId, toRemove.PlayerId);
                Connections[gameId].Remove(toRemove);
            }
        }

        static public IEnumerable<Connection> GetConnections(int gameId)
        {
            if (Connections.ContainsKey(gameId)) return Connections[gameId];
            return null;
        }

        static public IEnumerable<Connection> GetConnectionsExcept(int gameId, string connectionId)
        {
            if (Connections.ContainsKey(gameId)) return Connections[gameId].Where(c => c.ConnectionId != connectionId);
            return null;
        }



        static public bool IsOnline(int gameId, int playerId)
        {
            if (Connections.ContainsKey(gameId))
            {
                return Connections[gameId].Where(c => c.PlayerId == playerId).FirstOrDefault() != null;
            }
            return false;
        }
    }
}