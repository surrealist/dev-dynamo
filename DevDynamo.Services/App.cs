using DevDynamo.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevDynamo.Services
{
    public sealed class App
    {
        internal readonly AppDb db;
        private Lazy<ProjectService> _projectService;

        public App(AppDb db)
        {
            this.db = db;

            //Projects = new(this);

            _projectService = new Lazy<ProjectService>(() => new ProjectService(this));
            Tickets = new(this);
            WorkflowSteps = new(this);
        }

        //public UserService Users { get; }
        //public User CurrentUser { get; private set; } = null;
        //public bool IsAuthenticated => CurrentUser != null;

        public ProjectService Projects { get => _projectService.Value; }
        public TicketService Tickets { get; }
        public WorkflowStepService WorkflowSteps { get; }

        public int SaveChanges() => db.SaveChanges();
        public Task<int> SaveChangesAsync() => db.SaveChangesAsync();

        public Func<DateTime> Now { get; private set; } = () => DateTime.Now;
        public void SetNow(DateTime now) => Now = () => now;
        public void ResetNow() => Now = () => DateTime.Now;
        public DateTime Today() => Now().Date;

        //public void SetCurrentUser(Guid id, string username)
        //{
        //    var user = Users.Find(id);
        //    if (user == null)
        //    {
        //        user = new User
        //        {
        //            Id = id,
        //            UserName = username,
        //            CreatedDate = Now(),
        //            Note = null
        //        };
        //        Users.Add(user);
        //        SaveChanges();
        //    }

        //    CurrentUser = user;
        //}

        //public void Throws(AppException ex)
        //{
        //    ex.UserName = CurrentUser?.UserName;

        //    throw ex;
        //}
    }
}
