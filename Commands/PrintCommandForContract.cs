using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVM_SocialContractProject.Commands
{
    public class PrintCommandForContract :CommandBase
    {

        private readonly StudentInfoViewModel _student;
        public PrintCommandForContract(StudentInfoViewModel student)
        {
            _student = student;
        }

        public SocialContractMonitoringSystem ScSystem { get; }

        public override void Execute(object parameter)
        {
            SocialContractPerUserViewModel scPUVM = parameter as SocialContractPerUserViewModel;
            SocialContractViewModel scVM = parameter as SocialContractViewModel;
            PrintDialog print = new PrintDialog();
            FlowDocument doc = CreateFlowDocument(scVM, scPUVM);
            doc.ColumnWidth = 999999;
            IDocumentPaginatorSource idpSource = doc;
            if(print.ShowDialog() == true)
            {
                print.PrintDocument(idpSource.DocumentPaginator, "PDF PRINTING");
            }
        }

        private FlowDocument CreateFlowDocument(SocialContractViewModel scVM, SocialContractPerUserViewModel scPUVM)
        {
            FlowDocument doc = new FlowDocument();

            //Create the section
            Section sec = new Section();
            //Table for the name, id etc.
            //Creating the table
            Table table = new Table();

            //Creating the columns;
            TableColumn tblColumn1 = new TableColumn();
            TableColumn tblColumn2 = new TableColumn();
            TableColumn tblColumn3 = new TableColumn();

            //Creating the table rows
            TableRowGroup RowGroup1 = new TableRowGroup();
            TableRow tblRow1 = new TableRow();
            RowGroup1.Rows.Add(tblRow1);
            //Row 1 Cell1
            TableCell tblRow1_Cell1 = new TableCell();
            tblRow1_Cell1.ColumnSpan = 2;

            Paragraph Cell1_sID = new Paragraph();
            Cell1_sID.Inlines.Add(new Run("Student ID: " + _student.StudentID));
            tblRow1_Cell1.Blocks.Add(Cell1_sID);
            //Row1 Cell2
            TableCell tblRow1_Cell2 = new TableCell();
            tblRow1_Cell2.ColumnSpan = 2;

            Paragraph Cell2_ys = new Paragraph();
            Cell2_ys.Inlines.Add(new Run("Year and Section: " + _student.Course + " " +_student.BatchNo));
            tblRow1_Cell2.Blocks.Add(Cell2_ys);


            //Row1 Cell3 -> Move to Row2
            TableRow tblRow2 = new TableRow();
            RowGroup1.Rows.Add(tblRow2);
            TableCell tblRow2_Cell1 = new TableCell();
            tblRow2_Cell1.ColumnSpan = 4;

            Paragraph Cell1_sName = new Paragraph();
            Cell1_sName.Inlines.Add(new Run("Student Name: " + _student.StudentName));
            tblRow2_Cell1.Blocks.Add(Cell1_sName);

            tblRow1.Cells.Add(tblRow1_Cell1);
            tblRow1.Cells.Add(tblRow1_Cell2);
            tblRow2.Cells.Add(tblRow2_Cell1);

            table.Columns.Add(tblColumn1);
            table.Columns.Add(tblColumn2);
            table.Columns.Add(tblColumn3);
            table.RowGroups.Add(RowGroup1);

            //Table for records
            Table table2 = new Table();

            TableColumn tbl2_TableColumn1 = new TableColumn();
            TableColumn tbl2_TableColumn2 = new TableColumn();
            TableColumn tbl2_TableColumn3 = new TableColumn();

            TableRowGroup tbl_2tblRowGroup = new TableRowGroup();
            TableRow tbl_2Row1 = new TableRow();
            tbl_2tblRowGroup.Rows.Add(tbl_2Row1);

            TableCell CellYear = new TableCell();
            Paragraph Year = new Paragraph();
            Year.Inlines.Add(new Run("Year"));
            CellYear.Blocks.Add(Year);

            TableCell CellFirstSem = new TableCell();
            Paragraph FirstSemester = new Paragraph();
            FirstSemester.Inlines.Add(new Run("First Semester"));
            CellFirstSem.Blocks.Add(FirstSemester);

            TableCell CellSecondSem = new TableCell();
            Paragraph SecondSemester = new Paragraph();
            SecondSemester.Inlines.Add(new Run("Second Semester"));
            CellSecondSem.Blocks.Add(SecondSemester);


            TableCell CellSummer = new TableCell();
            Paragraph Summer = new Paragraph();
            Summer.Inlines.Add(new Run("Summer"));
            CellSummer.Blocks.Add(Summer);

            tbl_2Row1.Cells.Add(CellYear);
            tbl_2Row1.Cells.Add(CellFirstSem);
            tbl_2Row1.Cells.Add(CellSecondSem);
            tbl_2Row1.Cells.Add(CellSummer);

            //Row 2


            int sctotalhours = 0;
            //The forloop part
            if (scVM != null)
            {
                TableRow tbl2_Row2 = new TableRow();
                tbl_2tblRowGroup.Rows.Add(tbl2_Row2);

                TableCell CellYearContents = new TableCell(new Paragraph(new Run(scVM.SchoolYear.ToString())));

                TableCell CellFirstSemContents = new TableCell(new Paragraph(new Run(scVM.FirstSem.ToString())));

                TableCell CellSecondSemContents = new TableCell(new Paragraph(new Run(scVM.SecondSem.ToString())));

                TableCell CellSummerContents = new TableCell(new Paragraph(new Run(scVM.Summer.ToString())));

                tbl2_Row2.Cells.Add(CellYearContents);
                tbl2_Row2.Cells.Add(CellFirstSemContents);
                tbl2_Row2.Cells.Add(CellSecondSemContents);
                tbl2_Row2.Cells.Add(CellSummerContents);
                sctotalhours += scVM.FirstSem + scVM.SecondSem + scVM.Summer;
            }
            else if(scPUVM != null)
            {
                foreach(SocialContractViewModel scVM2 in scPUVM.SocialContract)
                {
                    TableRow tbl2_Row2 = new TableRow();
                    tbl_2tblRowGroup.Rows.Add(tbl2_Row2);

                    TableCell CellYearContents = new TableCell(new Paragraph(new Run(scVM2.SchoolYear.ToString())));

                    TableCell CellFirstSemContents = new TableCell(new Paragraph(new Run(scVM2.FirstSem.ToString())));

                    TableCell CellSecondSemContents = new TableCell(new Paragraph(new Run(scVM2.SecondSem.ToString())));

                    TableCell CellSummerContents = new TableCell(new Paragraph(new Run(scVM2.Summer.ToString())));

                    tbl2_Row2.Cells.Add(CellYearContents);
                    tbl2_Row2.Cells.Add(CellFirstSemContents);
                    tbl2_Row2.Cells.Add(CellSecondSemContents);
                    tbl2_Row2.Cells.Add(CellSummerContents);
                    sctotalhours += scVM2.FirstSem + scVM2.SecondSem + scVM2.Summer;
                }
            }

            //Row3
            TableRow tbl2_Row3 = new TableRow();
            tbl_2tblRowGroup.Rows.Add(tbl2_Row3);

            TableCell CellTotalHours = new TableCell();
            CellTotalHours.ColumnSpan = 3;
            Paragraph TotalHours = new Paragraph();
            TotalHours.Inlines.Add(new Run("Total Hours"));
            CellTotalHours.Blocks.Add(TotalHours);


            TableCell CellTotalHoursContents = new TableCell();
            Paragraph TotalHoursContents = new Paragraph();

            foreach (TableRow row in tbl_2tblRowGroup.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    
                }
            }
            TotalHoursContents.Inlines.Add(new Run(sctotalhours.ToString()));
            CellTotalHoursContents.Blocks.Add(TotalHoursContents);

            tbl2_Row3.Cells.Add(CellTotalHours);
            tbl2_Row3.Cells.Add(CellTotalHoursContents);


            table2.Columns.Add(tbl2_TableColumn1);
            table2.Columns.Add(tbl2_TableColumn2);
            table2.Columns.Add(tbl2_TableColumn3);
            table2.RowGroups.Add(tbl_2tblRowGroup);
            sec.Blocks.Add(table);
            sec.Blocks.Add(table2);

            //Adding Image at the end
            Table table3 = new Table();
            TableColumn tbl3_TableColumn1 = new TableColumn();
            TableRowGroup tbl3_RowGroup = new TableRowGroup();
            table3.Columns.Add(tbl3_TableColumn1);
            table3.RowGroups.Add(tbl3_RowGroup);
            sec.Blocks.Add(table3);
            Rectangle line2 = new Rectangle();
            BlockUIContainer line = new BlockUIContainer(line2);
            line2.StrokeThickness = 3;
            line2.Height = 3;
            line2.Stroke = new SolidColorBrush(Colors.Black);
            if (scVM != null)
            {
                TableRow tbl3Row = new TableRow();
                tbl3_RowGroup.Rows.Add(tbl3Row);
                TableCell CellImage = new TableCell();
                Image ImageLink = new Image();
                ImageLink.Source = new BitmapImage(new Uri(@scVM.ImageSource));
                BlockUIContainer ImageContainer = new BlockUIContainer(ImageLink);
                CellImage.Blocks.Add(ImageContainer);
                tbl3Row.Cells.Add(CellImage);
            }
            else if (scPUVM != null)
            {
                foreach (SocialContractViewModel scVM2 in scPUVM.SocialContract)
                {
                    TableRow tbl3Row = new TableRow();
                    tbl3_RowGroup.Rows.Add(tbl3Row);
                    TableCell CellImage = new TableCell();
                    Image ImageLink = new Image();
                    ImageLink.Source = new BitmapImage(new Uri(@scVM2.ImageSource));
                    BlockUIContainer ImageContainer = new BlockUIContainer(ImageLink);
                    Rectangle line3 = new Rectangle();
                    BlockUIContainer line4 = new BlockUIContainer(line3);
                    CellImage.Blocks.Add(ImageContainer);
                    CellImage.BorderBrush = new SolidColorBrush(Colors.Black);
                    CellImage.BorderThickness = new Thickness(0, 5, 0, 0);
                    CellImage.Padding = new Thickness(0,5,0,0);
                    tbl3Row.Cells.Add(CellImage);
                }
            }

            sec.Blocks.Add(line);
            doc.Blocks.Add(sec);

            //Styles here:

            table2.BorderThickness = new Thickness(1);
            table2.BorderBrush = new SolidColorBrush(Colors.Black);
            table2.CellSpacing = 0;

            foreach (TableCell cell in tbl_2Row1.Cells)
            {
                cell.BorderBrush = new SolidColorBrush(Colors.Black);
                cell.BorderThickness = new Thickness(1);
            }
            
            foreach (TableRow row in tbl_2tblRowGroup.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.BorderBrush = new SolidColorBrush(Colors.Black);
                    cell.BorderThickness = new Thickness(1);
                }
            }

            foreach (TableCell cell in tbl2_Row3.Cells)
            {
                cell.BorderBrush = new SolidColorBrush(Colors.Black);
                cell.BorderThickness = new Thickness(1);
            }

            return doc;
        }
    }
}
