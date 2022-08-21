namespace ChernabogJailApp.RuleBook
{
    public enum EnumDiceType
    {
        d4 = 4,
        d6 = 6,
        d8 = 8,
        d10 = 10,
        d12 = 12,
        d20 = 20,
        d100 = 100
    }
    public class DiceRoll: IConvertible, IComparable<DiceRoll>
    {
        private readonly Random _random;
        public EnumDiceType Type { get; set; }
        public int Count { get; set; }
        public int Modifier { get; set; }

        public DiceRoll(EnumDiceType type = EnumDiceType.d20, int count = 1, int modifier = 0)
        {
            _random = new Random();

            Type = type;
            Count = count;
            Modifier = modifier;
        }

        private int Roll()
        {
            var roll = 0;
            for (int i = 0; i <= Count; i++)
            {
                roll += _random.Next(1, (int)Type + 1);
            }
            return roll + Modifier;
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider? provider)
        {
            return (byte)Roll();
        }

        public char ToChar(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider? provider)
        {
            return (decimal)Roll();
        }

        public double ToDouble(IFormatProvider? provider)
        {
            return (double)Roll();
        }

        public short ToInt16(IFormatProvider? provider)
        {
            return (Int16)Roll();
        }

        public int ToInt32(IFormatProvider? provider)
        {
            return (Int32)Roll();
        }

        public long ToInt64(IFormatProvider? provider)
        {
            return (Int64)Roll();
        }

        public sbyte ToSByte(IFormatProvider? provider)
        {
            return (sbyte)Roll();
        }

        public float ToSingle(IFormatProvider? provider)
        {
            return (Single)Roll();
        }

        public string ToString(IFormatProvider? provider)
        {
            var modifier = Modifier == 0 ? "" : Modifier > 0 ? $"+{Modifier}" : $"+{Modifier}";
            var count = Count == 0 ? "" : $"{Count}";
            return $"{count}{Type}{modifier}";
        }

        public object ToType(Type conversionType, IFormatProvider? provider)
        {
            return Convert.ChangeType(this, conversionType, provider);
        }

        public ushort ToUInt16(IFormatProvider? provider)
        {
            return (UInt16)Roll();
        }

        public uint ToUInt32(IFormatProvider? provider)
        {
            return (UInt32)Roll();
        }

        public ulong ToUInt64(IFormatProvider? provider)
        {
            return (UInt64)Roll();
        }

        public int CompareTo(DiceRoll? other)
        {
            if (other == null) return 1;
            var MaxRoll = this.Count * (int)this.Type + this.Modifier;
            var OtherMaxRoll = other.Count * (int)other.Type + other.Modifier;
            return MaxRoll.CompareTo(OtherMaxRoll);
        }
    }
}
