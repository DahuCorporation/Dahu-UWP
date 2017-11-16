using DahuUWP.Models.ModelManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Services
{
    public interface IDataService
    {
        object GetUserManager();
        object GetProjectManager();
    }

    class DataService : IDataService
    {
        private UserManager UserManager = new UserManager();
        private ProjectManager ProjectManager = new ProjectManager();

        public object GetUserManager() { return UserManager; }
        public object GetProjectManager() { return ProjectManager; }

    }

    class DesignDataService : IDataService
    {
        private DesignUserManager DesignUserManager = new DesignUserManager();
        private DesignProjectManager DesignProjectManager = new DesignProjectManager();

        public object GetUserManager() { return DesignUserManager; }
        public object GetProjectManager() { return DesignProjectManager; }
    }
}
