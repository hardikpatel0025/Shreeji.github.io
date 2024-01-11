using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace WebBolt.Client.Models
{
    public class Entry
    {
        [Key]
        public int Entry_Id { get; set; }

        [ForeignKey("Client")]
        public int Client_Id { get; set; }

        [ForeignKey("Membership")]
        public int Membership_Id { get; set; }

        public DateTime Date { get; set; }

        public String? Bar_code { get; set; }

        [ForeignKey("Gym")]
        public int Gym_Id { get; set; }
    }
}