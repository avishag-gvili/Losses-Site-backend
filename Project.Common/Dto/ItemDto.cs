
using Project.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public int Status { get; set; }
        public string? Location { get; set; }
        public string? Area { get; set; }
        public string? Discraption { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
