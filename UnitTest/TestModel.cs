using System;

namespace UnitTest
{
    public class TestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateUtcTime { get; set; }
    }
}