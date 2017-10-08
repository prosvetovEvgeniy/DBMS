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
    public partial class AddFieldForm : Form
    {
        private StructureForm owner;
        private string tableName;
        private Database db;
        private Table table;

        public AddFieldForm()
        {
            InitializeComponent();
        }

        public void initOwner()
        {
            owner = this.Owner as StructureForm;

            db = owner.getDb();
            tableName = owner.getTableName();

            table = db.getTableByName(tableName);

            setColumns();
        }

        private void setColumns()
        {
            comboBox1.Items.AddRange(Description.getTypesList().ToArray());
            comboBox1.SelectedIndex = 0;


            if(table.CountRows > 0) {

                checkBox1.Checked = true;

            }
            else
            {
                checkBox1.Checked = false;
            }

            if (table.hasPK())
            {
                checkBox3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fieldName = textBox1.Text;
            string type = comboBox1.SelectedItem.ToString();

            bool defaultNull = Convert.ToBoolean(checkBox1.CheckState);
            bool isIndex = Convert.ToBoolean(checkBox2.CheckState);
            bool pk = Convert.ToBoolean(checkBox3.CheckState);

            if (table.fieldNameFree(fieldName))
            {
                table.addField(fieldName, type, defaultNull, isIndex, pk);

                owner.setColumns();
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка, имя для поля уже занято");
            }
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(checkBox3.CheckState))
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;

                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
            }
        }
    }
}
