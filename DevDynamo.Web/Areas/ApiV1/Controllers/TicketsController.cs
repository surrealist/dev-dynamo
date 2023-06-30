﻿using DevDynamo.Models;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
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
                return NotFound(new ProblemDetails() { Title = "Ticket is not found" });
            }
            return TicketResponse.FromModel(item);
        }

        [HttpPost]
        public ActionResult<TicketResponse> Create(CreateTicketRequest request)
        {
            //Initial Status
            var workflow = db.WorkflowSteps.SingleOrDefault(x => x.Id == 1);
            if (workflow is null)
            {
                return NotFound(new ProblemDetails() { Title = "Workflow is not found" });
            }

            var project = db.Projects.SingleOrDefault(x => x.Id.ToString() == request.Project);
            if (project is null)
            {
                return NotFound(new ProblemDetails() { Title = "ProjectID is not found" });
            }

            var t = new Ticket();
            t.Initial(request.Title, Guid.Parse(request.Project), workflow.ToStatus);

            db.Tickets.Add(t);
            db.SaveChanges();

            var res = TicketResponse.FromModel(t);
            return CreatedAtAction(nameof(GetById), new { id = t.Id }, res);
        }

        [HttpPut("{id}")]
        public ActionResult<TicketResponse> UpdateInfo(int id, UpdateTicketRequest request)
        {
            var ticket = db.Tickets.SingleOrDefault(x => x.Id == id);
            if (ticket is null)
            {
                return NotFound(new ProblemDetails() { Title = "Ticket is not found" });
            }

            ticket.Title = request.Title;
            ticket.Description = request.Description;

            db.Tickets.Update(ticket);
            db.SaveChanges();

            return NoContent();
        }

    }
}
