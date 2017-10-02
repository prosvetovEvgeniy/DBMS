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
    public partial class ConnectionsForm : Form
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

            setColumns();
        }

        private void setColumns()
        {
            //создаем поля datagridview
            DataGridViewComboBoxColumn dgvComboBoxColumn = new DataGridViewComboBoxColumn();
            dgvComboBoxColumn.HeaderText = "Поле";


            DataGridViewComboBoxColumn dgvComboBoxLinkedTable = new DataGridViewComboBoxColumn();
            dgvComboBoxLinkedTable.HeaderText = "Ссылающ. таблица";

            DataGridViewComboBoxColumn dgvComboBoxLinkedColumn = new DataGridViewComboBoxColumn();
            dgvComboBoxLinkedColumn.HeaderText = "Ссылающ. поле";

            //считываем поле внешнего ключа
            for(int i = 0; i < table.CountConnections; i++)
            {
                Connection connection = table.getConnectionByIndex(i);

                if (connection.IsSlave)
                {
                    dgvComboBoxColumn.Items.AddRange(connection.Column);
                    dgvComboBoxLinkedColumn.Items.Add(connection.LindkedColumn);
                }
            }
            //получаем список всех таблиц

            dgvComboBoxLinkedTable.Items.AddRange(db.getTableNamesWithPK().ToArray());

            dataGridView1.Columns.AddRange(dgvComboBoxColumn, dgvComboBoxLinkedTable, dgvComboBoxLinkedColumn);

            //заполняем данными
            for (int i = 0; i < table.CountConnections; i++)
            {
                Connection connection = table.getConnectionByIndex(i);

                if (connection.IsSlave)
                {
                    List<string> list = connection.getConnectionsAsList();

                    string column = connection.Column;
                    string linkedTable = connection.LinkedTableName;
                    string linkedColumn = connection.LindkedColumn;

                    dataGridView1.Rows.Add(column, linkedTable, linkedColumn);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (table.hasFieldsWithoutForeignKey())
            {
                dataGridView1.RowCount++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
