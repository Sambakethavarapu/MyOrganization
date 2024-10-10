namespace MyOrganization.DataModel
{
    public class ComplaintDetails
    {
        public int complaintId { get; set; }
        public string ComplaintDescription { get; set; }
        public string ComplaintStatus { get; set; }
        public DateTime RegirsterDate { get; set; }
        public Int32 MobileNumber { get; set; }
        public string ComplaintUserName { get; set;}
    }
}
