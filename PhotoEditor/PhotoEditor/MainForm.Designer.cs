﻿namespace PhotoEditor
{
    partial class MainForm
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
            this.mainFormListView = new System.Windows.Forms.ListView();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locateOnDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectRootFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainFormListView
            // 
            this.mainFormListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.mainFormListView.Location = new System.Drawing.Point(360, 85);
            this.mainFormListView.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.mainFormListView.MultiSelect = false;
            this.mainFormListView.Name = "mainFormListView";
            this.mainFormListView.Size = new System.Drawing.Size(719, 796);
            this.mainFormListView.TabIndex = 0;
            this.mainFormListView.UseCompatibleStateImageBehavior = false;
            this.mainFormListView.SelectedIndexChanged += new System.EventHandler(this.mainFormListView_SelectedIndexChanged);
            this.mainFormListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mainFormListView_MouseDoubleClick);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(13, 85);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(333, 796);
            this.treeView1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1098, 42);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locateOnDiskToolStripMenuItem,
            this.selectRootFolderToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(71, 38);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // locateOnDiskToolStripMenuItem
            // 
            this.locateOnDiskToolStripMenuItem.Name = "locateOnDiskToolStripMenuItem";
            this.locateOnDiskToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.locateOnDiskToolStripMenuItem.Size = new System.Drawing.Size(387, 44);
            this.locateOnDiskToolStripMenuItem.Text = "&Locate on Disk";
            this.locateOnDiskToolStripMenuItem.Click += new System.EventHandler(this.locateOnDiskToolStripMenuItem_Click);
            // 
            // selectRootFolderToolStripMenuItem
            // 
            this.selectRootFolderToolStripMenuItem.Name = "selectRootFolderToolStripMenuItem";
            this.selectRootFolderToolStripMenuItem.Size = new System.Drawing.Size(387, 44);
            this.selectRootFolderToolStripMenuItem.Text = "&Select Root Folder...";
            this.selectRootFolderToolStripMenuItem.Click += new System.EventHandler(this.selectRootFolderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(384, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(387, 44);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailsToolStripMenuItem,
            this.smallToolStripMenuItem,
            this.largeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(85, 38);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Checked = true;
            this.detailsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.detailsToolStripMenuItem.Text = "&Details";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
            // 
            // smallToolStripMenuItem
            // 
            this.smallToolStripMenuItem.Name = "smallToolStripMenuItem";
            this.smallToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.smallToolStripMenuItem.Text = "&Small";
            this.smallToolStripMenuItem.Click += new System.EventHandler(this.smallToolStripMenuItem_Click);
            // 
            // largeToolStripMenuItem
            // 
            this.largeToolStripMenuItem.Name = "largeToolStripMenuItem";
            this.largeToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.largeToolStripMenuItem.Text = "&Large";
            this.largeToolStripMenuItem.Click += new System.EventHandler(this.largeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(84, 38);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(212, 44);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Location = new System.Drawing.Point(0, 870);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1098, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 892);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.mainFormListView);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "MainForm";
            this.Text = "Photo Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView mainFormListView;
        private TreeView treeView1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem locateOnDiskToolStripMenuItem;
        private ToolStripMenuItem selectRootFolderToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem detailsToolStripMenuItem;
        private ToolStripMenuItem smallToolStripMenuItem;
        private ToolStripMenuItem largeToolStripMenuItem;
        private StatusStrip statusStrip1;
    }
}