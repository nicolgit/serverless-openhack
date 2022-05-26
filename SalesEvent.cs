using System;

namespace nicold.function
{
    public class SalesEventHeader
    {
        public string salesNumber { get; set; }
        public string dateTime { get; set; }
        public string locationId { get; set; }
        public string locationName { get; set; }
        public string locationAddress { get; set; }
        public string locationPostcode { get; set; }
        public string totalCost { get; set; }
        public string totalTax { get; set; }
        public string receiptUrl { get; set; }        
    }

    public class SalesEventDetail
    {
        public string productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string quantity { get; set; }
        public string unitCost { get; set; }
        public string totalCost { get; set; }
        public string totalTax { get; set; }
    }

    public class SalesEvent
    {
        public SalesEventHeader header { get; set; }
        public SalesEventDetail[] details { get; set; }
    }
}