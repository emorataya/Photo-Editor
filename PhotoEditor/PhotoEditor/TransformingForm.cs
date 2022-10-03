using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class TransformingForm : Form
    {
        // Function signature
        public delegate void FormClosedEvent();
        
        public event FormClosedEvent FormClosed;

        public TransformingForm()
        {
            InitializeComponent();
        }

        // Closing the window via Alt-F4 or the close button should do the same thing as pressing Cancel.
        // This will cancel the thread whether user click "Cancel" or presses "Alt + F4"
        private void TransformingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Notify any observers that the windows was closed
            FormClosed();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
