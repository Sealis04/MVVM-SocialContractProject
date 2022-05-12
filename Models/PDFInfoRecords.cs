using MVVM_SocialContractProject.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Models
{
    class PDFInfoRecords
    {
        private readonly List<PDFInfo> _pdfInfo;
        private readonly DatabaseQueries _dbQueries;

        public PDFInfoRecords()
        {
            _pdfInfo = new List<PDFInfo>();
            _dbQueries = new DatabaseQueries();
        }

        public void AddPDFEvent(PDFInfo pdf)
        {
            //foreach(PDFInfo _pdf in _pdfInfo)
            //{
                
            //}
            _dbQueries.AddPdf(pdf);
            _pdfInfo.Add(pdf);
        }

        //Get all Event list
        public IEnumerable<PDFInfo> GetAllEvents(string SearchQuery, int page)
        {
            _pdfInfo.Clear();
            _dbQueries.GetAllPDF(_pdfInfo, SearchQuery, page);
            return _pdfInfo;
        }
    }
}
