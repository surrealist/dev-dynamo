using DevDynamo.Models;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{project_id}/tickets")]
        public ActionResult<IEnumerable<TicketResponse>> GetTicketsByProject(Guid project_id)
        {
            var items = db.Tickets.Where(x => x.ProjectId == project_id).ToList();
            return items.ConvertAll(x => TicketResponse.FromModel(x));
        }

        [HttpGet("{id}")]
        public ActionResult<ProjectResponse> GetById(Guid id)
        {
            var item = db.Projects.SingleOrDefault(x => x.Id == id);
            if (item is null)
            {
                //return NotFound("Project not found");
                return AppNotFound(nameof(Project), id); //NotFound(new ProblemDetails() { Title = "Project is not found" });
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
                String[] fileNames = Directory.GetFiles(@"./WorkflowTemplates", "*.txt").Select(fileName => Path.GetFileNameWithoutExtension(fileName)).ToArray();
                string errorMessage = null! ;

                if (!fileNames.Any()) // Folder ./WorkflowTemplates no any file.
                {
                    errorMessage = "No any WorkflowTemplate file in this folder.";
                }
                else
                {
                    string allTemplateNames = (fileNames.Count() > 1) ? string.Join(", ", fileNames.Take(fileNames.Length - 1)) + " and " + fileNames.Last() : fileNames[0];
                    errorMessage = $"All available templates are {allTemplateNames}.";
                }


                return AppNotFound(nameof(Project), message: errorMessage);
            }

            var workflow = System.IO.File.ReadAllText(path);
            p.LoadWorkflowTemplate(workflow);

            p.TemplateName = request.Template;

            db.Projects.Add(p);
            db.SaveChanges();

            var res = ProjectResponse.FromModel(p);
            return CreatedAtAction(nameof(GetById), new { id = p.Id }, res);
        }


        [HttpPut("{id}")]
        public ActionResult<ProjectResponse> Update(Guid id, UpdateProjectRequest request)
        {

            if (string.IsNullOrEmpty(request.Name)) return BadRequest(new UpdateProjectRequest { Name = $"cannot null or empty" });
            if (string.IsNullOrEmpty(request.Description)) return BadRequest(new UpdateProjectRequest { Description = $"cannot null or empty" });

            var checkData = db.Projects.SingleOrDefault(x => x.Id == id);

            if (checkData == null) return BadRequest(new UpdateProjectRequest { Description = $"cannot find Id => {id} " });

            checkData.Name = request.Name;
            checkData.Description = request.Description;

            db.SaveChanges();

            return NoContent();
        }

    }
}
