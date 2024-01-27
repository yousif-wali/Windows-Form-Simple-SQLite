namespace Sqlite
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Database.Get(dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView1.CurrentRow.Selected = true;
                    idText.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                    nameText.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                }
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM test WHERE id = '{idText.Text}' AND Name = '{nameText.Text}' limit 1";
            Database.Query(sql, dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO test Values ('{idText.Text}', '{nameText.Text}')";
            Database.Query(sql, dataGridView1);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}