using MVVM_SocialContractProject.ViewModels;
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
    /// Interaction logic for ViewStudentRecords.xaml
    /// </summary>
    public partial class ViewStudentRecords : UserControl
    {
        public ViewStudentRecords()
        {
            InitializeComponent();

        }

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var studentInfo = ((ListViewItem)sender).Content as StudentInfoViewModel; 
        }
    }
}
