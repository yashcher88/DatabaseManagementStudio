using Avalonia.Controls;
using DatabaseManagementStudio.Classes;

namespace DatabaseManagementStudio.Views
{
    public class BaseForm : Window
    {
        public Store? store;
        public BaseForm()
        {
            Opened += (_, _) =>
            {
                ApplyLanguage();
                ApplyStyles();
            };
        }
        protected virtual void Init(BaseForm F) {
            store = F.store;
        }

        protected virtual void ApplyLanguage()
        {
            // Логика применения локализации, например:
            // this.FindControl<TextBlock>("TitleText").Text = Localization.Get("MainWindow_Title");
        }

        protected virtual void ApplyStyles()
        {
            // Логика применения стилей, например смена темы
            // this.Styles.Add(new FluentTheme { Mode = FluentThemeMode.Dark });
        }
    }
}