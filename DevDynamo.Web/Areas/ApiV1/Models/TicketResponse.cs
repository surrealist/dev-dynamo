using DevDynamo.Models;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
    public class TicketResponse
    {
        public int Id { get; set; }
        [StringLength(100)] public string Title { get; set; }
        public string? Description { get; set; }
        [StringLength(50)] public string Status { get; set; } = null!;
        public static TicketResponse FromModel(Ticket T)
        {
            return new TicketResponse
            {
                Id = T.Id,
                Title = T.Title,
                Description = T.Description,
                Status = T.Status
            };

        }
        public class TicketStatusResponse
        {
            public string ToStatus { get; set; } = null!;
            public string Action { get; set; } = null!;
        }

    }
}
