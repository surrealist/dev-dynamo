﻿using DevDynamo.Models;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Sockets;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
    public class TicketController : AppControllerBase
    {
        private readonly AppDb db;

        public TicketController(AppDb db)
        {
            this.db = db;
        }

        [HttpGet("{ticket_id}/next-status")]
        public ActionResult<List<TicketNextStatusResponse>> GetAvailableNextTicketStatus(int ticket_id)
        {
            var ticket = db.Tickets.FirstOrDefault(x => x.Id == ticket_id);
            if (ticket == null)
            {
                return AppNotFound($"Ticket is not found");
            }

            var workFlowsSteps = db.WorkflowSteps.Where(x => x.ProjectId == ticket.ProjectId && x.FromStatus == ticket.Status).
                                 Select(x => new TicketNextStatusResponse { ToStatus = x.ToStatus, Action = x.Action }).ToList();

            if (!workFlowsSteps.Any())
            {
                return AppNotFound($"Workflow step not found");
            }

            return workFlowsSteps;
        }
    }
}