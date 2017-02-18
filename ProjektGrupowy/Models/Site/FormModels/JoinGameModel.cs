using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Site.FormModels
{
    public class JoinGameModel
    {
        public string GameId { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }

        public Result Validate()
        {
            Result res = new Result();

            if (GameId == null || GameId == "") res.AddMessage(Resources.Errors.GameIdIsRequired);
            if (Nick == null || Nick == "") res.AddMessage(Resources.Errors.NickIsRequired);

            if (res.HasMessages())
            {
                throw new Error.ValidationException(res);
            }
            return Result.Succes;
        }
    }
}