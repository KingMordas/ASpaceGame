/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.Interfaces;

namespace ASpaceGame.CoreComponents.Repositories;

public interface IRepoManagement
{
    void Initialize();
    void Save<T>(T entity) where T : IGameEntity;
    T? Load<T>(Guid id) where T : IGameEntity;
    IEnumerable<T> Load<T>() where T : IGameEntity;
    void Delete<T>(Guid id) where T : IGameEntity;
}
