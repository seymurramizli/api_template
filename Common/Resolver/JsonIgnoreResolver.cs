using Common.Attribute;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Common.Resolver;

public class JsonIgnoreResolver : CamelCasePropertyNamesContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member,
        MemberSerialization memberSerialization)
    {
        JsonProperty property = base.CreateProperty(member, memberSerialization);

        if (member.IsDefined(typeof(IgnoreLogAttribute), false))
        {
            property.ShouldSerialize = _ => false;
        }

        return property;
    }
}