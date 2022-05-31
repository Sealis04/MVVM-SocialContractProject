using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_SocialContractProject.Views
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : UserControl
    {
        public CreateUserWindow()
        {
            InitializeComponent();
            Confirmpassword();
        }
        private void Confirmpassword()
        {
            if (confirmPassTB.Password.Length >= 7)
            {
                Submit.IsEnabled = true;
                confirmWarning.Opacity = 0;
                if (passTB.Password.Length >= 7)
                {
                    Submit.IsEnabled = true;
                    passwordWarning.Opacity = 0;
                }
                else
                {
                    Submit.IsEnabled = false;
                    passwordWarning.Opacity = 100;
                }
            }
            else
            {
                Submit.IsEnabled = false;
                confirmWarning.Opacity = 100;
            }
           
        }
        private void ConfirmPassTB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
            Confirmpassword();
          
        }

        private void PassTB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).ConfirmPassword = ((PasswordBox)sender).SecurePassword;
            }

            Confirmpassword();
        }

        private void UsernameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (usernameTB.Text.Length > 0)
            {
                uNameWarning.Opacity = 0;
            }
            else
            {
                uNameWarning.Opacity = 100;
            }
        }
    }
}
