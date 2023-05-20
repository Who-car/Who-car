using System;
using System.Windows.Media.Imaging;
using Prism.Commands;
using Prism.Mvvm;

namespace test_reflection_ui;

public class MapVm : BindableBase
{
    private readonly MainModel _model;
    private DataGridItem[,] _map;
    private bool _isDark;

    public DataGridItem[,] Map
    {
        get => GetMap();
        set => _map = value;
    }
    
    public BitmapImage BackgroundImage => _isDark 
        ? new BitmapImage(new Uri(@"D:\homework\test-reflection\test-reflection-ui\images\forrest_dark.jpg", UriKind.Absolute)) 
        : new BitmapImage(new Uri(@"D:\homework\test-reflection\test-reflection-ui\images\forrest_light.jpg", UriKind.Absolute)); 
    public string ButtonText => _isDark 
        ? "Да будет свет" 
        : "Пусть спустится тьма";
    public DelegateCommand LoadMap { get; }
    public DelegateCommand StartTask { get; }
    public DelegateCommand Restart { get; }
    public DelegateCommand SwitchMode { get; }
    public delegate void ErrorHandler(ErrorArgs args);
    public MapVm(MainModel model, ErrorHandler handler)
    {
        _model = model;
        _map = new DataGridItem[model.FirstDimension, model.SecondDimension];
        _model.PropertyChanged += (sender, args) =>
        {
            RaisePropertyChanged(args.PropertyName);
        };
        _model.OnError = handler.Invoke;
        _isDark = true;
        LoadMap = new DelegateCommand(model.LoadMap);
        StartTask = new DelegateCommand(model.UpdateMap);
        Restart = new DelegateCommand(model.Restart);
        SwitchMode = new DelegateCommand(() =>
        {
            _isDark = !_isDark;
            RaisePropertyChanged(nameof(BackgroundImage));
            RaisePropertyChanged(nameof(ButtonText));
        });
    }

    private DataGridItem[,] GetMap()
    {
        for (var i = 0; i < _model.FirstDimension; i++)
        {
            for (var j = 0; j < _model.SecondDimension; j++)
            {
                _map[i, j] ??= new DataGridItem();
                _map[i, j].IntValue = _model.Map[i, j];
            }
        }
        return _map;
    }
}
