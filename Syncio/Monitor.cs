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


        /*if a change is detected in the left tree, search for the relevant node parent node (directory) that contains 
        the information, add the new/altered node to the right tree. */

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
            watcher.Error += OnError;

            
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

        }


        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            var log = $"Changed: {e.FullPath}";

            Debug.WriteLine(log);

        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            var log = $"Created: {e.FullPath}";
            Debug.WriteLine(log);

        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {

            Debug.WriteLine($"Deleted: {e.FullPath}");

        }



        private void OnRenamed(object sender, RenamedEventArgs e)
        {

            Debug.WriteLine($"Renamed:");
            Debug.WriteLine($"Old: {e.OldFullPath}");
            Debug.WriteLine($"New: {e.FullPath}");
        }

        private void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

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