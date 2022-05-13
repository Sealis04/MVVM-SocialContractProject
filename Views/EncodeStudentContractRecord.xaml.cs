using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EncodeStudentContractRecord.xaml
    /// </summary>
    public partial class EncodeStudentContractRecord : UserControl
    {
        public EncodeStudentContractRecord()
        {
            
            InitializeComponent();
        }

        private void Bn_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(bn_Tb.Text))
            {
                bn_Tb.Text = "0";
            }
        }

        private void Sy_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(sy_Tb.Text))
            {
                sy_Tb.Text = "0";
            }
        }

        private void FirstSem_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(firstSem_Tb.Text))
            {
                firstSem_Tb.Text = "0";
            }
        }

        private void SecondSem_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(SecondSem_Tb.Text))
            {
                SecondSem_Tb.Text = "0";
            }
        }

        private void Summer_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(summer_Tb.Text))
            {
                summer_Tb.Text = "0";
            }
        }
    }
}
