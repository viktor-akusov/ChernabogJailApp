using ChernabogJailApp.RuleBook;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ChernabogJailApp.RuleBook.CommonEnums;

namespace ChernabogJailApp.Models
{
    public class Armor: BaseEquipment
    {
        [Required]
        [DisplayName("Тип оружия")]
        [EnumDataType(typeof(EnumArmorType))]
        public EnumArmorType ArmorType { get; set; }
        [NotMapped]
        [DisplayName("Класс брони")]
        public int ArmorClass { 
            get 
            { 
                switch(ArmorType)
                {
                    case EnumArmorType.Light:
                    case EnumArmorType.Shield:
                        return 12;
                    case EnumArmorType.Medium:
                        return 14;
                    case EnumArmorType.Heavy:
                        return 16;
                    case EnumArmorType.Elite:
                        return 18;
                    default:
                        return 10;
                }
            } 
            set 
            {
                return;
            } 
        }
        [NotMapped]
        [DisplayName("Цена")]
        public new int Price
        {
            get
            {
                switch (ArmorType)
                {
                    case EnumArmorType.Light:
                        return 25;
                    case EnumArmorType.Medium:
                        return 50;
                    case EnumArmorType.Heavy:
                        return 500;
                    case EnumArmorType.Elite:
                        return 5000;
                    default:
                        return 10;
                }
            }
            set
            {
                return;
            }
        }
        [NotMapped]
        [DisplayName("Вес")]
        public new int Weight
        {
            get
            {
                switch (ArmorType)
                {
                    case EnumArmorType.Medium:
                        return 2;
                    case EnumArmorType.Heavy:
                        return 3;
                    case EnumArmorType.Elite:
                        return 4;
                    default:
                        return 1;
                }
            }
            set
            {
                return;
            }
        }
        [NotMapped]
        [DisplayName("Вес экипированный")]
        public int WeightEquiped
        {
            get
            {
                switch (ArmorType)
                {
                    case EnumArmorType.Medium:
                    case EnumArmorType.Shield:
                        return 1;
                    case EnumArmorType.Heavy:
                        return 2;
                    case EnumArmorType.Elite:
                        return 3;
                    default:
                        return 0;
                }
            }
            set
            {
                return;
            }
        }
        [NotMapped]
        [DisplayName("Великолепие")]
        public int Magnificence
        {
            get
            {
                switch (ArmorType)
                {
                    case EnumArmorType.Heavy:
                        return 1;
                    case EnumArmorType.Elite:
                        return 2;
                    default:
                        return 0;
                }
            }
            set
            {
                return;
            }
        }
        [NotMapped]
        [DisplayName("Штрафы")]
        public int PenaltyCount
        {
            get
            {
                switch (ArmorType)
                {
                    case EnumArmorType.Medium:
                        return 1;
                    case EnumArmorType.Heavy:
                        return 2;
                    case EnumArmorType.Elite:
                        return 3;
                    default:
                        return 0;
                }
            }
            set
            {
                return;
            }
        }
    }
}
