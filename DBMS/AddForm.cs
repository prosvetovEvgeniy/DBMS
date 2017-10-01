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

            //устанавливаем столбцы для datagridview
            for (int i = 0; i < table.CountFields; i++)
            {
                string fieldName = table.getFieldByIndex(i).FieldName;

                //если какое то поле зависмое от другой таблицы делаем combobox
                if (table.checkFieldHasSlaveConnection(fieldName))
                {
                    DataGridViewComboBoxColumn dgvComboBox = new DataGridViewComboBoxColumn();
                    dgvComboBox.HeaderText = fieldName;

                    Connection connection = table.getConnectionByColumnName(fieldName);
                    List<string> dataByColumn = owner.db.getDataByColumnFromMasterTable(connection.LinkedTableName, connection.LindkedColumn);

                    dgvComboBox.Items.AddRange(dataByColumn.ToArray());

                    dataGridView1.Columns.Add(dgvComboBox);
                }
                else
                {
                    DataGridViewTextBoxColumn dgvTextBox = new DataGridViewTextBoxColumn();
                    dgvTextBox.HeaderText = fieldName;
                    dataGridView1.Columns.Add(dgvTextBox);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*List<List<string>> rows = new List<List<string>>();

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
            }*/

            List<List<string>> rows = new List<List<string>>();

            //считываем данные из datagridview в матрицу
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                List<string> row = new List<string>();
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    Type type = dataGridView1.Rows[i].Cells[j].GetType();
                    var cell = dataGridView1.Rows[i].Cells[j];

                    if (type.Name == "DataGridViewTextBoxCell")
                    {
                        if(cell.Value == null)
                        {
                            row.Add("");
                        }
                        else
                        {
                            row.Add((cell as DataGridViewTextBoxCell).FormattedValue.ToString());
                        }
                    }
                    else if (type.Name == "DataGridViewComboBoxCell")
                    {
                        row.Add((cell as DataGridViewComboBoxCell).FormattedValue.ToString());
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
