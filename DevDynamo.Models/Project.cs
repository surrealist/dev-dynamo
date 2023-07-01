using System.ComponentModel.DataAnnotations;

namespace DevDynamo.Models
{
    public class Project
    {
        public Project(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Tickets = new HashSet<Ticket>();
            WorkflowSteps = new HashSet<WorkflowStep>();
        }

        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string TemplateName { get; set; } = null!;

        // virtual => enable EF lazy-loading (on-demand)
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<WorkflowStep> WorkflowSteps { get; set; }

        /// <summary>
        /// Recreate WorkflowSteps from stateDiagram template
        /// </summary>
        /// <param name="template"></param>
        public void LoadWorkflowTemplate(string template)
        {
            if (WorkflowSteps.Any()) throw new InvalidOperationException("Workflow already setup");

            var lines = template.Split(new char[] { '\r', '\n' },
              StringSplitOptions.RemoveEmptyEntries).ToList();
            if (lines.Count == 0) throw new InvalidOperationException("Template is empty");
            if (lines[0] != "stateDiagram") throw new InvalidOperationException("Invalid template");

            foreach (var line in lines.Skip(1))
            {
                var data = line.Split(new string[] { "-->", ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (data.Length < 2) continue;

                var step = new WorkflowStep();
                step.FromStatus = data[0].Trim();
                step.ToStatus = data[1].Trim();
                if (data.Length >= 3) step.Action = data[2].Trim();

                WorkflowSteps.Add(step);
            }
        }
    }
}