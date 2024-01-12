using System.ComponentModel.DataAnnotations;

#nullable enable

namespace WebBolt.Client.Models
{
    public class Gym
    {
        [Key]
        public int Gym_id { get; set; }

        [Required]
        public String? Name { get; set; }

        public bool Is_deleted { get; set; } = false;
    }
}