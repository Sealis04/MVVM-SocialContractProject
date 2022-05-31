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
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : UserControl
    {
        public LogInWindow()
        {
            InitializeComponent();
            //if (passTB.Password.Length <= 7)
            //{
            //    LoginBTN.IsEnabled = false;
            //}
        }

        private void PassTB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
            }
            //if (passTB.Password.Length <= 7)
            //{
            //    LoginBTN.IsEnabled = false;
            //}
            //else
            //{
            //    LoginBTN.IsEnabled = true;
            //}
        }
    }
}
