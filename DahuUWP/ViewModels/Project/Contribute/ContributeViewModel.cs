using DahuUWP.DahuTech.Container;
using DahuUWP.Services;
using DahuUWP.Views;
using DahuUWP.Views.Project.Contribute;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DahuUWP.ViewModels.Project.Contribute
{
    public class ContributeViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ContributeViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            Project = (DahuUWP.Models.Project)NavigationParam;
            List<Step> steps = new List<Step>();
            Step step1 = new Step()
            {
                StepProgressBarElem = new StepProgressBarElem()
                {
                    TextBlockStyle = Application.Current.Resources["DahuTextLinkBold"] as Style,
                    Name = "Contribution"
                    
                },
                StepView = new StepView()
                {
                    View = typeof(ContributeWithMoney)
                },
                
                DahuButtonBindings = new DahuTech.Inputs.DahuButtonBindings()
                {
                    IsBusy = false,
                    Name = "Valider",
                    Parameter = (new ContributeWithMoney().Content as FrameworkElement).DataContext
                },
                Project = Project
            };
            Step step2 = new Step()
            {
                StepProgressBarElem = new StepProgressBarElem()
                {
                    Name = "Adresse"

                },
                Status = Status.Active,
                StepView = new StepView()
                {
                    View = typeof(ContributeAdressCounterparty)
                },
                DahuButtonBindings = new DahuTech.Inputs.DahuButtonBindings()
                {
                    IsBusy = false,
                    Name = "Valider",
                    Parameter = (new ContributeAdressCounterparty().Content as FrameworkElement).DataContext
                },
                Project = Project

            };
            Step step3 = new Step()
            {
                StepProgressBarElem = new StepProgressBarElem()
                {
                    Name = "Paiement"

                },
                StepView = new StepView()
                {
                    View = typeof(ContributeSuccessPayment)
                },
                DahuButtonBindings = new DahuTech.Inputs.DahuButtonBindings()
                {
                    IsBusy = false,
                    Name = "Valider",
                    Parameter = (new ContributeSuccessPayment().Content as FrameworkElement).DataContext
                },
                Project = Project


            };
            
            // Utiliser tempStepper et non contributeStepper histoire que le user control ne soit pas notifier d'un changement du stepper dés qu'on ajoute un step...
            Stepper tempStepper = new Stepper();
            tempStepper.Steps = steps;
            tempStepper.Steps.Add(step1);
            tempStepper.Steps.Add(step2);
            tempStepper.Steps.Add(step3);
            ContributeStepper = tempStepper;

            HomePage.DahuFrame.Navigate(typeof(ContributeWithMoney), Project);


        }

        private Stepper _contributeStepper;
        public Stepper ContributeStepper
        {
            get { return _contributeStepper; }
            set
            {
                NotifyPropertyChanged(ref _contributeStepper, value);
            }
        }

        private DahuUWP.Models.Project _project;
        public DahuUWP.Models.Project Project
        {
            get { return _project; }
            set
            {
                NotifyPropertyChanged(ref _project, value);
            }
        }
    }
}
