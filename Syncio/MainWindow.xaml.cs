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
        String source, target;
        public MainWindow()
        {
            InitializeComponent();  
        }

        


        private void BaseDirectoryInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();


            source = dialog.SelectedPath;

            //pass user selected directory to the FileSystemWatcher
            monitor.InitializeWatcher(source);
            Debug.WriteLine(source);


        }

        private void TargetDirectoryInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();


            target = dialog.SelectedPath;

            //pass user selected directory to the FileSystemWatcher
            //monitor.InitializeWatcher(source);
            Debug.WriteLine(target);


        }



    }
}
