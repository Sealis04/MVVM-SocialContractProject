using MVVM_SocialContractProject.Models.Database;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM_SocialContractProject.Commands
{
    public class RemoveSocialContract : CommandBase
    {
        private readonly DatabaseQueries _dbQueries;
        private readonly NavigationService thisModel;

        public RemoveSocialContract(NavigationService thisModel, StudentInfoViewModel student)
        {
            _dbQueries = new DatabaseQueries();
            this.thisModel = thisModel;
            Student = student;
        }

        public StudentInfoViewModel Student { get; }

        public override void Execute(object parameter)
        {
            SocialContractViewModel scVM = parameter as SocialContractViewModel;
            MessageBoxResult result = MessageBox.Show("Confirm Removal of Social Contract record?", "Remove Record", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                //Db query
                _dbQueries.RemoveSocialContract(scVM.SocialContractID);
               var x =  new NavigateCommand(thisModel, Student);
                x.Execute(null);
            }
            else if(result == MessageBoxResult.Cancel)
            {
                return;
            }
        }
    }
}
