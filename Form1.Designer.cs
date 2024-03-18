
namespace VNPT_LIST
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnGetShop = new System.Windows.Forms.Button();
            this.getDigishop = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnData = new System.Windows.Forms.Button();
            this.lbStt = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.txtIndex = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGetShop
            // 
            this.btnGetShop.Enabled = false;
            this.btnGetShop.Location = new System.Drawing.Point(12, 12);
            this.btnGetShop.Name = "btnGetShop";
            this.btnGetShop.Size = new System.Drawing.Size(117, 43);
            this.btnGetShop.TabIndex = 0;
            this.btnGetShop.Text = "Start";
            this.btnGetShop.UseVisualStyleBackColor = true;
            this.btnGetShop.Click += new System.EventHandler(this.btnGetShop_Click);
            // 
            // getDigishop
            // 
            this.getDigishop.Location = new System.Drawing.Point(148, 12);
            this.getDigishop.Name = "getDigishop";
            this.getDigishop.Size = new System.Drawing.Size(117, 43);
            this.getDigishop.TabIndex = 2;
            this.getDigishop.Text = "DIGISHOP";
            this.getDigishop.UseVisualStyleBackColor = true;
            this.getDigishop.Click += new System.EventHandler(this.getDigishop_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Location = new System.Drawing.Point(12, 126);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStatus.Size = new System.Drawing.Size(383, 100);
            this.txtStatus.TabIndex = 3;
            // 
            // btnData
            // 
            this.btnData.Location = new System.Drawing.Point(284, 12);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(117, 43);
            this.btnData.TabIndex = 4;
            this.btnData.Text = "DATA";
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // lbStt
            // 
            this.lbStt.AutoSize = true;
            this.lbStt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStt.Location = new System.Drawing.Point(12, 229);
            this.lbStt.Name = "lbStt";
            this.lbStt.Size = new System.Drawing.Size(28, 20);
            this.lbStt.TabIndex = 5;
            this.lbStt.Text = "stt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Đầu số";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(146, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Thứ tự";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrefix.Location = new System.Drawing.Point(14, 90);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPrefix.Size = new System.Drawing.Size(115, 30);
            this.txtPrefix.TabIndex = 10;
            // 
            // txtIndex
            // 
            this.txtIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIndex.Location = new System.Drawing.Point(148, 90);
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtIndex.Size = new System.Drawing.Size(115, 30);
            this.txtIndex.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 255);
            this.Controls.Add(this.txtIndex);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbStt);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.getDigishop);
            this.Controls.Add(this.btnGetShop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "VNPT_LIST";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetShop;
        private System.Windows.Forms.Button getDigishop;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Label lbStt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.TextBox txtIndex;
    }
}

