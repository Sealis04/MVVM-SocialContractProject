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
    /// Interaction logic for UploadPDFEvent.xaml
    /// </summary>
    public partial class UploadPDFEvent : UserControl
    {
        public UploadPDFEvent()
        {
            InitializeComponent();
        }

        private void ActNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (actNameTb.Text.Length > 0)
            {
                activityWarning.Opacity = 0;
            }
            else
            {
                activityWarning.Opacity = 100;
            }
        }

        private void VenueTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (venueTb.Text.Length > 0)
            {
                venueWarning.Opacity = 0;
            }
            else
            {
                venueWarning.Opacity = 100;
            }
        }

        private void SupervisorTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (supervisorTb.Text.Length > 0)
            {
                sViserWarning.Opacity = 0;
            }
            else
            {
                sViserWarning.Opacity = 100;
            }
        }
    }
}
