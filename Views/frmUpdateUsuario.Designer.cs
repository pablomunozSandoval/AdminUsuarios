namespace AdminUsuarios.Views
{
    partial class frmUpdateUsuario
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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.rcTxbDatos = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(12, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(98, 50);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Buscar";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnClickBuscar);
            // 
            // rcTxbDatos
            // 
            this.rcTxbDatos.Location = new System.Drawing.Point(90, 145);
            this.rcTxbDatos.Name = "rcTxbDatos";
            this.rcTxbDatos.Size = new System.Drawing.Size(594, 260);
            this.rcTxbDatos.TabIndex = 1;
            this.rcTxbDatos.Text = "";
            // 
            // btnBuscarDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rcTxbDatos);
            this.Controls.Add(this.btnUpdate);
            this.Name = "btnBuscarDatos";
            this.Text = "UpdateUsuario";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.RichTextBox rcTxbDatos;
    }
}