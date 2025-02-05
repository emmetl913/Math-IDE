using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System;
using System.Diagnostics;

namespace Math_IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string fileDialogName = "";
        public string[] readText = new string[10000];
        public string[] writeText = new string[10000];
        public int totalLineNumber = 1;
        public MainWindow()
        {
            InitializeComponent();
            updateLineNumbers();

        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\Users\elars\OneDrive\Desktop\MathIDEdocuments";
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            fileDialog.Multiselect = false;
            fileDialog.ShowDialog();
            fileDialogName = fileDialog.FileName;
            if (fileDialogName != "")
            {
                fileNameBlock.Text = "";
                fileSpaceBox.Text = "";
                fileNameBlock.Text = fileDialogName;
                readText = File.ReadAllLines(fileDialogName);
                for (int i = 0; i < readText.Length; i++)
                { 
                    fileSpaceBox.Text += readText[i] + "\n";
                }
            }
        }

        private void saveFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (fileDialogName != "")
            {
                writeText = fileSpaceBox.Text.Split('\n');
                File.WriteAllLines(fileDialogName, writeText);
            }
            else
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = @"C:\Users\elars\OneDrive\Desktop\MathIDEdocuments";
                saveFileDialog.DefaultExt = ".txt";
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.ShowDialog();
                fileDialogName = saveFileDialog.FileName;
                if (fileDialogName != "")
                {
                    writeText = fileSpaceBox.Text.Split('\n');
                    File.WriteAllLines(fileDialogName, writeText);
                }
            }

        }
        private void updateLineNumbers()
        {
            int lineCount = fileSpaceBox.LineCount;
            LineNumbersPanel.Children.Clear();

            for (int i = 1; i <= lineCount; i++)
            {
                TextBlock line = new TextBlock
                {
                    Text = i.ToString(),
                    FontFamily = new System.Windows.Media.FontFamily("Consolas"),
                    FontSize = 14,
                    Margin = new Thickness(5, 0, 10, 0),
                    Foreground = System.Windows.Media.Brushes.Gray
                };
                LineNumbersPanel.Children.Add(line);
            }

        }

        private void EditorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateLineNumbers();
        }
        private void TextScroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            LineNumbersScroll.ScrollToVerticalOffset(e.VerticalOffset);
        }

        private void executeCode_Click(object sender, RoutedEventArgs e)
        {
            var tokenizer = new Tokenize();
            Debug.WriteLine("New Tokenizer successfully created! \n");
            var interpreter = new Interpreter();
            Debug.WriteLine("New Interpreter successfully created! \n");

            tokenizedCode.Text = "";
            outputConsole.Text = "";

            var code = tokenizer.TokenizeText(fileSpaceBox.Text);
            foreach (var token in code)
            {
                tokenizedCode.Text += token;
            }
            interpreter.parseCode(code);
            foreach (string item in interpreter.output_log)
            {
                outputConsole.Text += item;
            }
       
        }
    }
}