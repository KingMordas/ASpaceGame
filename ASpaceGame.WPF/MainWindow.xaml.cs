/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.WPF.Helpers;
using ASpaceGame.WPF.Pages;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ASpaceGame.WPF;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    private Page _currentPage = new AboutPage();

    public Page CurrentPage
    {
        get => _currentPage;
        set
        {
            _currentPage = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MainWindow()
    {
        DataContext = this;
        InitializeComponent();
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        e.Cancel = !ApplicationHelper.ShouldIQuit();
    }

    private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        ApplicationHelper.FindChild<DataGrid>(this, "DgData")?.SelectedItems.Clear();
    }
}
