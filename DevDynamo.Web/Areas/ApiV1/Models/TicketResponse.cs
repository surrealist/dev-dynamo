using DevDynamo.Models;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
    public class TicketResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Status { get; set; } = null!;

        public Guid ProjectId { get; set; }

        public static TicketResponse FromModel(Ticket t)
        {
            return new TicketResponse
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status,
                Description = t.Description,
                ProjectId = t.ProjectId
            };

        }
        public class TicketStatusResponse
        {
            public string ToStatus { get; set; } = null!;
            public string Action { get; set; } = null!;
        }

    }
}