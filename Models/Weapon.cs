using ChernabogJailApp.RuleBook;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ChernabogJailApp.RuleBook.CommonEnums;

namespace ChernabogJailApp.Models
{
    public class Weapon : BaseEquipment
    {
        [Required]
        [DisplayName("Тип оружия")]
        [EnumDataType(typeof(EnumAttributes))]
        public EnumWeaponType WeaponType { get; set; }
        [NotMapped]
        [DisplayName("Характеристика 1")]
        [EnumDataType(typeof(EnumAttributes))]
        public EnumAttributes? FirstAttribute {
            get
            {
                switch (WeaponType)
                {
                    case EnumWeaponType.Light:
                    case EnumWeaponType.Medium:
                    case EnumWeaponType.Heavy:
                        return EnumAttributes.Strength;
                    case EnumWeaponType.LongRangeOne:
                    case EnumWeaponType.LongRangeTwo:
                        return EnumAttributes.Dexterity;
                    default:
                        return null;
                }
            }
            set
            {
                return;
            }
        }
        [NotMapped]
        [DisplayName("Характеристика 2")]
        [EnumDataType(typeof(EnumAttributes))]
        public EnumAttributes? SecondAttribute {
            get
            {
                switch (WeaponType)
                {
                    case EnumWeaponType.Light:
                        return EnumAttributes.Dexterity;
                    default:
                        return null;
                }
            }
            set
            {
                return;
            }
        }
        [NotMapped]
        [DisplayName("Кость урона")]
        public EnumDiceType? DiceType
        {
            get
            {
                switch (WeaponType)
                {
                    case EnumWeaponType.Light:
                    case EnumWeaponType.LongRangeOne:
                    case EnumWeaponType.Bombs:
                        return EnumDiceType.d6;
                    case EnumWeaponType.Medium:
                    case EnumWeaponType.LongRangeTwo:
                        return EnumDiceType.d8;
                    case EnumWeaponType.Heavy:
                        return EnumDiceType.d10;
                    default:
                        return null;
                }
            }
            set
            {
                return;
            }
        }
        [NotMapped]
        [DisplayName("Вес")]
        public new int? Weight {
            get
            {
                switch (WeaponType)
                {
                    case EnumWeaponType.Light:
                    case EnumWeaponType.Medium:
                    case EnumWeaponType.LongRangeOne:
                    case EnumWeaponType.Bombs:
                    case EnumWeaponType.Ammunition:
                        return 1;
                    case EnumWeaponType.Heavy:
                    case EnumWeaponType.LongRangeTwo:
                        return 2;
                    default:
                        return null;
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
                switch (WeaponType)
                {
                    case EnumWeaponType.Light:
                    case EnumWeaponType.LongRangeOne:
                        return 10;
                    case EnumWeaponType.Medium:
                        return 15;
                    case EnumWeaponType.Heavy:
                        return 30;
                    case EnumWeaponType.LongRangeTwo:
                        return 20;
                    case EnumWeaponType.Bombs:
                        return 100;
                    case EnumWeaponType.Ammunition:
                        return 5;
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
