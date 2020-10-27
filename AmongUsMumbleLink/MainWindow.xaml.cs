using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using MahApps.Metro.Controls;
using System.Windows.Input;
using NHotkey.Wpf;
using System.IO;

namespace AmongUsMumbleLink
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static KeyGesture HotKeyGesture = new KeyGesture(Key.M , ModifierKeys.Control);
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += DoWork;
            backgroundWorker.RunWorkerCompleted += WorkerCompleted;
            backgroundWorker.ProgressChanged += WorkerUpdateWindow;
            UpdateWindow();
        }

        private void WorkerUpdateWindow(object sender, ProgressChangedEventArgs e)
        {
            UpdateWindow();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
            if (Program.Init())
            {
                while (true)
                {
                    //UpdateWindow();
                    worker.ReportProgress(0);
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;

                        break;
                    }
                    else
                    {
                        Program.Update();
                        System.Threading.Thread.Sleep(20);
                        //UpdateWindow();
                    }
                }
            }
            
        }
        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                WriteLine("Among Us Capture Successfully Cancelled!");
                //StopRunning();
            }
            else if (e.Error != null)
            {
                WriteLine("Among Us Capture Crashed! Make sure you start the capture AFTER you have joined a lobby at least once.");
                //StopRunning();
            }
            else
            {
                WriteLine("Initialisation failed, is Among Us running?");
                //StopRunning();
            }
        }
        private void UpdateWindow()
        {
            if (backgroundWorker.IsBusy)
            {
                CaptureText.Text = "Capture RUNNING";
                CaptureText.Foreground = new SolidColorBrush(Colors.Lime);
                GameState.Text = "LOBBY";
                GameState.Foreground = new SolidColorBrush(Colors.Blue);
                PlayerID.Text = Program.playerID.ToString();
                PlayerID.Foreground = new SolidColorBrush(Colors.Red);
                if (Program.GameStart)
                {
                    GameState.Text = "RUNNING";
                    GameState.Foreground = new SolidColorBrush(Colors.Lime);
                    PlayerID.Foreground = new SolidColorBrush(Colors.Black);
                    PlayerX.Text = Math.Round(Program.playerX, 2).ToString();
                    PlayerY.Text = Math.Round(Program.playerY, 2).ToString();
                }
            }
            else
            {
                CaptureText.Text = "Capture NOT RUNNING";
                CaptureText.Foreground = new SolidColorBrush(Colors.Red);
                GameState.Text = "UNKNOWN";
                GameState.Foreground = new SolidColorBrush(Colors.Red);
                PlayerID.Text = "0";
                PlayerID.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (Program.Meeting)
            {
                MeetingStatus.Text = "ON";
                MeetingStatus.Foreground = new SolidColorBrush(Colors.Lime);
                PlayerX.Foreground = new SolidColorBrush(Colors.Blue);
                PlayerY.Foreground = new SolidColorBrush(Colors.Blue);
            }
            else
            {
                MeetingStatus.Text = "OFF";
                MeetingStatus.Foreground = new SolidColorBrush(Colors.Red);
                PlayerX.Foreground = new SolidColorBrush(Colors.Black);
                PlayerY.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (Program.CheckingShip)
            {
                ShipReadNotif.Visibility = Visibility.Visible;
            }
            else
            {
                ShipReadNotif.Visibility = Visibility.Hidden;
            }

            ConsoleBox.ScrollToEnd();
        }
        private void ToggleCapture_Click(object sender, RoutedEventArgs e)
        {
            if (backgroundWorker.IsBusy != true)
            {
                backgroundWorker.RunWorkerAsync();
                //StartRunning();
                WriteLine("Started Among Us Data Capture");
            }
            else if (backgroundWorker.WorkerSupportsCancellation == true)
            {
                backgroundWorker.CancelAsync();
                //StopRunning();
                WriteLine("Stopping Data Capture");
            }
        }

        private void HotkeyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HotKeyGesture = new KeyGesture(HKeyBox.HotKey.Key, HKeyBox.HotKey.ModifierKeys);
                HotkeyManager.Current.AddOrReplace("ASDA", HotKeyGesture, MeetToggle);
            }
            catch
            {
                MessageBox.Show("Hotkey not valid, please select a different key combination.");
            }
            

        }

        private void WriteLine(string s)
        {
            s = "[" + DateTime.Now + "]: " + s;
            Paragraph p = new Paragraph();
            p.Inlines.Add(s);
            p.LineHeight = 1;
            UpdateWindow();
            ConsoleBox.Document.Blocks.Add(p);
        }
        private void ToggleMeetingAction(object sender, RoutedEventArgs e)
        {
            if (Program.Meeting)
            {
                Program.Meeting = false;
                WriteLine("Meeting deactivated.");
                
            }
            else
            {
                Program.Meeting = true;
                WriteLine("Meeting active: Fixing player position to meeting table.");
                
            }
        }
        private void MeetToggle(object sender, NHotkey.HotkeyEventArgs e)
        {
            if (ToggleMeeting.IsOn)
            {
                ToggleMeeting.IsOn = false;
                e.Handled = true;
            }
            else
            {
                ToggleMeeting.IsOn = true;
                e.Handled = true;
            }
        }

    }
}
