using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChernabogJailApp.Models
{
    public class BeastVariation
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Название")]
        public string? Name { get; set; }
        [Required]
        public int BeastId { get; set; }
        [Required]
        [DisplayName("Существо")]
        public Beast Beast { get; set; }
        [Required]
        [DisplayName("Кость урона")]
        public int HitDice { get; set; }
        [Required]
        [DisplayName("Класс брони")]
        public int ArmorClass { get; set; }
        [Required]
        [DisplayName("Атака")]
        public string Attack { get; set; }
        [Required]
        [DisplayName("Повреждение")]
        public string Damage { get; set; }
        [Required]
        [DisplayName("Движение")]
        public int Movement { get; set; } = 0;
        [Required]
        [DisplayName("Полет")]
        public int Fly { get; set; } = 0;
        [Required]
        [DisplayName("Вплавь")]
        public int Swim { get; set; } = 0;
        [Required]
        [DisplayName("Телепортация")]
        public int Teleport { get; set; } = 0;
        [Required]
        [DisplayName("Боевой дух")]
        public int BattleSpirit { get; set; }
        [Required]
        [DisplayName("Спас-бросок")]
        public int SaveRoll { get; set; }
        [Required]
        [DisplayName("Навык")]
        public int Skill { get; set; }
    }
}
