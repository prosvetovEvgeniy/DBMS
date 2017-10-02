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

        public ConnectionsForm()
        {
            InitializeComponent();
        }

        public void initOwner()
        {
            owner = this.Owner as Form1;

            db = owner.getDb();
            tableName = owner.getTableName();

            setColumns();
        }

        private void setColumns()
        {
            DataGridViewComboBoxColumn dgvComboBoxColumn = new DataGridViewComboBoxColumn();
            dgvComboBoxColumn.HeaderText = "Поле";

            DataGridViewTextBoxColumn dgvTableName = new DataGridViewTextBoxColumn();
            dgvTableName.HeaderText = "Название таблицы";

            DataGridViewComboBoxColumn dgvComboBoxLinkedTable = new DataGridViewComboBoxColumn();
            dgvComboBoxLinkedTable.HeaderText = "Ссылающ. таблица";

            DataGridViewComboBoxColumn dgvComboBoxLinkedColumn = new DataGridViewComboBoxColumn();
            dgvComboBoxLinkedColumn.HeaderText = "Ссылающ. поле";

            Table table = db.getTableByName(tableName);

            /*for(int i = 0; i < table.CountConnections; i++)
            {
                Connection connection = table.getConnectionByIndex(i);

                dgvComboBoxColumn.Items.AddRange(connection.Column);
            }*/

            dataGridView1.Columns.AddRange(dgvComboBoxColumn, dgvTableName, dgvComboBoxLinkedTable, dgvComboBoxLinkedColumn);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
