/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.WPF.Helpers;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ASpaceGame.WPF.Pages;

public partial class AboutPage : Page
{
    public AboutPage()
    {
        InitializeComponent();
        TbASpaceGame.Text = $"ASpaceGame ({ApplicationHelper.ApplicationSettings.CurrentVersion})";
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        _ = Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        e.Handled = true;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (Window.GetWindow(this) is MainWindow parentWindow)
        {
            parentWindow.NavGeneralMenu.NavBreadcrumbPage.Text = ApplicationHelper.GetPageNameFromTag(PagesEnum.About);
            Title = parentWindow.NavGeneralMenu.NavBreadcrumbPage.Text;
        }
    }
}
