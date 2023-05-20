using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using Prism.Mvvm;
using Treaty;

namespace test_reflection_ui;

public class MainModel : BindableBase
{
    private Assembly? _assembly;
    private Type? _instanceType;
    private IMapManager? _instance;
    public string? AssemblyPath { get; set; }
    public int FirstDimension { get; set; }
    public int SecondDimension { get; set; }
    public int[,] Map => _instance is null ? new int[FirstDimension, SecondDimension] : _instance.GetMap();
    public delegate void ErrorHandler();
    public ErrorHandler? OnError;
    
    private void BeforeLoading()
    {
        _assembly = Assembly.LoadFrom(AssemblyPath!);
        _instanceType = _assembly.GetTypes().FirstOrDefault(x => x.IsAssignableTo(typeof(IMapManager)))!;
        if (_instanceType is null)
        {
            MessageBox.Show("Кажется, ваша сборка не содержит решения задачи. Вероятнее всего, вы загрузили " +
                            "не ту сборку, либо не реализовали высланный мною контракт :D");
            OnError!.Invoke();
        }
        else
        {
            _instance = (IMapManager?)_assembly.CreateInstance(_instanceType.ToString(), false, 
                BindingFlags.ExactBinding, null, new object[] { FirstDimension, SecondDimension },
                null, null);
        }
    }

    public void LoadMap()
    {
        BeforeLoading();
        RaisePropertyChanged(nameof(Map));
    }

    public void UpdateMap()
    {
        _instance!.UpdateMap();
        RaisePropertyChanged(nameof(Map));
    }

    public void Restart()
    {
        _instance!.Clear();
        RaisePropertyChanged(nameof(Map));
    }
}