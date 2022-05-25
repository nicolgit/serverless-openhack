// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System;

namespace nicold.function
{  
    public class UserObject
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string fullName { get; set; }
    }
}