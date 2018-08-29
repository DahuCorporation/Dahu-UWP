using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DahuUWP.DahuTech.Container
{
    public class Stepper
    {
        public List<Step> Steps { get; set; }

        public bool CheckLastStepIsActive(Step step)
        {
            for (int i = 0; i < Steps.Count; i++)
            {
                if (i != 0 && Steps[i].Equals(step) && Steps[i - 1].Status == Status.Active)
                {
                    return true;
                }
            }
            return false;
        }

        public void MakeStepActive(Step step)
        {
            step.Status = Status.Active;
            step.StepProgressBarElem.TextBlockStyle = Application.Current.Resources["DahuTextLinkBold"] as Style;
            foreach (Step elem in Steps)
            {
                if (!elem.Equals(step) && elem.Status == Status.Active)
                {
                    elem.Status = Status.Passed;
                    elem.StepProgressBarElem.TextBlockStyle = Application.Current.Resources["DahuTextLink"] as Style;
                    break;
                }
            }
        }
    }
}
