using Trascend.Bolet.ControlesByT;

namespace Trascend.Bolet.ControlesByT.Ventanas
{
    partial class ChildWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChildWindow));
            this._detalle = new System.Windows.Forms.TextBox();
            this._btnAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _detalle
            // 
            this._detalle.Location = new System.Drawing.Point(12, 32);
            this._detalle.Multiline = true;
            this._detalle.Name = "_detalle";
            this._detalle.Size = new System.Drawing.Size(268, 198);
            this._detalle.TabIndex = 0;
            this._detalle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._detalle_KeyPress);
            // 
            // _btnAceptar
            // 
            this._btnAceptar.BackColor = System.Drawing.Color.White;
            this._btnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this._btnAceptar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this._btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this._btnAceptar.Location = new System.Drawing.Point(205, 236);
            this._btnAceptar.Name = "_btnAceptar";
            this._btnAceptar.Size = new System.Drawing.Size(75, 23);
            this._btnAceptar.TabIndex = 1;
            this._btnAceptar.Text = "Aceptar";
            this._btnAceptar.UseVisualStyleBackColor = false;
            this._btnAceptar.Click += new System.EventHandler(this._btnAceptar_Click);
            // 
            // ChildWindow
            // 
            this.AcceptButton = this._btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(235)))), ((int)(((byte)(194)))));
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this._btnAceptar);
            this.Controls.Add(this._detalle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "ChildWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bolet & Terrero - Zoom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _detalle;
        private System.Windows.Forms.Button _btnAceptar;

    }
}