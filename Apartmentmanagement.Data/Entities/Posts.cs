using ApartmentManagement.Data.Entities;
using ApartmentManagement.Data.Enums;
using ApartmentManagement.Data.Interfaces;
using ApartmentManagement.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentManagement.Data.Entities
{
    public class Posts : DomainEntity<Guid>, IDateTracking, IHasSoftDelete, ISortable, ISwitchable
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public Boolean Like { get; set; }
        public string LinkImage { get; set; }
        public string linkVideo { get; set; }
        public string LinkMap { get; set; }
        public float Areage { get; set; }
        public string GetInTouch { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public Status Status { get; set; }

        [ForeignKey("AreaId")]
        public Guid AreaId { get; set; }
        public Area area { get; set; }
        [ForeignKey("TypePostsId")]
        public Guid PypePostsId { get; set; }
        public TypePosts TypePosts { get; set; }
       
        [ForeignKey("AccountId")]
        public Guid AccountId { get; set; }
        public Account account { get; set; }
        public List<PostCategory> postCategories { get; set; }

    }
}
