namespace Aoxe.Mongo.UnitTest
{
    public class UpdateDoubleUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(0)]
        [InlineData(double.MaxValue)]
        public void DoubleTest(double value)
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { Double = value }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.Double);
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void DoubleArrayTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var doubleArray = new[] { double.MinValue, 0, double.MaxValue };
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { DoubleArray = doubleArray }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(doubleArray, modifyModel.DoubleArray));
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void DoubleListTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var doubleList = new List<double> { double.MinValue, 0, double.MaxValue };
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { DoubleList = doubleList }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(doubleList, modifyModel.DoubleList));
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(0)]
        [InlineData(double.MaxValue)]
        public async Task DoubleTestAsync(double value)
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { Double = value }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.Double);
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task DoubleArrayTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var doubleArray = new[] { double.MinValue, 0, double.MaxValue };
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { DoubleArray = doubleArray }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(doubleArray, modifyModel.DoubleArray));
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task DoubleListTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var doubleList = new List<double> { double.MinValue, 0, double.MaxValue };
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { DoubleList = doubleList }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(doubleList, modifyModel.DoubleList));
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }
    }
}
