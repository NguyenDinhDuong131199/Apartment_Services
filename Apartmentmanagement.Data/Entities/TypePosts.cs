using ApartmentManagement.Data.Enums;
using ApartmentManagement.Data.Interfaces;
using ApartmentManagement.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;

namespace ApartmentManagement.Data.Entities
{
    public class TypePosts : DomainEntity<Guid>, IDateTracking, IHasSoftDelete, ISortable, ISwitchable
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public Status Status { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public List<Posts> Posts { get; set; }
    }
}
