using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
    public class UpdateTicketRequest
    {
        [StringLength(100)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }
    }
}
