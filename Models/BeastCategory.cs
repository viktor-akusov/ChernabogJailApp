using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChernabogJailApp.Models
{
    public class BeastCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
