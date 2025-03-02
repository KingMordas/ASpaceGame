/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

namespace ASpaceGame.CoreComponents.Utilities;

public class SkillsUtilities
{
    private Random _randomGenerator = new();

    public static int NormalizeSkill(int skill, int maxValue = 100, int minValue = 1)
    {
        return Math.Clamp(skill, minValue, maxValue);
    }

    public virtual int RollDice(int diceFaces, int numberOfDices = 1)
    {
        int diceResult = 0;

        for (int i = 0; i < numberOfDices; i++)
        {
            diceResult += _randomGenerator.Next(1, diceFaces + 1);
        }

        return diceResult;
    }
}
