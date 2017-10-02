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
    public partial class StructureForm : Form
    {
        private Form1 owner;
        private string tableName;
        private Database db;

        public StructureForm()
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
            DataGridViewTextBoxColumn dgvTextBox = new DataGridViewTextBoxColumn();
            dgvTextBox.HeaderText = "Название";

            DataGridViewComboBoxColumn dgvComboBox = new DataGridViewComboBoxColumn();
            dgvComboBox.HeaderText = "Тип";
            dgvComboBox.Items.AddRange("integer", "string");

            dataGridView1.Columns.AddRange(dgvTextBox, dgvComboBox);

            Table table = db.getTableByName(tableName);

            for (int i = 0; i < table.CountFields; i++)
            {
                Description description = table.getFieldByIndex(i);

                dataGridView1.Rows.Add(description.FieldName, description.FieldType);
            }
        }

        private void setTable()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            owner.setTable();
        }
    }
}
