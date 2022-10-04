using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PhotoEditor
{
    public partial class MainForm : Form
    {
        //Global variables that will be used throughout the project
        private string photoRootDirectory;
        DirectoryInfo directory;
        private CancellationTokenSource cancellationTokenSource;

        public MainForm()
        {
            //Get folder path
            directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            photoRootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            InitializeComponent();

            //TreeView
            PopulateTreeView();

            //Adding columns to the image list view
            mainFormListView.Columns.Add("Name", 300, HorizontalAlignment.Left);
            mainFormListView.Columns.Add("Date", -2, HorizontalAlignment.Left);
            mainFormListView.Columns.Add("Size", -2, HorizontalAlignment.Left);
            // Show default view
            mainFormListView.View = View.Details;
            mainFormListView.FullRowSelect = true;
        }

        private void PopulateTreeView()
        {
            //For future use
            treeView1.Nodes.Clear();
            treeView1.ImageList = imageList1;
            ListTreeDir(treeView1, directory.FullName);
        }

        //Listing the tree view directory
        private void ListTreeDir(TreeView treeView, string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            treeView1.ImageList = imageList1;

            treeView1.Nodes.Add(CreateTreeNode(directoryInfo));
        }

        private static TreeNode CreateTreeNode(DirectoryInfo directoryInfo)
        {
            TreeNode dirNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                dirNode.Nodes.Add(CreateTreeNode(directory));
            }

            dirNode.Tag = directoryInfo;
            return dirNode;
        }

        //PhotoRootDirectory is a string - need to change it
        public void UpdateDirectory(DirectoryInfo directory)
        {
            cancellationTokenSource.Cancel();
            PopulateImageList();
        }

        //Populate Image List() - LIST VIEW
        private async void PopulateImageList()
        {
            progressBar1.Visible = true;

            mainFormListView.Items.Clear();

            //Thread
            await Task.Run(() =>
            {
                //Images - Small
                ImageList smallImageList = new ImageList();
                smallImageList.ImageSize = new Size(64, 64);

                //Images - Large
                ImageList largeImageList = new ImageList();
                largeImageList.ImageSize = new Size(128, 128);

                //Invoke - used for progress bar
                Invoke((Action)(() =>
                {
                    mainFormListView.SmallImageList = smallImageList;
                    mainFormListView.LargeImageList = largeImageList;
                }));

                var num = 10;

                foreach (FileInfo file in directory.GetFiles("*.jpg"))
                {
                    //Thread
                    Invoke((Action)(() =>
                    {
                        byte[] bytes = System.IO.File.ReadAllBytes(file.FullName);
                        Image image = Image.FromStream(new MemoryStream(bytes));

                        smallImageList.Images.Add(file.FullName, image);
                        largeImageList.Images.Add(file.FullName, image);

                        progressBar1.Value = num++;
                    }));
                    

                    //Subitems using temp listViewItem variable
                    ListViewItem listViewItem = new ListViewItem
                    {
                        ImageKey = file.FullName,
                        Name = file.Name,
                        Text = file.Name
                    };

                    listViewItem.SubItems.Add(file.LastWriteTime.ToString());

                    var tempFileSize = FileSize(file);

                    listViewItem.SubItems.Add(tempFileSize);
                    listViewItem.Tag = file;

                    ListViewItem temp = listViewItem;

                    Invoke((Action)(() =>
                    {
                        mainFormListView.Items.Add(listViewItem);
                    }));
                }
            });

            progressBar1.Visible = false;

        }

        private string FileSize(FileInfo file)
        {
            string temp;
            if (file.Length >= 1024)
            {
                return temp = file.Length / 1024 + " KB";
            }
            else
            {
                return temp = file.Length + " MB";
            }
        }

        private void mainFormListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private Image LoadImage(string filename)
        {
            // Use this method to load images so the image files do not remain locked
            byte[] bytes = File.ReadAllBytes(filename);
            MemoryStream ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }

        //Menu Strip: File
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void selectRootFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //McCown notes
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            //Need dialog
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                directory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                //Insert helper function for directory change
                UpdateDirectory(directory);
                //Acting like a "refresh" function - will just repopulate the image list view
                //PopulateImageList();
                PopulateTreeView();
            }
        }

        //Referenced https://social.msdn.microsoft.com/Forums/expression/en-US/0ea2edea-9a98-4c7a-8d7c-cf2ee56f4a79/open-folder-and-select-the-file?forum=winforms
        private void locateOnDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Need to make sure the user actually clicks on a picture first
            if (mainFormListView.SelectedItems.Count == 1)
            {
                string fileTemp = mainFormListView.SelectedItems[0].ImageKey;
                Process.Start("explorer.exe", @"/select," + fileTemp);

            }
            else if (mainFormListView.SelectedItems.Count < 1)
            {
                MessageBox.Show("Select a image first before clicking 'Select Root Folder'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Menu strip: About 
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pops up the dialog box
            About about = new About();
            about.ShowDialog();
        }

        //Menu Strip: View - Details Tab
        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainFormListView.View = View.Details;
            //Want user to only check 1 thing at a time
            detailsToolStripMenuItem.Checked = true;
            smallToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainFormListView.View = View.SmallIcon;
            smallToolStripMenuItem.Checked = true;
            detailsToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainFormListView.View = View.LargeIcon;
            largeToolStripMenuItem.Checked = true;
            detailsToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
        }

        // The EditPhotoForm has to activeate when there is a double click on the image,
        // We had it in a way where it activated when there was one click (SelectedIndexChanged)
        // But SelectedIndexChanged should do something different
        private void mainFormListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            photoRootDirectory = directory.ToString();

            if (mainFormListView.SelectedItems.Count == 0)
                return;

            var editPhotoForm = new EditPhotoForm(photoRootDirectory + '\\' + mainFormListView.SelectedItems[0].Text);

            var selectedFile = photoRootDirectory + '\\' + mainFormListView.SelectedItems[0].Text;
            editPhotoForm.photoPictureBox.Image = LoadImage(selectedFile);
            editPhotoForm.ShowDialog();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode != null)
            {
                treeNode.Expand();
                UpdateDirectory((DirectoryInfo)treeNode.Tag);
            }
        }
    }
}