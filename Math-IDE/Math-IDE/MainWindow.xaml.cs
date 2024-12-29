using System.Text;
using System.Windows;
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

namespace Math_IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string fileDialogName = "";
        public string[] readText = new string[1000];
        public string[] writeText = new string[1000];
        public int totalLineNumber = 1;
        public MainWindow()
        {
            InitializeComponent();
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

        private void fileSpaceBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var caretIndex = fileSpaceBox.CaretIndex;
            if (e.Key == Key.Enter)
            {
                fileSpaceBox.Text = fileSpaceBox.Text.Insert(caretIndex, "\n");
                totalLineNumber++;
                lineNumber.Text = lineNumber.Text + "\n" + totalLineNumber;
                fileSpaceBox.CaretIndex = caretIndex + 1;
                Console.WriteLine(fileSpaceBox.Text[fileSpaceBox.CaretIndex - 1]);
            }

            if (caretIndex > 0 && caretIndex < fileSpaceBox.Text.Length)
            {
                
                if (e.Key == Key.Back && fileSpaceBox.Text[caretIndex - 1] == '\n')
                {
                    totalLineNumber--;
                    lineNumber.Text = "";
                    for (int i = 1; i <= totalLineNumber; i++)
                    {
                        lineNumber.Text = lineNumber.Text + i + "\n";
                    }
                }
            }
        }

        private void executeCode_Click(object sender, RoutedEventArgs e)
        {
            var tokenizer = new Tokenize();
            //var interpreter = new Interpreter();
            var code = tokenizer.TokenizeText(fileSpaceBox.Text);
            tokenizedCode.Text = code;

        }
    }
}