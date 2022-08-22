using System.ComponentModel.DataAnnotations;

namespace ChernabogJailApp.RuleBook
{
    public class CommonEnums
    {
        public enum EnumAttributes
        {
            [Display(Name = "Сила")]
            Strength,
            [Display(Name = "Ловкость")]
            Dexterity,
            [Display(Name = "Телосложение")]
            Constitution,
            [Display(Name = "Мудрость")]
            Wisdom,
            [Display(Name = "Интеллект")]
            Intellegence,
            [Display(Name = "Харизма")]
            Charisma
        }

        public enum EnumArmorType
        {
            [Display(Name = "Легкая")]
            Light,
            [Display(Name = "Средняя")]
            Medium,
            [Display(Name = "Тяжелая")]
            Heavy,
            [Display(Name = "Элитна")]
            Elite,
            [Display(Name = "Щит")]
            Shield
        }
        public enum EnumWeaponType
        {
            [Display(Name = "Легкое")]
            Light,
            [Display(Name = "Среднее")]
            Medium,
            [Display(Name = "Тяжелое")]
            Heavy,
            [Display(Name = "Дальнее I")]
            LongRangeOne,
            [Display(Name = "Дальнее II")]
            LongRangeTwo,
            [Display(Name = "Гранаты")]
            Bombs,
            [Display(Name = "Боеприпасы")]
            Ammunition
        }
        public enum EnumLevel
        {
            [Display(Name = "Младший")]
            Minor,
            [Display(Name = "Старший")]
            Major,
            [Display(Name = "Великий")]
            Great,
            [Display(Name = "Легендарный")]
            Legendary,
            [Display(Name = "Эпический")]
            Epic
        }
        public enum EnumTime
        {
            [Display(Name = "Мгновенно")]
            Instant,
            [Display(Name = "Основное действие")]
            MainAction,
            [Display(Name = "На ходу")]
            Running,
            [Display(Name = "Постоянно")]
            Constantly,
            [Display(Name = "5 минут")]
            FiveMinutes,
            [Display(Name = "10 минут")]
            TenMinutes,
            [Display(Name = "1 час")]
            OneHour,
            [Display(Name = "6 часов")]
            SixHours,
            [Display(Name = "1 день")]
            OneDay,
            [Display(Name = "Особое")]
            Special
        }
        public enum EnumSphere
        {
            [Display(Name = "Исцеления")]
            Healing,
            [Display(Name = "Смерти")]
            Death,
            [Display(Name = "Животных")]
            Animals,
            [Display(Name = "Страсти")]
            Passion,
            [Display(Name = "Духов")]
            Spirits,
            [Display(Name = "Солнца")]
            Sun,
            [Display(Name = "Войны")]
            War,
            [Display(Name = "Воды")]
            Water
        }
        public enum EnumClass
        {
            [Display(Name = "Воин")]
            Warrior,
            [Display(Name = "Вор")]
            Thief,
            [Display(Name = "Жрец")]
            Cleric,
            [Display(Name = "Волшебник")]
            Wizard,
            [Display(Name = "Посвященный")]
            Enlightened,
            [Display(Name = "Наследник")]
            Inheritor,
            [Display(Name = "Последний принц")]
            LastPrince,
            [Display(Name = "Еретик")]
            Heretic,
        }
    }
}
