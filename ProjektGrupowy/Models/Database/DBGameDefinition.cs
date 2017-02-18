using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Database
{
    public class DBGameDefinition
    {
        public int ID { get; set; }
        public int ImageID { get; set; }
        public DateTime Created { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public float Rate { get; set; }

        virtual public Image Image { get; set; }
    }
}