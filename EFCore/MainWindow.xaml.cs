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
using EFCore.DataAccess.Concrete;
using EFCore.Models;
using EFCore.Views;

namespace EFCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Repository<Product> Products { get; set; } = new();
        public Repository<Category> Categories { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();

            CategoryDG.ItemsSource = Categories.GetAll().Select(x => new { x.Id, x.Name }).ToList();
            ProductDG.ItemsSource = Products.GetAll().Select(x => new { x.Id, x.Name, x.Price, x.CategoryId }).ToList();

            CatDelBtn.IsEnabled = false;
            CatEditBtn.IsEnabled = false;
            ProdDelBtn.IsEnabled = false;
            ProdEditBtn.IsEnabled = false;
        }

        private void CategoryDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryDG.SelectedItem is not null)
            {
                CatEditBtn.IsEnabled = true;
                CatDelBtn.IsEnabled = true;
                ProductDG.SelectedItem = null;
            }
            else
            {
                CatDelBtn.IsEnabled = false;
                CatEditBtn.IsEnabled = false;
            }
        }

        private void ProductDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductDG.SelectedItem is not null)
            {
                ProdEditBtn.IsEnabled = true;
                ProdDelBtn.IsEnabled = true;
                CategoryDG.SelectedItem = null;
            }
            else
            {
                ProdDelBtn.IsEnabled = false;
                ProdEditBtn.IsEnabled = false;
            }
        }

        private void ProdEditBtn_Click(object sender, RoutedEventArgs e)
        {
            var temp = new ProductAddWindow(Products, ProductDG.SelectedItem as Product);
            var linkedList = Categories.GetAll().ToList();
            temp.CategoryCB.ItemsSource = linkedList;
            temp.CategoryCB.DisplayMemberPath = "Name";
            var temp2 = Categories.Get(x => x.Id == (ProductDG.SelectedItem as Product).CategoryId).FirstOrDefault();
            temp.CategoryCB.SelectedItem = temp2;
            temp.ShowDialog();
            ProductDG.ItemsSource = Products.GetAll().ToList();
        }

        private void ProdDelBtn_Click(object sender, RoutedEventArgs e)
        {
            Products.Delete(ProductDG.SelectedItem as Product);

            ProductDG.ItemsSource = Products.GetAll().ToList();
        }

        private void CatAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var temp = new CategoryAddWindow(Categories);
            temp.ShowDialog();

            CategoryDG.ItemsSource = Categories.GetAll().ToList();
            ProductDG.ItemsSource = Products.GetAll().ToList();
        }

        private void CatEditBtn_Click(object sender, RoutedEventArgs e)
        {
            var temp = new CategoryAddWindow(Categories, CategoryDG.SelectedItem as Category);
            temp.ShowDialog();
            CategoryDG.ItemsSource = Categories.GetAll().ToList();
        }

        private void CatDelBtn_Click(object sender, RoutedEventArgs e)
        {
            Categories.Delete(CategoryDG.SelectedItem as Category);

            CategoryDG.ItemsSource = Categories.GetAll().ToList();
        }

        private void ProdAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var temp = new ProductAddWindow(Products);
            var linkedList = Categories.GetAll().ToList();
            temp.CategoryCB.ItemsSource = linkedList;
            temp.CategoryCB.DisplayMemberPath = "Name";
            temp.ShowDialog();

            ProductDG.ItemsSource = Products.GetAll().ToList();
        }
    }
}
