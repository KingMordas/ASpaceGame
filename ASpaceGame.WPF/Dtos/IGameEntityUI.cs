/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

namespace ASpaceGame.WPF.Dtos;

public interface IGameEntityUI
{
    Guid? Id { get; set; }
    string? Name { get; set; }
}
