namespace wfaSQLite
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lvLogs = new ListView();
            textBox1 = new TextBox();
            buCityAdd = new Button();
            buCityShow = new Button();
            dataGridView1 = new DataGridView();
            buLogShow = new Button();
            textBox2 = new TextBox();
            buRunSQL = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lvLogs
            // 
            lvLogs.Location = new Point(12, 12);
            lvLogs.Name = "lvLogs";
            lvLogs.Size = new Size(339, 426);
            lvLogs.TabIndex = 0;
            lvLogs.UseCompatibleStateImageBehavior = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(357, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(262, 27);
            textBox1.TabIndex = 1;
            // 
            // buCityAdd
            // 
            buCityAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buCityAdd.Location = new Point(625, 12);
            buCityAdd.Name = "buCityAdd";
            buCityAdd.Size = new Size(163, 29);
            buCityAdd.TabIndex = 2;
            buCityAdd.Text = "Добавить город ";
            buCityAdd.UseVisualStyleBackColor = true;
            // 
            // buCityShow
            // 
            buCityShow.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buCityShow.Location = new Point(407, 57);
            buCityShow.Name = "buCityShow";
            buCityShow.Size = new Size(323, 29);
            buCityShow.TabIndex = 3;
            buCityShow.Text = "Показать ВСЕ города в таблице";
            buCityShow.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(381, 132);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(432, 195);
            dataGridView1.TabIndex = 4;
            // 
            // buLogShow
            // 
            buLogShow.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buLogShow.Location = new Point(407, 92);
            buLogShow.Name = "buLogShow";
            buLogShow.Size = new Size(323, 29);
            buLogShow.TabIndex = 5;
            buLogShow.Text = "Показать ВСЕ логи в таблице";
            buLogShow.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(381, 343);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(223, 75);
            textBox2.TabIndex = 6;
            textBox2.Text = "select count(id) from cities";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // buRunSQL
            // 
            buRunSQL.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buRunSQL.Location = new Point(610, 343);
            buRunSQL.Name = "buRunSQL";
            buRunSQL.Size = new Size(153, 75);
            buRunSQL.TabIndex = 7;
            buRunSQL.Text = "Run";
            buRunSQL.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(825, 466);
            Controls.Add(buRunSQL);
            Controls.Add(textBox2);
            Controls.Add(buLogShow);
            Controls.Add(dataGridView1);
            Controls.Add(buCityShow);
            Controls.Add(buCityAdd);
            Controls.Add(textBox1);
            Controls.Add(lvLogs);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvLogs;
        private TextBox textBox1;
        private Button buCityAdd;
        private Button buCityShow;
        private DataGridView dataGridView1;
        private Button buLogShow;
        private TextBox textBox2;
        private Button buRunSQL;
    }
}
