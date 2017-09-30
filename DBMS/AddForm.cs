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
using DBMS.Core.Checker;

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
            List<List<string>> rows = new List<List<string>>();

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                List<string> row = new List<string>();
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if(dataGridView1[j, i].Value == null)
                    {
                        row.Add("");
                    }
                    else
                    {
                        row.Add(dataGridView1[j, i].Value.ToString());
                    }
                    
                }
                rows.Add(row);
            }

            Table table = owner.db.getTableByName(owner.currentTableName);

            if (TypeChecker.checkTypes(rows, table))
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    owner.db.getTableByName(owner.currentTableName).addRow(rows[i]);
                }
                owner.db.getTableByName(owner.currentTableName).save();
                owner.setTable();
                Close();
            }
            else
            {
                MessageBox.Show("Данные не совпадают с типами!");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
