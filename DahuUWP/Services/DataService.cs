using DahuUWP.Models.ModelManager;
using DahuUWP.Services.ModelManager;
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
        object GetSkillManager();
    }

    class DataService : IDataService
    {
        private UserManager UserManager = new UserManager();
        private ProjectManager ProjectManager = new ProjectManager();
        private SkillManager SkillManager = new SkillManager();
        private ChatManager ChatManager = new ChatManager();

        public object GetUserManager() { return UserManager; }
        public object GetProjectManager() { return ProjectManager; }
        public object GetSkillManager() { return SkillManager; }
        public object GetChatManager() { return ChatManager; }

    }

    class DesignDataService : IDataService
    {
        private DesignUserManager DesignUserManager = new DesignUserManager();
        private DesignProjectManager DesignProjectManager = new DesignProjectManager();
        private DesignSkillManager DesignSkillManager = new DesignSkillManager();

        public object GetUserManager() { return DesignUserManager; }
        public object GetProjectManager() { return DesignProjectManager; }
        public object GetSkillManager() { return DesignSkillManager; }
    }
}
