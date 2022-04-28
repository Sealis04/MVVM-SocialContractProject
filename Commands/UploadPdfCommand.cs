using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVVM_SocialContractProject.Commands
{
    public class UploadPdfCommand : CommandBase
    {

        private readonly CreatePDFEventViewModel pdfVM;
        private readonly EncodeSCViewModel encodeVM;

        public UploadPdfCommand(CreatePDFEventViewModel _pdfVM)
        {
            pdfVM = _pdfVM;
        }

        public UploadPdfCommand(EncodeSCViewModel _encodeVM)
        {
            encodeVM = _encodeVM;
        }

        public override void Execute(object parameter)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    //Loads image preview]
                    //pdfVM.ImageSource = new BitmapImage(new Uri(filePath));
                    if(pdfVM != null)
                    {
                        pdfVM.ImageSource = filePath;
                    }
                    else
                    {
                        encodeVM.ImageSource = filePath;
                    }
                   
                }
            }
        }
    }
}
