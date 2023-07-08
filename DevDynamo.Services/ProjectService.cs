using DevDynamo.Models;
using DevDynamo.Services.Core;
using DevDynamo.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevDynamo.Services
{
    public class ProjectService : ServiceBase<Project>
    {
        public ProjectService(App app) : base(app)
        {
        }

        public Project Create(string projectName, 
            string workflowTemplateName,
            string workflowTemplateBody)
        {
            var p = new Project(projectName);
            p.TemplateName = workflowTemplateName;
            p.LoadWorkflowTemplate(workflowTemplateBody);

            app.Projects.Add(p);
            app.SaveChanges();
            return p;
        }

        public async Task UpdateAsync(Guid id, 
            string name, string description)
        {
            var p = await app.Projects.FindAsync(id);
            if (p == null)
            {
                throw new NotFoundException(nameof(Project), id);
            }

            p.Name = name;
            p.Description = description;
            app.SaveChanges();
        }
    }
}
