using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;


namespace Fuddit.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nickname { get; set; }
        public DateTime Birthdate { get; set; }
    
        public string Bio { get; set; }
    
        public string Gender { get; set; }
    
        public string Location { get; set; }
        
        public byte[] Profilepicture { get; set; }
        
    }
}