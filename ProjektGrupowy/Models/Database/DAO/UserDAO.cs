using ProjektGrupowy.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ProjektGrupowy.Models.Database.DAO
{
    public class UserDAO : DataAccessObject
    {
        public User GetUser(string email)
        {
            return db.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public void CheckUser(string email, string password)
        {
            var user = db.Users.Where(u => u.Email == email).FirstOrDefault();

            if (user == null || !Crypto.VerifyHashedPassword(user.Password, password != null ? password : ""))
            {
                throw new Error.InvalidLoginData();
            }
        }

        public void AddUser(string email, string password)
        {
            if (GetUser(email) != null)
            {
                throw new Error.EmailExists();
            }

            User user = new Models.Database.User
            {
                Email = email,
                Password = Crypto.HashPassword(password),
                RegisterDate = DateTime.Now
            };
            db.Users.Add(user);
            db.SaveChanges();
        }

    }
}