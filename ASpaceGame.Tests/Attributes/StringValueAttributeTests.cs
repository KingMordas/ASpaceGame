/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

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
