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
using DBMS.Core;

namespace DBMS
{
    public partial class Form1 : Form
    {
        private FileManager fm;
        private string currentDb;
        private string currentTable;

        public Form1()
        {
            InitializeComponent();

            fm = new FileManager();

            setDatabases();
        }

        private void button1_Click(object sender, EventArgs e) { 

        }
        
        private void setDatabases()
        {
            List<string> dbs = fm.getDatabases();
            foreach (var db in dbs)
            {
                comboBox1.Items.Add(db);
            }
            if (dbs.Count > 0)
            {
                this.currentDb = dbs[0];
                comboBox1.SelectedItem = this.currentDb;
                setTable(this.currentDb);
            }
        }

        private void setTable(string dbName)
        {
            listBox1.Items.Clear();
            List<string> tables = fm.getTables(this.currentDb);

            foreach(var table in tables)
            {
                listBox1.Items.Add(table);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.currentDb = comboBox1.SelectedItem.ToString();
            setTable(this.currentDb);
        }
    }
}
