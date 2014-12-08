using System;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

namespace AnimationMaker.Services
{
	public sealed class UserDialogService : IUserDialogService
	{
		private const string DefaultExtension = ".nam";
		private const string DialogFilter = "Nekki animation (.nam)|*.nam";
		private const string DefaultAnimationName = "newAnimation" + DefaultExtension;

		private string _lastSaveFileName = string.Empty;

		public OpenDialogResult GetSavePath()
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = DefaultExtension,
				Filter = DialogFilter,
				FileName = string.IsNullOrEmpty(_lastSaveFileName) ? DefaultAnimationName : _lastSaveFileName,
				CheckFileExists = false,
				OverwritePrompt = false,
				RestoreDirectory = true
			};

			var dialogResult = dialog.ShowDialog();

			_lastSaveFileName = dialog.FileName;
			return new OpenDialogResult(dialogResult.Value, dialog.FileName);
		}

		public OpenDialogResult GetLoadPath()
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = DefaultExtension,
				Filter = DialogFilter,
				FileName = _lastSaveFileName,
				RestoreDirectory = true
			};

			var dialogResult = dialog.ShowDialog();
			_lastSaveFileName = dialog.FileName;
			return new OpenDialogResult(dialogResult.Value, dialog.FileName);
		}

		public Task Alert(string text)
		{
			if (String.IsNullOrWhiteSpace(text)) throw new ArgumentException("Should not be empty", "text");

			var metroWindow = (MetroWindow)Application.Current.MainWindow;
			metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;

			return metroWindow.ShowMessageAsync("Error", text, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
		}
	}
}