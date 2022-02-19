using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models
{
    public class Command
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string HowTo { get; set; } // how to do a particular activity

        [Required]
        public string CommandLine { get; set; }

        [Required]
        public int PlatformId { get; set; } // foreign key for platform id within platform db
        public Platform Platform { get; set; } // this property is called 'navigation property' "Navigation property: A property defined on the principal and/or dependent entity that references the related entity."

    }
}