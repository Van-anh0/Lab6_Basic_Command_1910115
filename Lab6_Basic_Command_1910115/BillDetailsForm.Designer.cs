
namespace Lab6_Basic_Command_1910115
{
    partial class BillDetailsForm
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
            this.dtgBillDetails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgBillDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgBillDetails
            // 
            this.dtgBillDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgBillDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgBillDetails.Location = new System.Drawing.Point(-2, -2);
            this.dtgBillDetails.Name = "dtgBillDetails";
            this.dtgBillDetails.RowHeadersWidth = 51;
            this.dtgBillDetails.RowTemplate.Height = 24;
            this.dtgBillDetails.Size = new System.Drawing.Size(809, 459);
            this.dtgBillDetails.TabIndex = 0;
            // 
            // BillDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtgBillDetails);
            this.Name = "BillDetailsForm";
            this.Text = "BillDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dtgBillDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgBillDetails;
    }
}