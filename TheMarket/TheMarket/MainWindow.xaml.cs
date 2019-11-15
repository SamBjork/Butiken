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
        private TextBlock recieptBlock;
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
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            //stretch so it's takes up all rows
            StackPanel infoPanel = CreateInfoPanel();
            mainGrid.Children.Add(infoPanel);
            Grid.SetRow(infoPanel, 0);
            Grid.SetColumn(infoPanel, 0);
            //gör om till grid oxå
            Grid SubGrid1 = CreateSubGrid1();
            mainGrid.Children.Add(SubGrid1);
            Grid.SetRow(SubGrid1, 0);
            Grid.SetColumn(SubGrid1, 1);

            Grid subGrid2 = CreateMainGrid2();
            mainGrid.Children.Add(subGrid2);
            Grid.SetRow(subGrid2, 0);
            Grid.SetColumn(subGrid2, 2);

            Grid subGrid3 = CreateSubGrid3();
            mainGrid.Children.Add(subGrid3);
            Grid.SetRow(subGrid3, 0);
            Grid.SetColumn(subGrid3, 3);
        }
        private StackPanel CreateInfoPanel()
        {
            StackPanel infoPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(5) };

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
        private Grid CreateSubGrid1()
        {
            Grid subGrid1 = new Grid ();
            subGrid1.ColumnDefinitions.Add(new ColumnDefinition());
            subGrid1.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            
            subGrid1.RowDefinitions.Add(new RowDefinition());
            subGrid1.RowDefinitions.Add(new RowDefinition());
            subGrid1.RowDefinitions.Add(new RowDefinition());
            subGrid1.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            subGrid1.RowDefinitions.Add(new RowDefinition());
            
 
            TextBlock productHeading = new TextBlock
            {
                Text = "Produkter",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 10, 0, 10),
                FontSize = 18,
                TextAlignment = TextAlignment.Center
            };
            subGrid1.Children.Add(productHeading);
            Grid.SetRow(productHeading, 0);
            Grid.SetColumn(productHeading, 0);
            
            ListBox productBox = new ListBox { Margin = new Thickness(5) };
            subGrid1.Children.Add(productBox);
            Grid.SetRow(productBox, 1);
            Grid.SetColumn(productBox, 0);
            

            productBox.Items.Add("Produkter");
            productBox.Items.Add("Produkter");
            productBox.Items.Add("Produkter");


            // textbox som läser rabattkod från användaren
            TextBox discountBox = new TextBox
            {
                Text = "",
                Margin = new Thickness(50, 20, 50, 20),
            };
            subGrid1.Children.Add(discountBox);
            Grid.SetRow(discountBox, 2);
            Grid.SetColumn(discountBox, 0);

            Button applyCodeButton = new Button
            {
                Content = "Tillämpa Rabatt",
                Margin = new Thickness(50, 10, 50, 10)

            };
            subGrid1.Children.Add(applyCodeButton);
            Grid.SetRow(applyCodeButton, 3);

            TextBlock totalPrice = new TextBlock
            {
                Text = "Totalt pris: 20192 Kr",
                Margin = new Thickness(5),
                FontSize = 17,
                TextAlignment = TextAlignment.Center
            };
            subGrid1.Children.Add(totalPrice);
            Grid.SetRow(totalPrice, 4);

            Button payButton = new Button
            {
                Content = "Betala",
                Margin = new Thickness(50, 10, 50, 10)
            };
            subGrid1.Children.Add(payButton);
            Grid.SetRow(payButton, 5);


            return subGrid1;
        }

        private Grid CreateMainGrid2()
        {
            Grid mainGrid2 = new Grid();
            mainGrid2.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid2.RowDefinitions.Add(new RowDefinition());
            mainGrid2.RowDefinitions.Add(new RowDefinition());
            mainGrid2.RowDefinitions.Add(new RowDefinition());
            mainGrid2.RowDefinitions.Add(new RowDefinition());
            mainGrid2.RowDefinitions.Add(new RowDefinition());
            mainGrid2.RowDefinitions.Add(new RowDefinition());
            mainGrid2.RowDefinitions.Add(new RowDefinition());

            StackPanel buttonPanel1 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(5)
            };
            mainGrid2.Children.Add(buttonPanel1);
            Grid.SetColumn(buttonPanel1, 0);
            Grid.SetRow(buttonPanel1, 0);

            Button addButton = new Button
            {
                Content = "Lägg till Produkt",
                Margin = new Thickness(50, 70, 50, 40)
            };
            buttonPanel1.Children.Add(addButton);


            Button removeButton = new Button
            {
                Content = "Ta bort Produkt",
                Margin = new Thickness(50, 40, 50, 40)
            };
            buttonPanel1.Children.Add(removeButton);

            Button saveButton = new Button
            {
                Content = "Spara Varukorgen",
                Margin = new Thickness(50, 40, 50, 40)
            };
            buttonPanel1.Children.Add(saveButton);

            return mainGrid2;

        }

        private Grid CreateSubGrid3()
        {
            Grid subGrid3 = new Grid();
            subGrid3.ColumnDefinitions.Add(new ColumnDefinition());
            
            subGrid3.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            subGrid3.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            subGrid3.RowDefinitions.Add(new RowDefinition());
            subGrid3.RowDefinitions.Add(new RowDefinition());
            subGrid3.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            subGrid3.RowDefinitions.Add(new RowDefinition());

            TextBlock cartHeading = new TextBlock
            {
                Text = "Varukorg",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 10, 0, 10),
                FontSize = 18,
                TextAlignment = TextAlignment.Center
            };
            subGrid3.Children.Add(cartHeading);
            Grid.SetRow(cartHeading, 0);
            Grid.SetColumn(cartHeading, 0);

            ListBox productBox = new ListBox { Margin = new Thickness(5) };
            subGrid3.Children.Add(productBox);
            Grid.SetRow(productBox, 1);
            Grid.SetColumn(productBox, 0); 

            productBox.Items.Add("Produkter");
            productBox.Items.Add("Produkter");
            productBox.Items.Add("Produkter");

            StackPanel recieptPanel = new StackPanel { Orientation = Orientation.Vertical };
            subGrid3.Children.Add(recieptPanel);
            Grid.SetColumn(recieptPanel, 0);
            Grid.SetRow(recieptPanel, 3);

            TextBlock recieptLabel = new TextBlock
            {
                Text = "Kvitto:",
                FontSize = 18,
               TextAlignment = TextAlignment.Center
            };
            recieptPanel.Children.Add(recieptLabel);
  

            //gör en for loop här kanske, för att lägga till alla produkter(och rabattkod) på "kvittot"
            //som här blir en mängd textblock

              recieptBlock = new TextBlock
            {
                Text = "1 st Banan: 6 kr",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };
            recieptPanel.Children.Add(recieptBlock);

            return subGrid3;
        }
    }
}
