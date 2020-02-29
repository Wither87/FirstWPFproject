﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        TextBox txt; 
        const byte distens = 25;
        int top = 60;
        int result;
        string startText = "Результат = ";

        private void plusButton_Click_1(object sender, RoutedEventArgs e)
        {
            CreateTextBox();
        }

        private void returnSumButton_Click(object sender, RoutedEventArgs e)
        {
            Sum();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:  Sum(); break;
                case Key.Space:  CreateTextBox(); break;
                case Key.Escape: Close(); break;
            }
        }


        void CreateTextBox()
        {
            txt = new TextBox();
            txt.HorizontalAlignment = HorizontalAlignment.Left;
            txt.VerticalAlignment = VerticalAlignment.Top;
            txt.Height = 20;
            txt.Width = 100;
            txt.Margin = new Thickness(10, top, 0, 0);
            top += distens;
            gMain.Children.Add(txt);
        }

        void Sum()
        {
            foreach (var item in gMain.Children)
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
                        else MessageBox.Show("Данные введены не корректно"); break;
                    }
                }
            }
            label.Content = startText;
            label.Content = label.Content + result.ToString();
            result = 0;
        }
    }
}