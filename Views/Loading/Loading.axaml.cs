using Avalonia.Controls;
using Avalonia.Logging;
using DatabaseManagementStudio.Classes;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DatabaseManagementStudio.Views
{
    public partial class Loading : BaseForm
    {
        public Main? mainWindow;
        public Loading()
        {
            InitializeComponent();
            store = new Store();
        }
        public void AfterShow(object? sender, EventArgs e) {
            Init();
        }
        public async void Init() {
            if (store == null) {
                store = new Store();
            }
            BuildBlock.Text = $"Build version: " + (Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString() ?? "1.0.0.0");
            StateText.Text = $"Loading drivers";
            store.LoadPack();
            PackageBlock.Text = $"Package version: " + store.versionPack.ToString();
            await Task.Delay(200);

            StateText.Text = $"Load default settings";
            store.LoadSets();
            await Task.Delay(200);

            StateText.Text = $"Load servers";
            store.LoadServers();
            await Task.Delay(200);

            StateText.Text = $"Load user settings";
            store.LoadUserSets();
            await Task.Delay(200);

            StateText.Text = $"Load user styles";
            store.LoadUserStyles();
            await Task.Delay(200);

            mainWindow = new Main();
            mainWindow.Init(this);
            mainWindow.Show();
            Hide();
        }
    }
}