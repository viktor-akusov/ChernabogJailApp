using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ChernabogJailApp.Models
{
    public class Beast
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Категория")]
        public int? BeastCategoryId { get; set; }
        [DisplayName("Категория")]
        public BeastCategory? BeastCategory { get; set; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
