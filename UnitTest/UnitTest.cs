using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Xunit;
using Zaabee.Mongo;
using Zaabee.Mongo.Abstractions;
using Zaabee.Mongo.Common;

namespace UnitTest
{
    public class UnitTest
    {
        private readonly IZaabeeMongoClient _client;

        public UnitTest()
        {
            _client = new ZaabeeMongoClient(new MongoDbConfiger(new List<string> {"192.168.78.152:27017"},
                "TestDB", "", "TestUser", "123"));
//            _client = new ZaabeeMongoClient("mongodb://TestUser:123@192.168.78.152:27017/TestDB","TestDB");
//            _client = new ZaabeeMongoClient("mongodb://CaiwuR:Bp1qZHMLY9@172.31.200.2:27017/?authSource=FlytOaData","FlytOaData");
        }

        [Fact]
        public void Add()
        {
            var model = GetModel();
            _client.Add(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.NotNull(result);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public async void AddAsync()
        {
            var model = GetModel();
            await _client.AddAsync(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.NotNull(result);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public void AddRange()
        {
            var models = GetModels(3).ToList();
            _client.AddRange(models);
            var ids = models.Select(g => g.Id).ToList();
            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id))
                .ToList();
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        [Fact]
        public async void AddRangeAsync()
        {
            var models = GetModels(4).ToList();
            await _client.AddRangeAsync(models);
            var ids = models.Select(g => g.Id).ToList();
            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id))
                .ToList();
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        [Fact]
        public void Delete()
        {
            var model = GetModel();
            _client.Add(model);
            Assert.Equal(1L, _client.Delete(model));
        }

        [Fact]
        public async void DeleteAsync()
        {
            var model = GetModel();
            await _client.AddAsync(model);
            Assert.Equal(1L, await _client.DeleteAsync(model));
        }

        [Fact]
        public void DeleteMany()
        {
            var models = GetModels(5);
            _client.AddRange(models);
            var strings = models.Select(p => p.String);
            Assert.Equal(5L, _client.Delete<TestModel>(p => strings.Contains(p.String)));
        }

        [Fact]
        public async void DeleteManyAsync()
        {
            var models = GetModels(5);
            await _client.AddRangeAsync(models);
            var strings = models.Select(p => p.String);
            Assert.Equal(5L, await _client.DeleteAsync<TestModel>(p => strings.Contains(p.String)));
        }

        [Fact]
        public void Update()
        {
            var model = GetModel();
            _client.Add(model);
            model.Int = 199;
            model.String = Guid.NewGuid().ToString();
            model.DateTimeUtc = DateTime.UtcNow;
            _client.Update(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public async void UpdateAsync()
        {
            var model = GetModel();
            await _client.AddAsync(model);
            model.Int = 199;
            model.String = Guid.NewGuid().ToString();
            model.DateTimeUtc = DateTime.UtcNow;
            await _client.UpdateAsync(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public void UpdateMany()
        {
            var models = GetModels(5);
            _client.AddRange(models);
            var strings = models.Select(p => p.String);
            var ids = models.Select(p => p.Id);
            var now = DateTime.Now;
            var utcNow = DateTime.UtcNow;
            var name = Guid.NewGuid().ToString();
            var kids = new List<TestModel>
            {
                new TestModel {Id = Guid.NewGuid(), DateTime = DateTime.Now, DateTimeUtc = DateTime.UtcNow}
            };
            var modifyQuantity = _client.Update(
                () => new TestModel
                {
                    DateTime = now,
                    DateTimeUtc = utcNow,
                    String = name,
                    Kids = kids,
                    EnumInt = EnumInt.Banana
                },
                p => strings.Contains(p.String));
            models.ForEach(model =>
            {
                model.DateTime = now;
                model.DateTimeUtc = utcNow;
                model.String = name;
                model.Kids = kids;
                model.EnumInt = EnumInt.Banana;
            });

            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id)).ToList();

            Assert.Equal(5L, modifyQuantity);
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        [Fact]
        public async void UpdateManyAsync()
        {
            var models = GetModels(5);
            _client.AddRange(models);
            var strings = models.Select(p => p.String);
            var ids = models.Select(p => p.Id);
            var now = DateTime.Now;
            var utcNow = DateTime.UtcNow;
            var name = Guid.NewGuid().ToString();
            var kids = new List<TestModel>
            {
                new TestModel {Id = Guid.NewGuid(), DateTime = DateTime.Now, DateTimeUtc = DateTime.UtcNow}
            };
            var modifyQuantity = await _client.UpdateAsync(
                () => new TestModel
                {
                    DateTime = now,
                    DateTimeUtc = utcNow,
                    String = name,
                    Kids = kids
                },
                p => strings.Contains(p.String));
            models.ForEach(model =>
            {
                model.DateTime = now;
                model.DateTimeUtc = utcNow;
                model.String = name;
                model.Kids = kids;
            });

            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id)).ToList();

            Assert.Equal(5L, modifyQuantity);
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        private List<TestModel> GetModels(int quantity)
        {
            return Enumerable.Range(0, quantity).Select(GetModel).ToList();
        }

        private TestModel GetModel(int seed = 0)
        {
            return new TestModel
            {
                Id = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                GuidArray = new[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                Guids = new List<Guid> {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                Short = -1,
                ShortArray = new short[] {-1, 0, 1},
                Shorts = new List<short> {-1, 0, 1},
                Int = -2,
                IntArray = new[] {-1, 0, 1},
                Ints = new List<int> {-1, 0, 1},
                Long = -3,
                LongArray = new long[] {-1, 0, 1},
                Longs = new List<long> {-1, 0, 1},
                UShort = 1,
                UShortArray = new ushort[] {0, 1, 2},
                UShorts = new List<ushort> {0, 1, 2},
                UInt = 2,
                UIntArray = new uint[] {0, 1, 2},
                UInts = new List<uint> {0, 1, 2},
                ULong = 3,
                ULongArray = new ulong[] {0, 1, 2},
                ULongs = new List<ulong> {0, 1, 2},
                Float = 0.1F,
                FloatArray = new float[] {-1, 0, 1},
                Floats = new List<float> {-1, 0, 1},
                Double = 0.2,
                DoubleArray = new double[] {-1, 0, 1},
                Doubles = new List<double> {-1, 0, 1},
                Decimal = 0.3M,
                DecimalArray = new decimal[] {-1, 0, 1},
                Decimals = new List<decimal> {-1, 0, 1},
                DateTime = DateTime.Now,
                DateTimeArray = new[] {DateTime.Now, DateTime.Now, DateTime.Now},
                DateTimes = new List<DateTime> {DateTime.Now, DateTime.Now, DateTime.Now},
                DateTimeUtc = DateTime.UtcNow,
                DateTimeUtcArray = new[] {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                DateTimeUtcs = new List<DateTime> {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                String = Guid.NewGuid().ToString(),
                StringArray = new[] {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                Strings = new List<string>
                    {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                Object = null,
                ObjectArray = new object[] {null, null, null},
                Objects = new List<object> {null, null, null},
                EnumByte = (EnumByte) (seed % 3),
                EnumByteArray = new[]{(EnumByte) (seed % 3),(EnumByte) (seed % 3),(EnumByte) (seed % 3)},
                EnumBytes = new List<EnumByte>{(EnumByte) (seed % 3),(EnumByte) (seed % 3),(EnumByte) (seed % 3)},
                EnumSByte = (EnumSByte) (seed % 3),
                EnumShort = (EnumShort) (seed % 3),
                EnumUShort = (EnumUShort) (seed % 3),
                EnumInt = (EnumInt) (seed % 3),
                EnumUInt = (EnumUInt) (seed % 3),
                EnumLong = (EnumLong) (seed % 3),
                EnumULong = (EnumULong) (seed % 3),
                Kid = new TestModel
                {
                    Id = Guid.NewGuid(),
                    Guid = Guid.NewGuid(),
                    GuidArray = new[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                    Guids = new List<Guid> {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                    Short = -1,
                    ShortArray = new short[] {-1, 0, 1},
                    Shorts = new List<short> {-1, 0, 1},
                    Int = -2,
                    IntArray = new[] {-1, 0, 1},
                    Ints = new List<int> {-1, 0, 1},
                    Long = -3,
                    LongArray = new long[] {-1, 0, 1},
                    Longs = new List<long> {-1, 0, 1},
                    UShort = 1,
                    UShortArray = new ushort[] {0, 1, 2},
                    UShorts = new List<ushort> {0, 1, 2},
                    UInt = 2,
                    UIntArray = new uint[] {0, 1, 2},
                    UInts = new List<uint> {0, 1, 2},
                    ULong = 3,
                    ULongArray = new ulong[] {0, 1, 2},
                    ULongs = new List<ulong> {0, 1, 2},
                    Float = 0.1F,
                    FloatArray = new float[] {-1, 0, 1},
                    Floats = new List<float> {-1, 0, 1},
                    Double = 0.2,
                    DoubleArray = new double[] {-1, 0, 1},
                    Doubles = new List<double> {-1, 0, 1},
                    Decimal = 0.3M,
                    DecimalArray = new decimal[] {-1, 0, 1},
                    Decimals = new List<decimal> {-1, 0, 1},
                    DateTime = DateTime.Now,
                    DateTimeArray = new[] {DateTime.Now, DateTime.Now, DateTime.Now},
                    DateTimes = new List<DateTime> {DateTime.Now, DateTime.Now, DateTime.Now},
                    DateTimeUtc = DateTime.UtcNow,
                    DateTimeUtcArray = new[] {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                    DateTimeUtcs = new List<DateTime> {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                    String = Guid.NewGuid().ToString(),
                    StringArray = new[]
                        {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                    Strings = new List<string>
                        {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                    Object = null,
                    ObjectArray = new object[] {null, null, null},
                    Objects = new List<object> {null, null, null},
                    EnumByte = (EnumByte) (seed % 3),
                    EnumSByte = (EnumSByte) (seed % 3),
                    EnumShort = (EnumShort) (seed % 3),
                    EnumUShort = (EnumUShort) (seed % 3),
                    EnumInt = (EnumInt) (seed % 3),
                    EnumUInt = (EnumUInt) (seed % 3),
                    EnumLong = (EnumLong) (seed % 3),
                    EnumULong = (EnumULong) (seed % 3)
                },
                KidArray = new[]
                {
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Guids = new List<Guid> {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Short = -1,
                        ShortArray = new short[] {-1, 0, 1},
                        Shorts = new List<short> {-1, 0, 1},
                        Int = -2,
                        IntArray = new[] {-1, 0, 1},
                        Ints = new List<int> {-1, 0, 1},
                        Long = -3,
                        LongArray = new long[] {-1, 0, 1},
                        Longs = new List<long> {-1, 0, 1},
                        UShort = 1,
                        UShortArray = new ushort[] {0, 1, 2},
                        UShorts = new List<ushort> {0, 1, 2},
                        UInt = 2,
                        UIntArray = new uint[] {0, 1, 2},
                        UInts = new List<uint> {0, 1, 2},
                        ULong = 3,
                        ULongArray = new ulong[] {0, 1, 2},
                        ULongs = new List<ulong> {0, 1, 2},
                        Float = 0.1F,
                        FloatArray = new float[] {-1, 0, 1},
                        Floats = new List<float> {-1, 0, 1},
                        Double = 0.2,
                        DoubleArray = new double[] {-1, 0, 1},
                        Doubles = new List<double> {-1, 0, 1},
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] {-1, 0, 1},
                        Decimals = new List<decimal> {-1, 0, 1},
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimes = new List<DateTime> {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[] {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        DateTimeUtcs = new List<DateTime> {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Strings = new List<string>
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Object = null,
                        ObjectArray = new object[] {null, null, null},
                        Objects = new List<object> {null, null, null},
                        EnumByte = (EnumByte) (seed % 3),
                        EnumSByte = (EnumSByte) (seed % 3),
                        EnumShort = (EnumShort) (seed % 3),
                        EnumUShort = (EnumUShort) (seed % 3),
                        EnumInt = (EnumInt) (seed % 3),
                        EnumUInt = (EnumUInt) (seed % 3),
                        EnumLong = (EnumLong) (seed % 3),
                        EnumULong = (EnumULong) (seed % 3)
                    },
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Guids = new List<Guid> {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Short = -1,
                        ShortArray = new short[] {-1, 0, 1},
                        Shorts = new List<short> {-1, 0, 1},
                        Int = -2,
                        IntArray = new[] {-1, 0, 1},
                        Ints = new List<int> {-1, 0, 1},
                        Long = -3,
                        LongArray = new long[] {-1, 0, 1},
                        Longs = new List<long> {-1, 0, 1},
                        UShort = 1,
                        UShortArray = new ushort[] {0, 1, 2},
                        UShorts = new List<ushort> {0, 1, 2},
                        UInt = 2,
                        UIntArray = new uint[] {0, 1, 2},
                        UInts = new List<uint> {0, 1, 2},
                        ULong = 3,
                        ULongArray = new ulong[] {0, 1, 2},
                        ULongs = new List<ulong> {0, 1, 2},
                        Float = 0.1F,
                        FloatArray = new float[] {-1, 0, 1},
                        Floats = new List<float> {-1, 0, 1},
                        Double = 0.2,
                        DoubleArray = new double[] {-1, 0, 1},
                        Doubles = new List<double> {-1, 0, 1},
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] {-1, 0, 1},
                        Decimals = new List<decimal> {-1, 0, 1},
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimes = new List<DateTime> {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[] {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        DateTimeUtcs = new List<DateTime> {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Strings = new List<string>
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Object = null,
                        ObjectArray = new object[] {null, null, null},
                        Objects = new List<object> {null, null, null},
                        EnumByte = (EnumByte) (seed % 3),
                        EnumSByte = (EnumSByte) (seed % 3),
                        EnumShort = (EnumShort) (seed % 3),
                        EnumUShort = (EnumUShort) (seed % 3),
                        EnumInt = (EnumInt) (seed % 3),
                        EnumUInt = (EnumUInt) (seed % 3),
                        EnumLong = (EnumLong) (seed % 3),
                        EnumULong = (EnumULong) (seed % 3)
                    },
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Guids = new List<Guid> {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Short = -1,
                        ShortArray = new short[] {-1, 0, 1},
                        Shorts = new List<short> {-1, 0, 1},
                        Int = -2,
                        IntArray = new[] {-1, 0, 1},
                        Ints = new List<int> {-1, 0, 1},
                        Long = -3,
                        LongArray = new long[] {-1, 0, 1},
                        Longs = new List<long> {-1, 0, 1},
                        UShort = 1,
                        UShortArray = new ushort[] {0, 1, 2},
                        UShorts = new List<ushort> {0, 1, 2},
                        UInt = 2,
                        UIntArray = new uint[] {0, 1, 2},
                        UInts = new List<uint> {0, 1, 2},
                        ULong = 3,
                        ULongArray = new ulong[] {0, 1, 2},
                        ULongs = new List<ulong> {0, 1, 2},
                        Float = 0.1F,
                        FloatArray = new float[] {-1, 0, 1},
                        Floats = new List<float> {-1, 0, 1},
                        Double = 0.2,
                        DoubleArray = new double[] {-1, 0, 1},
                        Doubles = new List<double> {-1, 0, 1},
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] {-1, 0, 1},
                        Decimals = new List<decimal> {-1, 0, 1},
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimes = new List<DateTime> {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[] {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        DateTimeUtcs = new List<DateTime> {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Strings = new List<string>
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Object = null,
                        ObjectArray = new object[] {null, null, null},
                        Objects = new List<object> {null, null, null},
                        EnumByte = (EnumByte) (seed % 3),
                        EnumSByte = (EnumSByte) (seed % 3),
                        EnumShort = (EnumShort) (seed % 3),
                        EnumUShort = (EnumUShort) (seed % 3),
                        EnumInt = (EnumInt) (seed % 3),
                        EnumUInt = (EnumUInt) (seed % 3),
                        EnumLong = (EnumLong) (seed % 3),
                        EnumULong = (EnumULong) (seed % 3)
                    }
                },
                Kids = new List<TestModel>
                {
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Guids = new List<Guid> {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Short = -1,
                        ShortArray = new short[] {-1, 0, 1},
                        Shorts = new List<short> {-1, 0, 1},
                        Int = -2,
                        IntArray = new[] {-1, 0, 1},
                        Ints = new List<int> {-1, 0, 1},
                        Long = -3,
                        LongArray = new long[] {-1, 0, 1},
                        Longs = new List<long> {-1, 0, 1},
                        UShort = 1,
                        UShortArray = new ushort[] {0, 1, 2},
                        UShorts = new List<ushort> {0, 1, 2},
                        UInt = 2,
                        UIntArray = new uint[] {0, 1, 2},
                        UInts = new List<uint> {0, 1, 2},
                        ULong = 3,
                        ULongArray = new ulong[] {0, 1, 2},
                        ULongs = new List<ulong> {0, 1, 2},
                        Float = 0.1F,
                        FloatArray = new float[] {-1, 0, 1},
                        Floats = new List<float> {-1, 0, 1},
                        Double = 0.2,
                        DoubleArray = new double[] {-1, 0, 1},
                        Doubles = new List<double> {-1, 0, 1},
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] {-1, 0, 1},
                        Decimals = new List<decimal> {-1, 0, 1},
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimes = new List<DateTime> {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[] {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        DateTimeUtcs = new List<DateTime> {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Strings = new List<string>
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Object = null,
                        ObjectArray = new object[] {null, null, null},
                        Objects = new List<object> {null, null, null},
                        EnumByte = (EnumByte) (seed % 3),
                        EnumSByte = (EnumSByte) (seed % 3),
                        EnumShort = (EnumShort) (seed % 3),
                        EnumUShort = (EnumUShort) (seed % 3),
                        EnumInt = (EnumInt) (seed % 3),
                        EnumUInt = (EnumUInt) (seed % 3),
                        EnumLong = (EnumLong) (seed % 3),
                        EnumULong = (EnumULong) (seed % 3)
                    },
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Guids = new List<Guid> {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Short = -1,
                        ShortArray = new short[] {-1, 0, 1},
                        Shorts = new List<short> {-1, 0, 1},
                        Int = -2,
                        IntArray = new[] {-1, 0, 1},
                        Ints = new List<int> {-1, 0, 1},
                        Long = -3,
                        LongArray = new long[] {-1, 0, 1},
                        Longs = new List<long> {-1, 0, 1},
                        UShort = 1,
                        UShortArray = new ushort[] {0, 1, 2},
                        UShorts = new List<ushort> {0, 1, 2},
                        UInt = 2,
                        UIntArray = new uint[] {0, 1, 2},
                        UInts = new List<uint> {0, 1, 2},
                        ULong = 3,
                        ULongArray = new ulong[] {0, 1, 2},
                        ULongs = new List<ulong> {0, 1, 2},
                        Float = 0.1F,
                        FloatArray = new float[] {-1, 0, 1},
                        Floats = new List<float> {-1, 0, 1},
                        Double = 0.2,
                        DoubleArray = new double[] {-1, 0, 1},
                        Doubles = new List<double> {-1, 0, 1},
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] {-1, 0, 1},
                        Decimals = new List<decimal> {-1, 0, 1},
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimes = new List<DateTime> {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[] {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        DateTimeUtcs = new List<DateTime> {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Strings = new List<string>
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Object = null,
                        ObjectArray = new object[] {null, null, null},
                        Objects = new List<object> {null, null, null},
                        EnumByte = (EnumByte) (seed % 3),
                        EnumSByte = (EnumSByte) (seed % 3),
                        EnumShort = (EnumShort) (seed % 3),
                        EnumUShort = (EnumUShort) (seed % 3),
                        EnumInt = (EnumInt) (seed % 3),
                        EnumUInt = (EnumUInt) (seed % 3),
                        EnumLong = (EnumLong) (seed % 3),
                        EnumULong = (EnumULong) (seed % 3)
                    },
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        GuidArray = new[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Guids = new List<Guid> {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
                        Short = -1,
                        ShortArray = new short[] {-1, 0, 1},
                        Shorts = new List<short> {-1, 0, 1},
                        Int = -2,
                        IntArray = new[] {-1, 0, 1},
                        Ints = new List<int> {-1, 0, 1},
                        Long = -3,
                        LongArray = new long[] {-1, 0, 1},
                        Longs = new List<long> {-1, 0, 1},
                        UShort = 1,
                        UShortArray = new ushort[] {0, 1, 2},
                        UShorts = new List<ushort> {0, 1, 2},
                        UInt = 2,
                        UIntArray = new uint[] {0, 1, 2},
                        UInts = new List<uint> {0, 1, 2},
                        ULong = 3,
                        ULongArray = new ulong[] {0, 1, 2},
                        ULongs = new List<ulong> {0, 1, 2},
                        Float = 0.1F,
                        FloatArray = new float[] {-1, 0, 1},
                        Floats = new List<float> {-1, 0, 1},
                        Double = 0.2,
                        DoubleArray = new double[] {-1, 0, 1},
                        Doubles = new List<double> {-1, 0, 1},
                        Decimal = 0.3M,
                        DecimalArray = new decimal[] {-1, 0, 1},
                        Decimals = new List<decimal> {-1, 0, 1},
                        DateTime = DateTime.Now,
                        DateTimeArray = new[] {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimes = new List<DateTime> {DateTime.Now, DateTime.Now, DateTime.Now},
                        DateTimeUtc = DateTime.UtcNow,
                        DateTimeUtcArray = new[] {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        DateTimeUtcs = new List<DateTime> {DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow},
                        String = Guid.NewGuid().ToString(),
                        StringArray = new[]
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Strings = new List<string>
                            {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                        Object = null,
                        ObjectArray = new object[] {null, null, null},
                        Objects = new List<object> {null, null, null},
                        EnumByte = (EnumByte) (seed % 3),
                        EnumSByte = (EnumSByte) (seed % 3),
                        EnumShort = (EnumShort) (seed % 3),
                        EnumUShort = (EnumUShort) (seed % 3),
                        EnumInt = (EnumInt) (seed % 3),
                        EnumUInt = (EnumUInt) (seed % 3),
                        EnumLong = (EnumLong) (seed % 3),
                        EnumULong = (EnumULong) (seed % 3)
                    }
                }
            };
        }
    }
}