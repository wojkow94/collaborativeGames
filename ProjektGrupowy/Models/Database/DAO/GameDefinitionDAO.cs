using ProjektGrupowy.Models.Game.Definitions;
using ProjektGrupowy.Models.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Database.DAO
{
    public class GameDefinitionDAO : DataAccessObject
    {

        public void AddGameDefinition(GameDefinition gameDef) 
        {
            XmlGameDefinition xml = new XmlGameDefinition();

            DBGameDefinition dbGame = new DBGameDefinition
            {
                Name = gameDef.Name,
                Created = DateTime.Now,
                Source = "",
                Rate = 0.0f,
                ImageID = gameDef.BackgorundImageId
            };

            var definedGame = db.GameDefinitions.FirstOrDefault(def => def.Name == dbGame.Name);

            if (definedGame != null)
                db.GameDefinitions.Remove(definedGame);

            db.GameDefinitions.Add(dbGame);
            db.SaveChanges();

            gameDef.Id = dbGame.ID;
            dbGame.Source = xml.Serialize(gameDef);
            db.SaveChanges();
        }

        public DBGameDefinition GetGameDefinition(int id)
        {
            return db.GameDefinitions.Where(def => def.ID == id).FirstOrDefault();
        }

        public GameDefinition RestoreGameDefinition(int id)
        {
            var dbGame = GetGameDefinition(id);
            if (dbGame != null)
            {
                XmlGameDefinition xml = new XmlGameDefinition();
                return xml.Deserialize(dbGame.Source);
            }
            return null;
        }
    }
}