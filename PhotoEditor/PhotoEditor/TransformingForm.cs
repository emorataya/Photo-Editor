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

        private void button1_Click(object sender, EventArgs e)
        {
            // Notify any observers that the windows was closed
            FormClosed();
            Close();
        }
    }
}
