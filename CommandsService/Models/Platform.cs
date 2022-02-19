using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; } // primary key of the platform (from platform service)
        
        [Required]
        public string Name { get; set; }
        public ICollection<Command> Commands { get; set; } = new List<Command>(); // this property is called 'collection navigation property'

    }
}