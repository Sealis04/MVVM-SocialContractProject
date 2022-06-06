using System;

namespace MVVM_SocialContractProject.Models
{
    public class PDFInfo
    {
        public PDFInfo(string EventID, string eventName, string eventSupervisor, string eventPDFSource, string eventVenue, DateTime eventDate)
        {
            this.EventName = eventName;
            this.EventSupervisor = eventSupervisor;
            this.EventPDFSource = eventPDFSource;
            this.EventVenue = eventVenue;
            this.EventDate = eventDate;
        }

        public string EventID { get; }
        public string EventName { get; }
        public string EventSupervisor { get; }
        public DateTime EventDate { get; }

        public string EventPDFSource { get; }
        public string EventVenue { get; }

        public override bool Equals(object obj)
        {
            return obj is PDFInfo info &&
                   EventName == info.EventName &&
                   EventSupervisor == info.EventSupervisor &&
                   EventDate == info.EventDate &&
                   EventPDFSource == info.EventPDFSource &&
                   EventVenue == info.EventVenue;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(PDFInfo pdf1, PDFInfo pdf2)
        {
            if (pdf1 is null && pdf2 is null)
            {
                return true;
            }
            return !(pdf1 is null) && pdf1.Equals(pdf2);
        }
        public static bool operator !=(PDFInfo pdf1, PDFInfo pdf2)
        {
            return pdf1 != pdf2;
        }
    }
}
