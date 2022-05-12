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
    public class GridViewColumnHeaderClick
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(GridViewColumnHeaderClick), new UIPropertyMetadata(null,
                GridViewColumnHeaderClick.CommandChanged));

        public static readonly DependencyProperty CommandBehaviourProperty =
            DependencyProperty.RegisterAttached("CommandBehaviour", typeof(GridViewColumnHeaderClickCommandBehaviour), typeof(GridViewColumnHeaderClick),
                new UIPropertyMetadata(null));

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static GridViewColumnHeaderClickCommandBehaviour GetCommandBehaviour(DependencyObject obj)
        {
            return (GridViewColumnHeaderClickCommandBehaviour)obj.GetValue(CommandBehaviourProperty);
        }

        public static void SetCommandBehaviour(DependencyObject obj, GridViewColumnHeaderClickCommandBehaviour value)
        {
            obj.SetValue(CommandBehaviourProperty, value);
        }

        private static void CommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            GridViewColumnHeaderClick.GetOrCreateBehaviour(sender).Command = e.NewValue as ICommand;
        }

        private static GridViewColumnHeaderClickCommandBehaviour GetOrCreateBehaviour(DependencyObject element)
        {
            GridViewColumnHeaderClickCommandBehaviour returnVal = GridViewColumnHeaderClick.GetCommandBehaviour(element);

            if (returnVal == null)
            {
                ListView typedElement = element as ListView;

                if (typedElement == null)
                {
                    throw new InvalidOperationException("GridViewColumnHeaderClick.Command property can only be set on instances of ListView");
                }

                returnVal = new GridViewColumnHeaderClickCommandBehaviour(typedElement);

                GridViewColumnHeaderClick.SetCommandBehaviour(element, returnVal);
            }

            return returnVal;
        }
    }
}
