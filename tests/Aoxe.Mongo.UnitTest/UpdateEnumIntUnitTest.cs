namespace Aoxe.Mongo.UnitTest
{
    public class UpdateEnumIntUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(EnumInt.Apple)]
        [InlineData(EnumInt.Banana)]
        [InlineData(EnumInt.Pear)]
        public void EnumIntTest(EnumInt value)
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumInt = value }
            );
            Assert.Equal(testModel.EnumInt == value ? 0L : 1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumInt);
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void EnumIntConstantTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumInt = EnumInt.Pear }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(EnumInt.Pear, modifyModel.EnumInt);
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void EnumIntArrayTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var enumIntArray = new[] { EnumInt.Apple, EnumInt.Banana, EnumInt.Pear };
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumIntArray = enumIntArray }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumIntArray, modifyModel.EnumIntArray));
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Fact]
        public void EnumIntListTest()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var enumIntList = new List<EnumInt> { EnumInt.Apple, EnumInt.Banana, EnumInt.Pear };
            var result = AoxeMongoClient.Update(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumIntList = enumIntList }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumIntList, modifyModel.EnumIntList));
            Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
        }

        [Theory]
        [InlineData(EnumInt.Apple)]
        [InlineData(EnumInt.Banana)]
        [InlineData(EnumInt.Pear)]
        public async Task EnumIntTestAsync(EnumInt value)
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumInt = value }
            );
            Assert.Equal(testModel.EnumInt == value ? 0L : 1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumInt);
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task EnumIntConstantTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumInt = EnumInt.Pear }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.Equal(EnumInt.Pear, modifyModel.EnumInt);
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task EnumIntArrayTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var enumIntArray = new[] { EnumInt.Apple, EnumInt.Banana, EnumInt.Pear };
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumIntArray = enumIntArray }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumIntArray, modifyModel.EnumIntArray));
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }

        [Fact]
        public async Task EnumIntListTestAsync()
        {
            var testModel = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(testModel);
            var enumIntList = new List<EnumInt> { EnumInt.Apple, EnumInt.Banana, EnumInt.Pear };
            var result = await AoxeMongoClient.UpdateAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumIntList = enumIntList }
            );
            Assert.Equal(1L, result);
            var modifyModel = AoxeMongoClient
                .GetQueryable<TestModel>()
                .First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumIntList, modifyModel.EnumIntList));
            Assert.Equal(
                1L,
                (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id))
            );
        }
    }
}
