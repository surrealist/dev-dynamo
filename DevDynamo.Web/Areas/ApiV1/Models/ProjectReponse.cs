using DevDynamo.Models;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
    public class ProjectReponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }


        public static ProjectReponse FromModel(Project p) 
        {
            return new ProjectReponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            };
        }
    }
}
