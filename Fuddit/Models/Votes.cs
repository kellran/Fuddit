using System.Collections;

namespace Fuddit.Models
{
    public class Votes
    {
        public int Id { get; set; }
        
        public ApplicationUser User { get; set; }
        
        public Post Post { get; set; }
        
        public int _like { get; set; }
        
        public int _dislike { get; set; }
        
    }
}