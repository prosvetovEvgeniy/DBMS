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

namespace DBMS
{
    public partial class Form1 : Form
    {
        private FileManager fm; //файловый менеджер
        private string currentDbName; //название выбранной бд
        private string currentTableName; //название выбранной таблицы

        public Form1()
        {
            InitializeComponent();

            fm = new FileManager();
            setDatabases();
        }
        //загружает названия сущ. бд в comboBox
        private void setDatabases()
        {
            List<string> dbs = fm.getDatabases();
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
        //загружает название таблиц listBox
        private void setTable(string dbName)
        {
            listBox1.Items.Clear();
            List<string> tables = fm.getTables(this.currentDbName);

            foreach(var table in tables)
            {
                listBox1.Items.Add(table);
            }
            if(tables.Count > 0)
            {
                this.currentDbName = tables[0];
            }
        }
        //срабатывает при изменении базы данных
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.currentDbName = comboBox1.SelectedItem.ToString();
            setTable(this.currentDbName);
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.currentTableName = listBox1.SelectedItem.ToString();
            
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
