using MVVM_SocialContractProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MVVM_SocialContractProject.ViewModels
{
    public class SocialContractViewModel : ViewModelBase
    {
        private readonly SocialContract _socialContract;

        public string StudentID => _socialContract.StudentID.ToString();

        public int SocialContractID => _socialContract.SocialContractID;
        public int SchoolYear => _socialContract.SchoolYear;
        public int FirstSem => _socialContract.FirstSemester;
        public int SecondSem => _socialContract.SecondSemester;
        public int Summer => _socialContract.Summer;

        public string ImageSource => _socialContract.SocialContractimage;

        public Image buttonImage;
        public BitmapImage ButtonImage
        {
            get
            {
                try
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(ImageSource, UriKind.Relative);
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    return image;
                }catch(Exception e)
                {
                    return null;
                }
            
            }
 
        }

        
        public int TotalHours => FirstSem + SecondSem + Summer;
        public SocialContractViewModel(SocialContract socialContract)
        {
            _socialContract = socialContract;
        }
    }
}
