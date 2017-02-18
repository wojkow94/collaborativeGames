using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Site.FormModels
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Pass { get; set; }

        public Result Validate()
        {
            Result res = new Result();

            if (Email == null || Email == "") res.AddMessage(Resources.Errors.EmailIsRequired);
            if (Pass == null || Pass == "") res.AddMessage(Resources.Errors.PasswordIsRequired);

            if (res.HasMessages()) throw new Error.ValidationException(res);
            return Result.Succes;
        }
    }
}