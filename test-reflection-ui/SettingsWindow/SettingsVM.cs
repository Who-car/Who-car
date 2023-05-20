using System.Windows;
using Prism.Commands;
using Prism.Mvvm;

namespace test_reflection_ui;

public class SettingsVm : BindableBase
{
    public DelegateCommand<object> SaveMapDimensions { get; }

    public delegate void ErrorHandler(ErrorArgs args);
    public ErrorHandler OnError { get; set; }
    public SettingsVm(MainModel model, ICodeBehind codeBehind)
    {
        SaveMapDimensions = new DelegateCommand<object>(obj =>
        {
            var values = (object[])obj;
            if (values[0].ToString()!.Length==0 || values[1].ToString()!.Length==0)
                OnError.Invoke(new ErrorArgs(ErrorType.EmptyField));
            else if (!int.TryParse(values[0].ToString(), out var x) ||
                     !int.TryParse(values[1].ToString(), out var y))
                OnError.Invoke(new ErrorArgs(ErrorType.NaN));
            else
            {
                var n = int.Parse(values[0].ToString()!);
                var m = int.Parse(values[1].ToString()!);
                if (n <= 0 || m <= 0 || n > 30 || m > 30)
                    OnError.Invoke(new ErrorArgs(ErrorType.OutOfRange));
                else
                {
                    model.FirstDimension = n;
                    model.SecondDimension = m;
                    codeBehind.LoadView(TypeView.Map);
                }
            }
        });
    }
}