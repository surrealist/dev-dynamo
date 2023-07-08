using DevDynamo.Models;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
  public class TicketNextStatusResponse
  {
        public string Action { get; set; } = null!;
        public string NextStatus { get; set; } = null!; 
  }
}