using System;

namespace WebBolt.Client.Models
{
    public class ActorInfo
    {
        public int ActorID { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public DateTime DoB { get; set; }
        public DateTime DoD { get; set; }
        public string GenderType { get; set; }
    }
}