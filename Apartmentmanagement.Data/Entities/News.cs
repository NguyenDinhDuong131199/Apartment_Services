using ApartmentManagement.Data.Entities;
using ApartmentManagement.Data.Enums;
using ApartmentManagement.Data.Interfaces;
using ApartmentManagement.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentManagement.Data.Entities
{
    public class News : DomainEntity<Guid>, IDateTracking, IHasSoftDelete, ISortable, ISwitchable
    {
        [Required]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Avartar { get; set; }
        public string LinkImage { get; set; }
        public string LinkVideo { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public Status Status { get; set; }
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey("NewsGroupId")]
        public Guid NewsGroupId { get; set; }
        public NewsGroup NewsGroup { get; set; }
        [ForeignKey("AccountId")]
        public Guid AccountId { get; set; }
        public Account account { get; set; }
    }
}
