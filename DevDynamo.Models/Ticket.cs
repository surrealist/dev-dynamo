using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevDynamo.Models
{
    public class Ticket
    {
        public Ticket(string title, string status)
        {
            Title = title;
            Status = status;
            //ProjectId = projectId;
        }
        public Ticket() { }
        public Guid ProjectId { get; set; }
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = null!;
    }
}
