using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Platform
{
    static public class Error
    {
        public class ValidationException : Exception
        {
            public Result Result
            {
                get;
                private set;
            }

            public ValidationException(Result r)
            {
                Result = r;
            }
        }

        public class AppError : Exception
        {
            public AppError(string message, AppError inner = null) : base(message, inner) {}

            public string Print()
            {
                return Message;
            }
        }

        public class EmailExists : AppError
        {
            public EmailExists() : base(Resources.Errors.EmailExists) { }
        }


        public class GameNotExists : AppError
        {
            public GameNotExists() : base(Resources.Errors.GameNotExists) { }
        }

        public class InvalidGameId : AppError
        {
            public InvalidGameId() : base(Resources.Errors.InvalidGameId) { }
        }

        public class InvalidLoginData : AppError
        {
            public InvalidLoginData() : base(Resources.Errors.InvalidLoginData) { }
        }

        public class InvalidPassword : AppError
        {
            public InvalidPassword() : base(Resources.Errors.InvalidPassword) { }
        }

        public class AttributeTypeError : AppError
        {
            public AttributeTypeError(string value, string type) : base(Resources.Errors.AttributeTypeError+" ("+value+", "+type+")") { }
        }

        public class AttributeIsRequired : AppError
        {
            public AttributeIsRequired(string name) : base(Resources.Errors.AttributeIsRequired+" ("+name+")") { }
        }

        public class NotFoundError : AppError
        {
            string Name { get; set; }
            int Id { get; set; }

            public NotFoundError(string msg, int id, string name="", AppError inner = null) : base(msg, inner) {
                Name = name;
                Id = id;
            }
        }

        public class GameNotFound : NotFoundError
        {
            public GameNotFound(int id) : base(Resources.Errors.GameNotFound, id) { }
        }

        public class ElementNotFound : NotFoundError
        {
            public ElementNotFound(int id, string name="") : base(Resources.Errors.ElementNotFound, id, name) { }
        }

        public class AttributeNotFound : NotFoundError
        {
            public AttributeNotFound(int id, string name="") : base(Resources.Errors.AttributeNotFound, id, name) { }
        }

        public class ElementDefinitionNotFound : NotFoundError
        {
            public ElementDefinitionNotFound(int id, string name="") : base(Resources.Errors.ElementDefinitionNotFound, id, name) { }
        }

        public class TokenDefinitionNotFound : NotFoundError
        {
            public TokenDefinitionNotFound(int id, string name = "") : base(Resources.Errors.TokenDefinitionNotFound, id, name) { }
        }

        public class AccessDenied : AppError
        {
            public AccessDenied() : base("Access denied") { }
        }
    }
}