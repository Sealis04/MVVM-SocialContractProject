using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_SocialContractProject.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Tells if the command can execute. False = disabled, if parameter changes, the event gets called again and checks;
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>false = can't. True = can</returns>
        public virtual bool CanExecute(object parameter)
        {
            // For now
            return true;
        }

        /// <summary>
        /// Code to execute when button is pressed
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);

        protected void OnCanExecuteChanged()
        {
            //Event args currently empty
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
