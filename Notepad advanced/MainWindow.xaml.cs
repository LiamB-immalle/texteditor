using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Notepad_advanced
{
    public partial class MainWindow : Window
    {
        private string currentTxtFile = "";
        private string currentCsvFile = "";
        private string currentFile = "";
        private string initialDir;
        List<Persoon> personen = new List<Persoon>();

        public MainWindow()
        {
            InitializeComponent();
            writePanel.Focus();

            initialDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            peopleGrid.ItemsSource = personen;
        }

        private void exitItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Savetxt_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile == "")
            {
                SaveFileDialog saveraar = new SaveFileDialog();
                saveraar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (saveraar.ShowDialog() == true)
                {
                    currentFile = saveraar.FileName + ".txt";
                    StreamWriter outputStream = File.CreateText(currentFile);
                    outputStream.Write(writePanel.Text);
                    outputStream.Close();
                }
            }
        }

        private void Savecsv_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile == "")
            {
                SaveFileDialog saveraar = new SaveFileDialog();
                saveraar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (saveraar.ShowDialog() == true)
                {
                    currentFile = saveraar.FileName + ".csv";
                    StreamWriter outputStream = File.CreateText(currentFile);
                    outputStream.Write(csvPanel.Text);
                    outputStream.Close();
                }
            }
        }

        private void Opentxt_Click(object sender, RoutedEventArgs e)
        {
            StreamReader inputStream; 
            OpenFileDialog openaar = new OpenFileDialog();
            string startdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openaar.InitialDirectory = startdir;
            openaar.Filter = "Doc Files|*.txt;";

            if (openaar.ShowDialog() == true)
            {
                currentTxtFile = openaar.FileName;
                inputStream =  File.OpenText(currentTxtFile);
                writePanel.Text = inputStream.ReadToEnd();
                inputStream.Close();
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gewoon typen kut", "About", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            writePanel.Clear();
        }

        private void Opencsv_Click(object sender, RoutedEventArgs e)
        {
            StreamReader inputStream;
            OpenFileDialog openaar = new OpenFileDialog();
            string startdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openaar.InitialDirectory = startdir;
            openaar.Filter = "Csv Files|*.csv;";

            if (openaar.ShowDialog() == true)
            {
                currentCsvFile = openaar.FileName;
                inputStream = File.OpenText(currentCsvFile);
                csvPanel.Text = inputStream.ReadToEnd();
                inputStream.Close();
            }
        }

        private void Parse_Click(object sender, RoutedEventArgs e)
        {
            string[] lines = csvPanel.Text.Split('\n');

            List<Persoon> parse = new List<Persoon>();
        
            foreach(var row in lines)
            {
                string[] fields = row.Split(';');
                var p = new Persoon();
                p.Voornaam = fields[0];
                p.Achternaam = fields[1];
                p.GeboorteDatum = DateTime.Parse(fields[2]);
                parse.Add(p);
            }
            peopleGrid.ItemsSource = parse;
        }
    }
}