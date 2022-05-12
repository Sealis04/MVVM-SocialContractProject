using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void AllInventoryRec_Click_1(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    string header = headerClicked.Column.Header as string;
                    Sort(header, direction);
                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }


        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(AllInventoryRec.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
    }
}
