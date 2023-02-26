namespace Hypnos.Desktop.Forms
{
    partial class ParentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParentForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.workloadItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managementItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrationItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workloadItem,
            this.managementItem,
            this.administrationItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(800, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "Главное меню";
            // 
            // workloadItem
            // 
            this.workloadItem.Name = "workloadItem";
            this.workloadItem.Size = new System.Drawing.Size(58, 20);
            this.workloadItem.Text = "&Задачи";
            // 
            // managementItem
            // 
            this.managementItem.Name = "managementItem";
            this.managementItem.Size = new System.Drawing.Size(88, 20);
            this.managementItem.Text = "&Руководство";
            // 
            // administrationItem
            // 
            this.administrationItem.Name = "administrationItem";
            this.administrationItem.Size = new System.Drawing.Size(134, 20);
            this.administrationItem.Text = "&Администрирование";
            this.administrationItem.Click += new System.EventHandler(this.OpenUsers);
            // 
            // ParentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "ParentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hypnos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Exit);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem workloadItem;
        private System.Windows.Forms.ToolStripMenuItem managementItem;
        private System.Windows.Forms.ToolStripMenuItem administrationItem;
    }
}