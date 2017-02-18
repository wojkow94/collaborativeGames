using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Platform
{
    [Serializable]
    public class Result
    {
        public dynamic Data;
        public bool Succeed;
        public List<string> Messages = new List<string>();

        static public Result Failure = new Result().AsFailure();
        static public Result Succes = new Result().AsSuccess();

        public Result(dynamic data, string msg)
        {
            Data = data;
            Messages.Add(msg);
        }

        public Result(string msg)
        {
            Messages.Add(msg);
        }

        public bool HasMessages()
        {
            return Messages.Count > 0;
        }

        public void AddMessage(string msg)
        {
            Messages.Add(msg);
        }

        public Result(dynamic data)
        {
            Data = data;
        }

        public Result()
        {
        }

        public Result AsSuccess()
        {
            Succeed = true;
            return this;
        }

        public Result AsFailure()
        {
            Succeed = false;
            return this;
        }
    }
}