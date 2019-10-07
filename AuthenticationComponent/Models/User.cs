using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace AuthenticationComponent.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public User()
        {
            //
        }

        public User(string username, string password)
        {
            Username = username;

            //hash and salt the password
            var hashedPassword = Crypto.HashPassword(password);

            Password = hashedPassword;

        }
    }
}