using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime;
using System.Text;
using System.Xml.Linq;
using DataDisplayLogic;

namespace Explorer5000
{
    public partial class Form1 : Form
    {
        private readonly DataOperations _dataDisplay;

        public Form1()
        {
            InitializeComponent();
            _dataDisplay = new DataOperations();
        }

        public void UpdateTreeViewNodes(TreeNode treeNode, bool clearExisting = false)
        {
            if (treeNode == null)
                return;

            string path = treeNode.FullPath;

            if (Directory.Exists(path))
            {
                var directories = _dataDisplay.GetDirectories(path);

                if (clearExisting)
                    treeNode.Nodes.Clear();

                foreach (var dir in directories)
                {
                    if (!treeNode.Nodes.ContainsKey(dir))
                    {
                        var childNode = new TreeNode(dir)
                        {
                            Name = dir
                        };
                        treeNode.Nodes.Add(childNode);
                    }
                }
            }
        }

        public void ShowDrives()
        {
            treeView1.BeginUpdate();
            var drives = _dataDisplay.GetDrives();
            foreach (var drive in drives)
            {
                var driveNode = new TreeNode(drive);
                treeView1.Nodes.Add(driveNode);
                UpdateTreeViewNodes(driveNode, false);
            }
            treeView1.EndUpdate();
        }

