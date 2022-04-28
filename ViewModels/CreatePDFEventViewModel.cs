using MVVM_SocialContractProject.Commands;
using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MVVM_SocialContractProject.ViewModels
{
    public class CreatePDFEventViewModel: ViewModelBase
    {
        private string _eventName;
        private string _eventVenue;
        private DateTime _eventDate = DateTime.Today;
        private string _eventSupervisor;

        public string EventName
        {
            get
            {
                return _eventName;
            }
            set
            {
                _eventName = value;
                OnPropertyChanged(EventName);
            }
        }

        public string EventVenue
        {
            get
            {
                return _eventVenue;
            }
            set
            {
                _eventVenue = value;
                OnPropertyChanged(EventVenue);
            }
        }

        public DateTime EventDate
        {
            get
            {
                return _eventDate;
            }
            set
            {
                _eventDate = value;
                OnPropertyChanged("EventDate");
            }
        }

        public string EventSupervisor
        {
            get
            {
                return _eventSupervisor;
            }
            set
            {
                _eventSupervisor = value;
                OnPropertyChanged(EventSupervisor);
            }
        }

        public string filePath;
        public string ImageSource {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
   

        public CreatePDFEventViewModel(SocialContractMonitoringSystem _SCSystem, NavigationService navService)
        {
            SubmitEvent = new SubmitPDFEventCommand(_SCSystem, navService, this);
            CancelEvent = new NavigateCommand(navService);
        }
        public ICommand SubmitEvent { get; }

        public ICommand CancelEvent { get; }

        public ICommand _uploadPdf;
        public ICommand UploadPDF
        {
            get
            {
                if(_uploadPdf == null)
                {
                    _uploadPdf = new UploadPdfCommand(this);
                }
                return _uploadPdf;
            }
        }

    }
}
