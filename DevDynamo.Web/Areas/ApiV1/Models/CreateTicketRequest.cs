using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
    public class CreateTicketRequest
    {
        [StringLength(100)]
        public string Project { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;
    }
}
