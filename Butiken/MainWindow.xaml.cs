using System;
using System.Collections.Generic;
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

namespace Butiken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        private void Start()
        {
            Title = "Butiken";
            Width = 1000;
            Height = 600;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ScrollViewer scroll = (ScrollViewer)Content;

            Grid mainGrid = new Grid();
            scroll.Content = mainGrid;
            mainGrid.Margin = new Thickness(5);
            mainGrid.RowDefinitions = new RowDefinition()
        }
    }
}
