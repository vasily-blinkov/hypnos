namespace Hypnos.Desktop.Controls
{
    partial class ExceptionControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTable = new System.Windows.Forms.TableLayoutPanel();
            this.stackPanel = new System.Windows.Forms.Panel();
            this.stackBox = new System.Windows.Forms.TextBox();
            this.stackLabel = new System.Windows.Forms.Label();
            this.messagePanel = new System.Windows.Forms.Panel();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.mainTable.SuspendLayout();
            this.stackPanel.SuspendLayout();
            this.messagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTable
            // 
            this.mainTable.ColumnCount = 1;
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTable.Controls.Add(this.stackPanel, 0, 1);
            this.mainTable.Controls.Add(this.messagePanel, 0, 0);
            this.mainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTable.Location = new System.Drawing.Point(0, 0);
            this.mainTable.Name = "mainTable";
            this.mainTable.RowCount = 2;
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.33333F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.66666F));
            this.mainTable.Size = new System.Drawing.Size(700, 300);
            this.mainTable.TabIndex = 0;
            // 
            // stackPanel
            // 
            this.stackPanel.Controls.Add(this.stackBox);
            this.stackPanel.Controls.Add(this.stackLabel);
            this.stackPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackPanel.Location = new System.Drawing.Point(3, 43);
            this.stackPanel.Name = "stackPanel";
            this.stackPanel.Size = new System.Drawing.Size(694, 254);
            this.stackPanel.TabIndex = 4;
            // 
            // stackBox
            // 
            this.stackBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackBox.Location = new System.Drawing.Point(0, 17);
            this.stackBox.Multiline = true;
            this.stackBox.Name = "stackBox";
            this.stackBox.ReadOnly = true;
            this.stackBox.Size = new System.Drawing.Size(694, 237);
            this.stackBox.TabIndex = 4;
            // 
            // stackLabel
            // 
            this.stackLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.stackLabel.Location = new System.Drawing.Point(0, 0);
            this.stackLabel.Name = "stackLabel";
            this.stackLabel.Size = new System.Drawing.Size(694, 17);
            this.stackLabel.TabIndex = 3;
            this.stackLabel.Text = "Стек вызовов";
            this.stackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // messagePanel
            // 
            this.messagePanel.Controls.Add(this.messageBox);
            this.messagePanel.Controls.Add(this.messageLabel);
            this.messagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagePanel.Location = new System.Drawing.Point(3, 3);
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Size = new System.Drawing.Size(694, 34);
            this.messagePanel.TabIndex = 3;
            // 
            // messageBox
            // 
            this.messageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageBox.Location = new System.Drawing.Point(0, 17);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(694, 17);
            this.messageBox.TabIndex = 4;
            // 
            // messageLabel
            // 
            this.messageLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.messageLabel.Location = new System.Drawing.Point(0, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(694, 17);
            this.messageLabel.TabIndex = 3;
            this.messageLabel.Text = "Сообщение";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExceptionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainTable);
            this.Name = "ExceptionControl";
            this.Size = new System.Drawing.Size(700, 300);
            this.mainTable.ResumeLayout(false);
            this.stackPanel.ResumeLayout(false);
            this.stackPanel.PerformLayout();
            this.messagePanel.ResumeLayout(false);
            this.messagePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTable;
        private System.Windows.Forms.Panel messagePanel;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Panel stackPanel;
        private System.Windows.Forms.TextBox stackBox;
        private System.Windows.Forms.Label stackLabel;
    }
}
