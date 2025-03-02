/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace ASpaceGame.WPF.Helpers;

public class ApplicationConfiguration
{
    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    private readonly string _wpfSettingsFile = "WPFSettings.json";
    private readonly string _fileSettings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "ASpaceGame", "ASpaceGame.ini");

    public string CurrentVersion { get; set; } = "vUndefined";
    public string RootDir { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "ASpaceGame");

    public void Load()
    {
        if (!File.Exists(_fileSettings))
        {
            CurrentVersion = LoadVersionFromSettings();
            RootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "ASpaceGame");
            Save();
        }
        else
        {
            CurrentVersion = ReadIniFile("CurrentVersion", "ApplicationInfo");
            if (string.IsNullOrWhiteSpace(CurrentVersion))
            {
                CurrentVersion = "vUndefined";
                Save();
            }

            RootDir = ReadIniFile("RootDir", "ApplicationInfo");
            if (string.IsNullOrWhiteSpace(RootDir))
            {
                RootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "ASpaceGame");
                Save();
            }
        }
    }

    public void Save()
    {
        string? directoryPath = Path.GetDirectoryName(_fileSettings);

        if (directoryPath != null)
        {
            _ = Directory.CreateDirectory(directoryPath);
        }

        WriteIniFile("CurrentVersion", CurrentVersion, "ApplicationInfo");
        WriteIniFile("RootDir", RootDir, "ApplicationInfo");
    }

    private void WriteIniFile(string key, string value, string section)
    {
        _ = WritePrivateProfileString(section, key, value, _fileSettings);
    }

    private string ReadIniFile(string key, string section)
    {
        StringBuilder retVal = new(255);

        _ = GetPrivateProfileString(section, key, string.Empty, retVal, 255, _fileSettings);

        return retVal.ToString();
    }

    private string LoadVersionFromSettings()
    {
        string json = File.ReadAllText(_wpfSettingsFile);
        JsonDocument jObject = JsonDocument.Parse(json);

        return jObject.RootElement.GetProperty("version").GetString() ?? "N/A";
    }
}
