using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = null;
        private string strCon = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Usuario;Data Source=localhost\SQLEXPRESS";
        private string strSql = string.Empty;


        private void btnCreate_Click(object sender, EventArgs e)
        {
            strSql = "EXEC CriarNovoModelo @Marca, @Modelo, @Ano, @Cor, @Valor, @Categoria";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@Marca", SqlDbType.VarChar).Value = tbMarca.Text;
            comando.Parameters.Add("@Modelo", SqlDbType.VarChar).Value = tbModelo.Text;
            comando.Parameters.Add("@Ano", SqlDbType.Int).Value = tbAno.Text;
            comando.Parameters.Add("@Cor", SqlDbType.VarChar).Value = tbCor.Text;
            comando.Parameters.Add("@Valor", SqlDbType.Int).Value = tbValor.Text;
            comando.Parameters.Add("@Categoria", SqlDbType.VarChar).Value = tbCategoria.Text;

            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro realizado com sucesso!");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                sqlCon.Close();
            }



        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            strSql = "SELECT * FROM EstoqueCarros WHERE Id=@Id ";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@Id", SqlDbType.Int).Value = tbId.Text;

            try
            {
                sqlCon.Open();
                SqlDataReader dr = comando.ExecuteReader();
                dr.Read();

                tbId.Text = Convert.ToString(dr["Id"]);
                tbMarca.Text = Convert.ToString(dr["Marca"]);
                tbAno.Text = Convert.ToString(dr["Ano"]);
                tbCor.Text = Convert.ToString(dr["Cor"]);
                tbValor.Text = Convert.ToString(dr["Valor"]);
                tbCategoria.Text = Convert.ToString(dr["Categoria"]);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
              /* if (tbId.Text == string.Empty)
                {
                   MessageBox.Show("Digite um ID");
                    tbId.SelectAll();
                    tbId.Focus();
                    return;
                }
               
                if (dr.HasRows == false)
                {
                    MessageBox.Show("Id nao cadastrado");
                    tbId.SelectAll();
                    tbId.Focus();
                    return;
                }
                sqlCon.Close();*/
            }

            finally { sqlCon.Close(); } 

        }
    }
}
