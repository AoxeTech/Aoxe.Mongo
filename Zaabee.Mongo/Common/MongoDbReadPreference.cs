namespace Zaabee.Mongo.Common
{
    public enum MongoDbReadPreference
    {
        /// <summary>
        /// 主节点，默认模式，读操作只在主节点，如果主节点不可用，报错或者抛出异常
        /// </summary>
        Primary = 0,

        /// <summary>
        /// 首选主节点，大多情况下读操作在主节点，如果主节点不可用，如故障转移，读操作在从节点。
        /// </summary>
        PrimaryPreferred = 1,

        /// <summary>
        /// 从节点，读操作只在从节点， 如果从节点不可用，报错或者抛出异常。
        /// </summary>
        Secondary = 2,

        /// <summary>
        /// 首选从节点，大多情况下读操作在从节点，特殊情况（如单主节点架构）读操作在主节点。
        /// </summary>
        SecondaryPreferred = 3,

        /// <summary>
        /// 最邻近节点，读操作在最邻近的成员，可能是主节点或者从节点
        /// </summary>
        Nearest = 4
    }
}
