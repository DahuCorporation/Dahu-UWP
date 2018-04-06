using DahuUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.Forum
{
    public class MessageContainer
    {
        public TopicMessage Message { get; set; }

        public User User { get; set; }
    }
}
