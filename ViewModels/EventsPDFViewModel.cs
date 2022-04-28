using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.ViewModels
{
    public class EventsPDFViewModel : ViewModelBase
    {
        private readonly PDFInfo pdf;

        public EventsPDFViewModel(PDFInfo _pdf)
        {
            pdf = _pdf;
        }

        public string EventName => pdf.EventName;
        public string EventDate => pdf.EventDate.ToShortDateString();

        public string EventVenue => pdf.EventVenue;

        public string EventImage => pdf.EventPDFSource;

        public string EventSupervisor => pdf.EventSupervisor;
    }
}
