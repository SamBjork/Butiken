using System;
using System.IO;
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
        string[] Discount;       
        float Bank = 2000;
        private TextBlock recieptBlock;
        private ComboBox storeBox;
        private List<Store> stores = new List<Store>();
        private Store SelectedStore; 

        const string AllStoresInfo = "AllStores.csv";

        public class Store
        {
            public string Name;
            public string ProductFilePath;
            public string CartFilePath;
            public string[] Products;
            public string[] Cart;

            public Store(string line)
            {
                string[] parts = line.Split(',');

                Name = parts[0];
                ProductFilePath = parts[1];
                CartFilePath = parts[2];

                ReadProducts();
                ReadCart();
            }

            private void ReadProducts()
            {
                try
                {
                    Products = File.ReadAllLines(ProductFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kunde inte läsa in:" + Name + "'s produktlista");
                }
            }
                private void ReadCart()
                {
                    try
                    {
                        Cart = File.ReadAllLines(CartFilePath);
                    }
                    catch
                    {
                       //Inget fel att cart inte finns
                    }
                }
        }

        public MainWindow()
        {
            InitializeComponent();
            Start1();
        }

        private void Start1()
        {
            LoadAllStores();

            const string DiscountFilePath = "Rabatt.txt";

            if (File.Exists(DiscountFilePath))
            {
                Discount = File.ReadAllLines(DiscountFilePath);
            }
            else
            {
                MessageBox.Show("Error: Filen som innehåller rabatter finns inte");
                Environment.Exit(0);
            }


            Title = "The Market";
            Width = 1000;
            Height = 600;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ScrollViewer scroll = (ScrollViewer)Content;
            Grid mainGrid = new Grid();
            scroll.Content = mainGrid;
            mainGrid.Margin = new Thickness(5);

            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            TextBlock chooseStore = new TextBlock
            {
                Text = "Välj butik!",
                FontSize = 50,
                TextAlignment = TextAlignment.Center
            };
            mainGrid.Children.Add(chooseStore);
            Grid.SetRow(chooseStore, 0);

            storeBox = new ComboBox();
            mainGrid.Children.Add(storeBox);
            Grid.SetRow(storeBox, 1);
            foreach (Store S in stores)
            {
                storeBox.Items.Add(S.Name);
            }
            storeBox.SelectionChanged += HandleSelectionChange;

           
            Button QuitProg = new Button
            {
                Content = "Avsluta program",
                Margin = new Thickness(40)
            };
            mainGrid.Children.Add(QuitProg);
            Grid.SetRow(QuitProg, 2);
            QuitProg.Click += QuitProg_Click;
        }
        public void LoadAllStores()
        {
            stores.Clear();
            string[] lines = File.ReadAllLines(AllStoresInfo);
            foreach (string line in lines)
            {
                try
                {                  
                    stores.Add(new Store(line));
                }
                catch
                {
                    MessageBox.Show("Fel vid inläsning");
                }
            }
        }
      
        void HandleSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            var x = storeBox.SelectedItem;
            
            foreach (Store s in stores)
            {
                if (s.Name == storeBox.SelectedItem.ToString())
                {
                    SelectedStore = s;
                    break;
                }
            }
            if (SelectedStore == null)
            {
                MessageBox.Show("Butik:" + storeBox.SelectedItem.ToString() + "finns inte");
            }
            else
            {
                Start2();
            }                       
        }       

        private void QuitProg_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }


        private void Start2()
        {

            Title = "Butiken";
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
            mainGrid.RowDefinitions.Add(new RowDefinition());


            StackPanel infoPanel = CreateInfoPanel();
            mainGrid.Children.Add(infoPanel);
            Grid.SetRow(infoPanel, 0);
            Grid.SetColumn(infoPanel, 0);


            Grid subGrid1 = new Grid();
            subGrid1.ColumnDefinitions.Add(new ColumnDefinition());
            subGrid1.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            subGrid1.RowDefinitions.Add(new RowDefinition());
            subGrid1.RowDefinitions.Add(new RowDefinition());
            subGrid1.RowDefinitions.Add(new RowDefinition());
            subGrid1.RowDefinitions.Add(new RowDefinition());
            subGrid1.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            mainGrid.Children.Add(subGrid1);
            Grid.SetColumn(subGrid1, 1);

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

            ListBox productBox = new ListBox
            { Margin = new Thickness(5) };
            subGrid1.Children.Add(productBox);
            Grid.SetRow(productBox, 1);
            Grid.SetColumn(productBox, 0);
            int lineLength = SelectedStore.Products.Length;

            List<string> Products = new List<string>();
            List<int> Prices = new List<int>();

            for (int i = 0; i < lineLength; i++)
            {
                string line = Products[i];

                int number = 0;
                string Serienummer = "";

                while (line[number] != ':')
                {
                    Serienummer += line[number];
                    number++;
                }

                string information = "";
                string Item = "";
                int Price = 0;
                bool DetectingPrice = false;

                for (int j = number + 2; j < line.Length; j++)
                {
                    char PosJ = line[j];
                    if (PosJ == '-')
                    {
                        information = information.Substring(0, j - (number + 3));
                        Item = information;
                        information = "";
                    }
                    else if (PosJ == '(')
                    {
                        information = "";
                        DetectingPrice = true;
                    }
                    else if (PosJ == 'k' && DetectingPrice == true)
                    {
                        Price = Convert.ToInt32(information);

                        Products.Add(Item);
                        Prices.Add(Price);

                        productBox.Items.Add(Item + " " + Price + "kr");

                    }
                    else
                    {
                        information += PosJ;
                    }
                }
            }

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

            float DiscountAmount = 1;
            applyCodeButton.Click += codevalidator;

            void codevalidator(object sender, RoutedEventArgs e)
            {
                for (int i = 0; i < Discount.Length; i++)
                {
                    string line = Discount[i];


                    int number = 0;
                    string Rabattkod = "";

                    while (line[number] != '(')
                    {
                        Rabattkod += line[number];
                        number++;
                    }

                    if (i == Discount.Length && DiscountAmount == 1)
                    {
                        MessageBox.Show("Du angav en felaktig kod");
                    }
                    else if (Rabattkod == discountBox.Text)
                    {
                        string information = "";

                        number++;
                        for (int j = number; j < line.Length; j++)
                        {
                            char PosJ = line[j];
                            if (PosJ == '%')
                            {
                                information = information.Substring(0, j - (number));


                                DiscountAmount = Convert.ToInt64(information);
                                DiscountAmount = (100 - DiscountAmount) / 100;
                                MessageBox.Show("du använde koden: " + discountBox.Text + " för " + information + "% avdrag från ditt köp");
                            }
                            else
                            {
                                information += PosJ;
                            }
                        }

                    }
                }
            }
            TextBlock totalPrice = new TextBlock
            {
                Text = "Dina pengar: " + Bank,
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



            Grid subGrid2 = new Grid();
            subGrid2.ColumnDefinitions.Add(new ColumnDefinition());
            subGrid2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            subGrid2.RowDefinitions.Add(new RowDefinition());
            subGrid2.RowDefinitions.Add(new RowDefinition());
            subGrid2.RowDefinitions.Add(new RowDefinition());
            subGrid2.RowDefinitions.Add(new RowDefinition());
            subGrid2.RowDefinitions.Add(new RowDefinition());
            subGrid2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.Children.Add(subGrid2);
            Grid.SetColumn(subGrid2, 2);

            StackPanel buttonPanel1 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(5)
            };
            subGrid2.Children.Add(buttonPanel1);
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
                Content = "Spara och Avsluta",
                Margin = new Thickness(50, 40, 50, 40)
            };
            buttonPanel1.Children.Add(saveButton);

            Grid subGrid3 = new Grid();
            subGrid3.ColumnDefinitions.Add(new ColumnDefinition());

            subGrid3.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            subGrid3.RowDefinitions.Add(new RowDefinition());
            subGrid3.RowDefinitions.Add(new RowDefinition());
            subGrid3.RowDefinitions.Add(new RowDefinition());
            subGrid3.RowDefinitions.Add(new RowDefinition());
            subGrid3.RowDefinitions.Add(new RowDefinition());
            subGrid3.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.Children.Add(subGrid3);
            Grid.SetColumn(subGrid3, 3);


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

            ListBox cartBox = new ListBox { Margin = new Thickness(5) };
            subGrid3.Children.Add(cartBox);
            Grid.SetRow(cartBox, 1);
            Grid.SetColumn(cartBox, 0);


            List<string> CartProducts = new List<string>();
            List<int> CartPrices = new List<int>();

            CartProducts.Add("");
            CartPrices.Add(0);

            if (File.Exists(SelectedStore.CartFilePath))
            {

                for (int i = 0; i < SelectedStore.Cart.Length; i++)
                {
                    string Pos = SelectedStore.Cart[i];
                    int number = 0;
                    string information = "";
                    while (Pos[number] != '(')
                    {
                        information += Pos[number];
                        number++;
                    }
                    CartProducts.Add(information);
                    number++;
                    information = "";
                    while (Pos[number] != ')')
                    {
                        information += Pos[number];
                        number++;
                    }
                    CartPrices.Add(Convert.ToInt32(information));
                }
                for (int i = 1; i < CartProducts.Count; i++)
                {
                    cartBox.Items.Add(CartProducts[i] + " " + CartPrices[i] + "kr");
                }
            }

            saveButton.Click += saveQuit;
            addButton.Click += transfer;
            removeButton.Click += remove;

            void saveQuit(object sender, RoutedEventArgs e)
            {
                if (cartBox.Items.Count > 0)
                {

                    List<string> Savings = new List<string>();
                    for (int i = 1; i < CartProducts.Count; i++)
                    {
                        Savings.Add(CartProducts[i] + "(" + CartPrices[i] + ")");
                    }

                    File.WriteAllLines(SelectedStore.CartFilePath, Savings);
                    Environment.Exit(0);
                }
                else
                {
                    MessageBox.Show("Varukorgen är tom och går inte att spara.");
                }

            }
            void transfer(object sender, RoutedEventArgs e)
            {
                cartBox.Items.Add(productBox.SelectedItem);

                int select = productBox.Items.IndexOf(productBox.SelectedItem);
                try
                {
                    CartProducts.Add(Products[select]);
                    CartPrices.Add(Prices[select]);
                }
                catch (Exception)
                {
                    MessageBox.Show("Du måste välja en produkt att lägga till i varukorgen");
                }

            }
            void remove(object sender, RoutedEventArgs e)
            {
                int select = cartBox.Items.IndexOf(cartBox.SelectedItem) + 1;

                if (cartBox.Items.Count != 0)
                {
                    if (cartBox.Items.IndexOf(cartBox.SelectedItem) >= 0)
                    {
                        try
                        {
                            CartPrices.RemoveAt(select);
                            CartProducts.RemoveAt(select);
                            cartBox.Items.RemoveAt(cartBox.Items.IndexOf(cartBox.SelectedItem));
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Du har inga produkter att ta bort från varukorgen!");
                        }

                    }
                    else if (cartBox.Items.Count >= 0)
                    {
                        try
                        {
                            CartPrices.RemoveAt(1);
                            CartProducts.RemoveAt(1);
                            cartBox.Items.RemoveAt(0);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Du har inga produkter att ta bort från varukorgen!");
                        }
                    }
                }
            }


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


            recieptBlock = new TextBlock
            {
                Text = "",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };
            recieptPanel.Children.Add(recieptBlock);

            float sum = 0;
            payButton.Click += Pay;

            void Pay(object sender, RoutedEventArgs e)
            {

                sum = CartPrices.Sum();
                sum *= DiscountAmount;
                if (Bank >= sum && CartProducts.Count > 0)
                {
                    Bank = Bank - sum;

                    totalPrice.Text = "Dina pengar: " + Bank + " kr";
                    recieptBlock.Text = "";
                    for (int i = 1; i < CartProducts.Count; i++)
                    {

                        recieptBlock.Text += "\n" + CartProducts[i] + "                                                " +
                        " " + CartPrices[i];

                    }
                    recieptBlock.Text += "\n-------------------------------------------";
                    recieptBlock.Text += "\nTotal" + "                                                " +
                    sum + "kr";


                }
                else
                {
                    MessageBox.Show("Du har inte nog med pengar");
                }

            }

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

            StackPanel instructionPanel = new StackPanel();
            infoPanel.Children.Add(instructionPanel);

            TextBlock instruction1 = new TextBlock()
            {
                Text = "Du kan enkelt lägga till varor i din varukorg genom att välja en produkt och klicka på Lägg till Produkt - knappen.",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
                FontSize = 15,
                TextAlignment = TextAlignment.Left
            };
            instructionPanel.Children.Add(instruction1);
            TextBlock instruction2 = new TextBlock()
            {
                Text = "För att lägga tillbaka en vara, välj den i varukorgen och klicka på Ta bort Produkt - knappen.",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
                FontSize = 15,
                TextAlignment = TextAlignment.Left
            };
            instructionPanel.Children.Add(instruction2);
            TextBlock instruction3 = new TextBlock()
            {
                Text = "Vill du spara din varukorg utan att köpa varorna nu och avlsuta, kan du klicka på Spara och Avsluta- knappen.",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
                FontSize = 15,
                TextAlignment = TextAlignment.Left
            };
            instructionPanel.Children.Add(instruction3);
            TextBlock instruction4 = new TextBlock()
            {
                Text = "Har du en rabattkod kan du skriva in den ovanför i fältet under Produktlistan, sedan klicka på Tillämpa rabatt - knappen.",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
                FontSize = 15,
                TextAlignment = TextAlignment.Left
            };
            instructionPanel.Children.Add(instruction4);


            return infoPanel;
        }

    }
}