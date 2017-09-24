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
    public partial class Form1 : Form
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
        private void setTable(string dbName)
        {

            db.clearTables();
            listBox1.Enabled = true;
            listBox1.Items.Clear();

            List<string> tableNames = dbManager.getTables(this.currentDbName);

            foreach(string name in tableNames){
                this.db.addTable(new Table(name));
            }

            if (db.CountTables == 0)
            {
                listBox1.Enabled = false;
            }

            for (int i = 0; i < db.CountTables; i++)
            {
                listBox1.Items.Add(db.getTableByIndex(i).TableName);
            }

            listBox1.SelectedIndex = 0;

            for(int i = 0; i < db.CountTables; i++)
            {
                Table table = db.getTableByIndex(i);

                List<Description> fields = tableManager.getFields(this.currentDbName, table.TableName);
                List<Line> lines = tableManager.getTableData(this.currentDbName, table.TableName);

                table.setFields(fields);
                table.setLines(lines);
            }

        }

        //срабатывает при изменении базы данных
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.currentDbName = comboBox1.SelectedItem.ToString();
            setTable(this.currentDbName);
        }

        //срабатывает при изменении таблицы
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.currentTableName = listBox1.SelectedItem.ToString();
            setTable();
        }

        private void setTable()
        {
            dataGridView1.Columns.Clear();

            Table table = db.getTableByName(this.currentTableName);

            dataGridView1.ColumnCount = table.CountFields;

            for (int i = 0; i < table.CountFields; i++)
            {
                dataGridView1.Columns[i].Name = table.getFieldByIndex(i).FieldName;
            }

            for(int i = 0; i < table.CountLines; i++)
            {
                string[] content = table.getLineByIndex(i).getContent().ToArray();
                dataGridView1.Rows.Add(content);
            }

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

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
