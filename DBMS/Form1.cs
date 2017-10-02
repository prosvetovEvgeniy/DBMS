using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DBMS.Core.Manager;
using DBMS.Core.OwnTypes;

namespace DBMS
{
    partial class Form1 : Form
    {
        private DbManager dbManager; //файловый менеджер
        private TableManager tableManager; //менеджер для работы с таблицами
        private string currentDbName; //название выбранной бд
        private string currentTableName; //название выбранной таблицы
        private Database db;

        public Form1()
        {

            InitializeComponent();
            dbManager = new DbManager();
            tableManager = new TableManager();
            db = new Database();

            setDatabases();
        }

        //загружает названия сущ. бд в comboBox
        private void setDatabases()
        {
            List<string> dbs = dbManager.getDatabases();
            foreach (var db in dbs)
            {
                comboBox1.Items.Add(db);
            }
            if (dbs.Count > 0)
            {
                this.currentDbName = dbs[0];
                comboBox1.SelectedItem = this.currentDbName;
            }
        }

        //загружает таблицы
        private void setTables(string dbName)
        {

            db.clearTables();
            db.setDbName(dbName);

            listBox1.Enabled = true;
            listBox1.Items.Clear();

            List<string> tableNames = dbManager.getTables(this.currentDbName);

            foreach (string name in tableNames) {
                this.db.addTable(new Table(name, this.currentDbName));
            }

            if (db.CountTables == 0)
            {
                listBox1.Enabled = false;
            }
            //заполняем таблицу столбцами
            for (int i = 0; i < db.CountTables; i++)
            {
                listBox1.Items.Add(db.getTableByIndex(i).TableName);
            }

            if (db.CountTables != 0)
            {
                listBox1.SelectedIndex = 0;
            }

            //заполням таблицу данными
            for (int i = 0; i < db.CountTables; i++)
            {
                Table table = db.getTableByIndex(i);

                List<Description> fields = tableManager.getFields(this.currentDbName, table.TableName);
                List<Row> lines = tableManager.getTableData(this.currentDbName, table.TableName);
                List<Connection> connections = tableManager.getTableConnections(this.currentDbName, table.TableName);

                table.setFields(fields);
                table.setRows(lines);
                table.setConnections(connections);
            }
            

        }

        //срабатывает при изменении базы данных
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.currentDbName = comboBox1.SelectedItem.ToString();
            setTables(this.currentDbName);
        }

        //срабатывает при изменении таблицы
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.currentTableName = listBox1.SelectedItem.ToString();
            setTable();
        }

        public void setTable()
        {
            dataGridView1.Columns.Clear();

            Table table = db.getTableByName(this.currentTableName);

            dataGridView1.ColumnCount = table.CountFields;

            for (int i = 0; i < table.CountFields; i++)
            {
                dataGridView1.Columns[i].Name = table.getFieldByIndex(i).FieldName;
            }

            for (int i = 0; i < table.CountRows; i++)
            {
                string[] content = table.getRowByIndex(i).getContent().ToArray();
                dataGridView1.Rows.Add(content);
            }

            dataGridView1.ReadOnly = true;
        }
        private void consoleWrite(string str)
        {
            richTextBox1.Text += str;
        }

        private void consoleWriteLine(string str)
        {
            richTextBox1.Text += str + "\n";
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            ChangeForm changeForm = new ChangeForm();
            changeForm.Owner = this;
            changeForm.initOwner();
            changeForm.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Table table = db.getTableByName(currentTableName);
            int numRow = dataGridView1.CurrentRow.Index;

            if (table.CountRows != 0)
            {
                string check = db.checkConnectionsOnDelete(table.TableName, numRow);

                if (check == "")
                {
                    table.removeRowByIndex(numRow);
                    table.save();
                    setTable();
                }
                else
                {
                    MessageBox.Show(check);
                }
            }
        }

        public Database getDb()
        {
            return db;
        }

        public string getTableName()
        {
            return currentTableName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Owner = this;
            addForm.initOwner();
            addForm.ShowDialog();
        }

        private void структураToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StructureForm addForm = new StructureForm();
            addForm.Owner = this;
            addForm.initOwner();
            addForm.ShowDialog();
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionsForm addForm = new ConnectionsForm();
            addForm.Owner = this;
            addForm.initOwner();
            addForm.ShowDialog();
        }
    }
}
