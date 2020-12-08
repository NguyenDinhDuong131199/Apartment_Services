using ApartmentManagement.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ApartmentManagement.Data.Entities
{
    public class Function : IdentityRole<Guid>, IDateTracking, IHasSoftDelete
    {
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
