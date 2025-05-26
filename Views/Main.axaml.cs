using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using DatabaseManagementStudio.Views;
using System;
using System.ComponentModel;

namespace DatabaseManagementStudio;

public partial class Main : Window
{
    public Loading L;
    public Main()
    {
        InitializeComponent();
    }

    public void Init(Loading W) {
        Closing += Window_Closed;
        L = W;
    }
    public void Window_Closed(object? sender, CancelEventArgs e) {
        L.Close();
    }

    public void Config(object? sender, RoutedEventArgs e) { 
        Configure conf = new Configure();
        conf.Show();
    }
}