using System.Windows.Forms.Design;

namespace PhotoEditor
{
    public partial class MainForm : Form
    {
        //This is the variable that is going to be used for the photo root directory
        private string photoRootDirectory;
        private List<FileInfo> photoFiles;

        public MainForm()
        {
            InitializeComponent();

            //Need to get folder path - want to get pictures from the pictures folder
            photoRootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            PopulateImageList();

        }

        //Populate Image List()
        private void PopulateImageList()
        {
            photoFiles = new List<FileInfo>();

            DirectoryInfo homeDir = new DirectoryInfo(photoRootDirectory);
            foreach (FileInfo file in homeDir.GetFiles("*.jpg"))
            {
                //photoFiles.Add(file);
                var temp = file.Name;
                mainFormListView.Items.Add(temp);
            }

            mainFormListView.Columns.Add("Name");
            mainFormListView.Columns.Add("Date");
            mainFormListView.Columns.Add("Size");

            // Show default view - put in a list for now
            mainFormListView.View = View.Details;

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

        //Will need to change the view if we choose a different directory
        public void RefreshTree()
        {
            //TreeView.Nodes.Clear();
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

            }
        }

        private void locateOnDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {

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