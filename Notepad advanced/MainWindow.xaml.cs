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
        private string currentFile = "";
        private string initialDir;
        List<Persoon> personen = new List<Persoon>();

        public MainWindow()
        {
            InitializeComponent();
            writePanel.Focus();

            initialDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            personen.Add(
                new Persoon() { Voornaam = "Willy", Achternaam = "Janssens", GeboorteDatum = new DateTime(1990, 1, 2) }
            );

            peopleListView.ItemsSource = personen;
        }

        private void exitItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile == "")
            {
                SaveFileDialog saveraar = new SaveFileDialog();
                saveraar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if(saveraar.ShowDialog() == true)
                {
                    currentFile = saveraar.FileName + ".txt";
                    StreamWriter outputStream = File.CreateText(currentFile);
                    outputStream.Write(writePanel.Text);
                    outputStream.Close();
                }
            }            
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openaar = new OpenFileDialog();
            string startdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openaar.InitialDirectory = startdir;
            openaar.Filter = "Doc Files|*.txt;";

            if(openaar.ShowDialog() == true)
            {
                MessageBox.Show(openaar.FileName);
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gewoon typen kut", "About", MessageBoxButton.OK ,MessageBoxImage.Warning);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            writePanel.Clear();
        }
    }
}