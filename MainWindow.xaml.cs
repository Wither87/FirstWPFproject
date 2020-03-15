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

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTextBox();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteTextBox();
            Sum();
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
            };
            txt.TextChanged += Txt_TextChanged;
            StackPanelWithTextBoxes.Children.Add(txt);
        }

        void DeleteTextBox()
        {
            int stackPanelLength = StackPanelWithTextBoxes.Children.Count-1;
            for (int i = stackPanelLength; i >= 0; i--)
            {
                if (StackPanelWithTextBoxes.Children[i] is TextBox)
                {
                    StackPanelWithTextBoxes.Children.RemoveAt(i);
                    break;
                }
            }
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sum();
        }

        void Sum()
        {
            result = 0;
            foreach (var item in StackPanelWithTextBoxes.Children)
            {
                if (item is TextBox)
                {
                    TextBox textInTextBox = (TextBox)item;
                    try
                    {
                        int num = int.Parse(textInTextBox.Text);
                        result += num;
                    }
                    catch
                    {
                        if (textInTextBox.Text == "") { }
                        else
                        {
                            MessageBox.Show("Данные введены не корректно");
                            textInTextBox.Text.Remove(textInTextBox.Text.Length - 1);
                        }
                        continue;
                    }
                }
            }
            label.Content = startText + result;
            result = 0;
        }
    }
}
