using ApartmentManagement.Data.Enums;
using ApartmentManagement.Data.Interfaces;
using ApartmentManagement.Infrastructure.SharedKernel;
using System;

namespace ApartmentManagement.Data.Entities
{
    public class Advertisement : DomainEntity<Guid>, IDateTracking, IHasSoftDelete, ISortable, ISwitchable
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string LinkImage { get; set; }
        public string LinkVideo { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public Status Status { get; set; }
        
    }
}
