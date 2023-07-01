using DevDynamo.Models;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? TemplateName { get; set; }

        public static ProjectResponse FromModel(Project p)
        {
            return new ProjectResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                TemplateName = p.TemplateName
            };
        }
    }
}