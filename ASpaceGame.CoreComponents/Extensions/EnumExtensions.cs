using ASpaceGame.CoreComponents.Attributes;
using System.Reflection;

namespace ASpaceGame.CoreComponents.Extensions;

public static class EnumExtensions
{
    public static string GetStringValue(this Enum enumValue)
    {
        FieldInfo? field = enumValue.GetType().GetField(enumValue.ToString()) ?? throw new ArgumentException($"Field not found for enum value: {enumValue}");
        StringValueAttribute? attribute = field.GetCustomAttribute<StringValueAttribute>();

        return attribute?.Value ?? enumValue.ToString();
    }
}
