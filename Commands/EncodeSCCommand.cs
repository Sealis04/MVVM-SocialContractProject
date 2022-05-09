using MVVM_SocialContractProject.Exceptions;
using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_SocialContractProject.Commands
{
    public class EncodeSCCommand : CommandBase
    {
        private readonly EncodeSCViewModel _encodeSCViewModel;
        private readonly SocialContractMonitoringSystem _scSystem;
        private readonly NavigationService navigationService;

        public EncodeSCCommand(EncodeSCViewModel encodeSCViewModel, 
            SocialContractMonitoringSystem scSystem,
            NavigationService navigationService)
        {
            _scSystem = scSystem;
            this.navigationService = navigationService;
            _encodeSCViewModel = encodeSCViewModel;

            _encodeSCViewModel.PropertyChanged += OnViewPropertyChanged;
        }

     

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_encodeSCViewModel.StudentID) &&
                   !string.IsNullOrEmpty(_encodeSCViewModel.Firstname) &&
                   !string.IsNullOrEmpty(_encodeSCViewModel.Lastname) &&
                   !string.IsNullOrEmpty(_encodeSCViewModel.Course) &&
                   !string.IsNullOrEmpty(_encodeSCViewModel.ImageSource) &&
                   _encodeSCViewModel.BatchNo > 0 && 
                    _encodeSCViewModel.SchoolYear > 0 &&
                   _encodeSCViewModel.FirstSem >= 0 &&
                   _encodeSCViewModel.SecondSem >=0 &&
                   _encodeSCViewModel.Summer >= 0 &&
                
                   base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            StudentInfo student = new StudentInfo(
                _encodeSCViewModel.StudentID, _encodeSCViewModel.Firstname, _encodeSCViewModel.Middlename, _encodeSCViewModel.Lastname ,_encodeSCViewModel.BatchNo, _encodeSCViewModel.Course);
            SocialContract socialContract = new SocialContract(0,student,_encodeSCViewModel.FirstSem, _encodeSCViewModel.SecondSem, _encodeSCViewModel.Summer,_encodeSCViewModel.SchoolYear, _encodeSCViewModel.ImageSource);
            try
            {
                _scSystem.CreateStudentInfo(student, socialContract, _scSystem);
                MessageBox.Show("Successfuly Encoded", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                navigationService.Navigate();
            }
            catch (SocialContractConflictExceptions)
            {
                MessageBoxResult result = MessageBox.Show("Records for the said year has already been inputted, Confirm Override?", "Error",
                    MessageBoxButton.YesNo, MessageBoxImage.Error);

                if (result == MessageBoxResult.Yes)
                {
                    _scSystem.UpdateSocialContract(student, socialContract);
                    MessageBox.Show("Successfuly Updated", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    navigationService.Navigate();
                }
                else
                {
                    return;
                }
            }
        }
        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(EncodeSCViewModel.StudentID)||
                e.PropertyName == nameof(EncodeSCViewModel.Firstname)||
                e.PropertyName == nameof(EncodeSCViewModel.Lastname)||
                e.PropertyName == nameof(EncodeSCViewModel.Course)||
                e.PropertyName == nameof(EncodeSCViewModel.BatchNo)||
                e.PropertyName == nameof(EncodeSCViewModel.SchoolYear)||
                e.PropertyName == nameof(EncodeSCViewModel.FirstSem)||
                e.PropertyName == nameof(EncodeSCViewModel.SecondSem)||
                e.PropertyName == nameof(EncodeSCViewModel.Summer) || 
                e.PropertyName == nameof(EncodeSCViewModel.ImageSource))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
