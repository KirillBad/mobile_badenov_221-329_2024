using SQLite;

namespace wfaSQLite
{
    public partial class Form1 : Form
    {
        private readonly SQLiteConnection db;

        public Form1()
        {
            InitializeComponent();

            db = new SQLiteConnection("myDB.db"); // либо подключение к базе данных, либо создание новой
            db.CreateTable<Log>();
            db.CreateTable<City>();
            db.CreateTable<User>();

            //db.Insert(new Log() { DateTime = DateTime.Now });
            db.Insert(new Log());
            lvLogs.Columns.Add("ДатаВремя", 250);
            lvLogs.View = View.Details;

            foreach (var log in db.Table<Log>())
            {
                lvLogs.Items.Add(log.DateTime.ToString());
            }

            buCityAdd.Click += (s, e) => db.Insert(new City() { Name = textBox1.Text });
            buCityShow.Click += (s, e) => dataGridView1.DataSource = db.Table<City>().ToList();
            buLogShow.Click += (s, e) => dataGridView1.DataSource = db.Table<Log>().ToList();

            buRunSQL.Click += (s, e) => MessageBox.Show(db.ExecuteScalar<int>(textBox2.Text).ToString());

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
