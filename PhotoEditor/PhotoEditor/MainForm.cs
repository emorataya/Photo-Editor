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

            //ListViewItem item1 = new ListViewItem("item1", 0);   // Text and image index
            //item1.SubItems.Add("1");   // Column 2
            //item1.SubItems.Add("2");   // Column 3
            //item1.SubItems.Add("3");   // Column 4

            //ListViewItem item2 = new ListViewItem("item2", 1);
            //item2.SubItems.Add("4");
            //item2.SubItems.Add("5");
            //item2.SubItems.Add("6");

            //ListViewItem item3 = new ListViewItem("item3", 2);
            //item3.SubItems.Add("7");
            //item3.SubItems.Add("8");
            //item3.SubItems.Add("9");

            //// Create columns (Width of -2 indicates auto-size)
            //mainFormListView.Columns.Add("Column 1", -2, HorizontalAlignment.Left);
            //mainFormListView.Columns.Add("Column 2", -2, HorizontalAlignment.Left);
            //mainFormListView.Columns.Add("Column 3", 40, HorizontalAlignment.Right);
            //mainFormListView.Columns.Add("Column 4", 40, HorizontalAlignment.Center);

            DirectoryInfo homeDir = new DirectoryInfo(photoRootDirectory);
            foreach (FileInfo file in homeDir.GetFiles("*.jpg"))
            {
                //photoFiles.Add(file);
                var temp = file.Name;
                mainFormListView.Items.Add(temp);
            }

            mainFormListView.Columns.Add("Column 1", -2, HorizontalAlignment.Left);
            mainFormListView.Columns.Add("Column 2", -2, HorizontalAlignment.Left);
            mainFormListView.Columns.Add("Column 3", -2, HorizontalAlignment.Left);
            mainFormListView.Columns.Add("Column 4", -2, HorizontalAlignment.Left);

            // Show default view - put in a list for now
            mainFormListView.View = View.List;

        }

        private void mainFormListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var editPhotoForm = new EditPhotoForm();

            if (mainFormListView.SelectedItems.Count == 0)
                return;

            var selectedFile = photoRootDirectory + '\\' + mainFormListView.SelectedItems[0].Text;
                editPhotoForm.photoPictureBox.Image = LoadImage(selectedFile);
                editPhotoForm.ShowDialog();
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
    }
}