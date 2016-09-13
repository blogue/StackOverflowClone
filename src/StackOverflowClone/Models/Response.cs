using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowClone.Models
{
    [Table("Responses")]
    public class Response
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
