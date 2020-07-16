using System;

namespace Zaabee.Mongo.Core.UnitTest
{
    public class KidTestModel
    {
        public int Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
    }
}