using ApartmentManagement.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentManagement.Data.Entities
{
    public class PostCategory : IHasSoftDelete

    {   [Key]
        [Column(Order =0)]
        [ForeignKey("CategoryId")]
        public Guid CatagoryId { get; set; }
        public Category category { get; set; }

        [Key]
        [Column(Order =1)]
        [ForeignKey("PostsId")]
        public Guid PostsId { get; set; }
        public Posts Posts { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

    }
}
