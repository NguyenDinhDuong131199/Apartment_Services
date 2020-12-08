using ApartmentManagement.Data.Entities;
using ApartmentManagement.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentManagement.Data.Entities
{
    [Table("Accounts")]
    public class Account : IdentityUser<Guid>, IDateTracking
    {

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
        public List<Permission> Permissions { get; set; }

        public List<Posts> Posts { get; set; }
        public List<News> News { get; set; }
    }
}
