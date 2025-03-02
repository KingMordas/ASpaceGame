/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.CoreComponents;
using ASpaceGame.CoreComponents.Enums;
using ASpaceGame.WPF.Dtos.Entities;
using ASpaceGame.WPF.Helpers;
using ASpaceGame.WPF.Mappers;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using static System.Environment;

namespace ASpaceGame.WPF.Pages;

public partial class EditOfficerPage : Page, INotifyPropertyChanged
{
    private DtoOfficer _officer = new();

    public DtoOfficer Officer
    {
        get => _officer;
        set
        {
            _officer = value;
            OnPropertyChanged();
        }
    }

    public EditOfficerPage()
    {
        DataContext = this;
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (Tag != null && Tag.GetType() == typeof(Guid))
        {
            Officer loadedOfficer = new();

            loadedOfficer.Load((Guid)Tag);

            if (loadedOfficer != null)
            {
                Officer = AutoMapperConfiguration.Mapper?.Map<DtoOfficer>(loadedOfficer) ?? new();
            }
        }

        if (Window.GetWindow(this) is MainWindow parentWindow)
        {
            parentWindow.NavGeneralMenu.NavBreadcrumbPage.Text = "Edit";
            Title = parentWindow.NavGeneralMenu.NavBreadcrumbPage.Text;
        }

        BindId();
        BindRankCombo((OfficerRanksEnum)Enum.Parse(typeof(OfficerRanksEnum), Officer.Rank ?? "Ensign"));
        BindPicture();

        OnPropertyChanged(nameof(Officer));
    }

    private void BindId()
    {
        LblId.Content = $"{Officer.Id ?? Guid.NewGuid()}";
    }

    private void BindPicture()
    {
        Officer.Picture ??= ApplicationHelper.GetImageBytes("NewOfficer.png");
    }

    private void BindRankCombo(OfficerRanksEnum selectedRank)
    {
        var ranks = Enum.GetValues(typeof(OfficerRanksEnum))
                        .Cast<OfficerRanksEnum>()
                        .Select(rank => new
                        {
                            Value = rank,
                            Text = ApplicationHelper.SeparateWordsWithSpace(rank.ToString())
                        })
                        .ToList();

        CbRank.ItemsSource = ranks;
        CbRank.DisplayMemberPath = "Text";
        CbRank.SelectedValuePath = "Value";
        CbRank.SelectedValue = selectedRank;
    }

    private void BtnChangePicture_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            InitialDirectory = GetFolderPath(SpecialFolder.MyPictures),
            Filter = "JPEG files (*.jpeg)|*.jpeg|JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png",
            Title = $"Select an Image (300x400 for best rendering)"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            Officer.Picture = ApplicationHelper.GetImageBytes(openFileDialog.FileName, true);
            OnPropertyChanged(nameof(Officer));
        }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        Officer? officer = AutoMapperConfiguration.Mapper?.Map<Officer>(Officer);

        officer?.Save();

        if (Window.GetWindow(this) is MainWindow parentWindow)
        {
            Page? destinationPage = ApplicationHelper.CreatePageInstance(PagesEnum.OfficersList);

            _ = parentWindow.FrmContent.Navigate(destinationPage);
        }
    }

    private void TextBox_NumberValidation_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            string senderText = textBox.Text ?? string.Empty;

            if (int.TryParse(senderText, out int senderValue))
            {
                if (senderValue < 0)
                {
                    textBox.Text = "0";
                    textBox.SelectAll();
                }
                else if (senderValue > 100)
                {
                    textBox.Text = "100";
                    textBox.SelectAll();
                }
            }
            else
            {
                textBox.Clear();
                _ = textBox.Focus();
            }
        }
    }
}
