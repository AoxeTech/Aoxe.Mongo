namespace Aoxe.Mongo.UnitTest
{
    public class EnumShortUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(EnumShort.Apple)]
        [InlineData(EnumShort.Banana)]
        [InlineData(EnumShort.Pear)]
        public void EnumShortTest(EnumShort value)
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumShort = value }
            );
            Assert.Equal(testModel.EnumShort == value ? 0L : 1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumShort);
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void EnumShortArrayTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var enumShortArray = new[] { EnumShort.Apple, EnumShort.Banana, EnumShort.Pear };
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumShortArray = enumShortArray }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumShortArray, modifyModel.EnumShortArray));
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void EnumShortListTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var enumShortList = new List<EnumShort>
            {
                EnumShort.Apple,
                EnumShort.Banana,
                EnumShort.Pear
            };
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumShortList = enumShortList }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumShortList, modifyModel.EnumShortList));
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Theory]
        [InlineData(EnumShort.Apple)]
        [InlineData(EnumShort.Banana)]
        [InlineData(EnumShort.Pear)]
        public async Task EnumShortTestAsync(EnumShort value)
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumShort = value }
            );
            Assert.Equal(testModel.EnumShort == value ? 0L : 1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumShort);
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task EnumShortArrayTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var enumShortArray = new[] { EnumShort.Apple, EnumShort.Banana, EnumShort.Pear };
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumShortArray = enumShortArray }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumShortArray, modifyModel.EnumShortArray));
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task EnumShortListTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var enumShortList = new List<EnumShort>
            {
                EnumShort.Apple,
                EnumShort.Banana,
                EnumShort.Pear
            };
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumShortList = enumShortList }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumShortList, modifyModel.EnumShortList));
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }
    }
}
