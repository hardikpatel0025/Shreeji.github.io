using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace WebBolt.Client.Models
{
    public class ClientMembership
    {
        [Key]
        public int Client_Membership_Id { get; set; }

        [ForeignKey("Client")]
        public int Client_Id { get; set; }

        [ForeignKey("Membership")]
        public int Membership_Id { get; set; }

        public DateTime Buying_Date { get; set; }

        public String? Bar_Code { get; set; }

        public int Entry_Count { get; set; } = 0;

        public Double Price { get; set; }

        public DateTime Available_until { get; set; }

        public DateTime First_Usage_Date { get; set; }

        [ForeignKey("Gym")]
        public int Gym_Id { get; set; }
    }
}