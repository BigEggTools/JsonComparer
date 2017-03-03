namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    internal class PrivateSetterCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property.Writable) { return property; }

            var propertyInfo = member as PropertyInfo;
            property.Writable = propertyInfo?.GetSetMethod(true) != null;

            return property;
        }
    }
}
