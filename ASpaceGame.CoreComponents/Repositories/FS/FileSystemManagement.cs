/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents.Interfaces;
using System.IO.Abstractions;
using System.Text.Json;

namespace ASpaceGame.CoreComponents.Repositories.FS;

public class FileSystemManagement : IRepoManagement
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IFileSystem _fileSystem;

    public string RootPath { get; private set; }

    public FileSystemManagement(string rootPath)
    {
        if (string.IsNullOrWhiteSpace(rootPath))
        {
            throw new ArgumentException("Invalid path.", nameof(rootPath));
        }

        _fileSystem = new FileSystem();
        RootPath = rootPath;

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public void Initialize()
    {
        string officersPath = Path.Combine(RootPath, "Officers");

        if (!Directory.Exists(RootPath))
        {
            _ = Directory.CreateDirectory(RootPath);
        }

        if (!Directory.Exists(officersPath))
        {
            _ = Directory.CreateDirectory(officersPath);
        }
    }

    public void Save<T>(T entity) where T : IGameEntity
    {
        string filePath;
        string json;

        filePath = _fileSystem.Path.Combine(RootPath, GetEntityPath(entity), $"{entity.Id}_{entity.Name}.json");
        json = JsonSerializer.Serialize(entity, _jsonSerializerOptions);

        if (!string.IsNullOrWhiteSpace(json))
        {
            _fileSystem.File.WriteAllText(filePath, json);
        }
    }

    public T? Load<T>(Guid id) where T : IGameEntity
    {
        T? entity;
        string? filePath = GetFilePathContainingStringInName(id.ToString(), GetEntityPath(CreateInstance<T>()));

        if (!string.IsNullOrWhiteSpace(filePath) && _fileSystem.File.Exists(filePath))
        {
            string json = _fileSystem.File.ReadAllText(filePath);

            T? data = JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
            entity = data ?? default;
        }
        else
        {
            entity = default;
        }

        return entity;
    }

    public IEnumerable<T> Load<T>() where T : IGameEntity
    {
        List<T> entities = [];
        string filesPath = GetEntityPath(CreateInstance<T>());

        foreach (string filePath in _fileSystem.Directory.GetFiles(filesPath, "*.json", SearchOption.AllDirectories))
        {
            string json = _fileSystem.File.ReadAllText(filePath);
            T? foundEntity = JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

            if (foundEntity != null)
            {
                entities.Add(foundEntity);
            }
        }

        return entities;
    }

    public void Delete<T>(Guid id) where T : IGameEntity
    {
        string? filePath = GetFilePathContainingStringInName(id.ToString(), GetEntityPath(CreateInstance<T>()));

        if (!string.IsNullOrWhiteSpace(filePath) && _fileSystem.File.Exists(filePath))
        {
            _fileSystem.File.Delete(filePath);
        }
    }

    public string GetEntityPath(IGameEntity? entity)
    {
        string filePath = string.Empty;
        bool isOfficer = entity is Officer;

        if (isOfficer)
        {
            filePath = Path.Combine(RootPath, "Officers");
        }

        return string.IsNullOrWhiteSpace(filePath) ? throw new ArgumentException("Invalid entity type.", nameof(entity)) : filePath;
    }

    private string? GetFilePathContainingStringInName(string searchString, string searchPath)
    {
        foreach (string filePath in _fileSystem.Directory.GetFiles(searchPath, "*.json", SearchOption.AllDirectories))
        {
            string fileName = _fileSystem.Path.GetFileName(filePath);

            if (fileName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            {
                return filePath;
            }
        }

        return null;
    }

    private static T CreateInstance<T>()
    {
        return typeof(T) == typeof(Officer) ? (T)(object)new Officer() : throw new InvalidOperationException("Unknown class type");
    }
}
