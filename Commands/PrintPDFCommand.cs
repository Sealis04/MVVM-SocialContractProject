using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVVM_SocialContractProject.Commands
{
    public class PrintPDFCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            EventsPDFViewModel evm = parameter as EventsPDFViewModel;

            var info = new ProcessStartInfo()
            {
                Verb = "print",
                CreateNoWindow = true,
                FileName = evm.EventImage,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(info);
        }
    }
}
