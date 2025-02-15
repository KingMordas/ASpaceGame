using ASpaceGame.CoreComponents.Attributes;

namespace ASpaceGame.Tests.Attributes;

public class StringValueAttributeTests
{
    [Fact]
    public void Constructor_ShouldSetStringValue()
    {
        // Arrange
        var expectedValue = "TestValue";

        // Act
        var attribute = new StringValueAttribute(expectedValue);

        // Assert
        Assert.Equal(expectedValue, attribute.Value);
    }
}
