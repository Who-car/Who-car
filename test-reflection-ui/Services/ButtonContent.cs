using System;
using System.Windows.Media.Imaging;

namespace test_reflection_ui;

public class ButtonContent
{
    private bool _isDark;
    public BitmapImage BackgroundImage { get; set; }
    public string ButtonText { get; set; }

    public ButtonContent()
    {
        _isDark = true;
        BackgroundImage = new BitmapImage(new Uri(@"D:\homework\test-reflection\test-reflection-ui\test-reflection-ui\images\forrest_dark.jpg", UriKind.Absolute));
        ButtonText = "Да будет свет";
    }

    public void SwitchMode()
    {
        
    }
}