        public void ShowFiles(string searchQuery = "")
        {
            string folderPath = treeView1.SelectedNode.FullPath;

            listView1.Items.Clear();

            try
            {
                var filesInfo = _dataDisplay.GetFilesInfo(folderPath, searchQuery);

                listView1.BeginUpdate();
                foreach (var fileInfo in filesInfo)
                {
                    var item = new ListViewItem(fileInfo.Item1);

                    string fileExtension = Path.GetExtension(fileInfo.Item1).ToLower();
                    string iconName = string.Empty;

                    switch (fileExtension)
                    {
                        case ".exe":
                            iconName = "exeIcon.png";
                            break;
                        case ".txt":
                            iconName = "txtIcon.png";
                            break;
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".gif":
                        case ".bmp":
                            iconName = "imageIcon.png";
                            break;
                        case ".mp3":
                        case ".wav":
                            iconName = "musicIcon.png";
                            break;
                        case ".mp4":
                        case ".avi":
                        case ".mkv":
                        case ".mov":
                            iconName = "videoIcon.png";
                            break;
                        case ".zip":
                        case ".rar":
                        case ".7z":
                        case ".tar":
                        case ".gz":
                            iconName = "zipIcon.png";
                            break;
                        default:
                            iconName = "fileIcon.png";
                            break;
                    }

                    item.ImageKey = iconName;

                    string fileSize = _dataDisplay.FormatFileSize(fileInfo.Item2);

                    item.SubItems.Add(fileSize);
                    item.SubItems.Add(fileInfo.Item3.ToString());
                    item.SubItems.Add(fileInfo.Item4.ToString());

                    string fullPath = Path.Combine(folderPath.Replace("\\\\", "\\"), fileInfo.Item1);
                    item.Tag = fullPath;
                    Debug.WriteLine(item.Tag);

                    listView1.Items.Add(item);
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                listView1.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowDrives();
        }

        private void UpdatePanelInfo(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DriveInfo drive = new DriveInfo(dirInfo.Root.FullName);

            string driveLetter = drive.Name.TrimEnd('\\', ':');
            string driveName = drive.VolumeLabel;

            long totalSize = drive.TotalSize;
            long availableFreeSpace = drive.AvailableFreeSpace;
            long usedSpace = totalSize - availableFreeSpace;

            int items = dirInfo.GetFiles().Count(file => (file.Attributes & FileAttributes.Hidden) == 0);

            labelDriveLetter.Text = $"Disk: {driveLetter}";
            labelDriveName.Text = $"{driveName}";

            int usedPercentage = (int)((usedSpace / (double)totalSize) * 100);
            progressBarTop.Value = usedPercentage;

            labelDriveSize.Text = $"{_dataDisplay.FormatFileSize(availableFreeSpace)} free of {_dataDisplay.FormatFileSize(totalSize)}";

            labelFolderSize.Text = $"{dirInfo.Name}";
            labelFolderItems.Text = $"Items: {items}";
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string fullPath = treeView1.SelectedNode.FullPath;

            DirectoryInfo dirInfo = new DirectoryInfo(fullPath);

            textBoxPath.Text = dirInfo.FullName;
            ShowFiles();

            UpdatePanelInfo(dirInfo.FullName);
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView1.BeginUpdate();
            foreach (TreeNode node in e.Node.Nodes)
            {
                UpdateTreeViewNodes(node, false);
            }
            treeView1.EndUpdate();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string path = treeView1.SelectedNode.FullPath;
            if (!path.EndsWith("\\"))
                path += "\\";
            path += listView1.FocusedItem.Text;
            if (File.Exists(path))
                Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && listView1.SelectedItems.Count > 0)
            {
                contextMenuStrip2.Show(listView1, e.Location);
            }
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && treeView1.SelectedNode != null)
            {
                contextMenuStrip1.Show(treeView1, e.Location);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedPath = null;

            if (listView1.FocusedItem != null)
            {
                selectedPath = Path.Combine(treeView1.SelectedNode.FullPath, listView1.FocusedItem.Text);
            }
            else if (treeView1.SelectedNode != null)
            {
                selectedPath = treeView1.SelectedNode.FullPath;
            }

            if (selectedPath == null)
            {
                MessageBox.Show("Не выбран элемент для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (File.Exists(selectedPath))
                {
                    File.Delete(selectedPath);
                    ShowFiles();
                    UpdatePanelInfo(treeView1.SelectedNode.FullPath);
                }
                else if (Directory.Exists(selectedPath))
                {
                    Directory.Delete(selectedPath, true);
                    UpdateTreeViewNodes(treeView1.SelectedNode.Parent, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                string filePath = Path.Combine(treeView1.SelectedNode.FullPath, listView1.Items[e.Item].Text);
                string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), e.Label);

                try
                {
                    if (File.Exists(filePath))
                    {
                        File.Move(filePath, newFilePath);
                    }
                    ShowFiles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка переименования: {ex.Message}");
                    e.CancelEdit = true;
                }
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                string oldPath = e.Node.FullPath;
                string newPath = Path.Combine(Path.GetDirectoryName(oldPath), e.Label);

                try
                {
                    if (Directory.Exists(oldPath))
                    {
                        Directory.Move(oldPath, newPath);
                        UpdatePanelInfo(newPath);
                        e.Node.Name = e.Label;
                    }
                    else
                    {
                        MessageBox.Show("Папка не найдена для переименования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.CancelEdit = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка переименования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.CancelEdit = true;
                }
            }
        }

        private void NavigateToPath(string path)
        {
            string fullPath = Path.GetFullPath(path);

            string[] pathParts = fullPath.Split(Path.DirectorySeparatorChar);
            pathParts[0] += Path.DirectorySeparatorChar;
            TreeNode currentNode = null;

            foreach (TreeNode node in treeView1.Nodes)
            {
                node.Collapse();
            }

            foreach (var part in pathParts)
            {
                if (currentNode == null)
                {
                    currentNode = treeView1.Nodes.Cast<TreeNode>().FirstOrDefault(node => node.Text.Equals(part, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    currentNode = currentNode.Nodes.Cast<TreeNode>().FirstOrDefault(node => node.Text.Equals(part, StringComparison.OrdinalIgnoreCase));
                }

                if (currentNode == null)
                {
                    MessageBox.Show("Не удалось найти часть пути в TreeView.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                treeView1.SelectedNode = currentNode;
                currentNode.Expand();
            }

            ShowFiles();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string enteredPath = textBoxPath.Text;

                if (Directory.Exists(enteredPath))
                {
                    NavigateToPath(enteredPath);
                }
                else
                {
                    MessageBox.Show("Указанный путь не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBoxSearch.Text.ToLower();

            ShowFiles(searchQuery);
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var filePaths = listView1.SelectedItems.Cast<ListViewItem>().Select(item => item.Tag.ToString()).ToArray();

            string filesAsString = string.Join(Environment.NewLine, filePaths);

            listView1.DoDragDrop(filesAsString, DragDropEffects.Move);
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            var targetNode = treeView1.GetNodeAt(treeView1.PointToClient(new Point(e.X, e.Y)));
            if (targetNode == null)
                return;

            string targetFolderPath = targetNode.FullPath;
            string filesAsString = e.Data.GetData(DataFormats.Text) as string;

            if (filesAsString != null)
            {
                string[] filePaths = filesAsString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (var filePath in filePaths)
                {
                    try
                    {
                        string fileName = Path.GetFileName(filePath);
                        string destinationPath = Path.Combine(targetFolderPath, fileName);

                        File.Move(filePath, destinationPath);
                        ShowFiles();
                        UpdatePanelInfo(Path.GetDirectoryName(filePath));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error moving file {filePath}: {ex.Message}");
                    }
                }
            }
        }

        private void CreateNewFolder(TreeNode parentNode)
        {
            string parentPath = parentNode.FullPath;

            string newFolderName = "Новая папка";
            string newFolderPath = Path.Combine(parentPath, newFolderName);

            int count = 1;
            while (Directory.Exists(newFolderPath))
            {
                newFolderName = $"Новая папка ({count++})";
                newFolderPath = Path.Combine(parentPath, newFolderName);
            }

            Directory.CreateDirectory(newFolderPath);

            TreeNode newNode = parentNode.Nodes.Add(newFolderName); 

            treeView1.SelectedNode = newNode; 
            newNode.BeginEdit(); 
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            CreateNewFolder(selectedNode);
        }

        private void CreateNewFile(TreeNode parentNode)
        {
            try
            {
                string folderPath = parentNode.FullPath;

                string tempFileName = "Новый файл.txt";
                string filePath = Path.Combine(folderPath, tempFileName);

                int counter = 1;
                while (File.Exists(filePath))
                {
                    tempFileName = $"Новый файл ({counter}).txt";
                    filePath = Path.Combine(folderPath, tempFileName);
                    counter++;
                }

                File.WriteAllText(filePath, string.Empty);

                ShowFiles();

                UpdatePanelInfo(folderPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            CreateNewFile(selectedNode);
        }
    }
}
