using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;

namespace test_reflection_ui
{
    public partial class MainWindow : Window, ICodeBehind
    {
        private MainModel Model { get; }
        public MainWindow()
        {
            InitializeComponent();
            Model = new MainModel();
        }

        public void LoadView(TypeView view)
        {
            switch (view)
            {
                case TypeView.Settings:
                    var viewSettings = new Settings();
                    var vmS = new SettingsVm(Model, this);
                    viewSettings.DataContext = vmS;
                    this.DataContext = vmS;
                    this.Content = viewSettings;
                    break;
                case TypeView.Map:
                    var viewMap = new MapWindow(Model);
                    viewMap.Show();
                    this.Close();
                    break;
            }
        }

        private void GetAssemblyPath(object sender, RoutedEventArgs args)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                if (!openFileDialog.FileName.EndsWith(".dll"))
                    MessageBox.Show(@"Пожалуйста, выберите файл с расширением "".dll"" ");
                else
                {
                    Model.AssemblyPath = openFileDialog.FileName;
                    LoadView(TypeView.Settings);
                }
            }
        }
    }
}