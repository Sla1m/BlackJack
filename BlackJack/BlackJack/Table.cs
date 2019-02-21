using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Table : Form
    {
        public Table()
        {
            InitializeComponent();
        }

        private void Table_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "profileDataSet.Info". При необходимости она может быть перемещена или удалена.
            this.infoTableAdapter.Fill(this.profileDataSet.Info);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
