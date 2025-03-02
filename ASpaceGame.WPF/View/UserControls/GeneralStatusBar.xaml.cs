/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas - https://github.com/KingMordas/ASpaceGame
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

using ASpaceGame.WPF.Helpers;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ASpaceGame.WPF.View.UserControls;

public partial class GeneralStatusBar : UserControl
{
    private readonly DispatcherTimer _timer;

    public GeneralStatusBar()
    {
        DataContext = this;
        InitializeComponent();

        TbVersion.Text = ApplicationHelper.ApplicationSettings.CurrentVersion;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += (sender, args) => SetClock();
        _timer.Start();
    }

    private void SetClock()
    {
        TbClock.Text = DateTime.Now.ToString("F");
    }
}
