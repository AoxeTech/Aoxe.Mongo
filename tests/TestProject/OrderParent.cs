using System;

namespace TestProject
{
    public class OrderParent
    {
        public Guid Id { get; set; }
        public DateTime UtcCreateTime { get; set; }
        public DateTime UtcModifyTime { get; set; }
        public DateTime UtcCancelTime { get; set; }
        public string UAccount { get; set; }
        public float UMoney { get; set; }
        public float LeftMoney { get; set; }
        public string CCode { get; set; }
        public int DepositPlatform { get; set; }
        public object Dan { get; set; }
        public object TransNo { get; set; }
        public int FeesType { get; set; }
        public int RechageType { get; set; }
        public string OrderId { get; set; }
        public string PtId { get; set; }
        public string PosttypeName { get; set; }
        public string CiId { get; set; }
        public string FsName { get; set; }
        public string RId { get; set; }
        public object TranceId { get; set; }
        public int IoFlag { get; set; }
        public int IState { get; set; }
        public int HedgingFlag { get; set; }
        public object HedgingGroup { get; set; }
        public int DeDuctType { get; set; }
        public int UId { get; set; }
        public object BalanceCycle { get; set; }
        public object BalanceDate { get; set; }
        public object BUserId { get; set; }
        public object BaUserId { get; set; }
        public object AuBalanceDate { get; set; }
        public bool SynMongoFlag { get; set; }
        public int UserId { get; set; }
        public DateTime OperateDate { get; set; }
        public string Remark { get; set; }
        public object HedgingId { get; set; }
        public object CostPtId { get; set; }
        public object CostPosttypeName { get; set; }
        public string CostMoney { get; set; }
        public int OrderCount { get; set; }
        public string Weight { get; set; }
        public int ChargebackPlatform { get; set; }
        public object[] SopIds { get; set; }
        public Userfeesclassitemslist[] UserFeesClassItemsList { get; set; }
        public object[] CostPostTypeFeesClassItemsList { get; set; }
    }

    public class Userfeesclassitemslist
    {
        public int ChargeClass { get; set; }
        public string CCode { get; set; }
        public float ItemMoney { get; set; }
        public int HedgingFlag { get; set; }
        public float ExchangeRate { get; set; }
        public float OriCurFeeValue { get; set; }
        public float CostPrice { get; set; }
        public string CName { get; set; }
    }
}