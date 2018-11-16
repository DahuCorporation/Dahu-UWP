﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class Project
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// C'est trop chiant de devoir faire ça, le back je vous le dis c'est chiant !
        /// Autant mettre project_uuid tout le temps, même quand on récup tout les projets de dahu parce que soit je fait ça, soit je fais deux class project
        /// </summary>
        [JsonProperty(PropertyName = "project_uuid")]
        private string ProjectUuid { set { Uuid = value; } }

        [JsonProperty(PropertyName = "creator_uuid")]
        public string OwnerUuid { get; set; }

        [JsonProperty(PropertyName = "account_id")]
        public int AccountId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "profile_picture")]
        public string ProfilePicture { get; set; }

        [JsonProperty(PropertyName = "banner_picture")]
        public string BannerPicture { get; set; }

        
        //public List<User> Members { get; set; }

        private List<User> _members;
        [JsonProperty(PropertyName = "members")]
        public List<User> Members
        {
            get
            {
                return _members;
            }

            set
            {
                _members = value;
                this.NotifyPropertyChanged("Members");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Media Media { get; set; }
    }
}
