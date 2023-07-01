using DevDynamo.Models;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using static DevDynamo.Web.Areas.ApiV1.Models.TicketResponse;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
    [Route("api/v1/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDb db;
        public TicketsController(AppDb db)
        {
            this.db = db;
        }

        //PUT /api/v1/tickets/{ticket_id}/status/{target_status_name}
        [HttpPut("{ticket_id}/status/{target_status_name}")]
        public ActionResult<TicketResponse> ChangeTicketStatus(int ticket_id,string target_status_name)
        {
            try
            {
                if (target_status_name == "") throw new InvalidOperationException("Status not found.");

                var item = db.Tickets.SingleOrDefault(x => x.Id == ticket_id);
                if (item is null)
                {
                   return NotFound(new ProblemDetails() { Title = $"Ticket with Id = {ticket_id} not found" });
                }

                var ItemNextSteps = db.WorkflowSteps.Where(x => x.ProjectId.ToString() == item.ProjectId.ToString() && x.FromStatus == item.Status).
                              Select(x => new TicketStatusResponse { ToStatus = x.ToStatus, Action = x.Action }).ToList();

                if (!ItemNextSteps.Any())
                {
                    return NotFound(new ProblemDetails() { Title = $"Project Id= {item.ProjectId} , ToStatus = {item.Status} not found" });

                } else if (ItemNextSteps.FirstOrDefault().ToStatus != target_status_name) {

                    return NotFound(new ProblemDetails() { Title = $"Status {target_status_name} incorrect" });

                }

                item.Status = target_status_name;

                db.Tickets.Update(item);
                db.SaveChanges();

                //var res = TicketResponse.FromModel(item);
                return NoContent( );
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails() { Title = "Error updating data" + ex .Message});
            }
           


        }
       
    }
}
