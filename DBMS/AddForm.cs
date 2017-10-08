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
        private Database db;
        private string tableName;

        public AddForm()
        {
            InitializeComponent();
        }

        public void initOwner()
        {
            owner = this.Owner as Form1;

            db = owner.getDb();
            tableName = owner.getTableName();
        
            setTable();
        }

        private void setTable()
        {
            dataGridView1.Columns.Clear();
            Table table = db.getTableByName(tableName);

            //устанавливаем столбцы для datagridview
            for (int i = 0; i < table.CountFields; i++)
            {
                Description field = table.getFieldByIndex(i);

                //если какое то поле зависмое от другой таблицы делаем combobox
                if (table.checkFieldHasSlaveConnection(field.FieldName))
                {
                    DataGridViewComboBoxColumn dgvComboBox = new DataGridViewComboBoxColumn();
                    dgvComboBox.HeaderText = field.FieldName;

                    Connection connection = table.getConnectionByColumnName(field.FieldName);
                    List<string> dataByColumn = db.getDataByColumnFromMasterTable(connection.LinkedTableName, connection.LindkedColumn);

                    if (field.DefaultNULL)
                    {
                        dgvComboBox.Items.Add("");
                        dgvComboBox.Items.AddRange(dataByColumn.ToArray());
                    }
                    else
                    {
                        dgvComboBox.Items.AddRange(dataByColumn.ToArray());
                    }

                    dataGridView1.Columns.Add(dgvComboBox);
                }
                else
                {
                    DataGridViewTextBoxColumn dgvTextBox = new DataGridViewTextBoxColumn();
                    dgvTextBox.HeaderText = field.FieldName;
                    dataGridView1.Columns.Add(dgvTextBox);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

            Table table = db.getTableByName(tableName);

            if (TypeChecker.checkTypes(rows, table))
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    db.getTableByName(tableName).addRow(rows[i]);
                }
                db.getTableByName(tableName).saveRowsInFile();
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
