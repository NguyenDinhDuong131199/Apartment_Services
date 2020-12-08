using ApartmentManagement.Data.Enums;
using ApartmentManagement.Data.Interfaces;
using ApartmentManagement.Infrastructure.SharedKernel;
using System;

namespace ApartmentManagement.Data.Entities
{
    public class Banner : DomainEntity<Guid>, IDateTracking, IHasSoftDelete, ISortable, ISwitchable
    {
        public string Name { get; set; }

        public string LinkImage { get; set; }

        public string Note { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
        public Status Status { get; set; }
    }
}
