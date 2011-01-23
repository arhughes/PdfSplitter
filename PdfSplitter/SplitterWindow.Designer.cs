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
            // SplitterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 482);
            this.Controls.Add(this.preview_size_combo);
            this.Controls.Add(this.preview_panel);
            this.Controls.Add(this.open_button);
            this.Name = "SplitterWindow";
            this.Text = "PDF Splitter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button open_button;
        private System.Windows.Forms.FlowLayoutPanel preview_panel;
        private System.Windows.Forms.ComboBox preview_size_combo;
    }
}

