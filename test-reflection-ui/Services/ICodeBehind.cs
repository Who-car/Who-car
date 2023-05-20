using System.Windows;
using System.Windows.Threading;
using Prism.Commands;

namespace test_reflection_ui;

public interface ICodeBehind
{
    public void LoadView(TypeView view);
}