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
using static WpfApp2.ProductsPage;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для DescriptionPage.xaml
    /// </summary>
    public partial class DescriptionPage : Page
    {
        public int ProductId { get; set; }
        public DescriptionPage(ProductsPage.Product product)
        {
            InitializeComponent();
            ProductId = product.id;
            InitTable();
            titleBlock.Text = $"{product.NameProduct} | {product.Time}";
        }

        void InitTable()
        {
            DB dB = new DB();

            DataTable dataTable = new DataTable();

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ProductWorkshop_View WHERE product_name_id=@id", dB.GetConnection());
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = ProductId;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(dataTable);

            ProductWorkshopData.ItemsSource = dataTable.DefaultView;
        }
    }
}
