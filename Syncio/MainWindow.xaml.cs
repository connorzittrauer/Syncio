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
        RecurseTree walkSource = new RecurseTree();
        RecurseTree walkTarget = new RecurseTree();
        List<Object> sourceTree = new List<object>();
        List<Object> targetTree = new List<object>();
        FileDriver driver = new FileDriver();

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
            monitor.Watch(source);
            Debug.WriteLine(source);

            DirectoryInfo dir = new DirectoryInfo(source);
            
            //recurse through the file system
            walkSource.WalkDirectoryTree(dir);

            sourceTree = walkSource.GetHashSet();

            Debug.WriteLine(sourceTree[0]);



        }



        private void TargetDirectoryInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            target = dialog.SelectedPath;

            TargetDirectoryInput.Text = dialog.SelectedPath;
            Debug.WriteLine("TARGET DIRECTORY: "+ target);

            DirectoryInfo dir = new DirectoryInfo(target);
            walkTarget.WalkDirectoryTree(dir);

            //targetTree = walkTarget.GetHashSet();

            //Debug.WriteLine(targetTree[0]);
        }

        private void ButtonSync_Click(object sender, RoutedEventArgs e)
        {

            //OK so need to append file name to directory in second argument
            //string file = sourceTree[0].ToString();
            //var dest = @"C:\Users\Connor\3D Objects\Test - Copy\plz-work.txt";
            //File.Copy(file, dest);



            //this monitors all of the sub directories
            var subs = walkSource.GetSubs();
            foreach (string subDirectory in subs)
            {
                monitor.Watch(subDirectory);
            }

        }



    }
}
