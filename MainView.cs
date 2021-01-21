using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
            InitComboboxes(2, 6, 1);
            y = new List<List<int>>();
            dataGridView1.AllowUserToAddRows = false;
        }

        private int n, m;
        private List<List<int>> y;
        private Pareto pareto;

        private void InitComboboxes(int minimum, int maximum, int step)
        {
            for (int i = minimum; i < maximum; i += step)
            {
                comboBox1.Items.Add(i);
                comboBox2.Items.Add(i);
            }
        }
        private void GetValuesFromGrid()
        {
            y = new List<List<int>>();
            for (int i = 0; i < m; i++)
            {
                var list = new List<int>();
                for (int j = 0; j < n; j++)
                    list.Add(Convert.ToInt32(dataGridView1[j, i].Value.ToString()));
                y.Add(list);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            y.Clear();

            if (y.Count == 0)
                GetValuesFromGrid();

            pareto = new Pareto();
            pareto.ind.Clear();
            pareto.GetParetoList(y);
            for(int i=0; i < pareto.ind.Count; i++)
            {
                dataGridView1.Rows.RemoveAt(pareto.ind[i]);
            }

            button3.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            n = Convert.ToInt32(comboBox1.Text);
            m = Convert.ToInt32(comboBox2.Text);
            dataGridView1.ColumnCount = n;
            dataGridView1.RowCount = m;

            for (int i = 0; i < n; i++)
            {
                dataGridView1.Columns[i].Name = "k" + (i + 1).ToString();
            }

            for (int i = 0; i < m; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = "Вариант №" + (i + 1).ToString();
            }

            button3.Enabled = true;
        }
        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                y = new File().ReadData(this.openFileDialog1.FileName);

                FillGridFromList(y);
            }
        }

        private void сохранитьКакToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (y.Count == 0)
                    GetValuesFromGrid();
                new File().WriteData(y, this.saveFileDialog1.FileName);
            }
        }

        // Random values
        private void button2_Click_1(object sender, EventArgs e)
        {
             Random random = new Random();
             for (int i = 0; i < m; i++)
             {
                 for (int j = 0; j < n; j++)
                 {
                     dataGridView1[j, i].Value = random.Next(20);
                 }
             }
        }

        private void FillGridFromList(List<List<int>> list)
        {
            n = list[0].Count;
            m = list.Count;
            dataGridView1.ColumnCount = n;
            dataGridView1.RowCount = m;
            comboBox1.Text = n.ToString();
            comboBox2.Text = m.ToString();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                    dataGridView1[j, i].Value = list[i][j];
            }
        }
    }
}
