using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Linq;
using System;
using Thread = HomeTask_UVS.Models.Thread;
using System.Data.SqlClient;

namespace HomeTask_UVS
{

    public partial class MainWindow : Window
    {
        private List<Task> _tasks;
        private CancellationTokenSource _cancellationTokenSource;
        private ObservableCollection<Thread> _generatedThread;
        private string _connectionString = "Server=.;Database=UVSHomeTaskDatabase;Integrated Security=SSPI;";

        public MainWindow()
        {
            InitializeComponent();
            _tasks = new List<Task>();
            _generatedThread = new ObservableCollection<Thread>();
            GeneratedDataListView.ItemsSource = _generatedThread;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            bool value = int.TryParse(valueComboBox.Text, out int threadCount);
            StartThreads(threadCount);
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void StartThreads(int threadCount)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            for (int i = 1; i <= threadCount; i++)
            {
                int threadId = i;
                var task = Task.Run(() => GenerateData(threadId, token), token);
                _tasks.Add(task);
            }
        }

        private async Task GenerateData(int threadId, CancellationToken token)
        {
            Random random = new Random();

            while (!token.IsCancellationRequested)
            {
                string generatedString = GenerateRandomString(random, 5, 10);
                string timestamp = DateTime.Now.ToString("HH:mm:ss");

                Dispatcher.Invoke(() =>
                {
                    if (_generatedThread.Count >= 20)
                    {
                        _generatedThread.RemoveAt(0);
                    }
                    _generatedThread.Add(new Thread { ThreadID = threadId, GeneratedString = generatedString });

                    InsertDataToDatabase(threadId, generatedString, timestamp);
                });

                int delay = random.Next(500, 2001);
                await Task.Delay(delay, token);
            }
        }

        private string GenerateRandomString(Random random, int minLength, int maxLength)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = random.Next(minLength, maxLength + 1);
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void InsertDataToDatabase(int threadId, string generatedString, string timestamp)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO ThreadData1 (ThreadID, Time, Data) VALUES (@ThreadID, @Time, @Data)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ThreadID", threadId);
                    command.Parameters.AddWithValue("@Time", timestamp);
                    command.Parameters.AddWithValue("@Data", generatedString);
                    command.ExecuteNonQuery();
                }
            }
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource.Cancel();
            Task.WhenAll(_tasks).ContinueWith(t =>
            {
                Dispatcher.Invoke(() =>
                {
                    btnStart.IsEnabled = true;
                    btnStop.IsEnabled = false;
                });
            });
        }
    }
}
