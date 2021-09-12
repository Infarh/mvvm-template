#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using MVVM.Services.Interfaces;

namespace MVVM.Services
{
    public class UserDialogService : IUserDialog
    {
        /// <summary>Активное окно приложения</summary>
        protected static Window? ActiveWindow => Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        /// <summary>Окно с фокусом ввода</summary>
        protected static Window? FocusedWindow => Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        /// <summary>Текущее окно приложения</summary>
        protected static Window? CurrentWindow => FocusedWindow ?? ActiveWindow;

        /// <summary>Открыть диалога выбора файла для чтения</summary>
        /// <param name="Title">Заголовок диалогового окна</param>
        /// <param name="Filter">Фильтр файлов диалога</param>
        /// <param name="DefaultFilePath">Путь к файлу по умолчанию</param>
        /// <returns>Выбранный файл, либо null, если диалог был отменён</returns>
        public virtual FileInfo? OpenFile(string Title, string Filter = "Все файлы (*.*)|*.*", string? DefaultFilePath = null)
        {
            var dialog = new OpenFileDialog
            {
                Title = Title,
                RestoreDirectory = true,
                Filter = Filter ?? throw new ArgumentNullException(nameof(Filter)),
            };
            if (DefaultFilePath is { Length: > 0 })
                dialog.FileName = DefaultFilePath;

            return dialog.ShowDialog(CurrentWindow) == true
                ? new(dialog.FileName)
                : DefaultFilePath is null ? null : new(DefaultFilePath);
        }

        /// <summary>Открыть диалога выбора файла для записи</summary>
        /// <param name="Title">Заголовок диалогового окна</param>
        /// <param name="Filter">Фильтр файлов диалога</param>
        /// <param name="DefaultFilePath">Путь к файлу по умолчанию</param>
        /// <returns>Выбранный файл, либо null, если диалог был отменён</returns>
        public virtual FileInfo? SaveFile(string Title, string Filter = "Все файлы (*.*)|*.*", string? DefaultFilePath = null)
        {
            var dialog = new SaveFileDialog
            {
                Title = Title,
                RestoreDirectory = true,
                Filter = Filter ?? throw new ArgumentNullException(nameof(Filter)),
            };
            if (DefaultFilePath is { Length: > 0 })
                dialog.FileName = DefaultFilePath;

            return dialog.ShowDialog(CurrentWindow) == true
                ? new(dialog.FileName)
                : DefaultFilePath is null ? null : new(DefaultFilePath);
        }

        /// <summary>Диалог с текстовым вопросом и вариантами выбора Yes/No</summary>
        /// <param name="Text">Заголовок окна диалога</param>
        /// <param name="Title">Текст в окне диалога</param>
        /// <returns>Истина, если был сделан выбор Yes</returns>
        public virtual bool YesNoQuestion(string Text, string Title = "Вопрос...")
        {
            var result = CurrentWindow is null
                ? MessageBox.Show(Text, Title, MessageBoxButton.YesNo, MessageBoxImage.Question)
                : MessageBox.Show(CurrentWindow, Text, Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        /// <summary>Диалог с текстовым вопросом и вариантами выбора Ok/Cancel</summary>
        /// <param name="Text">Заголовок окна диалога</param>
        /// <param name="Title">Текст в окне диалога</param>
        /// <returns>Истина, если был сделан выбор Ok</returns>
        public virtual bool OkCancelQuestion(string Text, string Title = "Вопрос...")
        {
            var result = CurrentWindow is null
                ? MessageBox.Show(Text, Title, MessageBoxButton.OKCancel, MessageBoxImage.Question)
                : MessageBox.Show(CurrentWindow, Text, Title, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            return result == MessageBoxResult.OK;
        }

        /// <summary>Диалог с информацией</summary>
        /// <param name="Text">Заголовок окна диалога</param>
        /// <param name="Title">Текст в окне диалога</param>
        public virtual void Information(string Text, string Title = "Вопрос...")
        {
            if (CurrentWindow is null)
                MessageBox.Show(Text, Title, MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(CurrentWindow, Text, Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>Диалог с предупреждением</summary>
        /// <param name="Text">Заголовок окна диалога</param>
        /// <param name="Title">Текст в окне диалога</param>
        public virtual void Warning(string Text, string Title = "Вопрос...")
        {
            if (CurrentWindow is null)
                MessageBox.Show(Text, Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                MessageBox.Show(CurrentWindow, Text, Title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>Диалог с ошибкой</summary>
        /// <param name="Text">Заголовок окна диалога</param>
        /// <param name="Title">Текст в окне диалога</param>
        public virtual void Error(string Text, string Title = "Вопрос...")
        {
            if (CurrentWindow is null)
                MessageBox.Show(Text, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(CurrentWindow, Text, Title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
