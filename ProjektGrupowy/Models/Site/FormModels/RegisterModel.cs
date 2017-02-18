using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Site.FormModels
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Pass { get; set; }
        public string RePass { get; set; }

        public Result Validate()
        {
            Result res = new Result();

            if (Email == null || Email == "") res.AddMessage(Resources.Errors.EmailIsRequired);
            if (Pass == null || Pass == "") res.AddMessage(Resources.Errors.PasswordIsRequired);
            else
            {
                if (Pass.Length < 3) res.AddMessage(Resources.Errors.PasswordShorterThan3);
                else if (Pass != RePass) res.AddMessage(Resources.Errors.PasswordsNotMatch);
            }

            if (res.HasMessages()) throw new Error.ValidationException(res);
            return Result.Succes;
        }
    }
}