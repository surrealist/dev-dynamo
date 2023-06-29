﻿using DevDynamo.Models;
using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Web.Areas.ApiV1.Models
{
  public class TicketResponse
  {
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    public static TicketResponse FromModel(Ticket p)
    {
      return new TicketResponse
      {
        Id = p.Id,
        Title = p.Title,
        Description = p.Description
      };
    }
  }
}