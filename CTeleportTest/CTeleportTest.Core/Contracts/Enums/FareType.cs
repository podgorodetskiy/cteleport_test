using System.Runtime.Serialization;

namespace CTeleportTest.Core.Contracts.Enums
{
    public enum FareType
    {
        [EnumMember(Value = "marine")]
        Marine,
        [EnumMember(Value = "mixed")]
        Mixed,
        [EnumMember(Value = "public")]
        Public
    }
}