using System;
using System.Collections.Generic;

namespace Zaabee.Mongo.Extension.UnitTest
{
    public class TestModel
    {
        public Guid Id { get; set; }
        public Guid Guid { get; set; }
        public Guid[] GuidArray { get; set; }
        public IList<Guid> GuidList { get; set; }
        public sbyte SByte { get; set; }
        public sbyte[] SByteArray { get; set; }
        public List<sbyte> SByteList { get; set; }
        public short Short { get; set; }
        public short[] ShortArray { get; set; }
        public List<short> ShortList { get; set; }
        public int Int { get; set; }
        public int[] IntArray { get; set; }
        public List<int> IntList { get; set; }
        public long Long { get; set; }
        public long[] LongArray { get; set; }
        public List<long> LongList { get; set; }
        public byte Byte { get; set; }
        public byte[] ByteArray { get; set; }
        public List<byte> ByteList { get; set; }
        public ushort UShort { get; set; }
        public ushort[] UShortArray { get; set; }
        public List<ushort> UShortList { get; set; }
        public uint UInt { get; set; }
        public uint[] UIntArray { get; set; }
        public List<uint> UIntList { get; set; }
        public ulong ULong { get; set; }
        public ulong[] ULongArray { get; set; }
        public List<ulong> ULongList { get; set; }
        public float Float { get; set; }
        public float[] FloatArray { get; set; }
        public List<float> FloatList { get; set; }
        public double Double { get; set; }
        public double[] DoubleArray { get; set; }
        public List<double> DoubleList { get; set; }
        public decimal Decimal { get; set; }
        public decimal[] DecimalArray { get; set; }
        public List<decimal> DecimalList { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime[] DateTimeArray { get; set; }
        public List<DateTime> DateTimeList { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public DateTime[] DateTimeUtcArray { get; set; }
        public List<DateTime> DateTimeUtcList { get; set; }
        public string String { get; set; }
        public string[] StringArray { get; set; }
        public List<string> StringList { get; set; }
        public object Object { get; set; }
        public object[] ObjectArray { get; set; }
        public List<object> ObjectList { get; set; }
        public TestModel Kid { get; set; }
        public TestModel[] KidArray { get; set; }
        public List<TestModel> KidList { get; set; }
        public EnumByte EnumByte { get; set; }
        public EnumByte[] EnumByteArray { get; set; }
        public List<EnumByte> EnumByteList { get; set; }
        public EnumSByte EnumSByte { get; set; }
        public EnumSByte[] EnumSByteArray { get; set; }
        public List<EnumSByte> EnumSByteList { get; set; }
        public EnumShort EnumShort { get; set; }
        public EnumShort[] EnumShortArray { get; set; }
        public List<EnumShort> EnumShortList { get; set; }
        public EnumUShort EnumUShort { get; set; }
        public EnumUShort[] EnumUShortArray { get; set; }
        public List<EnumUShort> EnumUShortList { get; set; }
        public EnumInt EnumInt { get; set; }
        public EnumInt[] EnumIntArray { get; set; }
        public List<EnumInt> EnumIntList { get; set; }
        public EnumUInt EnumUInt { get; set; }
        public EnumUInt[] EnumUIntArray { get; set; }
        public List<EnumUInt> EnumUIntList { get; set; }
        public EnumLong EnumLong { get; set; }
        public EnumLong[] EnumLongArray { get; set; }
        public List<EnumLong> EnumLongList { get; set; }
        public EnumULong EnumULong { get; set; }
        public EnumULong[] EnumULongArray { get; set; }
        public List<EnumULong> EnumULongList { get; set; }
    }

    public enum EnumSByte : sbyte
    {
        Apple,
        Banana,
        Pear
    }

    public enum EnumByte : byte
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