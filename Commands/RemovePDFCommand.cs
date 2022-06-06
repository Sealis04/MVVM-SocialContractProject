using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Models.Database;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_SocialContractProject.Commands
{
    public class RemovePDFCommand : CommandBase
    {
        private readonly DatabaseQueries _dbQueries;
        private readonly NavigationService thisModel;

        public RemovePDFCommand(NavigationService thisModel, SocialContractMonitoringSystem scSystem)
        {
            _dbQueries = new DatabaseQueries();
            this.thisModel = thisModel;
        }

        public override void Execute(object parameter)
        {
            EventsPDFViewModel PDF = parameter as EventsPDFViewModel;
            MessageBoxResult result = MessageBox.Show("Confirm Removal of PDF record?", "Remove Record", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                //Db query
                _dbQueries.RemovePDF(PDF.EventID, PDF.EventImage);
                var x = new NavigateCommand(thisModel);
                x.Execute(parameter);
            }
            else if (result == MessageBoxResult.Cancel)
            {
                return;
            }
        }
    }
}
