namespace PdfSplitter
{
    partial class SplitterWindow
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
            this.open_button = new System.Windows.Forms.Button();
            this.preview_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.preview_size_combo = new System.Windows.Forms.ComboBox();
            this.status_strip = new System.Windows.Forms.StatusStrip();
            this.status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.springy_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_progress_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_progress = new System.Windows.Forms.ToolStripProgressBar();
            this.file_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.status_strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // open_button
            // 
            this.open_button.Location = new System.Drawing.Point(12, 12);
            this.open_button.Name = "open_button";
            this.open_button.Size = new System.Drawing.Size(75, 23);
            this.open_button.TabIndex = 0;
            this.open_button.Text = "Open";
            this.open_button.UseVisualStyleBackColor = true;
            this.open_button.Click += new System.EventHandler(this.open_button_Click);
            // 
            // preview_panel
            // 
            this.preview_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.preview_panel.AutoScroll = true;
            this.preview_panel.Location = new System.Drawing.Point(12, 41);
            this.preview_panel.Name = "preview_panel";
            this.preview_panel.Size = new System.Drawing.Size(660, 245);
            this.preview_panel.TabIndex = 2;
            // 
            // preview_size_combo
            // 
            this.preview_size_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.preview_size_combo.FormattingEnabled = true;
            this.preview_size_combo.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "Large"});
            this.preview_size_combo.Location = new System.Drawing.Point(551, 14);
            this.preview_size_combo.Name = "preview_size_combo";
            this.preview_size_combo.Size = new System.Drawing.Size(121, 21);
            this.preview_size_combo.TabIndex = 3;
            // 
            // status_strip
            // 
            this.status_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_label,
            this.springy_label,
            this.status_progress_label,
            this.status_progress});
            this.status_strip.Location = new System.Drawing.Point(0, 460);
            this.status_strip.Name = "status_strip";
            this.status_strip.Size = new System.Drawing.Size(684, 22);
            this.status_strip.TabIndex = 4;
            this.status_strip.Text = "statusStrip1";
            // 
            // status_label
            // 
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(0, 17);
            // 
            // springy_label
            // 
            this.springy_label.Name = "springy_label";
            this.springy_label.Size = new System.Drawing.Size(669, 17);
            this.springy_label.Spring = true;
            // 
            // status_progress_label
            // 
            this.status_progress_label.Name = "status_progress_label";
            this.status_progress_label.Size = new System.Drawing.Size(0, 17);
            this.status_progress_label.Visible = false;
            // 
            // status_progress
            // 
            this.status_progress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.status_progress.Name = "status_progress";
            this.status_progress.Size = new System.Drawing.Size(100, 16);
            this.status_progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.status_progress.Visible = false;
            // 
            // file_panel
            // 
            this.file_panel.AllowDrop = true;
            this.file_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.file_panel.Location = new System.Drawing.Point(12, 292);
            this.file_panel.Name = "file_panel";
            this.file_panel.Size = new System.Drawing.Size(660, 165);
            this.file_panel.TabIndex = 5;
            // 
            // SplitterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 482);
            this.Controls.Add(this.file_panel);
            this.Controls.Add(this.status_strip);
            this.Controls.Add(this.preview_size_combo);
            this.Controls.Add(this.preview_panel);
            this.Controls.Add(this.open_button);
            this.Name = "SplitterWindow";
            this.Text = "PDF Splitter";
            this.status_strip.ResumeLayout(false);
            this.status_strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button open_button;
        private System.Windows.Forms.FlowLayoutPanel preview_panel;
        private System.Windows.Forms.ComboBox preview_size_combo;
        private System.Windows.Forms.StatusStrip status_strip;
        private System.Windows.Forms.ToolStripStatusLabel status_label;
        private System.Windows.Forms.ToolStripProgressBar status_progress;
        private System.Windows.Forms.ToolStripStatusLabel status_progress_label;
        private System.Windows.Forms.ToolStripStatusLabel springy_label;
        private System.Windows.Forms.FlowLayoutPanel file_panel;
    }
}

