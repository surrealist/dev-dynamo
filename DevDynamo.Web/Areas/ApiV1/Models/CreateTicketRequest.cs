using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
    public class CreateTicketRequest
    {
        public Guid ProjectId { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;
    }
}
