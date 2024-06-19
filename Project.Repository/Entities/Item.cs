using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Entities
{
    //public enum Status { Loosing=1,Finding=2,Returned=3}
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ?Category { get; set; }
        public int Status { get; set; }
        public string ?Location { get; set; }
        public string? Area { get; set; }
        public string? Discraption { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User ?User { get; set; }

    }
}
