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

        RecurseDirectoryTree walkSource = new RecurseDirectoryTree();
        RecurseDirectoryTree walkTarget = new RecurseDirectoryTree();
        Node<Model> sourceTree, targetTree;
        FileManager driver = new FileManager();

        String source, target;
        public MainWindow()
        {
            InitializeComponent();
        }
        //if a change is detected in the left tree, search the relevant node parent node (directory) that contains 
        //the information, add the new/altered node to the right tree.

        private void BaseDirectoryInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();


            source = dialog.SelectedPath;

            BaseDirectoryInput.Text = dialog.SelectedPath;

            //pass user selected directory to the FileSystemWatcher
            AddMonitors();

            sourceDir = new DirectoryInfo(source);

            walkSource.WalkDirectoryTree(sourceDir);


            sourceTree = walkSource.GetTree();
            sourceTree.printTree(sourceTree, " ");

        }



        private void TargetDirectoryInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            target = dialog.SelectedPath;

            TargetDirectoryInput.Text = dialog.SelectedPath;


            targetDir = new DirectoryInfo(target);

            walkTarget.WalkDirectoryTree(targetDir);
            targetTree = walkTarget.GetTree();

            targetTree.printTree(targetTree, " ");

            
        }

        private void ButtonSync_Click(object sender, RoutedEventArgs e)
        {

            driver.InitialCopy(source, target, true);

        }

        /*
          This adds monitors to the root directory, to every sub directory and so on
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


  


    }
}