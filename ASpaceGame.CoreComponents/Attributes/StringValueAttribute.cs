namespace ASpaceGame.CoreComponents.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class StringValueAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}
