using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        List<Todo> todo = new List<Todo>();
        private DBConnect DB = new DBConnect();

        public Form1()
        {
            InitializeComponent();
            UpdateBinding();
        }

        private void UpdateBinding()
        {
            todo = DB.Select();
            dataGridView1.DataSource = todo;
            //dataGridView1.DataMember = "Extended";
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["task"].HeaderText= "Compito";
            dataGridView1.Columns["task"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["done"].HeaderText = "Stato";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var SelectedTask = dataGridView1.CurrentRow.DataBoundItem as Todo;
            textBox1.Text = SelectedTask.task;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var SelectedTask = dataGridView1.CurrentRow.DataBoundItem as Todo;
            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                if (dataGridView1.CurrentCell.Value is true)
                {
                    DB.Done(SelectedTask.task, 0);
                }
                else
                {
                    DB.Done(SelectedTask.task, 1);
                }
                UpdateBinding();

            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") {
                MessageBox.Show("Devi inserire qualcosa nel campo di testo!", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string Task = textBox1.Text;
                textBox1.Text = null;
                DB.Insert(Task);

                UpdateBinding();
            }

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Devi inserire qualcosa nel campo di testo!", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                DialogResult result = MessageBox.Show("Sei sicuro di voler modificare il compito?", "Avviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var SelectedTask = dataGridView1.CurrentRow.DataBoundItem as Todo;
                    DB.Update(SelectedTask.task, textBox1.Text);

                    UpdateBinding();
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                DialogResult result = MessageBox.Show("Sei sicuro di voler eliminare il compito?", "Avviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var SelectedTask = dataGridView1.CurrentRow.DataBoundItem as Todo;
                    DB.Delete(SelectedTask.task);

                    UpdateBinding();
                }
            }
            else
            {
                MessageBox.Show("Seleziona una casella di testo valida", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.github.com/RossDiliberto");
        }
    }
}