using ProjektGrupowy.Models.Game.Common;
using ProjektGrupowy.Models.Game.Instances;
using ProjektGrupowy.Models.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ProjektGrupowy.Models.Database.DAO
{
    public class GameInstanceDAO : DataAccessObject
    {

        public void AddGameInstance(GameInstance game, string name, string password)
        {
            XmlGameInstance xml = new XmlGameInstance();

            DBGameInstance dbGame = new DBGameInstance
            {
                Created = DateTime.Now,
                Source = "",
                Name = name,
                Password = password == null ? "" : Crypto.HashPassword(password),
                DefinitionID = game.Definition.Id
            };
            db.GameInstances.Add(dbGame);
            db.SaveChanges();

            game.Id = dbGame.ID;
            dbGame.Source = xml.Serialize(game);
            db.SaveChanges();
        }

        public void UpdateGameInstance(GameInstance game)
        {
            DBGameInstance dbGame = GetGameInstance(game.Id);
            XmlGameInstance xml = new XmlGameInstance();

            if (dbGame != null)
            {
                dbGame.Source = xml.Serialize(game);
                db.SaveChanges();
            }
        }

        public DBGameInstance GetGameInstance(int id)
        {
            return db.GameInstances.Where(def => def.ID == id).FirstOrDefault();
        }

        public List<DBGameInstance> GetGameInstancesByUser(string user, Permission.TYPE permission)
        {
            return db.Permissions.Where(p => p.UserEmail == user && p.Type == permission).Select(p => p.Game).ToList();
        }

        public List<DBGameInstance> GetGameInstancesByGuid(string guid, Permission.TYPE permission)
        {
            return db.CookiePermissions.Where(p => p.Guid == guid && p.Type == permission).Select(p => p.Game).ToList();
        }

        public GameInstance RestoreGameInstance(int id)
        {
            var dbGame = GetGameInstance(id);
            if (dbGame != null)
            {
                XmlGameInstance xml = new XmlGameInstance();
                return xml.Deserialize(dbGame.Source);
            }
            return null;
        }
    }
}