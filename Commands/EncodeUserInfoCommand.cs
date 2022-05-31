
using MVVM_SocialContractProject.Exceptions;
using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Models.Database;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_SocialContractProject.Commands
{
    class EncodeUserInfoCommand : CommandBase
    {
        private readonly SignUpViewModel signUpViewModel;
        private readonly SocialContractMonitoringSystem _scSystem;
        private readonly NavigationService navigationService;
        private readonly DatabaseQueries databaseQueries;

        public EncodeUserInfoCommand(SignUpViewModel signUpViewModel, SocialContractMonitoringSystem scSystem, NavigationService navigationService)
        {
            this.signUpViewModel = signUpViewModel;
            _scSystem = scSystem;
            this.navigationService = navigationService;
            databaseQueries = new DatabaseQueries();

            this.signUpViewModel.PropertyChanged += OnViewPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            try
            {
                return !string.IsNullOrEmpty(signUpViewModel.Username) &&
                 base.CanExecute(parameter);
            }
            catch (NullReferenceException)
            {
                return false;
            }
            
        }
        public override void Execute(object parameter)
        {
            string result2;
            try
            {
                if(IsEqualTo(signUpViewModel.Password, signUpViewModel.ConfirmPassword))
                {
                    byte[] saltvar = salt();
                    string password = Convert.ToBase64String(databaseQueries.DeriveKey(signUpViewModel.Password, saltvar));
                    string theString = new NetworkCredential("", signUpViewModel.Password).Password;
                    result2 = _scSystem.CreateUserInfo(new UserInfo(signUpViewModel.Username, password, Convert.ToBase64String(saltvar), 0));
                    _scSystem.GetUserInfo(signUpViewModel.Username);
                    if (result2 == "adminconflict")
                    {
                        MessageBoxResult result = MessageBox.Show("User is an admin, cannot override", "Error",
                                       MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if(result2 == "false")
                    {
                        MessageBoxResult result = MessageBox.Show("User already exists, overwrite?", "Error",
                                         MessageBoxButton.YesNo, MessageBoxImage.Error);
                        if (result == MessageBoxResult.Yes)
                        {
                            if (IsEqualTo(signUpViewModel.Password, signUpViewModel.ConfirmPassword))
                            {
                                _scSystem.UpdateUserInfo(new UserInfo(signUpViewModel.Username, password, Convert.ToBase64String(saltvar), 0));
                                navigationService.Navigate();
                            }
                            else
                            {
                                MessageBox.Show("Passwords do not match", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        navigationService.Navigate();
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match", "Error",
                 MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (UserLoginConflictException)
            {
                
            }
        }

        public static bool IsEqualTo(SecureString ss1, SecureString ss2)
        {
            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;
            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);
                int length1 = Marshal.ReadInt32(bstr1, -4);
                int length2 = Marshal.ReadInt32(bstr2, -4);
                if (length1 == length2)
                {
                    for (int x = 0; x < length1; ++x)
                    {
                        byte b1 = Marshal.ReadByte(bstr1, x);
                        byte b2 = Marshal.ReadByte(bstr2, x);
                        if (b1 != b2) return false;
                    }
                }
                else return false;
                return true;
            }
            finally
            {
                if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
                if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);
            }
        }
        private byte[] salt()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[10];
            provider.GetBytes(salt);
            return salt;
        }
        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(signUpViewModel.Username) ||
              e.PropertyName == nameof(signUpViewModel.Password) ||
              e.PropertyName == nameof(signUpViewModel.ConfirmPassword) )
            {
                OnCanExecuteChanged();
            }
        }
    }
}
