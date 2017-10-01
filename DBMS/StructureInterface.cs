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
    public partial class StructureInterface : Form
    {
        private Form1 owner;
        private string tableName;
        private Database db;

        public StructureInterface()
        {
            InitializeComponent();
        }

        public void initOwner()
        {
            owner = this.Owner as Form1;

            db = owner.getDb();
            db.clearTables();
            setColumns();
        }

        private void setColumns()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            owner.setTable();
        }
    }
}
