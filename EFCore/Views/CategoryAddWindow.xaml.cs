using System.Windows;
using EFCore.DataAccess.Concrete;
using EFCore.Models;

namespace EFCore.Views
{
    public partial class CategoryAddWindow : Window
    {
        public Repository<Category> Repository { get; set; }
        public bool IsEdit { get; set; }
        public Category MyCategory { get; set; }
        public CategoryAddWindow(Repository<Category> repository)
        {
            InitializeComponent();
            Repository = repository;
            IsEdit = false;
        }
        public CategoryAddWindow(Repository<Category> repository, Category Edit)
        {
            InitializeComponent();
            Repository = repository;
            MyCategory = Edit;
            IsEdit = true;
            txt.Text = MyCategory.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsEdit == false)
            {
                if (!string.IsNullOrWhiteSpace(txt.Text))
                {
                    Repository.Insert(new Category { Name = $"{txt.Text}" });
                    Close();
                }
                else
                {
                    MessageBox.Show("Text Is Empty");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txt.Text))
                {
                    MyCategory.Name = txt.Text;
                    Repository.Update(MyCategory);
                    Close();
                }
                else
                {
                    MessageBox.Show("Text Is Empty");
                }
            }
        }
    }
}
