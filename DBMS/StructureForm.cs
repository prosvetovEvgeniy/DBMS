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
    partial class StructureForm : Form
    {
        private Form1 owner;
        private string tableName;
        private Database db;
        private Table table;

        public StructureForm()
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

        public void setColumns()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            DataGridViewTextBoxColumn dgvTextBox = new DataGridViewTextBoxColumn();
            dgvTextBox.HeaderText = "Название";

            DataGridViewTextBoxColumn dgvTypes = new DataGridViewTextBoxColumn();
            dgvTypes.HeaderText = "Тип";

            DataGridViewCheckBoxColumn dgvNotNullCheckBox = new DataGridViewCheckBoxColumn();
            dgvNotNullCheckBox.HeaderText = Description.DEFAULT_NULL;

            DataGridViewCheckBoxColumn dgvIndexCheckBox = new DataGridViewCheckBoxColumn();
            dgvIndexCheckBox.HeaderText = Description.INDEX;

            DataGridViewCheckBoxColumn dgvPK = new DataGridViewCheckBoxColumn();
            dgvPK.HeaderText = Description.PK;

            dataGridView1.Columns.AddRange(dgvTextBox, dgvTypes, dgvNotNullCheckBox, dgvIndexCheckBox, dgvPK);

            Table table = db.getTableByName(tableName);

            for (int i = 0; i < table.CountFields; i++)
            {
                Description description = table.getFieldByIndex(i);

                dataGridView1.Rows.Add(description.FieldName, description.FieldType, description.DefaultNULL, description.Index, description.PrimaryKey);
            }
        }

        private void setTable()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddFieldForm addForm = new AddFieldForm();
            addForm.Owner = this;
            addForm.initOwner();
            addForm.ShowDialog();
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
            owner.setTable();
            Close();
        }

        private void StructureForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                int numField = dataGridView1.CurrentRow.Index;

                string fieldName = dataGridView1[0, numField].Value.ToString();

                if (!table.fieldHasConnection(fieldName))
                {
                    table.removeField(fieldName);
                    owner.setTable();
                }
                else
                {
                    MessageBox.Show("Ошибка, поле связано с другой таблицей");
                }

                setColumns();
            }
            else
            {
                MessageBox.Show("Отсутствуют поля для удаления");
            }
        }

        /*(private void button4_Click(object sender, EventArgs e)
        {

            /*if(dataGridView1.RowCount > 0)
            {
                int numField = dataGridView1.CurrentRow.Index;

                string fieldName = dataGridView1[0, numField].Value.ToString();

                ChangeFieldForm addForm = new ChangeFieldForm(fieldName);
                addForm.Owner = this;
                addForm.initOwner();
                addForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Отсутствуют поля для изменения");
            }
        }*/
    }
}
