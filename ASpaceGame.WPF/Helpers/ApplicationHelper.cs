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
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ASpaceGame.WPF.Helpers;

public static partial class ApplicationHelper
{
    [GeneratedRegex("(?<!^)([A-Z])")]
    private static partial Regex SpaceBetweenWords();
    public static ApplicationConfiguration ApplicationSettings { get; set; } = new();

    public static bool ShouldIQuit()
    {
        MessageBoxResult messageBoxResult = MessageBox.Show(
            "Are you sure you want to quit? All unsaved progress will be lost.",
            "Exit Program",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        return messageBoxResult == MessageBoxResult.Yes;
    }

    public static string GetPageNameFromTag(object tag)
    {
        return tag is not null and PagesEnum page ? SeparateWordsWithSpace(page.ToString()).Replace(" List", string.Empty) : "N/A";
    }

    public static string SeparateWordsWithSpace(string input)
    {
        return SpaceBetweenWords().Replace(input, " $1");
    }

    public static T? FindParent<T>(DependencyObject child) where T : DependencyObject
    {
        DependencyObject parent = VisualTreeHelper.GetParent(child);

        while (parent != null && parent is not T)
        {
            parent = VisualTreeHelper.GetParent(parent);
        }

        return parent as T;
    }

    public static T? FindChild<T>(DependencyObject? parent, string childName) where T : DependencyObject
    {
        T? foundChild = null;

        if (parent != null)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject? child = VisualTreeHelper.GetChild(parent, i);

                if (child != null)
                {
                    if (child is T typedChild && ((FrameworkElement)child).Name == childName)
                    {
                        foundChild = typedChild;
                        break;
                    }

                    foundChild = FindChild<T>(child, childName);

                    if (foundChild != null)
                    {
                        break;
                    }
                }
            }
        }

        return foundChild;
    }

    public static Page? CreatePageInstance(PagesEnum pageEnum, IGameEntityUI? gameEntity = null)
    {
        Page? page = null;
        string pageName = IsPageList(pageEnum) ? "ListPage" : pageEnum.ToString() + "Page";
        string namespaceName = "ASpaceGame.WPF.Pages";
        Type? pageType = Type.GetType($"{namespaceName}.{pageName}");

        if (pageType != null && typeof(Page).IsAssignableFrom(pageType))
        {
            page = IsPageOpen(pageEnum) ? Activator.CreateInstance(pageType, gameEntity) as Page : Activator.CreateInstance(pageType) as Page;

            if (page != null)
            {
                page.Tag = pageEnum;
            }
        }

        return page;
    }

    public static PagesEnum GetPagesListEnumFromEntity(IGameEntityUI entity)
    {
        PagesEnum pageEnum = entity switch
        {
            DtoOfficer => PagesEnum.OfficersList,

            _ => PagesEnum.About
        };

        return pageEnum;
    }

    public static PagesEnum GetPagesEditEnumFromEntity(IGameEntityUI entity)
    {
        PagesEnum pageEnum = entity switch
        {
            DtoOfficer => PagesEnum.EditOfficer,

            _ => PagesEnum.About
        };

        return pageEnum;
    }

    public static byte[] GetImageBytes(string relativeUri, bool isFileOnDisk = false)
    {
        if (isFileOnDisk)
        {
            return File.ReadAllBytes(relativeUri);
        }
        else
        {
            Uri uri = new($"pack://application:,,,/Assets/{relativeUri}", UriKind.Absolute);

            using Stream stream = Application.GetResourceStream(uri).Stream;
            using MemoryStream memoryStream = new();

            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }

    private static bool IsPageOpen(PagesEnum pageEnum)
    {
        return pageEnum == PagesEnum.Open;
    }

    private static bool IsPageList(PagesEnum pageEnum)
    {
        bool isPageList = pageEnum switch
        {
            PagesEnum.OfficersList or PagesEnum.FacilitiesList or PagesEnum.ShipClassesList or PagesEnum.StarshipsList or PagesEnum.MissionsList => true,
            _ => false,
        };

        return isPageList;
    }
}
