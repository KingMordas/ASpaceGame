/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents;
using ASpaceGame.CoreComponents.Repositories.FS;
using ASpaceGame.WPF.Dtos;
using ASpaceGame.WPF.Dtos.Entities;
using ASpaceGame.WPF.Helpers;
using ASpaceGame.WPF.Mappers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ASpaceGame.WPF.Pages;

public partial class ListPage : Page, INotifyPropertyChanged
{
    private readonly string _rootPath = ApplicationHelper.ApplicationSettings.RootDir;

    private ObservableCollection<IGameEntityUI> _entries;

    public ObservableCollection<IGameEntityUI> Entries
    {
        get => _entries;
        set
        {
            _entries = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(EntriesCount));
        }
    }

    public int EntriesCount => _entries.Count;

    public ListPage()
    {
        DataContext = this;
        _entries = [];
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete && sender is DataGrid dataGrid)
        {
            FileSystemManagement repo = new(_rootPath);
            List<IGameEntityUI> selectedItems = [.. dataGrid.SelectedItems.Cast<IGameEntityUI>()];

            MessageBoxResult confirmation = MessageBox.Show(
            $"Are you sure you wish to delete the selected {selectedItems.Count:N0} items? The operation cannot be undone.",
            "Delete Operation",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {
                foreach (IGameEntityUI item in selectedItems)
                {
                    if (item.Id.HasValue)
                    {
                        Type entityType = item is DtoOfficer ? typeof(Officer) : throw new InvalidOperationException("Unknown entity type to delete.");
                        MethodInfo? deleteMethod = typeof(FileSystemManagement).GetMethod("Delete")?.MakeGenericMethod(entityType);

                        _ = (deleteMethod?.Invoke(repo, [item.Id.Value]));
                        _ = _entries.Remove(item);
                    }
                }

                OnPropertyChanged(nameof(EntriesCount));
                OnPropertyChanged(nameof(Entries));
            }
        }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (Window.GetWindow(this) is MainWindow parentWindow)
        {
            parentWindow.NavGeneralMenu.NavBreadcrumbPage.Text = ApplicationHelper.GetPageNameFromTag(Tag);
            Title = parentWindow.NavGeneralMenu.NavBreadcrumbPage.Text;
            TbIntroCaption.Text = Title;
        }

        if (Tag is not null and PagesEnum page)
        {
            switch (page)
            {
                case PagesEnum.OfficersList:
                    GetEntitiesList<DtoOfficer, Officer>();
                    BtnAdd.Tag = PagesEnum.EditOfficer;
                    break;

                default:
                    break;
            }
        }

        OnPropertyChanged(nameof(EntriesCount));
    }

    private void GetEntitiesList<T, R>()
        where T : IGameEntityUI
        where R : CoreComponents.Interfaces.IGameEntity
    {
        try
        {
            FileSystemManagement repo = new(_rootPath);
            IEnumerable<R> officersList = repo.Load<R>();

            AddCustomColumns(typeof(T));

            foreach (R newEntry in officersList.Cast<R>())
            {
                AddEntityToDataSource(newEntry);
            }
        }
        catch (JsonException)
        {
            _ = MessageBox.Show(
                "I can't load one or more file(s) in the default directory; there could be some files with the wrong name format. Please, check your directory and, if necessary, clean it up, then try again.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void AddEntityToDataSource(object newEntry)
    {
        IGameEntityUI? dtoEntity = null;
        bool isOfficer = newEntry is Officer;

        if (isOfficer)
        {
            dtoEntity = AutoMapperConfiguration.Mapper?.Map<DtoOfficer>(newEntry);
        }

        if (dtoEntity != null)
        {
            _entries.Add(dtoEntity);
        }
    }

    private void AddCustomColumns(Type type)
    {
        foreach (PropertyInfo property in type.GetProperties())
        {
            DataGridColumn? column = null;

            if (property.Name == "Biography")
            {
                continue;
            }

            if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
            {
                column = new DataGridCheckBoxColumn
                {
                    Header = ApplicationHelper.SeparateWordsWithSpace(property.Name),
                    Binding = new Binding(property.Name),
                    IsReadOnly = true,
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };
            }

            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                column = new DataGridTextColumn
                {
                    Header = ApplicationHelper.SeparateWordsWithSpace(property.Name),
                    Binding = new Binding(property.Name),
                    IsReadOnly = true,
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };
            }
            else if (property.PropertyType == typeof(string) || property.PropertyType.IsPrimitive)
            {
                column = new DataGridTextColumn
                {
                    Header = ApplicationHelper.SeparateWordsWithSpace(property.Name),
                    Binding = new Binding(property.Name),
                    IsReadOnly = true,
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                    Visibility = property.Name == "Id" ? Visibility.Collapsed : Visibility.Visible
                };
            }

            if (column != null)
            {
                DgData.Columns.Add(column);
            }
        }
    }

    private void Page_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.OriginalSource is DependencyObject originalSource)
        {
            DataGrid? dataGrid = ApplicationHelper.FindParent<DataGrid>(originalSource);

            if (dataGrid == null || dataGrid.Name != "DgData")
            {
                DgData.SelectedItems.Clear();
            }
        }
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        string? tag = (sender as Control)?.Tag?.ToString();

        if (!string.IsNullOrWhiteSpace(tag) && Window.GetWindow(this) is MainWindow parentWindow)
        {
            if (Enum.TryParse(tag, out PagesEnum pageEnum))
            {
                Page? destinationPage = ApplicationHelper.CreatePageInstance(pageEnum);

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

    private void DgData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is DataGrid dg && dg.SelectedItem is IGameEntityUI selectedItem && selectedItem.Id.HasValue)
        {
            Guid entityId = selectedItem.Id.Value;

            if (Window.GetWindow(this) is MainWindow parentWindow)
            {
                PagesEnum pagesEnum = ApplicationHelper.GetPagesEditEnumFromEntity(selectedItem);
                Page? editPage = ApplicationHelper.CreatePageInstance(pagesEnum, selectedItem);

                if (editPage != null)
                {
                    editPage.Tag = entityId;
                    _ = parentWindow.FrmContent.Navigate(editPage);
                }
                else
                {
                    _ = MessageBox.Show("The requested feature has not been implemented yet.", "Not Available", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }
}
