﻿/*
 * Copyright (c) 2019 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Windows;
using System.Windows.Controls;
using SafeExamBrowser.Contracts.Configuration;
using SafeExamBrowser.Contracts.Core;
using SafeExamBrowser.Contracts.UserInterface.Taskbar.Events;
using SafeExamBrowser.UserInterface.Desktop.Utilities;

namespace SafeExamBrowser.UserInterface.Desktop.Controls
{
	public partial class ApplicationInstanceButton : UserControl
	{
		private IApplicationInfo info;
		private IApplicationInstance instance;

		internal event ApplicationButtonClickedEventHandler Clicked;

		public ApplicationInstanceButton(IApplicationInstance instance, IApplicationInfo info)
		{
			this.info = info;
			this.instance = instance;

			InitializeComponent();
			InitializeApplicationInstanceButton();
		}

		private void InitializeApplicationInstanceButton()
		{
			Icon.Content = IconResourceLoader.Load(info.IconResource);
			Text.Text = instance.Name;
			Button.ToolTip = instance.Name;

			instance.IconChanged += Instance_IconChanged;
			instance.NameChanged += Instance_NameChanged;
		}

		private void Instance_IconChanged(IIconResource icon)
		{
			Dispatcher.BeginInvoke(new Action(() => Icon.Content = IconResourceLoader.Load(icon)));
		}

		private void Instance_NameChanged(string name)
		{
			Dispatcher.Invoke(() =>
			{
				Text.Text = name;
				Button.ToolTip = name;
			});
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Clicked?.Invoke(instance.Id);
		}
	}
}