using System.Runtime.Serialization;

namespace CTeleportTest.Core.Contracts.Enums
{
    public enum State
    {
        [EnumMember(Value = "cancelled")]
        Cancelled,
        [EnumMember(Value = "confirmed")]
        Confirmed,
        [EnumMember(Value = "issued")]
        Issued,
        [EnumMember(Value = "refund_pending")]
        RefundPending,
        [EnumMember(Value = "used")]
        Used
    }
}