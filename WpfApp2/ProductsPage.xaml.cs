using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            InitTable();
        }

        public class Product
        {
            public int id { get; set; }
            public string TypeAndNameProduct { get; set; }
            public string Type { get; set; }
            public string NameProduct { get; set; }
            public string ArticulNumber { get; set; }
            public string MinPrice { get; set; }
            public string MainMaterial { get; set; }
            public string Time { get; set; }

        }

        string DiscountTime(int IdWorkshop)
        {
            string time;

            DB dB = new DB();

            dB.OpenConnection();

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SELECT SUM (time) AS time FROM ProductWortshop WHERE product_name_id=@id", dB.GetConnection());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = IdWorkshop;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = cmd;
            sqlDataAdapter.Fill(dt);


            time = dt.Rows[0]["time"].ToString();

            if(time == "")
            {
                return "0";
            }

            return time;

        }

        void InitTable()
        {
            List<Product> products = new List<Product>();

            DB dB = new DB();
            dB.OpenConnection();

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Product_View", dB.GetConnection());

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    id = (int)Convert.ToInt64(reader["id"]),
                    TypeAndNameProduct = $"{reader["type_production"]} | {reader["name_product"]}",
                    Type = reader["type_production"].ToString(),
                    NameProduct = reader["name_product"].ToString(),
                    ArticulNumber = reader["articul"].ToString(),
                    MinPrice = reader["min_price_for_partner"].ToString(),
                    MainMaterial = reader["name"].ToString(),
                    Time = $"Время иготовления: {DiscountTime(Convert.ToInt32(reader["id"]))}"
                });
            }

            reader.Close();
            dB.CloseConnection();

            ProductList.ItemsSource = products;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
            InitTable();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Product SelectedProduct = (Product) ProductList.SelectedItem;
            if (SelectedProduct == null)
            {
                MessageBox.Show("Сначала выберите объект для редактирования", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            EditProductWindow editProductWindow = new EditProductWindow(SelectedProduct);
            editProductWindow.ShowDialog();
            InitTable();
        }

        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            Product SelectedProduct = (Product)ProductList.SelectedItem;
            if (SelectedProduct == null)
            {
                MessageBox.Show("Сначала выберите объект для Открытия", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DescriptionPage descriptionPage = new DescriptionPage(SelectedProduct);
            NavigationService.Navigate(descriptionPage);
        }
    }
}
