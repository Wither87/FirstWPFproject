using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            label.Content = startText + result;
        }

        TextBox txt;
        readonly string startText = "Результат сложения: ";
        int result = 0;

        private void PlusButton_Click_1(object sender, RoutedEventArgs e)
        {
            CreateTextBox();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space: CreateTextBox(); break;
                case Key.Escape: Close(); break;
                default: Sum(); break;
            }
        }

        void CreateTextBox()
        {
            txt = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 20,
                Width = 100,
                Margin = new Thickness(10, 10, 0, 0),
            };
            txt.TextChanged += Txt_TextChanged;
            WrapPanelWithTextBoxes.Children.Add(txt);
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sum();
        }

        void Sum()
        {
            result = 0;
            foreach (var item in WrapPanelWithTextBoxes.Children)
            {
                if (item is TextBox textInTextBox)
                {
                    try
                    {
                        int num = int.Parse(textInTextBox.Text);
                        result += num;
                    }
                    catch
                    {
                        if (textInTextBox.Text == "") { }
                        else MessageBox.Show("Данные введены не корректно"); break;
                    }
                }
            }
            label.Content = startText + result;
            result = 0;
        }
    }
}
