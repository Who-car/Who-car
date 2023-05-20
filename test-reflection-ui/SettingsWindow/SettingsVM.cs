using System.Windows;
using Prism.Commands;
using Prism.Mvvm;

namespace test_reflection_ui;

public class SettingsVm : BindableBase
{
    public DelegateCommand<object> SaveMapDimensions { get; }
    public SettingsVm(MainModel model, ICodeBehind codeBehind)
    {
        SaveMapDimensions = new DelegateCommand<object>(obj =>
        {
            var values = (object[])obj;
            if (values[0].ToString()!.Length==0 || values[1].ToString()!.Length==0)
                MessageBox.Show("Пожалуйста, заполните все поля");
            else if (!int.TryParse(values[0].ToString(), out var x) ||
                     !int.TryParse(values[1].ToString(), out var y))
                MessageBox.Show("Пожалуйста, вводите только цифры");
            else
            {
                var n = int.Parse(values[0].ToString()!);
                var m = int.Parse(values[1].ToString()!);
                if (n <= 0 || m <= 0 || n > 50 || m > 50)
                    MessageBox.Show("Числа должны находиться в диапазоне от 1 до 50");
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