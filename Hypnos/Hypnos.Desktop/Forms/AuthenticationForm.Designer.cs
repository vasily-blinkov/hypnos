namespace Hypnos.Desktop.Forms
{
    partial class AuthenticationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthenticationForm));
            this.authenticationTabs = new System.Windows.Forms.TabControl();
            this.loginTab = new System.Windows.Forms.TabPage();
            this.checkLoginLink = new System.Windows.Forms.LinkLabel();
            this.loginBox = new System.Windows.Forms.MaskedTextBox();
            this.passwordTab = new System.Windows.Forms.TabPage();
            this.backLabel = new System.Windows.Forms.LinkLabel();
            this.checkPasswordLink = new System.Windows.Forms.LinkLabel();
            this.passwordBox = new System.Windows.Forms.MaskedTextBox();
            this.authenticationTable = new System.Windows.Forms.TableLayoutPanel();
            this.cancelLink = new System.Windows.Forms.LinkLabel();
            this.authenticationTabs.SuspendLayout();
            this.loginTab.SuspendLayout();
            this.passwordTab.SuspendLayout();
            this.authenticationTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // authenticationTabs
            // 
            this.authenticationTabs.Controls.Add(this.loginTab);
            this.authenticationTabs.Controls.Add(this.passwordTab);
            this.authenticationTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.authenticationTabs.Location = new System.Drawing.Point(3, 3);
            this.authenticationTabs.Name = "authenticationTabs";
            this.authenticationTabs.SelectedIndex = 0;
            this.authenticationTabs.Size = new System.Drawing.Size(326, 84);
            this.authenticationTabs.TabIndex = 2;
            this.authenticationTabs.TabStop = false;
            // 
            // loginTab
            // 
            this.loginTab.Controls.Add(this.checkLoginLink);
            this.loginTab.Controls.Add(this.loginBox);
            this.loginTab.Location = new System.Drawing.Point(4, 22);
            this.loginTab.Name = "loginTab";
            this.loginTab.Padding = new System.Windows.Forms.Padding(3);
            this.loginTab.Size = new System.Drawing.Size(318, 58);
            this.loginTab.TabIndex = 0;
            this.loginTab.Text = "Логин";
            this.loginTab.UseVisualStyleBackColor = true;
            // 
            // checkLoginLink
            // 
            this.checkLoginLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkLoginLink.Enabled = false;
            this.checkLoginLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.checkLoginLink.Location = new System.Drawing.Point(6, 29);
            this.checkLoginLink.Name = "checkLoginLink";
            this.checkLoginLink.Size = new System.Drawing.Size(306, 26);
            this.checkLoginLink.TabIndex = 1;
            this.checkLoginLink.TabStop = true;
            this.checkLoginLink.Text = "Введите логин";
            this.checkLoginLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkLoginLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CheckLogin);
            // 
            // loginBox
            // 
            this.loginBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginBox.Location = new System.Drawing.Point(6, 6);
            this.loginBox.Mask = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(306, 20);
            this.loginBox.TabIndex = 0;
            this.loginBox.TextChanged += new System.EventHandler(this.ValidateLogin);
            // 
            // passwordTab
            // 
            this.passwordTab.Controls.Add(this.backLabel);
            this.passwordTab.Controls.Add(this.checkPasswordLink);
            this.passwordTab.Controls.Add(this.passwordBox);
            this.passwordTab.Location = new System.Drawing.Point(4, 22);
            this.passwordTab.Name = "passwordTab";
            this.passwordTab.Padding = new System.Windows.Forms.Padding(3);
            this.passwordTab.Size = new System.Drawing.Size(318, 58);
            this.passwordTab.TabIndex = 1;
            this.passwordTab.Text = "Пароль";
            this.passwordTab.UseVisualStyleBackColor = true;
            // 
            // backLabel
            // 
            this.backLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.backLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.backLabel.Location = new System.Drawing.Point(6, 28);
            this.backLabel.Name = "backLabel";
            this.backLabel.Size = new System.Drawing.Size(147, 26);
            this.backLabel.TabIndex = 4;
            this.backLabel.TabStop = true;
            this.backLabel.Text = "Назад";
            this.backLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.backLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ReturnBack);
            // 
            // checkPasswordLink
            // 
            this.checkPasswordLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkPasswordLink.Enabled = false;
            this.checkPasswordLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.checkPasswordLink.Location = new System.Drawing.Point(159, 28);
            this.checkPasswordLink.Name = "checkPasswordLink";
            this.checkPasswordLink.Size = new System.Drawing.Size(153, 26);
            this.checkPasswordLink.TabIndex = 3;
            this.checkPasswordLink.TabStop = true;
            this.checkPasswordLink.Text = "Введите пароль";
            this.checkPasswordLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkPasswordLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CheckPassword);
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordBox.Location = new System.Drawing.Point(6, 5);
            this.passwordBox.Mask = "CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC";
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '●';
            this.passwordBox.Size = new System.Drawing.Size(306, 20);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.TextChanged += new System.EventHandler(this.ValidatePassword);
            // 
            // authenticationTable
            // 
            this.authenticationTable.ColumnCount = 1;
            this.authenticationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.authenticationTable.Controls.Add(this.authenticationTabs, 0, 0);
            this.authenticationTable.Controls.Add(this.cancelLink, 0, 1);
            this.authenticationTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.authenticationTable.Location = new System.Drawing.Point(0, 0);
            this.authenticationTable.Name = "authenticationTable";
            this.authenticationTable.RowCount = 2;
            this.authenticationTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.43137F));
            this.authenticationTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.56863F));
            this.authenticationTable.Size = new System.Drawing.Size(332, 116);
            this.authenticationTable.TabIndex = 1;
            // 
            // cancelLink
            // 
            this.cancelLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cancelLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.cancelLink.Location = new System.Drawing.Point(3, 90);
            this.cancelLink.Name = "cancelLink";
            this.cancelLink.Size = new System.Drawing.Size(326, 26);
            this.cancelLink.TabIndex = 2;
            this.cancelLink.TabStop = true;
            this.cancelLink.Text = "Отмена";
            this.cancelLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cancelLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Exit);
            // 
            // AuthenticationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelLink;
            this.ClientSize = new System.Drawing.Size(332, 116);
            this.Controls.Add(this.authenticationTable);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AuthenticationForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hypnos – Аутентификация";
            this.authenticationTabs.ResumeLayout(false);
            this.loginTab.ResumeLayout(false);
            this.loginTab.PerformLayout();
            this.passwordTab.ResumeLayout(false);
            this.passwordTab.PerformLayout();
            this.authenticationTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl authenticationTabs;
        private System.Windows.Forms.TabPage loginTab;
        private System.Windows.Forms.TabPage passwordTab;
        private System.Windows.Forms.TableLayoutPanel authenticationTable;
        private System.Windows.Forms.LinkLabel cancelLink;
        private System.Windows.Forms.LinkLabel checkLoginLink;
        private System.Windows.Forms.MaskedTextBox loginBox;
        private System.Windows.Forms.LinkLabel backLabel;
        private System.Windows.Forms.LinkLabel checkPasswordLink;
        private System.Windows.Forms.MaskedTextBox passwordBox;
    }
}

