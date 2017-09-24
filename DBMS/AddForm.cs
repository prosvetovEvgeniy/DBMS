using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBMS.Core.OwnTypes;

namespace DBMS
{
    public partial class AddForm : Form
    {
        private Form1 owner;
        public AddForm()
        {
            InitializeComponent();
        }

        public void initOwner()
        {
            owner = this.Owner as Form1;
            setTable();
        }

        private void setTable()
        {
            dataGridView1.Columns.Clear();
            
            Table table = owner.db.getTableByName(owner.currentTableName);

            dataGridView1.ColumnCount = table.CountFields;

            for (int i = 0; i < table.CountFields; i++)
            {
                dataGridView1.Columns[i].Name = table.getFieldByIndex(i).FieldName + " (" + table.getFieldByIndex(i).FieldType + ")";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int amountRows = dataGridView1.RowCount - 1;
            int amountColumns = dataGridView1.ColumnCount;

            //string[,] rows = new string[amountRows, amountColumns];

            List<List<string>> rows = new List<List<string>>();

            for (int i = 0; i < amountRows; i++)
            {
                List<string> row = new List<string>();
                for (int j = 0; j < amountColumns; j++)
                {
                    row.Add(dataGridView1[j, i].Value.ToString());
                    //rows[i, j] = dataGridView1[j,i].Value.ToString();
                }
                rows.Add(row);
            }
            /*for(int i = 0; i < amountRows; i++)
            {
                string[] row = new string[amountColumns];
                for(int j = 0; j < amountColumns; j++)
                {
                    row[j] = rows[i, j];
                }
                owner.db.getTableByName(owner.currentTableName).addRow(row);
            }*/

            for (int i = 0; i < rows.Count; i++)
            {
                owner.db.getTableByName(owner.currentTableName).addRow(rows[i]);
            }

            owner.setTable();
            Close();
        }
        
        private bool checkDataTypes()
        {
            return true;
        }
    }
}
