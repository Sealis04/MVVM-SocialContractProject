using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MVVM_SocialContractProject.Commands
{
    public class SortViewCommand
    {
        GridViewColumnHeader previousSortHeader = null;
        ListSortDirection previousSortDirection = ListSortDirection.Ascending;
        private void SortResults(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(this.SearchResults);    // where SearchResults is the data to which the ListView is bound
            dataView.SortDescriptions.Clear();

            SortDescription sortDescription = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sortDescription);
            dataView.Refresh();
        }

        private void SortViewCommandHandler(object parameter)
        {
            GridViewColumnHeader typedParameter = parameter as GridViewColumnHeader;

            ListSortDirection direction;

            if (typedParameter != null)
            {
                if (typedParameter.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (typedParameter != this.previousSortHeader)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (this.previousSortDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    string headerLabel = typedParameter.Column.Header as string;

                    this.SortResults(headerLabel, direction);

                    //if (direction == ListSortDirection.Ascending)
                    //{
                    //    typedParameter.Column.HeaderTemplate =
                    //      Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    //}
                    //else
                    //{
                    //    typedParameter.Column.HeaderTemplate =
                    //      Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    //}

                    // Remove arrow from previously sorted header
                    if ((this.previousSortHeader != null) && (this.previousSortHeader != typedParameter))
                    {
                        this.previousSortHeader.Column.HeaderTemplate = null;
                    }

                    this.previousSortHeader = typedParameter;
                    this.previousSortDirection = direction;
                }
            }
        }
    }
}
