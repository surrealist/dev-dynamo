using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevDynamo.Models
{
    public class WorkflowStep
    {
        public int Id { get; set; }
        public Guid ProjectId { get; set; }

        [StringLength(50)]
        public string FromStatus { get; set; } = null!;
        [StringLength(50)]
        public string ToStatus { get; set; } = null!;
        [StringLength(50)]
        public string Action { get; set; } = null!;
    }
}
