namespace Aoxe.Mongo.UnitTest
{
    public class UpdateIntUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public void IntTest(int value)
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { Int = value }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.Int);
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void IntArrayTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var intArray = new[] { int.MinValue, 0, int.MaxValue };
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { IntArray = intArray }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(intArray, modifyModel.IntArray));
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void IntListTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var intList = new List<int> { int.MinValue, 0, int.MaxValue };
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { IntList = intList }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(intList, modifyModel.IntList));
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public async Task IntTestAsync(int value)
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { Int = value }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.Int);
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task IntArrayTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var intArray = new[] { int.MinValue, 0, int.MaxValue };
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { IntArray = intArray }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(intArray, modifyModel.IntArray));
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task IntListTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var intList = new List<int> { int.MinValue, 0, int.MaxValue };
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { IntList = intList }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(intList, modifyModel.IntList));
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }
    }
}
