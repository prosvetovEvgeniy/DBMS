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

            DataGridViewTextBoxColumn dgvTableName = new DataGridViewTextBoxColumn();
            dgvTableName.HeaderText = "Название таблицы";

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
            dgvComboBoxLinkedTable.Items.AddRange(db.getTableNames().ToArray());

            dataGridView1.Columns.AddRange(dgvComboBoxColumn, dgvTableName, dgvComboBoxLinkedTable, dgvComboBoxLinkedColumn);

            //заполняем данными
            for (int i = 0; i < table.CountConnections; i++)
            {
                Connection connection = table.getConnectionByIndex(i);

                if (connection.IsSlave)
                {
                    List<string> list = connection.getConnectionsAsList();

                    dataGridView1.Rows.Add(connection.getConnectionsAsList().ToArray());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.RowCount++;
            bool b = table.hasFieldsWithoutForeignKey();
            int a = 5;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
