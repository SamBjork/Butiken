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

namespace TheMarket
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
            Title = "The Market";
            Width = 1000;
            Height = 600;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ScrollViewer scroll = (ScrollViewer)Content;

            Grid mainGrid = new Grid();
            scroll.Content = mainGrid;
            mainGrid.Margin = new Thickness(5);
                  

            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });            
            mainGrid.RowDefinitions.Add(new RowDefinition ());            
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
              
            //stretch so it's takes up all rows
            StackPanel infoPanel = CreateInfoPanel();
            mainGrid.Children.Add(infoPanel);
            Grid.SetRow(infoPanel, 0);
            Grid.SetColumn(infoPanel, 0);

            StackPanel mainPanel1 = CreateMainPanel1();
            mainGrid.Children.Add(mainPanel1);
            Grid.SetRow(mainPanel1, 0);
            Grid.SetColumn(mainPanel1, 1);

            StackPanel mainPanel2 = CreateMainPanel2();
            mainGrid.Children.Add(mainPanel2);
            Grid.SetRow(mainPanel2, 0);
            Grid.SetColumn(mainPanel2, 2);

            StackPanel mainPanel3 = CreateMainPanel3();
            mainGrid.Children.Add(mainPanel3);
            Grid.SetRow(mainPanel3, 0);
            Grid.SetColumn(mainPanel3, 3);
        }
        private StackPanel CreateInfoPanel()
        {
            StackPanel infoPanel = new StackPanel { Orientation = Orientation.Vertical };

            TextBlock mainHeading = new TextBlock
            {
                Text = "Välkommen till Butiken!",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),                
                FontSize = 22,
                TextAlignment = TextAlignment.Left
            };
            infoPanel.Children.Add(mainHeading);

            TextBlock instructionsHeading = new TextBlock
            {
                Text = "Instruktioner:",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
                FontSize = 18,
                TextAlignment = TextAlignment.Left
            };
            infoPanel.Children.Add(instructionsHeading);

            //Kanske läsa in instrukioner från en textfil istället för att hårdkoda
            
            TextBlock instructionBlock = new TextBlock()
            {
                Text = "Här kommer massa instruktioner stå och berätta om hur applikationen fungerar ",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
                FontSize = 15,
                TextAlignment = TextAlignment.Left

            };
            infoPanel.Children.Add(instructionBlock);

            return infoPanel;
        }
        private StackPanel CreateMainPanel1()
        {
            StackPanel mainPanel1 = new StackPanel { Orientation = Orientation.Vertical };

            TextBlock productHeading = new TextBlock
            {
                Text = "Produkter",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 10 , 0 , 10),
                FontSize = 18,
                TextAlignment = TextAlignment.Center
            };
            mainPanel1.Children.Add(productHeading);
            
            
            ListBox productBox = new ListBox { Margin = new Thickness(5) };
            mainPanel1.Children.Add(productBox);
            productBox.Items.Add("Produkter");
            productBox.Items.Add("Produkter");
            productBox.Items.Add("Produkter");
            Grid.SetColumn(productBox, 1);
            Grid.SetRow(productBox, 1);
            //lägga in funktionalitet, eventhandler

            TextBlock totalPrice = new TextBlock
            {
                Text = "Totalt pris:",
                Margin = new Thickness(5),
                FontSize = 18,
                TextAlignment = TextAlignment.Center
            };
            mainPanel1.Children.Add(totalPrice);

            TextBlock discountText = new TextBlock
            {
                Text = "Rabattkod:",
                Margin = new Thickness(5),
                FontSize = 18,
                TextAlignment = TextAlignment.Center
            };
            mainPanel1.Children.Add(discountText);

            Button applyCode = new Button
            {
                Content = "Tillämpa Rabatt",
                Margin = new Thickness(5)
            };
            mainPanel1.Children.Add(applyCode);

            Button payButton = new Button
            {
                Content = "Betala",
                Margin = new Thickness(5)
            };
            mainPanel1.Children.Add(payButton);


            return mainPanel1;
        }

        private StackPanel CreateMainPanel2()
        {
            StackPanel mainPanel2 = new StackPanel { Orientation = Orientation.Vertical };
            

            return mainPanel2;

        }

        private StackPanel CreateMainPanel3()
        {
            StackPanel mainPanel3 = new StackPanel { Orientation = Orientation.Vertical };
            

            return mainPanel3;
        } 
    }
}
