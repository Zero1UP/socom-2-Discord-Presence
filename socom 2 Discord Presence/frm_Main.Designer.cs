namespace socom_2_Discord_Presence
{
    partial class frm_Main
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
            this.components = new System.ComponentModel.Container();
            this.tmr_CheckPCSX2 = new System.Windows.Forms.Timer(this.components);
            this.tmr_GetPCSX2Data = new System.Windows.Forms.Timer(this.components);
            this.lbl_PCSX2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmr_CheckPCSX2
            // 
            this.tmr_CheckPCSX2.Enabled = true;
            this.tmr_CheckPCSX2.Tick += new System.EventHandler(this.tmr_CheckPCSX2_Tick);
            // 
            // tmr_GetPCSX2Data
            // 
            this.tmr_GetPCSX2Data.Enabled = true;
            this.tmr_GetPCSX2Data.Interval = 3000;
            this.tmr_GetPCSX2Data.Tick += new System.EventHandler(this.tmr_GetPCSX2Data_Tick);
            // 
            // lbl_PCSX2
            // 
            this.lbl_PCSX2.AutoSize = true;
            this.lbl_PCSX2.Location = new System.Drawing.Point(12, 13);
            this.lbl_PCSX2.Name = "lbl_PCSX2";
            this.lbl_PCSX2.Size = new System.Drawing.Size(35, 13);
            this.lbl_PCSX2.TabIndex = 1;
            this.lbl_PCSX2.Text = "label1";
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(221, 35);
            this.Controls.Add(this.lbl_PCSX2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(237, 74);
            this.MinimumSize = new System.Drawing.Size(237, 74);
            this.Name = "frm_Main";
            this.Text = "SOCOM II";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmr_CheckPCSX2;
        private System.Windows.Forms.Timer tmr_GetPCSX2Data;
        private System.Windows.Forms.Label lbl_PCSX2;
    }
}

