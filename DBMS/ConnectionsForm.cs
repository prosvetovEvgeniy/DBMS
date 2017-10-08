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
    partial class ConnectionsForm : Form
    {
        private Form1 owner;
        private string tableName;
        private Database db;
        private Table table;

        public ConnectionsForm()
        {
            InitializeComponent();
        }

        public void initOwner()
        {
            owner = this.Owner as Form1;

            db = owner.getDb();
            tableName = owner.getTableName();
            table = db.getTableByName(tableName);

            label1.Text = table.TableName;

            setColumns();
        }

        public void setColumns()
        {
            dataGridView1.Columns.Clear();

            Table table = db.getTableByName(tableName);

            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Name = "Поле";
            dataGridView1.Columns[1].Name = "Название таблицы";
            dataGridView1.Columns[2].Name = "Ссылающ. таблица";
            dataGridView1.Columns[3].Name = "Ссылающ. поле";

            for (int i = 0; i < table.CountConnections; i++)
            {
                Connection connection = table.getConnectionByIndex(i);
                if (connection.IsSlave)
                {
                    dataGridView1.Rows.Add(connection.getConnectionsAsList().ToArray());
                }

                dataGridView1.ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (table.hasFieldsWithoutForeignKey())
            {
                AddConnectionForm addForm = new AddConnectionForm();
                addForm.Owner = this;
                addForm.initOwner();
                addForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("У таблицы не осталось полей без связей");
            }
        }

        public Database getDb()
        {
            return db;
        }

        public string getTableName()
        {
            return tableName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            if(dataGridView1.RowCount > 0)
            {
                int numConnection = dataGridView1.CurrentRow.Index;

                string columnName = dataGridView1[0, numConnection].Value.ToString();
                string tableName = dataGridView1[1, numConnection].Value.ToString();
                string linkedColumnName = dataGridView1[2, numConnection].Value.ToString();
                string linkedTableName = dataGridView1[3, numConnection].Value.ToString();

                
                db.deleteConnection(columnName, tableName, linkedTableName, linkedColumnName);


                setColumns();
            }
            else
            {
                MessageBox.Show("Отсутствуют связи для удаления");
            }
        }
    }
}
