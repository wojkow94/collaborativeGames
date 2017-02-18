using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Database
{
    public class DataAccessObject : IDisposable
    {
        protected DBContext db = new DBContext();

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}