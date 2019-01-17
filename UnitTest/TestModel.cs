using System;
using System.Collections.Generic;

namespace UnitTest
{
    public class TestModel
    {
        public Guid Id { get; set; }
        public Guid Guid { get; set; }
        public short Short { get; set; }
        public int Int { get; set; }
        public long Long { get; set; }
        public ushort Ushort { get; set; }
        public uint Uint { get; set; }
        public ulong Ulong { get; set; }
        public float Float { get; set; }
        public double Double { get; set; }
        public decimal Decimal { get; set; }
        public DateTime Now { get; set; }
        public DateTime UtcNow { get; set; }
        public TestEnum TestEnum { get; set; }
        public string String { get; set; }
        public object Object { get; set; }
        public TestModel Kid { get; set; }
        public List<TestModel> Kids { get; set; }
    }

    public enum TestEnum : byte
    {
        Apple,
        Banana,
        Pear
    }
}