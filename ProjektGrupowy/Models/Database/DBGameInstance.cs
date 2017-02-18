using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Database
{
    public class DBGameInstance
    {
        public int ID { get; set; }
        public int DefinitionID { get; set; }
        public DateTime Created { get; set; }
        public string Source { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }


        virtual public DBGameDefinition Definition { get; set; }
        public virtual ICollection<DBPermission> Permissions { get; set; }
    }
}