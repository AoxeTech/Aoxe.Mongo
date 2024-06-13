using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoxe.Mongo.UnitTest
{
    public static class TestModelFactory
    {
        internal static List<TestModel> GetModels(int quantity) =>
            Enumerable.Range(0, quantity).Select(GetModel).ToList();

        internal static TestModel GetModel(int seed = 0)
        {
            return new TestModel
            {
                Id = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                GuidArray = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                GuidList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                SByte = 1,
                SByteArray = new sbyte[] { -1, 0, 1 },
                SByteList = new List<sbyte> { -1, 0, 1 },
                Short = -1,
                ShortArray = new short[] { -1, 0, 1 },
                ShortList = new List<short> { -1, 0, 1 },
                Int = -2,
                IntArray = new[] { -1, 0, 1 },
                IntList = new List<int> { -1, 0, 1 },
                Long = -3,
                LongArray = new long[] { -1, 0, 1 },
                LongList = new List<long> { -1, 0, 1 },
                Byte = 1,
                ByteArray = new byte[] { 0, 1, 2 },
                ByteList = new List<byte> { 0, 1, 2 },
                UShort = 1,
                UShortArray = new ushort[] { 0, 1, 2 },
                UShortList = new List<ushort> { 0, 1, 2 },
                UInt = 2,
                UIntArray = new uint[] { 0, 1, 2 },
                UIntList = new List<uint> { 0, 1, 2 },
                ULong = 3,
                ULongArray = new ulong[] { 0, 1, 2 },
                ULongList = new List<ulong> { 0, 1, 2 },
                Float = 0.1F,
                FloatArray = new float[] { -1, 0, 1 },
                FloatList = new List<float> { -1, 0, 1 },
                Double = 0.2,
                DoubleArray = new double[] { -1, 0, 1 },
                DoubleList = new List<double> { -1, 0, 1 },
                Decimal = 0.3M,
                DecimalArray = new decimal[] { -1, 0, 1 },
                DecimalList = new List<decimal> { -1, 0, 1 },
                DateTime = DateTime.Now,
                DateTimeArray = new[] { DateTime.Now, DateTime.Now, DateTime.Now },
                DateTimeList = new List<DateTime> { DateTime.Now, DateTime.Now, DateTime.Now },
                DateTimeUtc = DateTime.UtcNow,
                DateTimeUtcArray = new[] { DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow },
                DateTimeUtcList = new List<DateTime>
                {
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    DateTime.UtcNow
                },
                String = Guid.NewGuid().ToString(),
                StringArray = new[]
                {
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString()
                },
                StringList = new List<string>
                {
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString()
                },
                Object = null,
                ObjectArray = new object[] { null, null, null },
                ObjectList = new List<object> { null, null, null },
                EnumByte = (EnumByte)(seed % 3),
                EnumByteArray = new[]
                {
                    (EnumByte)(seed % 3),
                    (EnumByte)(seed % 3),
                    (EnumByte)(seed % 3)
                },
                EnumByteList = new List<EnumByte>
                {
                    (EnumByte)(seed % 3),
                    (EnumByte)(seed % 3),
                    (EnumByte)(seed % 3)
                },
                EnumSByte = (EnumSByte)(seed % 3),
                EnumShort = (EnumShort)(seed % 3),
                EnumUShort = (EnumUShort)(seed % 3),
                EnumInt = (EnumInt)(seed % 3),
                EnumUInt = (EnumUInt)(seed % 3),
                EnumLong = (EnumLong)(seed % 3),
                EnumULong = (EnumULong)(seed % 3),
                Kid = new TestModel
                {
                    Id = Guid.NewGuid(),
                    Guid = Guid.NewGuid(),
                    GuidArray = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                    GuidList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                    SByte = 1,
                    SByteArray = new sbyte[] { -1, 0, 1 },
                    SByteList = new List<sbyte> { -1, 0, 1 },
                    Short = -1,
                    ShortArray = new short[] { -1, 0, 1 },
                    ShortList = new List<short> { -1, 0, 1 },
                    Int = -2,
                    IntArray = new[] { -1, 0, 1 },
                    IntList = new List<int> { -1, 0, 1 },
                    Long = -3,
                    LongArray = new long[] { -1, 0, 1 },
                    LongList = new List<long> { -1, 0, 1 },
                    Byte = 1,
                    ByteArray = new byte[] { 0, 1, 2 },
                    ByteList = new List<byte> { 0, 1, 2 },
                    UShort = 1,
                    UShortArray = new ushort[] { 0, 1, 2 },
                    UShortList = new List<ushort> { 0, 1, 2 },
                    UInt = 2,
                    UIntArray = new uint[] { 0, 1, 2 },
                    UIntList = new List<uint> { 0, 1, 2 },
                    ULong = 3,
                    ULongArray = new ulong[] { 0, 1, 2 },
                    ULongList = new List<ulong> { 0, 1, 2 },
                    Float = 0.1F,
                    FloatArray = new float[] { -1, 0, 1 },
                    FloatList = new List<float> { -1, 0, 1 },
                    Double = 0.2,
                    DoubleArray = new double[] { -1, 0, 1 },
                    DoubleList = new List<double> { -1, 0, 1 },
                    Decimal = 0.3M,
                    DecimalArray = new decimal[] { -1, 0, 1 },
                    DecimalList = new List<decimal> { -1, 0, 1 },
                    DateTime = DateTime.Now,
                    DateTimeArray = new[] { DateTime.Now, DateTime.Now, DateTime.Now },
                    DateTimeList = new List<DateTime> { DateTime.Now, DateTime.Now, DateTime.Now },
                    DateTimeUtc = DateTime.UtcNow,
                    DateTimeUtcArray = new[] { DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow },
                    DateTimeUtcList = new List<DateTime>
                    {
                        DateTime.UtcNow,
                        DateTime.UtcNow,
                        DateTime.UtcNow
                    },
                    String = Guid.NewGuid().ToString(),
                    StringArray = new[]
                    {
                        Guid.NewGuid().ToString(),
                        Guid.NewGuid().ToString(),
                        Guid.NewGuid().ToString()
                    },
                    StringList = new List<string>
                    {
                        Guid.NewGuid().ToString(),
                        Guid.NewGuid().ToString(),
                        Guid.NewGuid().ToString()
                    },
                    Object = null,
                    ObjectArray = new object[] { null, null, null },
                    ObjectList = new List<object> { null, null, null },
                    EnumByte = (EnumByte)(seed % 3),
                    EnumSByte = (EnumSByte)(seed % 3),
                    EnumShort = (EnumShort)(seed % 3),
                    EnumUShort = (EnumUShort)(seed % 3),
                    EnumInt = (EnumInt)(seed % 3),
                    EnumUInt = (EnumUInt)(seed % 3),
                    EnumLong = (EnumLong)(seed % 3),
                    EnumULong = (EnumULong)(seed % 3)
                },
                KidArray = new[]
                {
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                        GuidList = new List<Guid>
                        {
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                            Guid.NewGuid()
                        },
                        SByte = 1,
                        SByteArray = new sbyte[] { -1, 0, 1 },
                        SByteList = new List<sbyte> { -1, 0, 1 },
                        Short = -1,
                        ShortArray = new short[] { -1, 0, 1 },
                        ShortList = new List<short> { -1, 0, 1 },
                        Int = -2,
                        IntArray = new[] { -1, 0, 1 },
                        IntList = new List<int> { -1, 0, 1 },
                        Long = -3,
                        LongArray = new long[] { -1, 0, 1 },
                        LongList = new List<long> { -1, 0, 1 },
                        Byte = 1,
                        ByteArray = new byte[] { 0, 1, 2 },
                        ByteList = new List<byte> { 0, 1, 2 },
                        UShort = 1,
                        UShortArray = new ushort[] { 0, 1, 2 },
                        UShortList = new List<ushort> { 0, 1, 2 },
                        UInt = 2,
                        UIntArray = new uint[] { 0, 1, 2 },
                        UIntList = new List<uint> { 0, 1, 2 },
                        ULong = 3,
                        ULongArray = new ulong[] { 0, 1, 2 },
                        ULongList = new List<ulong> { 0, 1, 2 },
                        Float = 0.1F,
                        FloatArray = new float[] { -1, 0, 1 },
                        FloatList = new List<float> { -1, 0, 1 },
                        Double = 0.2,
                        DoubleArray = new double[] { -1, 0, 1 },
                        DoubleList = new List<double> { -1, 0, 1 },
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] { -1, 0, 1 },
                        DecimalList = new List<decimal> { -1, 0, 1 },
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] { DateTime.Now, DateTime.Now, DateTime.Now },
                        DateTimeList = new List<DateTime>
                        {
                            DateTime.Now,
                            DateTime.Now,
                            DateTime.Now
                        },
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[]
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        DateTimeUtcList = new List<DateTime>
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        StringList = new List<string>
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        Object = null,
                        ObjectArray = new object[] { null, null, null },
                        ObjectList = new List<object> { null, null, null },
                        EnumByte = (EnumByte)(seed % 3),
                        EnumSByte = (EnumSByte)(seed % 3),
                        EnumShort = (EnumShort)(seed % 3),
                        EnumUShort = (EnumUShort)(seed % 3),
                        EnumInt = (EnumInt)(seed % 3),
                        EnumUInt = (EnumUInt)(seed % 3),
                        EnumLong = (EnumLong)(seed % 3),
                        EnumULong = (EnumULong)(seed % 3)
                    },
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                        GuidList = new List<Guid>
                        {
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                            Guid.NewGuid()
                        },
                        SByte = 1,
                        SByteArray = new sbyte[] { -1, 0, 1 },
                        SByteList = new List<sbyte> { -1, 0, 1 },
                        Short = -1,
                        ShortArray = new short[] { -1, 0, 1 },
                        ShortList = new List<short> { -1, 0, 1 },
                        Int = -2,
                        IntArray = new[] { -1, 0, 1 },
                        IntList = new List<int> { -1, 0, 1 },
                        Long = -3,
                        LongArray = new long[] { -1, 0, 1 },
                        LongList = new List<long> { -1, 0, 1 },
                        Byte = 1,
                        ByteArray = new byte[] { 0, 1, 2 },
                        ByteList = new List<byte> { 0, 1, 2 },
                        UShort = 1,
                        UShortArray = new ushort[] { 0, 1, 2 },
                        UShortList = new List<ushort> { 0, 1, 2 },
                        UInt = 2,
                        UIntArray = new uint[] { 0, 1, 2 },
                        UIntList = new List<uint> { 0, 1, 2 },
                        ULong = 3,
                        ULongArray = new ulong[] { 0, 1, 2 },
                        ULongList = new List<ulong> { 0, 1, 2 },
                        Float = 0.1F,
                        FloatArray = new float[] { -1, 0, 1 },
                        FloatList = new List<float> { -1, 0, 1 },
                        Double = 0.2,
                        DoubleArray = new double[] { -1, 0, 1 },
                        DoubleList = new List<double> { -1, 0, 1 },
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] { -1, 0, 1 },
                        DecimalList = new List<decimal> { -1, 0, 1 },
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] { DateTime.Now, DateTime.Now, DateTime.Now },
                        DateTimeList = new List<DateTime>
                        {
                            DateTime.Now,
                            DateTime.Now,
                            DateTime.Now
                        },
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[]
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        DateTimeUtcList = new List<DateTime>
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        StringList = new List<string>
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        Object = null,
                        ObjectArray = new object[] { null, null, null },
                        ObjectList = new List<object> { null, null, null },
                        EnumByte = (EnumByte)(seed % 3),
                        EnumSByte = (EnumSByte)(seed % 3),
                        EnumShort = (EnumShort)(seed % 3),
                        EnumUShort = (EnumUShort)(seed % 3),
                        EnumInt = (EnumInt)(seed % 3),
                        EnumUInt = (EnumUInt)(seed % 3),
                        EnumLong = (EnumLong)(seed % 3),
                        EnumULong = (EnumULong)(seed % 3)
                    },
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                        GuidList = new List<Guid>
                        {
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                            Guid.NewGuid()
                        },
                        SByte = 1,
                        SByteArray = new sbyte[] { -1, 0, 1 },
                        SByteList = new List<sbyte> { -1, 0, 1 },
                        Short = -1,
                        ShortArray = new short[] { -1, 0, 1 },
                        ShortList = new List<short> { -1, 0, 1 },
                        Int = -2,
                        IntArray = new[] { -1, 0, 1 },
                        IntList = new List<int> { -1, 0, 1 },
                        Long = -3,
                        LongArray = new long[] { -1, 0, 1 },
                        LongList = new List<long> { -1, 0, 1 },
                        Byte = 1,
                        ByteArray = new byte[] { 0, 1, 2 },
                        ByteList = new List<byte> { 0, 1, 2 },
                        UShort = 1,
                        UShortArray = new ushort[] { 0, 1, 2 },
                        UShortList = new List<ushort> { 0, 1, 2 },
                        UInt = 2,
                        UIntArray = new uint[] { 0, 1, 2 },
                        UIntList = new List<uint> { 0, 1, 2 },
                        ULong = 3,
                        ULongArray = new ulong[] { 0, 1, 2 },
                        ULongList = new List<ulong> { 0, 1, 2 },
                        Float = 0.1F,
                        FloatArray = new float[] { -1, 0, 1 },
                        FloatList = new List<float> { -1, 0, 1 },
                        Double = 0.2,
                        DoubleArray = new double[] { -1, 0, 1 },
                        DoubleList = new List<double> { -1, 0, 1 },
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] { -1, 0, 1 },
                        DecimalList = new List<decimal> { -1, 0, 1 },
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] { DateTime.Now, DateTime.Now, DateTime.Now },
                        DateTimeList = new List<DateTime>
                        {
                            DateTime.Now,
                            DateTime.Now,
                            DateTime.Now
                        },
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[]
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        DateTimeUtcList = new List<DateTime>
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        StringList = new List<string>
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        Object = null,
                        ObjectArray = new object[] { null, null, null },
                        ObjectList = new List<object> { null, null, null },
                        EnumByte = (EnumByte)(seed % 3),
                        EnumSByte = (EnumSByte)(seed % 3),
                        EnumShort = (EnumShort)(seed % 3),
                        EnumUShort = (EnumUShort)(seed % 3),
                        EnumInt = (EnumInt)(seed % 3),
                        EnumUInt = (EnumUInt)(seed % 3),
                        EnumLong = (EnumLong)(seed % 3),
                        EnumULong = (EnumULong)(seed % 3)
                    }
                },
                KidList = new List<TestModel>
                {
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                        GuidList = new List<Guid>
                        {
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                            Guid.NewGuid()
                        },
                        SByte = 1,
                        SByteArray = new sbyte[] { -1, 0, 1 },
                        SByteList = new List<sbyte> { -1, 0, 1 },
                        Short = -1,
                        ShortArray = new short[] { -1, 0, 1 },
                        ShortList = new List<short> { -1, 0, 1 },
                        Int = -2,
                        IntArray = new[] { -1, 0, 1 },
                        IntList = new List<int> { -1, 0, 1 },
                        Long = -3,
                        LongArray = new long[] { -1, 0, 1 },
                        LongList = new List<long> { -1, 0, 1 },
                        Byte = 1,
                        ByteArray = new byte[] { 0, 1, 2 },
                        ByteList = new List<byte> { 0, 1, 2 },
                        UShort = 1,
                        UShortArray = new ushort[] { 0, 1, 2 },
                        UShortList = new List<ushort> { 0, 1, 2 },
                        UInt = 2,
                        UIntArray = new uint[] { 0, 1, 2 },
                        UIntList = new List<uint> { 0, 1, 2 },
                        ULong = 3,
                        ULongArray = new ulong[] { 0, 1, 2 },
                        ULongList = new List<ulong> { 0, 1, 2 },
                        Float = 0.1F,
                        FloatArray = new float[] { -1, 0, 1 },
                        FloatList = new List<float> { -1, 0, 1 },
                        Double = 0.2,
                        DoubleArray = new double[] { -1, 0, 1 },
                        DoubleList = new List<double> { -1, 0, 1 },
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] { -1, 0, 1 },
                        DecimalList = new List<decimal> { -1, 0, 1 },
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] { DateTime.Now, DateTime.Now, DateTime.Now },
                        DateTimeList = new List<DateTime>
                        {
                            DateTime.Now,
                            DateTime.Now,
                            DateTime.Now
                        },
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[]
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        DateTimeUtcList = new List<DateTime>
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        StringList = new List<string>
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        Object = null,
                        ObjectArray = new object[] { null, null, null },
                        ObjectList = new List<object> { null, null, null },
                        EnumByte = (EnumByte)(seed % 3),
                        EnumSByte = (EnumSByte)(seed % 3),
                        EnumShort = (EnumShort)(seed % 3),
                        EnumUShort = (EnumUShort)(seed % 3),
                        EnumInt = (EnumInt)(seed % 3),
                        EnumUInt = (EnumUInt)(seed % 3),
                        EnumLong = (EnumLong)(seed % 3),
                        EnumULong = (EnumULong)(seed % 3)
                    },
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                        GuidList = new List<Guid>
                        {
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                            Guid.NewGuid()
                        },
                        SByte = 1,
                        SByteArray = new sbyte[] { -1, 0, 1 },
                        SByteList = new List<sbyte> { -1, 0, 1 },
                        Short = -1,
                        ShortArray = new short[] { -1, 0, 1 },
                        ShortList = new List<short> { -1, 0, 1 },
                        Int = -2,
                        IntArray = new[] { -1, 0, 1 },
                        IntList = new List<int> { -1, 0, 1 },
                        Long = -3,
                        LongArray = new long[] { -1, 0, 1 },
                        LongList = new List<long> { -1, 0, 1 },
                        Byte = 1,
                        ByteArray = new byte[] { 0, 1, 2 },
                        ByteList = new List<byte> { 0, 1, 2 },
                        UShort = 1,
                        UShortArray = new ushort[] { 0, 1, 2 },
                        UShortList = new List<ushort> { 0, 1, 2 },
                        UInt = 2,
                        UIntArray = new uint[] { 0, 1, 2 },
                        UIntList = new List<uint> { 0, 1, 2 },
                        ULong = 3,
                        ULongArray = new ulong[] { 0, 1, 2 },
                        ULongList = new List<ulong> { 0, 1, 2 },
                        Float = 0.1F,
                        FloatArray = new float[] { -1, 0, 1 },
                        FloatList = new List<float> { -1, 0, 1 },
                        Double = 0.2,
                        DoubleArray = new double[] { -1, 0, 1 },
                        DoubleList = new List<double> { -1, 0, 1 },
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] { -1, 0, 1 },
                        DecimalList = new List<decimal> { -1, 0, 1 },
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] { DateTime.Now, DateTime.Now, DateTime.Now },
                        DateTimeList = new List<DateTime>
                        {
                            DateTime.Now,
                            DateTime.Now,
                            DateTime.Now
                        },
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[]
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        DateTimeUtcList = new List<DateTime>
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        StringList = new List<string>
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        Object = null,
                        ObjectArray = new object[] { null, null, null },
                        ObjectList = new List<object> { null, null, null },
                        EnumByte = (EnumByte)(seed % 3),
                        EnumSByte = (EnumSByte)(seed % 3),
                        EnumShort = (EnumShort)(seed % 3),
                        EnumUShort = (EnumUShort)(seed % 3),
                        EnumInt = (EnumInt)(seed % 3),
                        EnumUInt = (EnumUInt)(seed % 3),
                        EnumLong = (EnumLong)(seed % 3),
                        EnumULong = (EnumULong)(seed % 3)
                    },
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                        GuidList = new List<Guid>
                        {
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                            Guid.NewGuid()
                        },
                        SByte = 1,
                        SByteArray = new sbyte[] { -1, 0, 1 },
                        SByteList = new List<sbyte> { -1, 0, 1 },
                        Short = -1,
                        ShortArray = new short[] { -1, 0, 1 },
                        ShortList = new List<short> { -1, 0, 1 },
                        Int = -2,
                        IntArray = new[] { -1, 0, 1 },
                        IntList = new List<int> { -1, 0, 1 },
                        Long = -3,
                        LongArray = new long[] { -1, 0, 1 },
                        LongList = new List<long> { -1, 0, 1 },
                        Byte = 1,
                        ByteArray = new byte[] { 0, 1, 2 },
                        ByteList = new List<byte> { 0, 1, 2 },
                        UShort = 1,
                        UShortArray = new ushort[] { 0, 1, 2 },
                        UShortList = new List<ushort> { 0, 1, 2 },
                        UInt = 2,
                        UIntArray = new uint[] { 0, 1, 2 },
                        UIntList = new List<uint> { 0, 1, 2 },
                        ULong = 3,
                        ULongArray = new ulong[] { 0, 1, 2 },
                        ULongList = new List<ulong> { 0, 1, 2 },
                        Float = 0.1F,
                        FloatArray = new float[] { -1, 0, 1 },
                        FloatList = new List<float> { -1, 0, 1 },
                        Double = 0.2,
                        DoubleArray = new double[] { -1, 0, 1 },
                        DoubleList = new List<double> { -1, 0, 1 },
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] { -1, 0, 1 },
                        DecimalList = new List<decimal> { -1, 0, 1 },
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] { DateTime.Now, DateTime.Now, DateTime.Now },
                        DateTimeList = new List<DateTime>
                        {
                            DateTime.Now,
                            DateTime.Now,
                            DateTime.Now
                        },
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[]
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        DateTimeUtcList = new List<DateTime>
                        {
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        StringList = new List<string>
                        {
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString(),
                            Guid.NewGuid().ToString()
                        },
                        Object = null,
                        ObjectArray = new object[] { null, null, null },
                        ObjectList = new List<object> { null, null, null },
                        EnumByte = (EnumByte)(seed % 3),
                        EnumSByte = (EnumSByte)(seed % 3),
                        EnumShort = (EnumShort)(seed % 3),
                        EnumUShort = (EnumUShort)(seed % 3),
                        EnumInt = (EnumInt)(seed % 3),
                        EnumUInt = (EnumUInt)(seed % 3),
                        EnumLong = (EnumLong)(seed % 3),
                        EnumULong = (EnumULong)(seed % 3)
                    }
                }
            };
        }
    }
}
