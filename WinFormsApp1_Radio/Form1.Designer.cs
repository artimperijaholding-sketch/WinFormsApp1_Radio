namespace WinFormsApp1_Radio
{
    partial class Form1
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
            buttonFavorite = new Button();
            buttonHistory = new Button();
            comboBox1 = new ComboBox();
            itemsPanel = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // buttonFavorite
            // 
            buttonFavorite.Location = new Point(346, 12);
            buttonFavorite.Name = "buttonFavorite";
            buttonFavorite.Size = new Size(75, 23);
            buttonFavorite.TabIndex = 0;
            buttonFavorite.Text = "Favorite";
            buttonFavorite.UseVisualStyleBackColor = true;
            buttonFavorite.Click += buttonFavorite_Click;
            // 
            // buttonHistory
            // 
            buttonHistory.Location = new Point(241, 11);
            buttonHistory.Name = "buttonHistory";
            buttonHistory.Size = new Size(75, 23);
            buttonHistory.TabIndex = 1;
            buttonHistory.Text = "History";
            buttonHistory.UseVisualStyleBackColor = true;
            buttonHistory.Click += buttonHistory_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(21, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(201, 23);
            comboBox1.TabIndex = 3;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // itemsPanel
            // 
            itemsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            itemsPanel.AutoScroll = true;
            itemsPanel.Location = new Point(2, 65);
            itemsPanel.Name = "itemsPanel";
            itemsPanel.Size = new Size(433, 338);
            itemsPanel.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 403);
            Controls.Add(itemsPanel);
            Controls.Add(comboBox1);
            Controls.Add(buttonHistory);
            Controls.Add(buttonFavorite);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button buttonFavorite;
        private Button buttonHistory;
        private ComboBox comboBox1;
        private FlowLayoutPanel itemsPanel;
    }
}
