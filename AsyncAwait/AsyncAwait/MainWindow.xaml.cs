using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;

namespace AsyncAwait
{
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        List<string> list = new List<string>();
        string name;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private async void OpenAudio_Click(object sender, RoutedEventArgs e)
        {
            await Open();
        }

        private void StopAudio_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void PlayAudio_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
                    if (name != null) 
            {
                text1.Text= name;
            }
        }
         async Task Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mediaPlayer.Open(new Uri(openFileDialog.FileName));
                name= openFileDialog.SafeFileName;
            }

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                testProgressBar.Minimum = 0;
                testProgressBar.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                testProgressBar.Value = mediaPlayer.Position.TotalSeconds;
            }
        }
        private async void AddB_Click(object sender, RoutedEventArgs e)
        {
            await Add();
        }
        private async void DeleteB_Click(object sender, RoutedEventArgs e)
        {
           await Delete();
        }
        async Task Add()
        {
            text2.Content = "";
            list.Add(text1.Text);
            foreach (var item in list)
            {
                text2.Content += $"{item}\n";
            }
        }
        async Task Delete()
        {
            text2.Content = "";
            list.Remove(text1.Text.ToString());
            foreach (var item in list)
            {
                text2.Content += $"{item}\n";
            }
        }
    }
}
