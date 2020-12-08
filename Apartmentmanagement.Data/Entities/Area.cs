using ApartmentManagement.Data.Entities;
using ApartmentManagement.Data.Enums;
using ApartmentManagement.Data.Interfaces;
using ApartmentManagement.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;

namespace ApartmentManagement.Data.Entities
{
    public class Area : DomainEntity<Guid>, IDateTracking, IHasSoftDelete, ISortable, ISwitchable
    {
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
        public Status Status { get; set; }
        public List<Posts> posts { get; set; }
        

    }
}
