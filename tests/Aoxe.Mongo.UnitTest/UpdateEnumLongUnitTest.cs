namespace Aoxe.Mongo.UnitTest
{
    public class UpdateEnumLongUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(EnumLong.Apple)]
        [InlineData(EnumLong.Banana)]
        [InlineData(EnumLong.Pear)]
        public void EnumLongTest(EnumLong value)
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLong = value }
            );
            Assert.Equal(testModel.EnumLong == value ? 0L : 1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumLong);
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void EnumLongArrayTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var enumLongArray = new[] { EnumLong.Apple, EnumLong.Banana, EnumLong.Pear };
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLongArray = enumLongArray }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumLongArray, modifyModel.EnumLongArray));
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void EnumLongListTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var enumLongList = new List<EnumLong>
            {
                EnumLong.Apple,
                EnumLong.Banana,
                EnumLong.Pear
            };
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLongList = enumLongList }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumLongList, modifyModel.EnumLongList));
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Theory]
        [InlineData(EnumLong.Apple)]
        [InlineData(EnumLong.Banana)]
        [InlineData(EnumLong.Pear)]
        public async Task EnumLongTestAsync(EnumLong value)
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLong = value }
            );
            Assert.Equal(testModel.EnumLong == value ? 0L : 1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumLong);
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task EnumLongArrayTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var enumLongArray = new[] { EnumLong.Apple, EnumLong.Banana, EnumLong.Pear };
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLongArray = enumLongArray }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumLongArray, modifyModel.EnumLongArray));
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task EnumLongListTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var enumLongList = new List<EnumLong>
            {
                EnumLong.Apple,
                EnumLong.Banana,
                EnumLong.Pear
            };
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLongList = enumLongList }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumLongList, modifyModel.EnumLongList));
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }
    }
}
