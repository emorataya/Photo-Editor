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

            //Part 1 - Due: Sept. 22
            //1. ListView on the main form shows all JPEG filenames in the user's Pictures directory.
            //   No photo is necessary, and no background thread is necessary to populate the list box

            //Need to get folder path - want to get pictures from the pictures folder
            photoRootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            PopulateImageList();

        }

        //Populate Image List()
        private void PopulateImageList()
        {
            //Using ListViewItem as an array
            List<ListViewItem> photoArray = new List<ListViewItem>();
            photoFiles = new List<FileInfo>();

            DirectoryInfo homeDir = new DirectoryInfo(photoRootDirectory);
            foreach (FileInfo file in homeDir.GetFiles("*.jpg"))
            {
                photoFiles.Add(file);
            }

            ListViewItem item1 = new ListViewItem(photoFiles[0].ToString(), 0);

            ListViewItem item2 = new ListViewItem(photoFiles[1].ToString(), 1);


            // Add the items to the list view
            listView1.Items.AddRange(new ListViewItem[] { item1, item2 });

            // Show default view - put in a list for now
            listView1.View = View.List;
        }
    }
}