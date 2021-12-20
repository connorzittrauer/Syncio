using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Syncio
{
    class Monitor
    {
        FileSystemWatcher watcher;
        public void Watch(String filePath)
        {
            watcher = new FileSystemWatcher(@filePath);
            
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
            //watcher.Error += OnError;

            //watcher.Filter = "*.txt";
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

        }

        //These methods need to to the target directory on changed
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            var log = $"Changed: {e.FullPath}";

            //Debug.WriteLine(log);

        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            var log = $"Created: {e.FullPath}";
            Debug.WriteLine(log);

        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            //this.Dispatcher.Invoke(() => ListBoxLog.Items.Add($"Deleted: {e.FullPath}"));
            var log = $"Deleted: {e.FullPath}";
            //Debug.WriteLine(log);
        }



        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            //this.Dispatcher.Invoke(() => ListBoxLog.Items.Add($"Renamed:"));
            //this.Dispatcher.Invoke(() => ListBoxLog.Items.Add($"    Old: {e.OldFullPath}"));
            //this.Dispatcher.Invoke(() => ListBoxLog.Items.Add($"    New: {e.FullPath}"));



            Debug.WriteLine($"Renamed:");
            Debug.WriteLine($"Old: {e.OldFullPath}");
            Debug.WriteLine($"New: {e.FullPath}");
        }

        //private void OnError(object sender, ErrorEventArgs e) =>
        //    PrintException(e.GetException());

        private void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Debug.WriteLine($"Message: {ex.Message}");
                Debug.WriteLine("Stacktrace:" + ex.StackTrace);
                Debug.WriteLine("Inner Exception: " + ex.InnerException);
               
            }
        }
    }
}
