namespace Hypnos.Desktop.Forms
{
    partial class UsersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.transposeButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fullNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.fullNamePanel = new System.Windows.Forms.Panel();
            this.fullNameLabel = new System.Windows.Forms.Label();
            this.fullNameBox = new System.Windows.Forms.TextBox();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.loginLabel = new System.Windows.Forms.Label();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.passwordPanel = new System.Windows.Forms.Panel();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.descriptionPanel = new System.Windows.Forms.Panel();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.rolesPanel = new System.Windows.Forms.Panel();
            this.rolesLabel = new System.Windows.Forms.Label();
            this.rolesBoxes = new System.Windows.Forms.CheckedListBox();
            this.viewLabel = new System.Windows.Forms.ToolStripLabel();
            this.crudLabel = new System.Windows.Forms.ToolStripLabel();
            this.createButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.upsertButton = new System.Windows.Forms.ToolStripButton();
            this.readButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.fullNamePanel.SuspendLayout();
            this.loginPanel.SuspendLayout();
            this.passwordPanel.SuspendLayout();
            this.descriptionPanel.SuspendLayout();
            this.rolesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crudLabel,
            this.createButton,
            this.readButton,
            this.upsertButton,
            this.deleteButton,
            this.transposeButton,
            this.viewLabel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(684, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // transposeButton
            // 
            this.transposeButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.transposeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.transposeButton.Image = ((System.Drawing.Image)(resources.GetObject("transposeButton.Image")));
            this.transposeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.transposeButton.Name = "transposeButton";
            this.transposeButton.Size = new System.Drawing.Size(108, 22);
            this.transposeButton.Text = "&Транспонировать";
            this.transposeButton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.transposeButton.Click += new System.EventHandler(this.Transpose);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 25);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel);
            this.splitContainer.Size = new System.Drawing.Size(684, 234);
            this.splitContainer.SplitterDistance = 176;
            this.splitContainer.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fullNameColumn,
            this.loginNameColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(176, 234);
            this.dataGridView1.TabIndex = 0;
            // 
            // fullNameColumn
            // 
            this.fullNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fullNameColumn.HeaderText = "ФИО";
            this.fullNameColumn.Name = "fullNameColumn";
            this.fullNameColumn.ReadOnly = true;
            // 
            // loginNameColumn
            // 
            this.loginNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.loginNameColumn.HeaderText = "Логин";
            this.loginNameColumn.Name = "loginNameColumn";
            this.loginNameColumn.ReadOnly = true;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.Controls.Add(this.fullNamePanel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.loginPanel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.passwordPanel, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.descriptionPanel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.rolesPanel, 3, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(504, 234);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // fullNamePanel
            // 
            this.tableLayoutPanel.SetColumnSpan(this.fullNamePanel, 4);
            this.fullNamePanel.Controls.Add(this.fullNameBox);
            this.fullNamePanel.Controls.Add(this.fullNameLabel);
            this.fullNamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fullNamePanel.Location = new System.Drawing.Point(3, 3);
            this.fullNamePanel.Name = "fullNamePanel";
            this.fullNamePanel.Size = new System.Drawing.Size(498, 42);
            this.fullNamePanel.TabIndex = 0;
            // 
            // fullNameLabel
            // 
            this.fullNameLabel.AutoSize = true;
            this.fullNameLabel.Location = new System.Drawing.Point(3, 0);
            this.fullNameLabel.Name = "fullNameLabel";
            this.fullNameLabel.Size = new System.Drawing.Size(133, 13);
            this.fullNameLabel.TabIndex = 0;
            this.fullNameLabel.Text = "Фамилия, имя, отчество";
            // 
            // fullNameBox
            // 
            this.fullNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fullNameBox.Location = new System.Drawing.Point(6, 16);
            this.fullNameBox.Name = "fullNameBox";
            this.fullNameBox.Size = new System.Drawing.Size(483, 20);
            this.fullNameBox.TabIndex = 1;
            // 
            // loginPanel
            // 
            this.tableLayoutPanel.SetColumnSpan(this.loginPanel, 2);
            this.loginPanel.Controls.Add(this.loginBox);
            this.loginPanel.Controls.Add(this.loginLabel);
            this.loginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginPanel.Location = new System.Drawing.Point(3, 51);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(246, 42);
            this.loginPanel.TabIndex = 1;
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(3, 0);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(38, 13);
            this.loginLabel.TabIndex = 0;
            this.loginLabel.Text = "Логин";
            // 
            // loginBox
            // 
            this.loginBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginBox.Location = new System.Drawing.Point(6, 16);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(237, 20);
            this.loginBox.TabIndex = 1;
            // 
            // passwordPanel
            // 
            this.tableLayoutPanel.SetColumnSpan(this.passwordPanel, 2);
            this.passwordPanel.Controls.Add(this.passwordBox);
            this.passwordPanel.Controls.Add(this.passwordLabel);
            this.passwordPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passwordPanel.Location = new System.Drawing.Point(255, 51);
            this.passwordPanel.Name = "passwordPanel";
            this.passwordPanel.Size = new System.Drawing.Size(246, 42);
            this.passwordPanel.TabIndex = 2;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(3, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(45, 13);
            this.passwordLabel.TabIndex = 0;
            this.passwordLabel.Text = "Пароль";
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordBox.Location = new System.Drawing.Point(6, 16);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '●';
            this.passwordBox.Size = new System.Drawing.Size(231, 20);
            this.passwordBox.TabIndex = 1;
            // 
            // descriptionPanel
            // 
            this.tableLayoutPanel.SetColumnSpan(this.descriptionPanel, 3);
            this.descriptionPanel.Controls.Add(this.descriptionBox);
            this.descriptionPanel.Controls.Add(this.descriptionLabel);
            this.descriptionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionPanel.Location = new System.Drawing.Point(3, 99);
            this.descriptionPanel.Name = "descriptionPanel";
            this.descriptionPanel.Size = new System.Drawing.Size(372, 132);
            this.descriptionPanel.TabIndex = 3;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(3, 0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(57, 13);
            this.descriptionLabel.TabIndex = 0;
            this.descriptionLabel.Text = "Описание";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionBox.Location = new System.Drawing.Point(6, 16);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(363, 109);
            this.descriptionBox.TabIndex = 1;
            // 
            // rolesPanel
            // 
            this.rolesPanel.Controls.Add(this.rolesBoxes);
            this.rolesPanel.Controls.Add(this.rolesLabel);
            this.rolesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rolesPanel.Location = new System.Drawing.Point(381, 99);
            this.rolesPanel.Name = "rolesPanel";
            this.rolesPanel.Size = new System.Drawing.Size(120, 132);
            this.rolesPanel.TabIndex = 4;
            // 
            // rolesLabel
            // 
            this.rolesLabel.AutoSize = true;
            this.rolesLabel.Location = new System.Drawing.Point(3, 0);
            this.rolesLabel.Name = "rolesLabel";
            this.rolesLabel.Size = new System.Drawing.Size(32, 13);
            this.rolesLabel.TabIndex = 0;
            this.rolesLabel.Text = "Роли";
            // 
            // rolesBoxes
            // 
            this.rolesBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rolesBoxes.FormattingEnabled = true;
            this.rolesBoxes.Items.AddRange(new object[] {
            "Администратор",
            "Руководитель",
            "Сотрудник"});
            this.rolesBoxes.Location = new System.Drawing.Point(6, 16);
            this.rolesBoxes.Name = "rolesBoxes";
            this.rolesBoxes.Size = new System.Drawing.Size(105, 109);
            this.rolesBoxes.TabIndex = 1;
            // 
            // viewLabel
            // 
            this.viewLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.viewLabel.Name = "viewLabel";
            this.viewLabel.Size = new System.Drawing.Size(30, 22);
            this.viewLabel.Text = "Вид:";
            // 
            // crudLabel
            // 
            this.crudLabel.Name = "crudLabel";
            this.crudLabel.Size = new System.Drawing.Size(53, 22);
            this.crudLabel.Text = "Данные:";
            // 
            // createButton
            // 
            this.createButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.createButton.Image = ((System.Drawing.Image)(resources.GetObject("createButton.Image")));
            this.createButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(54, 22);
            this.createButton.Text = "&Создать";
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(55, 22);
            this.deleteButton.Text = "&Удалить";
            // 
            // upsertButton
            // 
            this.upsertButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.upsertButton.Image = ((System.Drawing.Image)(resources.GetObject("upsertButton.Image")));
            this.upsertButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.upsertButton.Name = "upsertButton";
            this.upsertButton.Size = new System.Drawing.Size(70, 22);
            this.upsertButton.Text = "С&охранить";
            // 
            // readButton
            // 
            this.readButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.readButton.Image = ((System.Drawing.Image)(resources.GetObject("readButton.Image")));
            this.readButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(75, 22);
            this.readButton.Text = "&Перечитать";
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 259);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 298);
            this.Name = "UsersForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Пользователи";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.fullNamePanel.ResumeLayout(false);
            this.fullNamePanel.PerformLayout();
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.passwordPanel.ResumeLayout(false);
            this.passwordPanel.PerformLayout();
            this.descriptionPanel.ResumeLayout(false);
            this.descriptionPanel.PerformLayout();
            this.rolesPanel.ResumeLayout(false);
            this.rolesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton transposeButton;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loginNameColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel fullNamePanel;
        private System.Windows.Forms.TextBox fullNameBox;
        private System.Windows.Forms.Label fullNameLabel;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.Panel passwordPanel;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Panel descriptionPanel;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Panel rolesPanel;
        private System.Windows.Forms.Label rolesLabel;
        private System.Windows.Forms.CheckedListBox rolesBoxes;
        private System.Windows.Forms.ToolStripLabel crudLabel;
        private System.Windows.Forms.ToolStripLabel viewLabel;
        private System.Windows.Forms.ToolStripButton createButton;
        private System.Windows.Forms.ToolStripButton readButton;
        private System.Windows.Forms.ToolStripButton upsertButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
    }
}