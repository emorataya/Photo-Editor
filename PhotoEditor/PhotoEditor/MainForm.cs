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
        private List<FileInfo> photoFiles;
        private CancellationTokenSource cancellationTokenSource;

        public MainForm()
        {
            InitializeComponent();

            //Need to get folder path - want to get pictures from the pictures folder
            directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            photoRootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            //Writing to the console, not the gui app
            Console.WriteLine(photoRootDirectory);

            //ListView
            PopulateImageList();

            //TreeView
            PopulateTreeView();

            //Testing to see if it will work here
            mainFormListView.Columns.Add("Name", -2, HorizontalAlignment.Left);
            mainFormListView.Columns.Add("Date", -2, HorizontalAlignment.Left);
            mainFormListView.Columns.Add("Size", -2, HorizontalAlignment.Left);

        }

        private void PopulateTreeView()
        {
            //For future use
            treeView1.Nodes.Clear();
            ListTreeDir(treeView1, directory.FullName);
        }

        //Listing the tree view directory
        private void ListTreeDir(TreeView treeView, string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
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
        public void UpdateDirectory(string photoRootDirectory)
        {
            cancellationTokenSource.Cancel();
            //Might need to change it to DirectoryInfo
          //  photoRootDirectory = Environment.GetFolderPath(photoRootDirectory);
            
        }

        //Populate Image List() - LIST VIEW
        private /*async Task*/ void PopulateImageList()
        {
            photoFiles = new List<FileInfo>();
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            DirectoryInfo homeDir = new DirectoryInfo(photoRootDirectory);

            //Thread
            //await Task.Run(() =>
            //{
                //Images - Small
                ImageList smallImageList = new ImageList();
                smallImageList.ImageSize = new Size(64, 64);

                //Images - Large
                ImageList largeImageList = new ImageList();
                largeImageList.ImageSize = new Size(128, 128);

                //Invoke - used for progress bar
                mainFormListView.SmallImageList = smallImageList;
                mainFormListView.LargeImageList = largeImageList;              

                foreach (FileInfo file in homeDir.GetFiles("*.jpg"))
                {
                    //Need to make sure to stop the thread if the cancellation has been requested
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                //Thread
                    byte[] bytes = System.IO.File.ReadAllBytes(file.FullName);
                    Image image = Image.FromStream(new MemoryStream(bytes));
               // Image image = LoadImage(photoRootDirectory);

                    smallImageList.Images.Add(file.FullName, image);
                    largeImageList.Images.Add(file.FullName, image);

                    //Subitems using temp view listViewItem variable
                    ListViewItem listViewItem = new ListViewItem
                    {
                        ImageKey = file.FullName,
                        Name = file.Name,
                        Text = file.Name
                    };

                    listViewItem.SubItems.Add(file.LastWriteTime.ToString());
                    listViewItem.Tag = file;

                    mainFormListView.Items.Add(listViewItem);


            }

                // Show default view
                mainFormListView.View = View.Details;
            //});
            
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
                photoRootDirectory = folderBrowserDialog.SelectedPath;
                //Insert helper function for directory change
                UpdateDirectory(photoRootDirectory);
                //Acting like a "refresh" function - will just repopulate the tree view
                PopulateImageList();
            }
        }

        private void locateOnDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Need to make sure the user actually clicks on a picture first
            if (mainFormListView.SelectedItems != null)
            {

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
            //Want user to only check 1 thing at a time
            detailsToolStripMenuItem.Checked = true;
            smallToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            smallToolStripMenuItem.Checked = true;
            detailsToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            largeToolStripMenuItem.Checked = true;
            detailsToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
        }

        // The EditPhotoForm has to activeate when there is a double click on the image,
        // We had it in a way where it activated when there was one click (SelectedIndexChanged)
        // But SelectedIndexChanged should do something different
        private void mainFormListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (mainFormListView.SelectedItems.Count == 0)
                return;

            var editPhotoForm = new EditPhotoForm(photoRootDirectory + '\\' + mainFormListView.SelectedItems[0].Text);

            var selectedFile = photoRootDirectory + '\\' + mainFormListView.SelectedItems[0].Text;
            editPhotoForm.photoPictureBox.Image = LoadImage(selectedFile);
            editPhotoForm.ShowDialog();
        }
    }
}