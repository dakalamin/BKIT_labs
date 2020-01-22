using System;
using System.Text;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using Microsoft.Win32;
using Lab5;

namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ItemOfList> searchResult    = new List<ItemOfList>();
        private List<ItemOfList> searchResultLev = new List<ItemOfList>();
        private List<string> words = new List<string>();

        private Stopwatch time;
        private void TimeStopReport(Label label, string prefix)
        {
            time.Stop();
            label.Content = $"{prefix} time: {time.Elapsed.TotalMilliseconds} ms";
        }

        private string[] data;
        private char[] delims = "\n\r\t .,!?(){}[]<>\"\':;*".ToCharArray();
        private bool distFlag = true;
        private bool readSuccess;

        public MainWindow()
        {
            time = new Stopwatch();

            InitializeComponent();
            resultListBox.ItemsSource  = searchResult;
            resultListBox5.ItemsSource = searchResultLev;
        }

        private void onReadButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                FileName   = "file.txt",
                DefaultExt = ".txt",
                Filter     = "Text documents (*.txt)|*.txt"
            };

            readSuccess = false;
            if (openFileDialog.ShowDialog() == true)
            {
                time.Restart();

                data = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8).Split(delims);
                words.Clear();
                foreach (string s in data)
                {
                    string sl = s.ToLower();
                    if ((s.Trim() != "") && !words.Contains(sl)) words.Add(sl);
                }

                TimeStopReport(readTimeLabel, "Read");

                if (words.Count > 0) readSuccess = true;
                else
                {
                    MessageBox.Show("FAILED - File is empty");
                }
            }
        }

        private void onSearchButton(object sender, RoutedEventArgs e)
        {
            if (!FilterPassed(out string w)) return;

            searchResult.Clear();
            time.Restart();
            foreach (string s in words)
            {
                if (s.Contains(w))
                {
                    searchResult.Add(new ItemOfList() { Word = s });
                }
            }

            TimeStopReport(searchTimeLabel4, "Search");
            resultListBox.Items.Refresh();
        }

        private void onSearchButtonLevenshtain(object sender, RoutedEventArgs e)
        {
            int max;

            if (!FilterPassed(out string w)) return;
            else if (!int.TryParse(levMaxValue.Text, out max))
            {
                MessageBox.Show("FAILED - MaxValue is incorrect");
                return;
            }

            searchResultLev.Clear();
            time.Restart();

            Func<string, string, int> distance;
            if (distFlag) distance = LevDistance.Distance;
            else          distance = LevDistance.DistanceDameray;

            foreach (string s in words)
            {
                if (distance(s, w) <= max)
                    searchResultLev.Add(new ItemOfList() { Word = s });
            }

            TimeStopReport(searchTimeLabel5, "Search");
            resultListBox5.Items.Refresh();
        }

        private bool FilterPassed(out string w)
        {
            w = searchWord.Text.Trim().ToLower();
            if (!readSuccess)
            {
                MessageBox.Show("FAILED - Read file first");
            }
            else if (w == "")
            {
                MessageBox.Show("FAILED - No word found to search for");
            }
            else return true;

            return false;

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            distFlag = !radioButton.Equals(DamerawDistance);
        }
    }

    public class ItemOfList
    {
        public string Word { get; set; }
    }
}
