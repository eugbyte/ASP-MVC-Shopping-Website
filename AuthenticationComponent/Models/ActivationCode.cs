using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AuthenticationComponent.Models
{
    public class ActivationCode
    {
        public Guid Id { get; set; }

        //1 Customer to Many Activation Codes
        public User User { get; set;}
        public int? UserId { get; set; }

        public ActivationCode()
        {
            Id = Guid.NewGuid();
        }
    }
}