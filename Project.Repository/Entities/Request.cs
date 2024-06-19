using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Entities
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User ?User { get; set; }
        
        [ForeignKey("CategoryID")]
        public int CategoryId { get; set; }
        public string ?Category { get; set; }
        public DateTime Date { get; set; }
    }
}
