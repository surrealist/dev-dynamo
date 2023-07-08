using DevDynamo.Models;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
  public class SystemResponse
  {
    public string? Version { get; set; }
    public string? Environment { get; set; }
    public string? Now { get; set; }

  }
}