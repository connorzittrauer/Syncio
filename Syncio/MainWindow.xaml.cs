using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Forms;

namespace Syncio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWatcher();

        }

        private void InitializeWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher(@"C:\Users\Connor\Desktop\Test");
            watcher.NotifyFilter = NotifyFilters.Attributes
                     | NotifyFilters.CreationTime
                     | NotifyFilters.DirectoryName
                     | NotifyFilters.FileName
                     | NotifyFilters.LastAccess
                     | NotifyFilters.LastWrite
                     | NotifyFilters.Security
                     | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.Filter = "*.txt";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            //ListBoxLog.Items.Add($"Changed: {e.FullPath}");
            this.Dispatcher.Invoke (() => ListBoxLog.Items.Add($"Changed: {e.FullPath}"));

        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            this.Dispatcher.Invoke(() => ListBoxLog.Items.Add(value));
           
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            this.Dispatcher.Invoke(() => ListBoxLog.Items.Add($"Deleted: {e.FullPath}"));
        }
            
            

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            this.Dispatcher.Invoke(() => ListBoxLog.Items.Add($"Renamed:"));
            this.Dispatcher.Invoke(() => ListBoxLog.Items.Add($"    Old: {e.OldFullPath}"));
            this.Dispatcher.Invoke(() => ListBoxLog.Items.Add($"    New: {e.FullPath}")); 
        }

        private void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                this.Dispatcher.Invoke(() => ListBoxLog.Items.Add($"Message: {ex.Message}")); 
                this.Dispatcher.Invoke(() => ListBoxLog.Items.Add("Stacktrace:"));
                this.Dispatcher.Invoke(() => ListBoxLog.Items.Add(ex.StackTrace));
                PrintException(ex.InnerException);
            }
        }


        private void BaseDirectoryInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();

            var directory = dialog.SelectedPath;



        }
    }
}
