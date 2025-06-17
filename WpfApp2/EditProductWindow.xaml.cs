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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {

        public int ProductId { get; set; }
        public EditProductWindow(ProductsPage.Product product)
        {
            InitializeComponent();
            LoadComboBoxType();
            LoadComboBoxTypeMaterial();
            ProductId = product.id;
            NameBox.Text = product.NameProduct;
            TypeBox.SelectedItem = product.Type;
            ArticulBox.Text = product.ArticulNumber;
            MinPriceBox.Text = product.MinPrice;
            MaterialBox.SelectedItem = product.MainMaterial;
        }

        void LoadComboBoxType()
        {
            List<string> list = new List<string>();

            DB dB = new DB();

            dB.OpenConnection();

            SqlCommand sqlCommand = new SqlCommand("SELECT type_production From ProductionType", dB.GetConnection());

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader["type_production"].ToString());
            }

            TypeBox.ItemsSource = list;
        }

        void LoadComboBoxTypeMaterial()
        {
            List<string> list = new List<string>();

            DB dB = new DB();

            dB.OpenConnection();

            SqlCommand sqlCommand = new SqlCommand("SELECT name From MaterialType", dB.GetConnection());

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader["name"].ToString());
            }

            MaterialBox.ItemsSource = list;

        }

        int IdFound(string name)
        {
            if (name == "Гостиные")
            {
                return 1;
            }
            if (name == "Прихожие")
            {
                return 2;
            }
            if (name == "Мягкая мебель")
            {
                return 3;
            }
            if (name == "Кровати")
            {
                return 4;
            }
            if (name == "Шкафы")
            {
                return 5;
            }
            if (name == "Комоды")
            {
                return 6;
            }
            else
            {
                return -1;
            }

        }

        int IdFoundMaterial(string name)
        {
            if (name == "Мебельный щит из массива дерева")
            {
                return 1;
            }
            if (name == "Ламинированное ДСП")
            {
                return 2;
            }
            if (name == "Фанера")
            {
                return 3;
            }
            if (name == "МДФ")
            {
                return 4;
            }
            else
            {
                return -1;
            }

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB dB = new DB();
                dB.OpenConnection();

                DataTable dataTable = new DataTable();

                SqlCommand cmd = new SqlCommand("UPDATE Products SET production_type_id=@type, name_product=@name, articul=@art, min_price_for_partner=@price, main_material_id=@mat WHERE id=@id", dB.GetConnection());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = ProductId;
                cmd.Parameters.Add("@type", SqlDbType.Int).Value = IdFound(TypeBox.SelectedItem.ToString());
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = NameBox.Text;
                cmd.Parameters.Add("@art", SqlDbType.BigInt).Value = Convert.ToInt64(ArticulBox.Text);
                cmd.Parameters.Add("@price", SqlDbType.Float).Value = Convert.ToDouble(MinPriceBox.Text);
                cmd.Parameters.Add("@mat", SqlDbType.Int).Value = IdFoundMaterial(MaterialBox.SelectedItem.ToString());

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = cmd;
                sqlDataAdapter.Fill(dataTable);

                dB.CloseConnection();

                MessageBox.Show("Данные успешно обновлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "ХУЙНЯ ЗАЛУПА ПЕРЕДЕЛЫВАЙ", MessageBoxButton.OK, MessageBoxImage.Hand);
                
            }
            
        }
    }
}
