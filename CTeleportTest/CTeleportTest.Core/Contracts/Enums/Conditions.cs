using System.Runtime.Serialization;

namespace CTeleportTest.Core.Contracts.Enums
{
    public enum Conditions
    {
        [EnumMember(Value = "-")]
        Empty,
        [EnumMember(Value = "non-refundable")]
        NonRefundable,
        [EnumMember(Value = "paid-refund")]
        PaidRefund,
        [EnumMember(Value = "refundable")]
        Refundable
    }
}