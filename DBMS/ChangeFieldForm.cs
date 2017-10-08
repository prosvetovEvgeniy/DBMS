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
    public partial class ChangeFieldForm : Form
    {
        private StructureForm owner;
        private string tableName;
        private Database db;
        private Table table;
        private string fieldName;

        public ChangeFieldForm(string fieldName)
        {
            InitializeComponent();

            this.fieldName = fieldName;
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
            Description field = table.getFieldByName(fieldName);

            textBox1.Text = field.FieldName;
            comboBox1.Items.AddRange(Description.getTypesList().ToArray());

            //устанавливаем типы в comboBox
            for(int i = 0; i < comboBox1.Items.Count; i++)
            {
                if(comboBox1.Items[i].ToString() == field.FieldType)
                {
                    comboBox1.SelectedIndex = i;
                }
            }

            if (field.DefaultNULL)
            {
                checkBox1.Checked = true;
            }

            if (field.Index)
            {
                checkBox2.Checked = true;
            }

            if (table.hasPK())
            {
                if (field.PrimaryKey)
                {
                    checkBox3.Checked = true;
                }
                else
                {
                    checkBox3.Enabled = false;
                }
            }
            else
            {
                checkBox3.Checked = false;
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
                

                owner.setColumns();
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка, имя для поля уже занято");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
