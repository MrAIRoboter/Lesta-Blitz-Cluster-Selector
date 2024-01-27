namespace Lesta_Blitz_Cluster_Selector
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DeveloperLinkLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ClustersCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // DeveloperLinkLabel
            // 
            this.DeveloperLinkLabel.AutoSize = true;
            this.DeveloperLinkLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeveloperLinkLabel.ForeColor = System.Drawing.Color.Silver;
            this.DeveloperLinkLabel.Location = new System.Drawing.Point(10, 228);
            this.DeveloperLinkLabel.Name = "DeveloperLinkLabel";
            this.DeveloperLinkLabel.Size = new System.Drawing.Size(138, 13);
            this.DeveloperLinkLabel.TabIndex = 1;
            this.DeveloperLinkLabel.Text = "Разработано MrAIRobot";
            this.DeveloperLinkLabel.Click += new System.EventHandler(this.DeveloperLinkLabel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выбрать кластеры:";
            // 
            // ClustersCheckedListBox
            // 
            this.ClustersCheckedListBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClustersCheckedListBox.FormattingEnabled = true;
            this.ClustersCheckedListBox.Location = new System.Drawing.Point(17, 38);
            this.ClustersCheckedListBox.Name = "ClustersCheckedListBox";
            this.ClustersCheckedListBox.Size = new System.Drawing.Size(361, 164);
            this.ClustersCheckedListBox.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(390, 250);
            this.Controls.Add(this.ClustersCheckedListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DeveloperLinkLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lesta Blitz Cluster Selector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label DeveloperLinkLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox ClustersCheckedListBox;
    }
}

