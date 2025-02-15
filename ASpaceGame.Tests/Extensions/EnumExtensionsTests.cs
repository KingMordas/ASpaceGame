/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.Attributes;
using ASpaceGame.CoreComponents.Extensions;

namespace ASpaceGame.Tests.Extensions;

public class EnumExtensionsTests
{
    private enum TestEnum
    {
        [StringValue("First Value")]
        First,
        [StringValue("Second Value")]
        Second,
        Third
    }

    [Fact]
    public void GetStringValue_WithStringValueAttribute_ReturnsAttributeValue()
    {
        // Arrange
        TestEnum enumValue = TestEnum.First;

        // Act
        string result = enumValue.GetStringValue();

        // Assert
        Assert.Equal("First Value", result);
    }

    [Fact]
    public void GetStringValue_WithoutStringValueAttribute_ReturnsEnumName()
    {
        // Arrange
        TestEnum enumValue = TestEnum.Third;

        // Act
        string result = enumValue.GetStringValue();

        // Assert
        Assert.Equal("Third", result);
    }

    [Fact]
    public void GetStringValue_FieldNotFound_ThrowsArgumentException()
    {
        // Arrange
        TestEnum enumValue = (TestEnum)999;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => enumValue.GetStringValue());
        Assert.Contains("Field not found for enum value", exception.Message);
    }
}
