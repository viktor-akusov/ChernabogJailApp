using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static ChernabogJailApp.RuleBook.CommonEnums;

namespace ChernabogJailApp.Models
{
    [DisplayName("Особая сила")]
    public class SpecialPower
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Класс")]
        [EnumDataType(typeof(EnumClass))]
        public EnumClass Class { get; set; }
        [EnumDataType(typeof(EnumLevel))]
        [DisplayName("Уровень")]
        public EnumLevel? Level { get; set; }
        [DisplayName("Сфера")]
        [EnumDataType(typeof(EnumSphere))]
        public EnumSphere? Sphere { get; set; }
        [DisplayName("Время")]
        [EnumDataType(typeof(EnumTime))]
        public EnumTime? Time { get; set; }
        [DisplayName("Очки")]
        public int? Points { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Описание")]
        public string Description { get; set; }

    }
}