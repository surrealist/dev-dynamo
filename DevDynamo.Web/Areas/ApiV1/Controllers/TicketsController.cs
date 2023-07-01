using DevDynamo.Models;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Newtonsoft.Json;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketsController : AppControllerBase
    {
        private readonly AppDb db;

        public TicketsController(AppDb db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TicketResponse>> GetAll()
        {
            var items = db.Tickets.ToList();
            return items.ConvertAll(x => TicketResponse.FromModel(x));
        }

        [HttpGet("{id}")]
        public ActionResult<TicketResponse> GetById(int id)
        {
            var item = db.Tickets.SingleOrDefault(x => x.Id == id);
            if (item is null)
            {
                return AppNotFound(nameof(Ticket), id);
            }
            return TicketResponse.FromModel(item);
        }

        [HttpPost]
        public ActionResult<TicketResponse> Create(CreateTicketRequest request)
        {
            var project = db.Projects.SingleOrDefault(x => x.Id == request.ProjectId);
            if (project is null)
            {
                return AppNotFound(nameof(Ticket), request.ProjectId);
            }

            //Initial Status
            var workflow = project.WorkflowSteps.SingleOrDefault(x => x.FromStatus == "[*]");
            if (workflow is null)
            {
                return AppNotFound(nameof(WorkflowStep));
            }

            var t = new Ticket(request.Title, workflow.ToStatus);
            project.Tickets.Add(t);
            db.SaveChanges();

            var res = TicketResponse.FromModel(t);
            return CreatedAtAction(nameof(GetById), new { id = t.Id }, res);
        }
    }
}
