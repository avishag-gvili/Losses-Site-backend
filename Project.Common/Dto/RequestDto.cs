
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Dto
{
    public class RequestDto
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
