#nullable enable
using System.Windows;
using MVVM.Commands.Base;

namespace MVVM.Commands
{
    public class CloseWindow : Command
    {
        public override bool CanExecute(object? parameter) => parameter is Window;
        public override void Execute(object? parameter) => (parameter as Window)?.Close();
    }
}
