using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MVVM_SocialContractProject.Commands
{
    public class PrintSocialContract : CommandBase
    {
        public override void Execute(object parameter)
        {
            PrintDialog print = new PrintDialog();
            if (print.ShowDialog() == true)
            {
                var info = new ProcessStartInfo()
                {
                    Verb = "",
                    CreateNoWindow = true,
                    FileName = "\\\\" + Properties.Settings.Default.Server+"\\SocialContractFolder\\SocialContractPDF.pdf",
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                try
                {
                    using (Process.Start(info))
                    {
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error Message" + e, "PRINTING ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
