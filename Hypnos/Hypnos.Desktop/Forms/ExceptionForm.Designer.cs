namespace Hypnos.Desktop.Forms
{
    partial class ExceptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionForm));
            this.mainTable = new System.Windows.Forms.TableLayoutPanel();
            this.linksFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.terminateLink = new System.Windows.Forms.LinkLabel();
            this.continueLink = new System.Windows.Forms.LinkLabel();
            this.detailsTabs = new System.Windows.Forms.TabControl();
            this.warningPage = new System.Windows.Forms.TabPage();
            this.warningLabel = new System.Windows.Forms.Label();
            this.mainTable.SuspendLayout();
            this.linksFlow.SuspendLayout();
            this.detailsTabs.SuspendLayout();
            this.warningPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTable
            // 
            this.mainTable.ColumnCount = 1;
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTable.Controls.Add(this.linksFlow, 0, 1);
            this.mainTable.Controls.Add(this.detailsTabs, 0, 0);
            this.mainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTable.Location = new System.Drawing.Point(0, 0);
            this.mainTable.Name = "mainTable";
            this.mainTable.RowCount = 2;
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.mainTable.Size = new System.Drawing.Size(800, 450);
            this.mainTable.TabIndex = 0;
            // 
            // linksFlow
            // 
            this.linksFlow.Controls.Add(this.terminateLink);
            this.linksFlow.Controls.Add(this.continueLink);
            this.linksFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linksFlow.Location = new System.Drawing.Point(3, 417);
            this.linksFlow.Name = "linksFlow";
            this.linksFlow.Size = new System.Drawing.Size(794, 30);
            this.linksFlow.TabIndex = 0;
            this.linksFlow.WrapContents = false;
            // 
            // terminateLink
            // 
            this.terminateLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.terminateLink.LinkColor = System.Drawing.Color.Red;
            this.terminateLink.Location = new System.Drawing.Point(3, 0);
            this.terminateLink.Name = "terminateLink";
            this.terminateLink.Size = new System.Drawing.Size(100, 23);
            this.terminateLink.TabIndex = 0;
            this.terminateLink.TabStop = true;
            this.terminateLink.Text = "Завершить";
            this.terminateLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.terminateLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Terminate);
            // 
            // continueLink
            // 
            this.continueLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.continueLink.LinkColor = System.Drawing.Color.Blue;
            this.continueLink.Location = new System.Drawing.Point(109, 0);
            this.continueLink.Name = "continueLink";
            this.continueLink.Size = new System.Drawing.Size(100, 23);
            this.continueLink.TabIndex = 1;
            this.continueLink.TabStop = true;
            this.continueLink.Text = "Продолжить";
            this.continueLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.continueLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ContinueProgram);
            // 
            // detailsTabs
            // 
            this.detailsTabs.Controls.Add(this.warningPage);
            this.detailsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsTabs.Location = new System.Drawing.Point(3, 3);
            this.detailsTabs.Name = "detailsTabs";
            this.detailsTabs.SelectedIndex = 0;
            this.detailsTabs.Size = new System.Drawing.Size(794, 408);
            this.detailsTabs.TabIndex = 1;
            // 
            // warningPage
            // 
            this.warningPage.Controls.Add(this.warningLabel);
            this.warningPage.Location = new System.Drawing.Point(4, 22);
            this.warningPage.Name = "warningPage";
            this.warningPage.Padding = new System.Windows.Forms.Padding(3);
            this.warningPage.Size = new System.Drawing.Size(786, 382);
            this.warningPage.TabIndex = 0;
            this.warningPage.Text = "Внимание!";
            this.warningPage.UseVisualStyleBackColor = true;
            // 
            // warningLabel
            // 
            this.warningLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.warningLabel.Location = new System.Drawing.Point(3, 3);
            this.warningLabel.Name = "warningLabel";
            this.warningLabel.Padding = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.warningLabel.Size = new System.Drawing.Size(780, 376);
            this.warningLabel.TabIndex = 0;
            this.warningLabel.Text = resources.GetString("warningLabel.Text");
            this.warningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ExceptionForm
            // 
            this.AcceptButton = this.terminateLink;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.continueLink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hypnos – Исключительная ситуация";
            this.mainTable.ResumeLayout(false);
            this.linksFlow.ResumeLayout(false);
            this.detailsTabs.ResumeLayout(false);
            this.warningPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTable;
        private System.Windows.Forms.FlowLayoutPanel linksFlow;
        private System.Windows.Forms.LinkLabel terminateLink;
        private System.Windows.Forms.LinkLabel continueLink;
        private System.Windows.Forms.TabControl detailsTabs;
        private System.Windows.Forms.TabPage warningPage;
        private System.Windows.Forms.Label warningLabel;
    }
}