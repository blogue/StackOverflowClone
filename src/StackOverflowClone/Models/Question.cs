using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowClone.Models
{
    [Table("Questions")]
    public class Question
    {
        
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Response> Responses { get; set; }

        
    }
}
