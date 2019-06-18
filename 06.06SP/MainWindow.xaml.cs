using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace _06._06SP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Timer timer = new Timer(IntermediateSaveFile, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(10));

            Console.ReadLine();
            timer.Dispose();
        }

        private void IntermediateSaveFile(object state)
        {
            string filePath = $@"C:\Users\{Environment.UserName}\tmp";

            using (var stream = File.Open(filePath, FileMode.Create))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text);
                stream.Write(dataBytes, 0, dataBytes.Length);
            }
        }

        private void SaveAsItemClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File(*.txt)|*.txt";
            if(saveFileDialog.ShowDialog()==true)
            {
                File.WriteAllText(saveFileDialog.FileName, new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text);
            }
        }

        private void OpenItemClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void CreateItemClick(object sender, RoutedEventArgs e)
        {
            richTextBox.SelectAll();
            richTextBox.Selection.Text = "";
        }
    }
}
