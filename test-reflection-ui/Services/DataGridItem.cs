using System;
using System.Windows.Media.Imaging;
using Prism.Mvvm;

namespace test_reflection_ui;

public class DataGridItem : BindableBase
{
    private int _intValue;

    public int IntValue
    {
        get => _intValue;
        set
        {
            if (value != _intValue)
            {
                _intValue = value;
                switch (value)
                {
                    case 2:
                        ImageValue = new BitmapImage(new Uri(@"D:\homework\test-reflection\test-reflection-ui\images\wolf.png", UriKind.Absolute));
                        break;
                    case 1:
                        ImageValue = new BitmapImage(new Uri(@"D:\homework\test-reflection\test-reflection-ui\images\rabbit.png", UriKind.Absolute));
                        break;
                    default:
                        ImageValue = null;
                        break;
                }
                RaisePropertyChanged(nameof(ImageValue));
            }
        }
    }
    public BitmapImage? ImageValue { get; private set; }

    public DataGridItem()
    {
        ImageValue = null;
        _intValue = 0;
    }
}