namespace Ex05.MemoryGameUI
{
    partial class FormGameBoard
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
            this.memoryGameLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.playersInfoPanel = new System.Windows.Forms.Panel();
            this.secondPlayerInfoLabel = new System.Windows.Forms.Label();
            this.firstPlayerInfoLabel = new System.Windows.Forms.Label();
            this.currentPlayerInfoLabel = new System.Windows.Forms.Label();
            this.playersInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // memoryGameLayoutPanel
            // 
            this.memoryGameLayoutPanel.ColumnCount = 2;
            this.memoryGameLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.memoryGameLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.memoryGameLayoutPanel.Location = new System.Drawing.Point(11, 10);
            this.memoryGameLayoutPanel.Margin = new System.Windows.Forms.Padding(10);
            this.memoryGameLayoutPanel.Name = "memoryGameLayoutPanel";
            this.memoryGameLayoutPanel.RowCount = 2;
            this.memoryGameLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.memoryGameLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.memoryGameLayoutPanel.Size = new System.Drawing.Size(840, 512);
            this.memoryGameLayoutPanel.TabIndex = 0;
            // 
            // playersInfoPanel
            // 
            this.playersInfoPanel.Controls.Add(this.secondPlayerInfoLabel);
            this.playersInfoPanel.Controls.Add(this.firstPlayerInfoLabel);
            this.playersInfoPanel.Controls.Add(this.currentPlayerInfoLabel);
            this.playersInfoPanel.Location = new System.Drawing.Point(12, 532);
            this.playersInfoPanel.Name = "playersInfoPanel";
            this.playersInfoPanel.Size = new System.Drawing.Size(839, 78);
            this.playersInfoPanel.TabIndex = 1;
            // 
            // secondPlayerInfoLabel
            // 
            this.secondPlayerInfoLabel.AutoSize = true;
            this.secondPlayerInfoLabel.Location = new System.Drawing.Point(3, 60);
            this.secondPlayerInfoLabel.Name = "secondPlayerInfoLabel";
            this.secondPlayerInfoLabel.Size = new System.Drawing.Size(0, 13);
            this.secondPlayerInfoLabel.TabIndex = 2;
            // 
            // firstPlayerInfoLabel
            // 
            this.firstPlayerInfoLabel.AutoSize = true;
            this.firstPlayerInfoLabel.Location = new System.Drawing.Point(3, 35);
            this.firstPlayerInfoLabel.Name = "firstPlayerInfoLabel";
            this.firstPlayerInfoLabel.Size = new System.Drawing.Size(0, 13);
            this.firstPlayerInfoLabel.TabIndex = 1;
            // 
            // currentPlayerInfoLabel
            // 
            this.currentPlayerInfoLabel.AutoSize = true;
            this.currentPlayerInfoLabel.Location = new System.Drawing.Point(3, 10);
            this.currentPlayerInfoLabel.Name = "currentPlayerInfoLabel";
            this.currentPlayerInfoLabel.Size = new System.Drawing.Size(0, 13);
            this.currentPlayerInfoLabel.TabIndex = 0;
            // 
            // FormGameBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 622);
            this.Controls.Add(this.playersInfoPanel);
            this.Controls.Add(this.memoryGameLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameBoard";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Memory Game";
            this.playersInfoPanel.ResumeLayout(false);
            this.playersInfoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel memoryGameLayoutPanel;
        private System.Windows.Forms.Panel playersInfoPanel;
        private System.Windows.Forms.Label currentPlayerInfoLabel;
        private System.Windows.Forms.Label secondPlayerInfoLabel;
        private System.Windows.Forms.Label firstPlayerInfoLabel;
    }
}