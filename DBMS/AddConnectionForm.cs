﻿using System;
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
    public partial class AddConnectionForm : Form
    {
        private ConnectionsForm owner;
        private string tableName;
        private Database db;
        private Table table;

        public AddConnectionForm()
        {
            InitializeComponent();
        }

        public void initOwner()
        {
            owner = this.Owner as ConnectionsForm;

            db = owner.getDb();
            tableName = owner.getTableName();
            table = db.getTableByName(tableName);

            setComboBoxes();
        }

        private void setComboBoxes()
        {
            List<string> fieldsWithoutConnections = table.getFieldsWithoutConnections();

            for (int i = 0; i < fieldsWithoutConnections.Count; i++)
            {
                comboBox1.Items.Add(fieldsWithoutConnections[i]);
            }

            List<string> tableNamesWithPK = db.getTableNamesWithPK();

            for(int i = 0; i < tableNamesWithPK.Count; i++)
            {
                comboBox2.Items.Add(tableNamesWithPK[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
            {
                string columnName = comboBox1.Text;
                string linkedTableName = comboBox2.Text;
                string linkedColumnName = comboBox3.Text;

                bool verify = db.verifyСonnectivity(columnName, table.TableName, linkedColumnName, linkedTableName);

                if (verify)
                {
                    db.connectTables(columnName, table.TableName, linkedColumnName, linkedTableName);
               
                    owner.setColumns();
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();

            string selectedTableName = comboBox2.SelectedItem.ToString();

            comboBox3.Items.AddRange(db.getTableByName(selectedTableName).getSuitableFieldsForConnections().ToArray());

            if(comboBox3.Items.Count > 0)
            {
                comboBox3.SelectedIndex = 0;
            }
            else
            {
                throw new Exception("У таблицые нет подходящих полей для связывания");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
