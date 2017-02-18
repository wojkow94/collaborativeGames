using ProjektGrupowy.Models.Database;
using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Database.DAO
{
    public class ImageDAO : DataAccessObject
    {
        private Dictionary<int, string> cache = new Dictionary<int, string>();

        public string GetImage(int id)
        {
            if (cache.ContainsKey(id))
            {
                return cache[id];
            }
            else
            {
                var image = db.Images.Where(i => i.ID == id).FirstOrDefault();
                if (image == null) return "";
                cache.Add(id, image.Path);
                return image.Path;
            }
        }

        public int AddImage(string url)
        {
            var image = new Image { Path = url };
            db.Images.Add(image);
            db.SaveChanges();
            cache.Add(image.ID, url);
            return image.ID;
        }
    }
}