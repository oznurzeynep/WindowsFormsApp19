using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp19
{
    public partial class NewOrder : Form
    {
        public NewOrder()
        {
            InitializeComponent();
            dataGridView1.RowTemplate.Height = 75;
            getMenu();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\receipt\";
            if (textBox1.Text != "")
            {
                DirectoryInfo dir = new DirectoryInfo(appPath);
                foreach (FileInfo file in dir.GetFiles())
                {
                    if (textBox1.Text == file.Name.Split('.')[0])
                    {
                        file.Delete();


                    }
                }
            }
            textBox1.Text = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[3].Value = 0;
                row.Cells[4].Value = 0;
            }
        }

        private void getMenu()
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\menu\"; // <---

            DirectoryInfo dir = new DirectoryInfo(appPath);
            
            foreach (FileInfo file in dir.GetFiles())
            {
                DataGridViewImageColumn img = new DataGridViewImageColumn();
                Image image = Image.FromFile(file.FullName);

                MemoryStream ms = new MemoryStream();
                String amount = (file.Name.Split('-')[1]).Split(' ')[0];
                dataGridView1.Rows.Add(image, file.Name.Split('-')[0], amount);


            }



        }

        private void cellValueChangedClicked(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentRow != null)
            {
                decimal amount = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[2].Value);
                decimal unit = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[3].Value);
                dataGridView1.CurrentRow.Cells[4].Value = unit * amount;
                decimal totalAmount = 0;
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    totalAmount = totalAmount  + Convert.ToDecimal(row.Cells[4].Value);
                }

                textBox2.Text = totalAmount.ToString();

            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[3].Value = 0;
                    row.Cells[4].Value = 0;
                }
            }
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\receipt\";
            DirectoryInfo dir = new DirectoryInfo(appPath);
            foreach (FileInfo file in dir.GetFiles())
            {
                if (textBox1.Text == file.Name.Split('.')[0])
                {
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(textBox1.Text + " adındaki müşteriye ait ödenmemiş sipariş var. Öncesinde geçmiş siparişi ödeyiniz. Geçmiş Siparişi ödemek istiyor musunuz?", null, buttons);
                    if (result == DialogResult.Yes)
                    {
                        file.Delete();
                        MessageBox.Show("Eski borç ödenmiştir.");

                    }
                    else
                    {
                        textBox1.Text = "";
                        return;
                    }


                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Müşteri adı boş olamaz.");
                return;
            }
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\receipt\";
            DirectoryInfo dir = new DirectoryInfo(appPath);
            foreach (FileInfo file in dir.GetFiles())
            {
                if (textBox1.Text == file.Name.Split('.')[0])
                {
                    MessageBox.Show("Bu müşteriye ait ödenmemiş sipariş var.");
                    return;
                }
            }
           

            StreamWriter sw = File.CreateText(appPath + textBox1.Text + ".txt");
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ("0" != row.Cells[4].Value && "" != row.Cells[4].Value && row.Cells[4].Value != null && !"0,00".Equals(row.Cells[4].Value))
                {
                    sw.WriteLine(row.Cells[1].Value + ";" + row.Cells[2].Value + ";" + row.Cells[3].Value + ";" + row.Cells[4].Value);
                }

            }
            sw.Close();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[3].Value = 0;
                row.Cells[4].Value = 0;
            }

            textBox1.Text = "";

            MessageBox.Show("Şiparişiniz sonra ödenmek üzere kaydedildi.");
        }
    }
    }

