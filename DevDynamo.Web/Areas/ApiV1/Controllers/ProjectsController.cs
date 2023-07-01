using DevDynamo.Models;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectsController : AppControllerBase
    {
        private readonly AppDb db;

        public ProjectsController(AppDb db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProjectResponse>> GetAll()
        {
            var items = db.Projects.ToList();
            return items.ConvertAll(x => ProjectResponse.FromModel(x));
        }

        [HttpGet("{id}")]
        public ActionResult<ProjectResponse> GetById(Guid id)
        {
            var item = db.Projects.SingleOrDefault(x => x.Id == id);
            if (item is null)
            {
                return AppNotFound(nameof(Project),id);
            }
            return ProjectResponse.FromModel(item);
        }

        [HttpPost]
        public ActionResult<ProjectResponse> Create(CreateProjectRequest request)
        {
            var p = new Project(request.Name);

            var path = $"./WorkflowTemplates/{request.Template}.txt";
            if (!System.IO.File.Exists(path))
            {
                return BadRequest(new ProblemDetails { Title = $"Template {request.Template} not found" });
            }

            var workflow = System.IO.File.ReadAllText(path);
            p.LoadWorkflowTemplate(workflow);

            db.Projects.Add(p);
            db.SaveChanges();

            var res = ProjectResponse.FromModel(p);
            return CreatedAtAction(nameof(GetById), new { id = p.Id }, res);
        }

    }
}
