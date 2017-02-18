using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.Models.Database
{
    public class User
    {
        [Key]
        public string Email { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }

        public virtual ICollection<DBPermission> Permissions { get; set; }
    }
}