using Avalonia.Controls;
using Avalonia.Threading;
using DatabaseManagementStudio.Classes.Pack;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DatabaseManagementStudio.Views
{
    public partial class Loading : Window
    {
        public Pack pack;
        public Main mainWindow;
        public Loading()
        {
            InitializeComponent();
        }
        public void AfterShow(object? sender, EventArgs e) {
            Init();
        }
        public async void Init() {
            pack = new Pack();
            StateText.Text = $"Loading";
            await Task.Delay(1000);
            pack.appSets.Init();
            StateText.Text = $"Apply settings";
            await Task.Delay(1000);
            pack.iFace.Init();
            StateText.Text = $"Apply interface settings";
            await Task.Delay(1000);
            pack.driverList.Init();
            StateText.Text = $"Load drivers";
            await Task.Delay(1000);

            mainWindow = new Main();
            mainWindow.Init(this);
            mainWindow.Show();

            Hide();
        }
    }
}