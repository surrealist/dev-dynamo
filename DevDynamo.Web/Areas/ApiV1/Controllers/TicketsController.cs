using DevDynamo.Models;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using static DevDynamo.Web.Areas.ApiV1.Models.TicketResponse;
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

            // Initial Status
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

        [HttpPut("{id}")]
        public ActionResult<TicketResponse> UpdateInfo(int id, UpdateTicketRequest request)
        {
            var ticket = db.Tickets.SingleOrDefault(x => x.Id == id);
            if (ticket is null)
            {
                return AppNotFound(nameof(Ticket), id);
            }

            ticket.Title = request.Title;
            ticket.Description = request.Description;

            db.Tickets.Update(ticket);
            db.SaveChanges();

            return NoContent();
        }

        [HttpGet("{ticket_id}/next-status")]
        public ActionResult<List<TicketNextStatusResponse>> GetAvailableNextTicketStatus(int ticket_id)
        {
            var ticket = db.Tickets.FirstOrDefault(x => x.Id == ticket_id);
            if (ticket == null)
            {
                return AppNotFound($"Ticket is not found {ticket_id}");
            }

            var workFlowsSteps = db.WorkflowSteps.Where(x => x.ProjectId == ticket.ProjectId && x.FromStatus == ticket.Status).
                                 Select(x => new TicketNextStatusResponse { Action = x.Action, NextStatus = x.ToStatus }).ToList();

            if (!workFlowsSteps.Any())
            {
                return AppNotFound($"Workflow step not found");
            }

            return workFlowsSteps;
        }

        // PUT /api/v1/tickets/{ticket_id}/status/{target_status_name}
        [HttpPut("{ticket_id}/status/{target_status_name}")]
        public ActionResult<TicketResponse> ChangeTicketStatus(int ticket_id, string target_status_name)
        {
            try
            {
                if (target_status_name == "") throw new InvalidOperationException("Status not found.");

                var item = db.Tickets.Find(ticket_id);
                if (item is null)
                {
                    return NotFound(new ProblemDetails() { Title = $"Ticket with Id = {ticket_id} not found" });
                }


                var ItemNextSteps = db.WorkflowSteps.Where(x => x.ProjectId.ToString() == item.ProjectId.ToString() && x.FromStatus == item.Status).
                              Select(x => new TicketStatusResponse { ToStatus = x.ToStatus, Action = x.Action }).ToList();

                if (!ItemNextSteps.Any())
                {
                    return AppNotFound(nameof(Ticket), target_status_name);

                }
                else if (ItemNextSteps.Where(x => x.ToStatus == target_status_name).Count() == 0)
                {
                    return AppNotFound(nameof(Ticket), target_status_name);
                }

                item.Status = target_status_name;

                db.Tickets.Update(item);
                db.SaveChanges();

                //var res = TicketResponse.FromModel(item);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails() { Title = "Error updating data" + ex.Message });
            }
        }
    }
}
