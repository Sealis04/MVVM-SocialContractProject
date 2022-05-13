using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Models.Database;
using MVVM_SocialContractProject.ViewModels;

namespace MVVM_SocialContractProject.Commands
{
    public class SortViewCommand : CommandBase
    {
        GridViewColumnHeader previousSortHeader = null;
        ListSortDirection previousSortDirection = ListSortDirection.Ascending;
        private SocialContractMonitoringSystem _scSystem;

        private readonly SocialContractRecordsViewModel _scVM;

        private readonly SocialContractPerUserViewModel _scPerUser;
        public SortViewCommand(SocialContractMonitoringSystem scSystem, SocialContractRecordsViewModel scVM)
        {
            _scSystem = scSystem;
            _scVM = scVM;
        }

        public SortViewCommand(SocialContractMonitoringSystem scSystem, SocialContractPerUserViewModel scVM)
        {
            _scSystem = scSystem;
            _scPerUser = scVM;
        }
        public object ItemSource { get; private set; }

        public override void Execute(object parameter)
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

        private void SortResults(string sortBy, ListSortDirection direction)
        {
            bool IsAscending;
            
            if(direction == ListSortDirection.Ascending)
            {
               IsAscending = true;
            }
            else
            {
               IsAscending = false;
            }
            if(_scVM != null)
            {
                int result = sortForStudentRecords(sortBy);
                _scVM.StudentQuery = result;
                _scVM.Direction = IsAscending;
                _scVM.UpdateReservations(_scVM.SearchText, _scVM.Start, result, IsAscending);
            }else if(_scPerUser != null)
            {
                int result = sortForPerUser(sortBy);
                _scPerUser.StudentQuery = result;
                _scPerUser.Direction = IsAscending;
                _scPerUser.LoadSocialContractInfo(_scPerUser.Student, result, IsAscending);
            }
        
        }
        private int sortForPerUser(string sortSearch)
        {
            switch (sortSearch)
            {
                case "School Year":
                    return 3;
                case "First Semester":
                    return 1;
                case "Second Semester":
                    return 2;
                case "Summer":
                    return 4;
                case "Image":
                    return 0;
                case "Print":
                    return 0;
                case "Remove":
                    return 0;
            }
            return 1;
        }

        private int sortForStudentRecords(string sortSearch)
        {
            switch (sortSearch)
            {
                case "StudentID":
                    return 1;
                case "Student Name":
                    return 2;
                case "Batch No.":
                    return 3;
                case "Course":
                    return 4;
                case "Current Hours":
                    return 5;
                case "Lacking Hours":
                    return 6;
            }
            return 1;
        }
    }
}
