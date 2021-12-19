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
        Monitor monitor = new Monitor();
        DirectoryInfo sourceDir, targetDir;

        RecurseTree walkSource = new RecurseTree();
        RecurseTree walkTarget = new RecurseTree();
        List<Object> sourceTree = new List<object>();
        List<Object> targetTree = new List<object>();
        FileManager driver = new FileManager();

        String source, target;
        public MainWindow()
        {
            InitializeComponent();  
        }

        

        //so now I've got two trees that represent each directory need to compare and copy over the relevant files

        private void BaseDirectoryInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();


            source = dialog.SelectedPath;

            BaseDirectoryInput.Text = dialog.SelectedPath;

            //pass user selected directory to the FileSystemWatcher
            AddMonitors();

            sourceDir = new DirectoryInfo(source);
            
            //recurse through the file system
            walkSource.WalkDirectoryTree(sourceDir);

            sourceTree = walkSource.GetHashSet();

            //Debug.WriteLine(sourceTree[0]);



        }



        private void TargetDirectoryInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            target = dialog.SelectedPath;

            TargetDirectoryInput.Text = dialog.SelectedPath;
            Debug.WriteLine("TARGET DIRECTORY: "+ target);

            targetDir = new DirectoryInfo(target);
            walkTarget.WalkDirectoryTree(targetDir);

            //targetTree = walkTarget.GetHashSet();

            //Debug.WriteLine(targetTree[0]);
        }

        private void ButtonSync_Click(object sender, RoutedEventArgs e)
        {

            driver.InitialCopy(source, target, true);



        }

        /*
          This adds monitors to the root director, to every sub directory and so on
         */
        private void AddMonitors()
        {
            //monitors root directory
            monitor.Watch(source);

            //this monitors all of the sub directories
            var subs = walkSource.GetSubs();
            foreach (string subDirectory in subs)
            {
                monitor.Watch(subDirectory);
            }

        }

        //lets just get the initial copy down first, need to copy folder 123 to the sync folder
        //private void InitialCopy()
        //{
        //    DirectoryCopy(source, target, true);
        //}

        //private void DirectoryCopy(string sourceDirName, string destDirname, bool copySubDirs)
        //{
        //    DirectoryInfo sourceDir = new DirectoryInfo(sourceDirName);

        //    if (!sourceDir.Exists)
        //    {
        //        System.Windows.MessageBox.Show("Please select a valid directory");
        //    }

        //    DirectoryInfo[] sourceDirs = sourceDir.GetDirectories();

        //    //if the destination directory doesn't exist, create it
        //    Directory.CreateDirectory(destDirname);

        //    FileInfo[] files = sourceDir.GetFiles();

            
        //    foreach(FileInfo file in files)
        //    {
        //        string tempPath = System.IO.Path.Combine(destDirname.ToString(), file.Name);
        //        file.CopyTo(tempPath, false);
        //    }

        //    if (copySubDirs)
        //    {
        //        foreach (DirectoryInfo subdir in sourceDirs)
        //        {
        //            string tempPath = System.IO.Path.Combine(destDirname.ToString(), subdir.Name);
        //            DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
        //        }
        //    }

        //}


    }
}
