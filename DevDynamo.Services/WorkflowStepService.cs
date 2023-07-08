using DevDynamo.Models;
using DevDynamo.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevDynamo.Services
{
    public class WorkflowStepService : ServiceBase<WorkflowStep>
    {
        public WorkflowStepService(App app) : base(app)
        {
        }
    }
}
