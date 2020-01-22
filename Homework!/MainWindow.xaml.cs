using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Lab5;

namespace Homework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class ItemOfList
    {
        public string Word { get; set; }
        public override string ToString()
        {
            return Word;
        }
    }

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

            TimeStopReport(searchTimeLabel, "Search"); //CHANGE 4
            resultListBox.Items.Refresh();
        }

        private void onSearchButtonLevenshtain(object sender, RoutedEventArgs e)
        {
            int max, threads;

            if (!FilterPassed(out string w)) return;
            else if (!int.TryParse(levMaxValue.Text, out max))
            {
                MessageBox.Show("FAILED - MaxValue is incorrect");
                return;
            }
            else if (!int.TryParse(threadCount.Text, out threads))
            {
                MessageBox.Show("FAILED - Threads quantity is incorrect");
                return;
            }

            if (max < 0) max = 0;
            if (threads < 0) threads = 0;

            time.Restart();

            List<ParallelSearchResult> result = new List<ParallelSearchResult>();
            List<MinMax> arrayDivList = SubArrays.DivideSubArrays(0, words.Count, threads);

            int count = arrayDivList.Count;
            Task<List<ParallelSearchResult>>[] tasks = new Task<List<ParallelSearchResult>>[count];

            for (int i = 0; i < count; i++)
            {
                List<string> tmpTaskList = words.GetRange(arrayDivList[i].Min, arrayDivList[i].Max - arrayDivList[i].Min);
                tasks[i] = new Task<List<ParallelSearchResult>>(ArrayThreadTask, new ParallelSearchThreadParams()
                {
                    TmpList     = tmpTaskList,
                    LevMaxValue = max,
                    ThreadQuant = i,
                    SearchWord  = w
                });
                tasks[i].Start();
            }

            Task.WaitAll(tasks);
            TimeStopReport(searchTimeLabel5, "Search");

            for (int i = 0; i < count; i++)
                result.AddRange(tasks[i].Result);

            searchResultLev.Clear();
            foreach (var i in result)
            {
                searchResultLev.Add(new ItemOfList { Word = i.Word });
            }
            
            
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

        static List<ParallelSearchResult> ArrayThreadTask(object paramObj)
        {
            ParallelSearchThreadParams param = (ParallelSearchThreadParams)paramObj;
            List<ParallelSearchResult> result = new List<ParallelSearchResult>();

            foreach (string str in param.TmpList)
            {
                int distance = LevDistance.Distance(str, param.SearchWord.Trim());
                if (distance <= param.LevMaxValue)
                {
                    ParallelSearchResult tmp = new ParallelSearchResult()
                    {
                        Word        = str,
                        Dist        = distance,
                        ThreadQuantity = param.ThreadQuant
                    };
                    result.Add(tmp);
                }
            }
            return result;
        }

        private void onSaveButtonClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName   = "Report_" + DateTime.Now.ToString("dd_MM_yyyy_hhmmss"),
                DefaultExt = ".html",
                Filter     = "HTML reports (.html)|*.html"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string reportFileName = saveFileDialog.FileName;

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("<html>");
                stringBuilder.AppendLine("\t<head>");
                stringBuilder.AppendLine("\t\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>");
                stringBuilder.AppendLine("\t\t<title>" + "Report: " + reportFileName + "</title>");
                stringBuilder.AppendLine("\t</head>");
                stringBuilder.AppendLine("\t<body>");
                stringBuilder.AppendLine("\t\t<h1>" + "Report: " + reportFileName + "</h1>");
                stringBuilder.AppendLine("\t\t<table border='1'>");

                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Read time(from file)</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + readTimeLabel.Content.ToString() + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Search word</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + searchWord.Text + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Count of unique words in file</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + words.Count.ToString() + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");


                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Max value for levDistance search</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + levMaxValue.Text + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Time(Levenshtain search)</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + searchTimeLabel5.Content.ToString() + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Time(exact search)</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + searchTimeLabel.Content.ToString() + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr valign='top'>");
                stringBuilder.AppendLine("\t\t\t\t<td>Search result(exact search)</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>");
                stringBuilder.AppendLine("\t\t\t\t\t<ul>");

                foreach (var i in resultListBox.Items)
                {
                    stringBuilder.AppendLine("\t\t\t\t\t\t<li>" + i.ToString() + "</li>");
                }

                stringBuilder.AppendLine("\t\t\t\t\t</ul>");
                stringBuilder.AppendLine("\t\t\t\t</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr valign='top'>");
                stringBuilder.AppendLine("\t\t\t\t<td>Search result(Levenshtain)</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>");
                stringBuilder.AppendLine("\t\t\t\t\t<ul>");

                foreach (var i in resultListBox5.Items)
                {
                    stringBuilder.AppendLine("\t\t\t\t\t\t<li>" + i.ToString() + "</li>");
                }

                stringBuilder.AppendLine("\t\t\t\t\t</ul>");
                stringBuilder.AppendLine("\t\t\t\t</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t</table>");
                stringBuilder.AppendLine("\t</body>");
                stringBuilder.AppendLine("</html>");

                File.AppendAllText(reportFileName, stringBuilder.ToString());
                MessageBox.Show("Report written to " + reportFileName);
            }
        }
    }
}
