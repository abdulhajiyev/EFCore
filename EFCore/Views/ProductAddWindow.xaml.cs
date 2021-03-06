using EFCore.DataAccess.Concrete;
using EFCore.Models;
using System.Windows;

namespace EFCore.Views
{
    public partial class ProductAddWindow : Window
    {
        public double Price
        {
            get => (double)GetValue(PriceProperty);
            set => SetValue(PriceProperty, value);
        }

        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(double), typeof(ProductAddWindow));

        public Repository<Product> Repository { get; set; }
        public Product MyProduct { get; set; }
        public bool IsEdit { get; set; }

        public ProductAddWindow(Repository<Product> repository)
        {
            InitializeComponent();
            DataContext = this;
            Repository = repository;
        }
        public ProductAddWindow(Repository<Product> repository, Product Edit)
        {
            InitializeComponent();
            DataContext = this;
            Repository = repository;
            MyProduct = Edit;
            IsEdit = true;
            ProdNameTxt.Text = MyProduct.Name;
            Price = MyProduct.Price;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsEdit == false)
            {
                if (!string.IsNullOrWhiteSpace(ProdNameTxt.Text) && !string.IsNullOrWhiteSpace(ProdPriceTxt.Text))
                {
                    Repository.Insert(CategoryCB.SelectedItem is not null
                        ? new Product
                        {
                            Name = ProdNameTxt.Text,
                            Price = Price,
                            CategoryId = (CategoryCB.SelectedItem as Category)?.Id
                        }
                        : new Product { Name = ProdNameTxt.Text, CategoryId = null, Price = Price });
                    Close();
                }
                else
                {
                    MessageBox.Show("Product Name Or Product Price Text Is Empty");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(ProdNameTxt.Text) && !string.IsNullOrWhiteSpace(ProdPriceTxt.Text))
                {
                    MyProduct.Name = ProdNameTxt.Text;
                    MyProduct.Price = Price;
                    MyProduct.CategoryId = (CategoryCB.SelectedItem as Category).Name != "Nothing"
                        ? (CategoryCB.SelectedItem as Category).Id
                        : null;
                    Repository.Update(MyProduct);
                    Close();
                }
                else
                {
                    MessageBox.Show("Product Name Or Product Price Text Is Empty");
                }
            }
        }
    }
}
