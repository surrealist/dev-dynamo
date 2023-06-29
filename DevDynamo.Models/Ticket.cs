﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevDynamo.Models
{
    public class Ticket
    {
        public Ticket()
        {
        }

        public int Id { get; set; }

        public Guid ProjectId { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = null!;

        public void Initial(string title, Guid projectid, string status)
        {
            Title = title;
            ProjectId = projectid;
            Status = status;
        }
    }
}
