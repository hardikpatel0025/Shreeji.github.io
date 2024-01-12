using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace WebBolt.Client.Models
{
    public class Membership
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Gym")]
        public int Gym_id { get; set; }

        [Required]
        public String? Name { get; set; }

        [Required]
        public Double Price { get; set; }

        public int Days_available { get; set; } = 0;

        public int Entries_available { get; set; } = 0;

        public String Start_time { get; set; } = "07:00";

        public String End_time { get; set; } = "22:00";

        public int Usable_number { get; set; } = 0;

        public bool Is_deleted { get; set; } = false;
    }
}