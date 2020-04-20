using System.Runtime.Serialization;

namespace CTeleportTest.Core.Contracts.Enums
{
    public enum CrewChangeMember
    {
        [EnumMember(Value = "offsigner")]
        Offsigner,
        [EnumMember(Value = "onsigner")]
        Onsigner
    }
}