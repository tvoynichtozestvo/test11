using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CalculationWindow.xaml
    /// </summary>
    public partial class CalculationWindow : Window
    {

        public double Procent {  get; set; }
        public double Koff { get; set; }

        public CalculationWindow()
        {
            InitializeComponent();
            LoadComboBoxType();
            LoadComboBoxTypeMaterial();
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

        double ProcentFound(string name)
        {
            if (name == "Гостиные")
            {
                return 3.5;
            }
            if (name == "Прихожие")
            {
                return 5.6;
            }
            if (name == "Мягкая мебель")
            {
                return 3;
            }
            if (name == "Кровати")
            {
                return 4.7;
            }
            if (name == "Шкафы")
            {
                return 1.5;
            }
            if (name == "Комоды")
            {
                return 2.3;
            }
            else
            {
                return -1;
            }

        }

        double ProcentFoundMaterial(string name)
        {
            if (name == "Мебельный щит из массива дерева")
            {
                return 0.8;
            }
            if (name == "Ламинированное ДСП")
            {
                return 0.7;
            }
            if (name == "Фанера")
            {
                return 0.55;
            }
            if (name == "МДФ")
            {
                return 0.3;
            }
            else
            {
                return -1;
            }

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

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CountBox.Text == "" || param1Box.Text == "" || param2Box.Text == "")
            {
                MessageBox.Show("Пажаласта заполните все данные");
            } else
            {
                string typeProduct = TypeBox.SelectedItem.ToString();
                string typeMaterial = MaterialBox.SelectedItem.ToString();

                double coff = ProcentFound(typeProduct);
                double procent = ProcentFoundMaterial(typeMaterial);

                int param1 = Convert.ToInt32(param1Box.Text);
                int param2 = Convert.ToInt32(param2Box.Text);

                double result = (Convert.ToInt32(CountBox.Text) * (1 - procent / 100) / (param1 * param2 * coff));

                MessageBox.Show($"Результат этой залупы парен: {Math.Floor(result)}", "Очко крота", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
