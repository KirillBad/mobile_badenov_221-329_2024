namespace Explorer5000
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            treeView1 = new TreeView();
            imageList1 = new ImageList(components);
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            splitter1 = new Splitter();
            textBoxPath = new TextBox();
            textBoxSearch = new TextBox();
            panelTop = new Panel();
            pictureBox1 = new PictureBox();
            labelFolderItems = new Label();
            labelFolderSize = new Label();
            labelDriveSize = new Label();
            labelDriveName = new Label();
            labelDriveLetter = new Label();
            progressBarTop = new ProgressBar();
            pictureBoxFolder = new PictureBox();
            panel1 = new Panel();
            contextMenuStrip1 = new ContextMenuStrip(components);
            deleteToolStripMenuItem = new ToolStripMenuItem();
            newFolderToolStripMenuItem = new ToolStripMenuItem();
            newFileToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip2 = new ContextMenuStrip(components);
            deleteToolStripMenuItem2 = new ToolStripMenuItem();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxFolder).BeginInit();
            panel1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.AllowDrop = true;
            treeView1.Dock = DockStyle.Left;
            treeView1.ImageIndex = 0;
            treeView1.ImageList = imageList1;
            treeView1.LabelEdit = true;
            treeView1.Location = new Point(0, 0);
            treeView1.Margin = new Padding(5);
            treeView1.MinimumSize = new Size(100, 450);
            treeView1.Name = "treeView1";
            treeView1.SelectedImageIndex = 0;
            treeView1.Size = new Size(150, 450);
            treeView1.TabIndex = 0;
            treeView1.AfterLabelEdit += treeView1_AfterLabelEdit;
            treeView1.BeforeExpand += treeView1_BeforeExpand;
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.DragDrop += treeView1_DragDrop;
            treeView1.DragEnter += treeView1_DragEnter;
            treeView1.MouseClick += treeView1_MouseClick;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "blueFolder.png");
            imageList1.Images.SetKeyName(1, "exeIcon.png");
            imageList1.Images.SetKeyName(2, "fileIcon.png");
            imageList1.Images.SetKeyName(3, "imageIcon.png");
            imageList1.Images.SetKeyName(4, "zipIcon.png");
            imageList1.Images.SetKeyName(5, "txtIcon.png");
            imageList1.Images.SetKeyName(6, "presentationIcon.png");
            imageList1.Images.SetKeyName(7, "tableIcon.png");
            imageList1.Images.SetKeyName(8, "musicIcon.png");
            imageList1.Images.SetKeyName(9, "videoIcon.png");
            // 
            // listView1
            // 
            listView1.AllowDrop = true;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            listView1.Dock = DockStyle.Fill;
            listView1.LabelEdit = true;
            listView1.Location = new Point(155, 25);
            listView1.Margin = new Padding(5);
            listView1.Name = "listView1";
            listView1.Size = new Size(614, 425);
            listView1.SmallImageList = imageList1;
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.AfterLabelEdit += listView1_AfterLabelEdit;
            listView1.ItemDrag += listView1_ItemDrag;
            listView1.DoubleClick += listView1_DoubleClick;
            listView1.MouseClick += listView1_MouseClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "File Name";
            columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Size";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Modified";
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Attr";
            // 
            // splitter1
            // 
            splitter1.BackColor = SystemColors.Control;
            splitter1.Cursor = Cursors.VSplit;
            splitter1.Location = new Point(150, 0);
            splitter1.Margin = new Padding(5);
            splitter1.MinExtra = 10;
            splitter1.MinSize = 5;
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(5, 450);
            splitter1.TabIndex = 2;
            splitter1.TabStop = false;
            // 
            // textBoxPath
            // 
            textBoxPath.BackColor = SystemColors.Window;
            textBoxPath.Dock = DockStyle.Left;
            textBoxPath.Location = new Point(0, 0);
            textBoxPath.Margin = new Padding(5);
            textBoxPath.Name = "textBoxPath";
            textBoxPath.Size = new Size(402, 23);
            textBoxPath.TabIndex = 3;
            textBoxPath.KeyDown += textBox1_KeyDown;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Dock = DockStyle.Right;
            textBoxSearch.Location = new Point(368, 0);
            textBoxSearch.Margin = new Padding(5);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.PlaceholderText = "Search";
            textBoxSearch.Size = new Size(246, 23);
            textBoxSearch.TabIndex = 4;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // panelTop
            // 
            panelTop.BackColor = SystemColors.Window;
            panelTop.BorderStyle = BorderStyle.Fixed3D;
            panelTop.Controls.Add(pictureBox1);
            panelTop.Controls.Add(labelFolderItems);
            panelTop.Controls.Add(labelFolderSize);
            panelTop.Controls.Add(labelDriveSize);
            panelTop.Controls.Add(labelDriveName);
            panelTop.Controls.Add(labelDriveLetter);
            panelTop.Controls.Add(progressBarTop);
            panelTop.Controls.Add(pictureBoxFolder);
            panelTop.Dock = DockStyle.Right;
            panelTop.Location = new Point(769, 0);
            panelTop.MinimumSize = new Size(100, 450);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(180, 450);
            panelTop.TabIndex = 5;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.diskIcon;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(7, -2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(166, 91);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // labelFolderItems
            // 
            labelFolderItems.Location = new Point(7, 416);
            labelFolderItems.Name = "labelFolderItems";
            labelFolderItems.Size = new Size(166, 23);
            labelFolderItems.TabIndex = 6;
            labelFolderItems.Text = "FolderItems";
            labelFolderItems.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelFolderSize
            // 
            labelFolderSize.Location = new Point(7, 393);
            labelFolderSize.Name = "labelFolderSize";
            labelFolderSize.Size = new Size(166, 23);
            labelFolderSize.TabIndex = 5;
            labelFolderSize.Text = "FolderSize";
            labelFolderSize.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelDriveSize
            // 
            labelDriveSize.Location = new Point(5, 161);
            labelDriveSize.Name = "labelDriveSize";
            labelDriveSize.Size = new Size(168, 15);
            labelDriveSize.TabIndex = 4;
            labelDriveSize.Text = "driveSize";
            labelDriveSize.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelDriveName
            // 
            labelDriveName.Location = new Point(5, 107);
            labelDriveName.Name = "labelDriveName";
            labelDriveName.Size = new Size(168, 15);
            labelDriveName.TabIndex = 3;
            labelDriveName.Text = "driveName";
            labelDriveName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelDriveLetter
            // 
            labelDriveLetter.Location = new Point(7, 92);
            labelDriveLetter.Name = "labelDriveLetter";
            labelDriveLetter.Size = new Size(166, 15);
            labelDriveLetter.TabIndex = 2;
            labelDriveLetter.Text = "driveLetter";
            labelDriveLetter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBarTop
            // 
            progressBarTop.BackColor = SystemColors.ActiveCaptionText;
            progressBarTop.ForeColor = Color.Cyan;
            progressBarTop.Location = new Point(7, 131);
            progressBarTop.Name = "progressBarTop";
            progressBarTop.Size = new Size(166, 23);
            progressBarTop.Step = 0;
            progressBarTop.TabIndex = 1;
            // 
            // pictureBoxFolder
            // 
            pictureBoxFolder.BackgroundImage = (Image)resources.GetObject("pictureBoxFolder.BackgroundImage");
            pictureBoxFolder.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxFolder.Location = new Point(7, 300);
            pictureBoxFolder.Name = "pictureBoxFolder";
            pictureBoxFolder.Size = new Size(166, 90);
            pictureBoxFolder.TabIndex = 0;
            pictureBoxFolder.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(textBoxSearch);
            panel1.Controls.Add(textBoxPath);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(155, 0);
            panel1.Margin = new Padding(5);
            panel1.MinimumSize = new Size(300, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(614, 25);
            panel1.TabIndex = 6;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem, newFolderToolStripMenuItem, newFileToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(135, 70);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(134, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // newFolderToolStripMenuItem
            // 
            newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            newFolderToolStripMenuItem.Size = new Size(134, 22);
            newFolderToolStripMenuItem.Text = "New Folder";
            newFolderToolStripMenuItem.Click += newFolderToolStripMenuItem_Click;
            // 
            // newFileToolStripMenuItem
            // 
            newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            newFileToolStripMenuItem.Size = new Size(134, 22);
            newFileToolStripMenuItem.Text = "New File";
            newFileToolStripMenuItem.Click += newFileToolStripMenuItem_Click;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem2 });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(108, 26);
            // 
            // deleteToolStripMenuItem2
            // 
            deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            deleteToolStripMenuItem2.Size = new Size(107, 22);
            deleteToolStripMenuItem2.Text = "Delete";
            deleteToolStripMenuItem2.Click += deleteToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(949, 450);
            Controls.Add(listView1);
            Controls.Add(panel1);
            Controls.Add(panelTop);
            Controls.Add(splitter1);
            Controls.Add(treeView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Explorer5000";
            Load += Form1_Load;
            panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxFolder).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            contextMenuStrip2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeView1;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private Splitter splitter1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ImageList imageList1;
        private TextBox textBoxPath;
        private TextBox textBoxSearch;
        private Panel panelTop;
        private Label labelDriveLetter;
        private ProgressBar progressBarTop;
        private Label labelDriveName;
        private Label labelDriveSize;
        private Panel panel1;
        private Label labelFolderItems;
        private Label labelFolderSize;
        private PictureBox pictureBoxFolder;
        private ContextMenuStrip contextMenuStripListView;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ContextMenuStrip contextMenuStripTreeView;
        private ToolStripMenuItem deleteToolStripMenuItem1;
        private ToolStripMenuItem newFolderToolStripMenuItem;
        private ToolStripMenuItem newFileToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem deleteToolStripMenuItem2;
        private PictureBox pictureBox1;
    }
}
