using System;
using System.Collections.Generic;

namespace UnitTest
{
    public class TestModel
    {
        public Guid Id { get; set; }
        public Guid Guid { get; set; }
        public Guid[] GuidArray { get; set; }
        public List<Guid> Guids { get; set; }
        public short Short { get; set; }
        public short[] ShortArray { get; set; }
        public List<short> Shorts { get; set; }
        public int Int { get; set; }
        public int[] IntArray { get; set; }
        public List<int> Ints { get; set; }
        public long Long { get; set; }
        public long[] LongArray { get; set; }
        public List<long> Longs { get; set; }
        public ushort UShort { get; set; }
        public ushort[] UShortArray { get; set; }
        public List<ushort> UShorts { get; set; }
        public uint UInt { get; set; }
        public uint[] UIntArray { get; set; }
        public List<uint> UInts { get; set; }
        public ulong ULong { get; set; }
        public ulong[] ULongArray { get; set; }
        public List<ulong> ULongs { get; set; }
        public float Float { get; set; }
        public float[] FloatArray { get; set; }
        public List<float> Floats { get; set; }
        public double Double { get; set; }
        public double[] DoubleArray { get; set; }
        public List<double> Doubles { get; set; }
        public decimal Decimal { get; set; }
        public decimal[] DecimalArray { get; set; }
        public List<decimal> Decimals { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime[] DateTimeArray { get; set; }
        public List<DateTime> DateTimes { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public DateTime[] DateTimeUtcArray { get; set; }
        public List<DateTime> DateTimeUtcs { get; set; }
        public string String { get; set; }
        public string[] StringArray { get; set; }
        public List<string> Strings { get; set; }
        public object Object { get; set; }
        public object[] ObjectArray { get; set; }
        public List<object> Objects { get; set; }
        public TestModel Kid { get; set; }
        public TestModel[] KidArray { get; set; }
        public List<TestModel> Kids { get; set; }
        public EnumByte EnumByte { get; set; }

        public EnumByte[] EnumByteArray { get; set; }
        public List<EnumByte> EnumBytes { get; set; }
        public EnumSByte EnumSByte { get; set; }

        public EnumSByte[] EnumSByteArray { get; set; }
        public List<EnumSByte> EnumSBytes { get; set; }
        public EnumShort EnumShort { get; set; }

        public EnumShort[] EnumShortArray { get; set; }
        public List<EnumShort> EnumShorts { get; set; }
        public EnumUShort EnumUShort { get; set; }

        public EnumUShort[] EnumUShortArray { get; set; }
        public List<EnumUShort> EnumUShorts { get; set; }
        public EnumInt EnumInt { get; set; }

        public EnumInt[] EnumIntArray { get; set; }
        public List<EnumInt> EnumInts { get; set; }
        public EnumUInt EnumUInt { get; set; }

        public EnumUInt[] EnumUIntArray { get; set; }
        public List<EnumUInt> EnumUInts { get; set; }
        public EnumLong EnumLong { get; set; }

        public EnumLong[] EnumLongArray { get; set; }
        public List<EnumLong> EnumLongs { get; set; }
        public EnumULong EnumULong { get; set; }

        public EnumULong[] EnumULongArray { get; set; }
        public List<EnumULong> EnumULongs { get; set; }
    }

    public enum EnumByte : byte
    {
        Apple,
        Banana,
        Pear
    }

    public enum EnumSByte : sbyte
    {
        Apple,
        Banana,
        Pear
    }

    public enum EnumShort : short
    {
        Apple,
        Banana,
        Pear
    }

    public enum EnumUShort : ushort
    {
        Apple,
        Banana,
        Pear
    }

    public enum EnumInt
    {
        Apple,
        Banana,
        Pear
    }

    public enum EnumUInt : uint
    {
        Apple,
        Banana,
        Pear
    }

    public enum EnumLong : long
    {
        Apple,
        Banana,
        Pear
    }

    public enum EnumULong : ulong
    {
        Apple,
        Banana,
        Pear
    }
}