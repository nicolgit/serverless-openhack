using System;

namespace nicold.function
{
    public class RatingObject
    {
        public Guid id { get; set; }
        public string userId { get; set; }
        public string productId { get; set; }
        public string timestamp { get; set; }
        public string locationName { get; set; }
        public int rating { get; set; }
        public string userNotes { get; set; }
    }
}