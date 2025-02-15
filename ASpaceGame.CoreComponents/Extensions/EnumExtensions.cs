/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

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
