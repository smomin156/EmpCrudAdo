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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DBLibrary;

namespace EmpCrudAdo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmpDataStore dataStore;
        public MainWindow()
        {
            InitializeComponent();
            dataStore = new EmpDataStore(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emp = new Employee();
                emp.EmpNo = Convert.ToInt32(txtEmpNo.Text);
                emp.EmpName = string.IsNullOrEmpty((txtEmpName.Text)) ? null : txtEmpName.Text;
                emp.HireDate = string.IsNullOrEmpty(txtHiredate.Text) ? (DateTime?)null : Convert.ToDateTime(txtHiredate.Text);
                emp.Salary = string.IsNullOrEmpty(txtSalary.Text) ? (decimal?)null : Convert.ToDecimal(txtSalary.Text);
                int result = dataStore.InsertEmp(emp);
                if (result != 0)
                {
                    MessageBox.Show("Insert Successful");
                }
                else
                {
                   MessageBox.Show("Insert not Successful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emp = new Employee();
                emp.EmpNo = Convert.ToInt32(txtEmpNo.Text);
                emp.EmpName = string.IsNullOrEmpty((txtEmpName.Text)) ? null : txtEmpName.Text;
                emp.HireDate = string.IsNullOrEmpty(txtHiredate.Text) ? (DateTime?)null : Convert.ToDateTime(txtHiredate.Text);
                emp.Salary = string.IsNullOrEmpty(txtSalary.Text) ? (decimal?)null : Convert.ToDecimal(txtSalary.Text);
                int result = dataStore.UpdateEmp(emp);
                if (result != 0)
                {
                    MessageBox.Show("Update Successful");
                }
                else
                {
                    MessageBox.Show("Update not Successful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Are you Sure to delete?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (boxResult == MessageBoxResult.Yes)
            {
                try
                {
                    int result = dataStore.DeleteEmp(Convert.ToInt32(txtEmpNo.Text));
                    MessageBox.Show(result + "rows affected");
                    if (result != 0)
                    {
                        MessageBox.Show("delete Successful");
                    }
                    else
                    {
                        MessageBox.Show("delete not Successful");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            LoadData();
            txtEmpNo.Text = string.Empty;
            txtEmpName.Text = string.Empty;
            txtHiredate.Text = string.Empty;
            txtSalary.Text = string.Empty;

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emp = dataStore.GetEmpById(Convert.ToInt32(txtEmpNo.Text));
                if (emp == null)
                {
                    MessageBox.Show("Empno does not exist");
                }
                else
                {
                    txtEmpName.Text = emp.EmpName;
                    txtHiredate.Text = emp.HireDate.ToString();
                    txtSalary.Text = emp.Salary.ToString();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtEmpNo.Text = string.Empty;
            txtEmpName.Text = string.Empty;
            txtHiredate.Text = string.Empty;
            txtSalary.Text = string.Empty;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadData()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
            string sql = "select * from emp";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet,"emp");
            EmpDataGrid.DataContext = dataSet.Tables["emp"];
        }

        private void Grid1_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
