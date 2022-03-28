
namespace AgentiiDeTurism.src.controller
{
    partial class MyForm
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
            this.tabLogin = new AgentiiDeTurism.src.controller.TabLogin();
            this.tabMain = new AgentiiDeTurism.src.controller.TabMain();
            this.SuspendLayout();
            // 
            // tabLogin
            // 
            this.tabLogin.Location = new System.Drawing.Point(12, 12);
            this.tabLogin.Name = "tabLogin";
            this.tabLogin.Size = new System.Drawing.Size(1137, 449);
            this.tabLogin.TabIndex = 20;
            // 
            // tabMain1
            // 
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain1";
            this.tabMain.Size = new System.Drawing.Size(1137, 449);
            this.tabMain.TabIndex = 21;
            // 
            // MyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 473);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.tabLogin);
            this.Name = "MyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);

        }

        #endregion
        private TabLogin tabLogin;
        private TabMain tabMain;
    }
}