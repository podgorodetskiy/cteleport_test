using System.Runtime.Serialization;

namespace CTeleportTest.Core.Contracts
{
    public enum CrewChangeMember
    {
        [EnumMember(Value = "offsigner")]
        Offsigner,
        [EnumMember(Value = "onsigner")]
        Onsigner
    }
}