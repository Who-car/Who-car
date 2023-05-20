using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace test_reflection_ui;

public partial class MapWindow : Window
{
    private readonly MapVm _vm;
    private CancellationTokenSource _cts;

    public MapWindow(MainModel model)
    {
        InitializeComponent();
        var vmM = new MapVm(model, () =>
        {
            MessageBox.Show("Произошла ошибка");
            this.Close();
        });
        DataContext = vmM;
        _vm = vmM;
        _vm.LoadMap.Execute();
        this.Loaded += MapUpdate;
    }

    private async void MapUpdate(object sender, RoutedEventArgs args)
    {
        _cts = new CancellationTokenSource();
        await Task.Run(async () =>
        {
            while (true)
            {
                if (_cts.IsCancellationRequested)
                    return;
                _vm.StartTask.Execute();
                await Task.Delay(2000);
            }
        }, _cts.Token);
    }

    private void Restart(object sender, RoutedEventArgs e)
    {
        _cts.Cancel();
        _vm.Restart.Execute();
        MapUpdate(this, new RoutedEventArgs());
    }

    private void SwitchMode(object sender, RoutedEventArgs e)
    {
        _vm.SwitchMode.Execute();
    }

    private void Exit(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}