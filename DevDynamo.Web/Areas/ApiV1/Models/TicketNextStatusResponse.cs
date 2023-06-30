using DevDynamo.Models;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
  public class TicketNextStatusResponse
  {
    public string ToStatus { get; set; } = null!;
    public string Action { get; set; } = null!;
  }
}