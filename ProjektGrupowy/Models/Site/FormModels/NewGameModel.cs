using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Site.FormModels
{
    public class NewGameModel
    {
        public string Name { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }

        public Result Validate()
        {
            Result res = new Result();

            if (Name == null || Name == "") res.AddMessage(Resources.Errors.NameIsRequired);
            if (Password == null || Password == "") res.AddMessage(Resources.Errors.PasswordIsRequired);
            if (Nick == null || Nick == "") res.AddMessage(Resources.Errors.NickIsRequired);

            if (res.HasMessages()) throw new Error.ValidationException(res);
            return res;
        }
    }
}