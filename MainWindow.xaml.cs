using System;
using System.Collections.Generic;
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
            CreateDockPanel();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteDockPanel();
        }

        /// <summary>
        /// Горячие клавиши для управления программой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space: CreateTextBox(); break; // Создать новый TextBox
                case Key.Escape: Close(); break;        // Закрыть приложение
                default: Calculate(); break;            // Посчитать
            }
        }

        /// <summary>
        /// Создаёт Докпанель
        /// </summary>
        void CreateDockPanel()
        {
            DockPanel dock = new DockPanel { Margin = new Thickness(5) };   // Создать докпанель с отступом в 5 единиц
            dock.Children.Add(CreateTextBox());                             // Добавление TextBox в докпанель
            dock.Children.Add(CreateComboBox());                            // Добавление комбобокса в докпанель
            StackPanelWithTextBoxes.Children.Add(dock);                     // Добавление докпанели в Стекпанель на форме
        }

        /// <summary>
        /// Считает содержимое всех Текстбоксов
        /// </summary>
        void Calculate()
        {
            foreach (var item in StackPanelWithTextBoxes.Children) 
            {
                if (item is DockPanel)
                {
                    DockPanel dock = (DockPanel)item;
                    TextBox textInTextBox = LookTextBox(dock);  // Найти TextBox
                    ComboBox comb = LookComboBox(dock);         // Найти ComboBox
                    int num;
                    try { num = int.Parse(textInTextBox.Text); }// Попробовать пропарсить содержимое TextBox
                    catch {
                        if (textInTextBox.Text == "") { }       // ничего не делать если TextBox пустой
                        else { MessageBox.Show("Данные введены не корректно"); textInTextBox.Text = ""; }   // Выдать MessageBox с ошибкой, очищает TextBox
                        continue;
                    }
                    switch (comb.SelectedItem) // Определение выделеного элемента в ComboBox
                    {
                        case "+":
                            result += num;
                            break;
                        case "-":
                            result -= num;
                            break;
                        case "*":
                            result *= num;
                            break;
                        case "/":
                            result /= num;
                            break;
                    }                     
                }
            }
            label.Content = startText + result;  // Вывод результата
            result = 0;
        }

        /// <summary>
        /// Удаляет последнюю DockPanel из Стекпанели
        /// </summary>
        void DeleteDockPanel()
        {
            int stackPanelLength = StackPanelWithTextBoxes.Children.Count - 1;  // Последний индекс на 1 меньше длинны коллекции
            for (int i = stackPanelLength; i >= 0; i--)
                if (StackPanelWithTextBoxes.Children[i] is DockPanel)
                {
                    StackPanelWithTextBoxes.Children.RemoveAt(i); // Удалить DockPanel
                    break;
                }
            Calculate();
        }

        /// <summary>
        /// Ищет ComboBox в докпанели
        /// </summary>
        /// <param name="dock"></param>
        /// <returns></returns>
        ComboBox LookComboBox(DockPanel dock)
        {
            foreach (var item in dock.Children)
                if (item is ComboBox)
                    return item as ComboBox;
            return null;
        }

        /// <summary>
        /// Ищет TextBox в докпанели
        /// </summary>
        /// <param name="dock"></param>
        /// <returns></returns>
        TextBox LookTextBox(DockPanel dock)
        {
            foreach (var item in dock.Children)
                if (item is TextBox)
                    return item as TextBox;
            return null;
        }

        /// <summary>
        /// Создаёт ComboBox
        /// </summary>
        /// <returns></returns>
        ComboBox CreateComboBox()
        {
            ComboBox comb = new ComboBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 50,
            };
            comb.Items.Add("+");
            comb.Items.Add("-");
            comb.Items.Add("*");
            comb.Items.Add("/");
            comb.SelectedItem = "+";
            comb.SelectionChanged += Comb_SelectionChanged;
            return comb;
        }

        /// <summary>
        /// Создаёт TextBox
        /// </summary>
        /// <returns></returns>
        TextBox CreateTextBox()
        {
            txt = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 20,
                Width = 100,
            };
            txt.TextChanged += Txt_Sum;
            return txt;
        }

        private void Comb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calculate();
        }

        private void Txt_Sum(object sender, TextChangedEventArgs e)
        {
            Calculate();
        }
    }
}
