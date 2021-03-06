﻿using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Profil.Private
{
    public class PrivateProfilMainInformationViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public PrivateProfilMainInformationViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
            FillDataView();
        }

        private async void OnPageLoaded()
        {
            UpdateProfilMainInformation = new DahuButtonBindings
            {
                IsBusy = false,
                FuncListener = UpdateMainInformation
            };
            UpdateProfilPictureBinding = new DahuButtonBindings
            {
                IsBusy = false,
                FuncListener = UpdateProfilPicture
            };
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                NotifyPropertyChanged(ref _userName, value);
            }
        }

        private string _userMailAdress;
        public string UserMailAdress
        {
            get { return _userMailAdress; }
            set
            {
                NotifyPropertyChanged(ref _userMailAdress, value);
            }
        }

        private string _userFirstName;
        public string UserFirstName
        {
            get { return _userFirstName; }
            set
            {
                NotifyPropertyChanged(ref _userFirstName, value);
            }
        }

        private string _userBiography;
        public string UserBiography
        {
            get { return _userBiography; }
            set
            {
                NotifyPropertyChanged(ref _userBiography, value);
            }
        }

        private string _userAddress;
        public string UserAddress
        {
            get { return _userAddress; }
            set
            {
                NotifyPropertyChanged(ref _userAddress, value);
            }
        }

        private DateTimeOffset _userBirthdate;
        public DateTimeOffset UserBirthdate
        {
            get { return _userBirthdate; }
            set
            {
                NotifyPropertyChanged(ref _userBirthdate, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        private DahuButtonBindings _updateProfilMainInformation;
        public DahuButtonBindings UpdateProfilMainInformation
        {
            get { return _updateProfilMainInformation; }
            set
            {
                NotifyPropertyChanged(ref _updateProfilMainInformation, value);
            }
        }

        private DahuButtonBindings _updateProfilPictureBinding;
        public DahuButtonBindings UpdateProfilPictureBinding
        {
            get { return _updateProfilPictureBinding; }
            set
            {
                NotifyPropertyChanged(ref _updateProfilPictureBinding, value);
            }
        }

        private async void FillDataView()
        {
            try
            {
                UserManager userManager = (UserManager)dataService.GetUserManager();

                Dictionary<string, object> userDicoCharge = new Dictionary<string, object>
            {
                { "_token", AppStaticInfo.Account.Token }
            };
                User user = await userManager.Charge(AppStaticInfo.Account.Uuid, userDicoCharge);
                UserFirstName = user.FirstName;
                UserName = user.LastName;
                UserMailAdress = user.Mail;
                UserBiography = user.Biography;
                UserAddress = user.Address;
                UserBirthdate = user.Birthdate;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
            }
        }

        private User RecupUserMainInformation()
        {
            User user = new User
            {
                FirstName = UserFirstName,
                LastName = UserName,
                Mail = UserMailAdress,
                Biography = UserBiography,
                Address = UserAddress,
                Birthdate = UserBirthdate.DateTime,
                City = "test",
                Country = "test",
                Gender = Utils.Enum.Gender.Sir,
                Phone = "0633936156",
                PostalCode = "03320"
            };
            return user;
        }

        private async void UpdateMainInformation(object param)
        {
            UpdateProfilMainInformation.IsBusy = true;
            UserManager userManager = (UserManager)dataService.GetUserManager();
            await userManager.Edit(RecupUserMainInformation());
            UpdateProfilMainInformation.IsBusy = false;
        }
        private async void UpdateProfilPicture(object param)
        {
            UpdateProfilPictureBinding.IsBusy = true;
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var stream = await file.OpenStreamForReadAsync();
                var bytes = new byte[(int)stream.Length];
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppStaticInfo.Account.Token);
                MultipartFormDataContent form = new MultipartFormDataContent
                {
                    { new StringContent("Kiki"), "name_for_user" },
                    { new ByteArrayContent(bytes, 0, bytes.Length), "image", "image.jpg" }
                };
                HttpResponseMessage response = await httpClient.PostAsync("http://lumen.dahu.t17.ovh/api/forward/medias", form);
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
            }
            else
            {
            }
            UpdateProfilPictureBinding.IsBusy = false;
        }
    }
}
