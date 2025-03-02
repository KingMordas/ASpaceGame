/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.Repositories;
using ASpaceGame.CoreComponents.Repositories.FS;
using System.Diagnostics;

namespace ASpaceGame.CoreComponents;

/// <summary>
/// This is the starter point for ASpaceGame. It is required to call the Start() method from every developed UI in order to load the required software components.
/// </summary>
public static class ASpaceGameStarter
{
    public static IRepoManagement RepoManagement { get; }

    static ASpaceGameStarter()
    {
        RepoManagement = new FileSystemManagement(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "ASpaceGame"));
    }

    /// <summary>
    /// Mandatory, application entry point.
    /// </summary>
    public static void Start()
    {
        Debug.WriteLine("Starting ASpaceGame...");

        RepositorySetup();

        Debug.WriteLine("Finished loading ASpaceGame.");
    }

    private static void RepositorySetup()
    {
        RepoManagement.Initialize();
    }
}
