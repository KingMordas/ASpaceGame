/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.WPF.Dtos;
using ASpaceGame.WPF.Dtos.Entities;
using ASpaceGame.WPF.Helpers;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace ASpaceGame.WPF.Pages;

public partial class OpenPage : Page
{
    private readonly IGameEntityUI _gameEntity;
    private readonly string _startingDirectory;

    public OpenPage(IGameEntityUI entity)
    {
        _gameEntity = entity;
        _startingDirectory = ApplicationHelper.ApplicationSettings.RootDir;
        DataContext = this;
        InitializeComponent();
    }

    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (Window.GetWindow(this) is MainWindow parentWindow)
        {
            parentWindow.NavGeneralMenu.NavBreadcrumbPage.Text = ApplicationHelper.GetPageNameFromTag(Tag) + " " + GetSection();
            Title = parentWindow.NavGeneralMenu.NavBreadcrumbPage.Text;
        }

        OpenFilePicker();
    }

    private void OpenFilePicker()
    {
        PagesEnum pageEnum = ApplicationHelper.GetPagesEditEnumFromEntity(_gameEntity);
        OpenFileDialog openFileDialog = new()
        {
            InitialDirectory = _startingDirectory,
            Filter = "JSON files (*.json)|*.json",
            Title = $"Select a {GetSection()} file"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string fileName = openFileDialog.SafeFileName;

            if (Window.GetWindow(this) is MainWindow parent)
            {
                Guid objectId = Guid.Parse(fileName.Split('_').First());
                Page? destinationPage = ApplicationHelper.CreatePageInstance(pageEnum, _gameEntity);

                if (destinationPage != null)
                {
                    destinationPage.Tag = objectId;
                    _ = parent.FrmContent.Navigate(destinationPage);
                }
            }
        }
        else
        {
            if (Window.GetWindow(this) is MainWindow parentWindow)
            {
                _ = parentWindow.FrmContent.Navigate(ApplicationHelper.CreatePageInstance(ApplicationHelper.GetPagesListEnumFromEntity(_gameEntity)));
            }
        }
    }

    private string GetSection()
    {
        Type type = _gameEntity.GetType();
        string section = string.Empty;

        if (type == typeof(DtoOfficer))
        {
            section = "Officer";
        }

        return section;
    }
}
