using DevDynamo.Models;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

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



                var t = new Ticket();
                t = item;
                t.Status = target_status_name;

                db.Tickets.Update(t);
                db.SaveChanges();

                var res = TicketResponse.FromModel(item);
                return res;
            }
            catch (Exception)
            {
                return NotFound(new ProblemDetails() { Title = "Error updating data" });
            }
           


        }
       
    }
}
