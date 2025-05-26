using Avalonia.Controls;
using Avalonia.Threading;
using System;

namespace DatabaseManagementStudio.Views
{
    public partial class Loading : Window
    {
        private DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
        public Loading()
        {
            InitializeComponent();
            SetupTimer();
        }
        private void SetupTimer()
        {
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            _timer.Stop(); // Остановить таймер

            // Создать и показать новое окно
            var newWindow = new Main();
            newWindow.Init(this);
            newWindow.Show();

            // Спрятать текущее окно
            this.Hide();
        }
    }
}