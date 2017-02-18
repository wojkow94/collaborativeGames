using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ProjektGrupowy.Models.Core;
using ProjektGrupowy.Models.Database.DAO;

namespace ProjektGrupowy.Areas.Api.Controllers
{
    public class ContentController : Controller
    {
        public FileContentResult script(string name, int version)
        {
            string path = HostingEnvironment.MapPath("~/Scripts/" + name + ".js");
            if (System.IO.File.Exists(path))
            {
                StringBuilder builder = new StringBuilder(System.IO.File.ReadAllText(path));
                builder.Append("\n//# sourceURL=" + name + ".js");
                return new FileContentResult(Encoding.UTF8.GetBytes(builder.ToString()), "text/javascript");
            }
            return null;
        }

        public FileContentResult scripts(string name, int version, string paths)
        {
            string path = "";
            StringBuilder builder = new StringBuilder();

            foreach(string p in paths.Split(','))
            {
                path = HostingEnvironment.MapPath("~/Scripts/" + p + ".js");
                if (System.IO.File.Exists(path))
                {
                    builder.Append(System.IO.File.ReadAllText(path));
                }
            }
            builder.Append("\n//# sourceURL=" + name + ".js");
            return new FileContentResult(Encoding.UTF8.GetBytes(builder.ToString()), "text/javascript");
        }

        public FileContentResult image(int id)
        {
            using (var dao = new ImageDAO())
            {
                string path = HostingEnvironment.MapPath(dao.GetImage(id));
                if (System.IO.File.Exists(path))
                {
                    return File(System.IO.File.ReadAllBytes(path), "image/png");
                }
                return null;
            }
        }
    }
}