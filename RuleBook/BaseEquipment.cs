using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChernabogJailApp.RuleBook
{
    public abstract class BaseEquipment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        public string? Description { get; set; }
        [Required]
        [DisplayName("Цена")]
        public int Price { get; set; }
        [DisplayName("Вес")]
        public int? Weight { get; set; }
    }
}
