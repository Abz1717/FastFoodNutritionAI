namespace FastFoodNutritionAI
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDLS = new System.Windows.Forms.Button();
            this.btnGS = new System.Windows.Forms.Button();
            this.btnBFGS = new System.Windows.Forms.Button();
            this.btnAS = new System.Windows.Forms.Button();
            this.lblTotalPathCost = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(84, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1116, 890);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnDLS
            // 
            this.btnDLS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDLS.Location = new System.Drawing.Point(1294, 439);
            this.btnDLS.Name = "btnDLS";
            this.btnDLS.Size = new System.Drawing.Size(236, 200);
            this.btnDLS.TabIndex = 1;
            this.btnDLS.Text = "Depth-Limited Search";
            this.btnDLS.UseVisualStyleBackColor = true;
            this.btnDLS.Click += new System.EventHandler(this.btnDLS_Click);
            // 
            // btnGS
            // 
            this.btnGS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGS.Location = new System.Drawing.Point(1294, 198);
            this.btnGS.Name = "btnGS";
            this.btnGS.Size = new System.Drawing.Size(236, 200);
            this.btnGS.TabIndex = 2;
            this.btnGS.Text = "Greedy Search";
            this.btnGS.UseVisualStyleBackColor = true;
            this.btnGS.Click += new System.EventHandler(this.btnGS_Click);
            // 
            // btnBFGS
            // 
            this.btnBFGS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBFGS.Location = new System.Drawing.Point(1577, 198);
            this.btnBFGS.Name = "btnBFGS";
            this.btnBFGS.Size = new System.Drawing.Size(236, 200);
            this.btnBFGS.TabIndex = 3;
            this.btnBFGS.Text = "Breadth-First Graph Search";
            this.btnBFGS.UseVisualStyleBackColor = true;
            this.btnBFGS.Click += new System.EventHandler(this.btnBFGS_Click);
            // 
            // btnAS
            // 
            this.btnAS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAS.Location = new System.Drawing.Point(1577, 439);
            this.btnAS.Name = "btnAS";
            this.btnAS.Size = new System.Drawing.Size(236, 200);
            this.btnAS.TabIndex = 4;
            this.btnAS.Text = "A* Search";
            this.btnAS.UseVisualStyleBackColor = true;
            this.btnAS.Click += new System.EventHandler(this.btnAS_Click);
            // 
            // lblTotalPathCost
            // 
            this.lblTotalPathCost.AutoSize = true;
            this.lblTotalPathCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPathCost.Location = new System.Drawing.Point(1297, 718);
            this.lblTotalPathCost.Name = "lblTotalPathCost";
            this.lblTotalPathCost.Size = new System.Drawing.Size(307, 46);
            this.lblTotalPathCost.TabIndex = 5;
            this.lblTotalPathCost.Text = "Total Path Cost:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1050);
            this.Controls.Add(this.lblTotalPathCost);
            this.Controls.Add(this.btnAS);
            this.Controls.Add(this.btnBFGS);
            this.Controls.Add(this.btnGS);
            this.Controls.Add(this.btnDLS);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "FastFoodNutritionAI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDLS;
        private System.Windows.Forms.Button btnGS;
        private System.Windows.Forms.Button btnBFGS;
        private System.Windows.Forms.Button btnAS;
        private System.Windows.Forms.Label lblTotalPathCost;
    }
}

