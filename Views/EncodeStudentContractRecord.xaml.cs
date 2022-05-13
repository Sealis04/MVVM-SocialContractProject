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
            if (Convert.ToInt32(bn_Tb.Text) > 0)
            {
                BatchWarning.Opacity = 0;
            }
            else
            {
                BatchWarning.Opacity = 100;
            }
        }

        private void Sy_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(sy_Tb.Text))
            {
                sy_Tb.Text = "0";
            }

            if (Convert.ToInt32(sy_Tb.Text)> 0)
            {
                SYWarning.Opacity = 0;
            }
            else
            {
                SYWarning.Opacity = 100;
            }
        }

        private void FirstSem_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(firstSem_Tb.Text))
            {
                firstSem_Tb.Text = "0";
                CheckOpacity();
            }
            if (Convert.ToInt32(firstSem_Tb.Text) > 0)
            {
                CheckOpacity();
            }
        }

        private void SecondSem_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(SecondSem_Tb.Text))
            {
                SecondSem_Tb.Text = "0";
                CheckOpacity();
            }
            if (Convert.ToInt32(SecondSem_Tb.Text) > 0)
            {
                CheckOpacity();
            }
        }

        private void Summer_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(summer_Tb.Text))
            {
                summer_Tb.Text = "0";
                CheckOpacity();
            }
            if (Convert.ToInt32(summer_Tb.Text) > 0)
            {
                CheckOpacity();
            }
        }

        private void CheckOpacity()
        {
            if (Convert.ToInt32(firstSem_Tb.Text) > 0 || Convert.ToInt32(SecondSem_Tb.Text) > 0
               || Convert.ToInt32(summer_Tb.Text) > 0)
            {
                RecordsWarning.Opacity = 0;
            }
            else
            {
                RecordsWarning.Opacity = 100 ;
            }
        }

        private void SID_tb_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            if (sID_tb.Text.Length > 0)
            {
                s_IDWarning.Opacity = 0;
            }
            else
            {
                s_IDWarning.Opacity = 100;
            }
            
        }

        private void Fn_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fn_Tb.Text.Length > 0)
            {
                fNameWarning.Opacity = 0;
            }
            else
            {
                fNameWarning.Opacity = 100;
            }
        }

        private void Ln_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ln_Tb.Text.Length > 0)
            {
                lNameWarning.Opacity = 0;
            }
            else
            {
                lNameWarning.Opacity = 100;
            }
        }

        private void Course_Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (course_Tb.Text.Length > 0)
            {
                CourseWarning.Opacity = 0;
            }
            else
            {
                CourseWarning.Opacity = 100;
            }
        }
    }
}
