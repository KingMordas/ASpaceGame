/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.WPF.Dtos;
using ASpaceGame.WPF.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace ASpaceGame.WPF.View.UserControls;

public partial class GeneralMenu : UserControl
{
    public GeneralMenu()
    {
        DataContext = this;
        InitializeComponent();
    }

    private void MenuItem_FileQuit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.MainWindow.Close();
    }

    private void MenuItem_NavigateTo_Click(object sender, RoutedEventArgs e)
    {
        string?[]? tags = (sender as Control)?.Tag?.ToString()?.Split('|');

        string? tag = (sender as Control)?.Tag?.ToString()?.Split('|')[0];
        string? dest = tags != null && tags.Length >= 2
            ? (sender as Control)?.Tag?.ToString()?.Split('|')[1]
            : null;

        if (!string.IsNullOrWhiteSpace(tag) && Window.GetWindow(this) is MainWindow parentWindow)
        {
            if (Enum.TryParse(tag, out PagesEnum pageEnum))
            {
                string className = $"ASpaceGame.WPF.Dtos.Entities.{dest ?? string.Empty}";
                Type? type = Type.GetType(className);
                IGameEntityUI? entity = type != null ? (IGameEntityUI?)Activator.CreateInstance(type) : null;
                Page? destinationPage = ApplicationHelper.CreatePageInstance(pageEnum, entity);

                if (destinationPage != null && parentWindow?.FrmContent != null)
                {
                    _ = parentWindow.FrmContent.Navigate(destinationPage);
                }
                else
                {
                    _ = MessageBox.Show("The requested feature has not been implemented yet.", "Not Available", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }
}
