using System.Collections.Generic;

namespace Fuddit.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public ApplicationUser User { get; set; }
        public int PostId { get; set; }
    }
}
