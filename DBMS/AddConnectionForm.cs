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

        }
    }
}
