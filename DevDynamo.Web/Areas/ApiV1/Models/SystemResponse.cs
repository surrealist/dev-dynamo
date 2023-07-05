using DevDynamo.Models;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
  public class SystemResponse
  {
    public string? version { get; set; }
    public string? environment { get; set; }
    public string? now { get; set; }

  }
}