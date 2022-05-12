using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_SocialContractProject.Behaviors
{
    public class GridViewColumnHeaderClickCommandBehaviour
    {
        public GridViewColumnHeaderClickCommandBehaviour(ListView element)
        {
            element.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(this.ClickEventHandler));
        }

        public ICommand Command { get; set; }

        private void ClickEventHandler(object sender, RoutedEventArgs e)
        {
            ICommand localCommand = this.Command;
            object parameter = e.OriginalSource as GridViewColumnHeader;

            if ((localCommand != null) && localCommand.CanExecute(parameter))
            {
                localCommand.Execute(parameter);
            }
        }
    }
}
