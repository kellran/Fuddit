using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fuddit.Models
{
    public class Post
    {
        public ApplicationUser User { get; set; }
        
        public int Id { get; set; }
        
        public string UserId { get; set;}
        
        [Required]
        [StringLength(100)]
        [DisplayName("Title")]
        public string Title { get; set; }
        
        [Required]
        [StringLength(2500)]
        [DisplayName("Body")]
        public string Body { get; set; }
        
        [Required]
        [StringLength(400)]
        [DisplayName("Tags")]
        public string Tags { get; set; }
        
        [StringLength(500)]
        [DisplayName("Comment")]
        public List<Comment> Comments { get; set; }
        
        [DisplayName("Like")]
        public int Like { get; set; }

        [Required]
        [StringLength(400)]
        [DisplayName("Category")]
        public string Category { get; set; }
        
        [DisplayName("Picture")]
        public byte[] Picture { get; set; }
        
        public string hyplink { get; set; }

    }
}