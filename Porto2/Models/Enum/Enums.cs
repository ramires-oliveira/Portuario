using System.ComponentModel;

namespace Porto.Models.Enums
{
    public static class Enums
    {
        public static string GetDescription(this Enum value)
        {
            var enumType = value.GetType();

            var field = enumType.GetField(value.ToString());

            var attributes = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes?.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
