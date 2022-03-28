
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
            this.panelLogin = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelPhoneNumber = new System.Windows.Forms.Label();
            this.labelNoSeats = new System.Windows.Forms.Label();
            this.labelClient = new System.Windows.Forms.Label();
            this.labelBefore = new System.Windows.Forms.Label();
            this.labelAfter = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxPhoneNumber = new System.Windows.Forms.TextBox();
            this.textBoxNoSeats = new System.Windows.Forms.TextBox();
            this.textBoxClient = new System.Windows.Forms.TextBox();
            this.buttonReserve = new System.Windows.Forms.Button();
            this.textBoxBefore = new System.Windows.Forms.TextBox();
            this.textBoxAfter = new System.Windows.Forms.TextBox();
            this.comboBoxDestination = new System.Windows.Forms.ComboBox();
            this.labelAgency = new System.Windows.Forms.Label();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.labelReservations = new System.Windows.Forms.Label();
            this.labelTrips = new System.Windows.Forms.Label();
            this.labelAgencies = new System.Windows.Forms.Label();
            this.tableReservations = new System.Windows.Forms.DataGridView();
            this.columnResId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnResClient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnResTrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnResSeats = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableTrips = new System.Windows.Forms.DataGridView();
            this.columnTripId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTripDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTripCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTripDepartureTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTripPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTripSeats = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listAgencies = new System.Windows.Forms.ListView();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.panelLogin.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableReservations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableTrips)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.panelMain);
            this.panelLogin.Controls.Add(this.labelPassword);
            this.panelLogin.Controls.Add(this.labelUsername);
            this.panelLogin.Controls.Add(this.textBoxUsername);
            this.panelLogin.Controls.Add(this.textBoxPassword);
            this.panelLogin.Controls.Add(this.buttonLogin);
            this.panelLogin.Location = new System.Drawing.Point(12, 12);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(1137, 449);
            this.panelLogin.TabIndex = 4;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.labelPhoneNumber);
            this.panelMain.Controls.Add(this.labelNoSeats);
            this.panelMain.Controls.Add(this.labelClient);
            this.panelMain.Controls.Add(this.labelBefore);
            this.panelMain.Controls.Add(this.labelAfter);
            this.panelMain.Controls.Add(this.buttonSearch);
            this.panelMain.Controls.Add(this.textBoxPhoneNumber);
            this.panelMain.Controls.Add(this.textBoxNoSeats);
            this.panelMain.Controls.Add(this.textBoxClient);
            this.panelMain.Controls.Add(this.buttonReserve);
            this.panelMain.Controls.Add(this.textBoxBefore);
            this.panelMain.Controls.Add(this.textBoxAfter);
            this.panelMain.Controls.Add(this.comboBoxDestination);
            this.panelMain.Controls.Add(this.labelAgency);
            this.panelMain.Controls.Add(this.buttonLogout);
            this.panelMain.Controls.Add(this.labelReservations);
            this.panelMain.Controls.Add(this.labelTrips);
            this.panelMain.Controls.Add(this.labelAgencies);
            this.panelMain.Controls.Add(this.tableReservations);
            this.panelMain.Controls.Add(this.tableTrips);
            this.panelMain.Controls.Add(this.listAgencies);
            this.panelMain.Location = new System.Drawing.Point(3, 3);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1128, 443);
            this.panelMain.TabIndex = 5;
            // 
            // labelPhoneNumber
            // 
            this.labelPhoneNumber.AutoSize = true;
            this.labelPhoneNumber.Location = new System.Drawing.Point(517, 388);
            this.labelPhoneNumber.Name = "labelPhoneNumber";
            this.labelPhoneNumber.Size = new System.Drawing.Size(101, 17);
            this.labelPhoneNumber.TabIndex = 19;
            this.labelPhoneNumber.Text = "Phone number";
            // 
            // labelNoSeats
            // 
            this.labelNoSeats.AutoSize = true;
            this.labelNoSeats.Location = new System.Drawing.Point(504, 358);
            this.labelNoSeats.Name = "labelNoSeats";
            this.labelNoSeats.Size = new System.Drawing.Size(112, 17);
            this.labelNoSeats.TabIndex = 18;
            this.labelNoSeats.Text = "Number of seats";
            // 
            // labelClient
            // 
            this.labelClient.AutoSize = true;
            this.labelClient.Location = new System.Drawing.Point(573, 332);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(43, 17);
            this.labelClient.TabIndex = 17;
            this.labelClient.Text = "Client";
            // 
            // labelBefore
            // 
            this.labelBefore.AutoSize = true;
            this.labelBefore.Location = new System.Drawing.Point(38, 385);
            this.labelBefore.Name = "labelBefore";
            this.labelBefore.Size = new System.Drawing.Size(124, 17);
            this.labelBefore.TabIndex = 16;
            this.labelBefore.Text = "Before (hh:mm:ss)";
            // 
            // labelAfter
            // 
            this.labelAfter.AutoSize = true;
            this.labelAfter.Location = new System.Drawing.Point(50, 355);
            this.labelAfter.Name = "labelAfter";
            this.labelAfter.Size = new System.Drawing.Size(112, 17);
            this.labelAfter.TabIndex = 15;
            this.labelAfter.Text = "After (hh:mm:ss)";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(168, 413);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(146, 23);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxPhoneNumber
            // 
            this.textBoxPhoneNumber.Location = new System.Drawing.Point(622, 385);
            this.textBoxPhoneNumber.Name = "textBoxPhoneNumber";
            this.textBoxPhoneNumber.Size = new System.Drawing.Size(138, 22);
            this.textBoxPhoneNumber.TabIndex = 14;
            // 
            // textBoxNoSeats
            // 
            this.textBoxNoSeats.Location = new System.Drawing.Point(622, 355);
            this.textBoxNoSeats.Name = "textBoxNoSeats";
            this.textBoxNoSeats.Size = new System.Drawing.Size(138, 22);
            this.textBoxNoSeats.TabIndex = 13;
            // 
            // textBoxClient
            // 
            this.textBoxClient.Location = new System.Drawing.Point(622, 327);
            this.textBoxClient.Name = "textBoxClient";
            this.textBoxClient.Size = new System.Drawing.Size(138, 22);
            this.textBoxClient.TabIndex = 12;
            // 
            // buttonReserve
            // 
            this.buttonReserve.Location = new System.Drawing.Point(622, 413);
            this.buttonReserve.Name = "buttonReserve";
            this.buttonReserve.Size = new System.Drawing.Size(138, 23);
            this.buttonReserve.TabIndex = 11;
            this.buttonReserve.Text = "Reserve";
            this.buttonReserve.UseVisualStyleBackColor = true;
            this.buttonReserve.Click += new System.EventHandler(this.buttonReserve_Click);
            // 
            // textBoxBefore
            // 
            this.textBoxBefore.Location = new System.Drawing.Point(168, 385);
            this.textBoxBefore.Name = "textBoxBefore";
            this.textBoxBefore.Size = new System.Drawing.Size(146, 22);
            this.textBoxBefore.TabIndex = 10;
            // 
            // textBoxAfter
            // 
            this.textBoxAfter.Location = new System.Drawing.Point(168, 356);
            this.textBoxAfter.Name = "textBoxAfter";
            this.textBoxAfter.Size = new System.Drawing.Size(146, 22);
            this.textBoxAfter.TabIndex = 9;
            // 
            // comboBoxDestination
            // 
            this.comboBoxDestination.FormattingEnabled = true;
            this.comboBoxDestination.Location = new System.Drawing.Point(168, 325);
            this.comboBoxDestination.Name = "comboBoxDestination";
            this.comboBoxDestination.Size = new System.Drawing.Size(146, 24);
            this.comboBoxDestination.TabIndex = 8;
            this.comboBoxDestination.SelectedIndexChanged += new System.EventHandler(this.comboBoxDestination_SelectedIndexChanged);
            // 
            // labelAgency
            // 
            this.labelAgency.AutoSize = true;
            this.labelAgency.Location = new System.Drawing.Point(3, 7);
            this.labelAgency.Name = "labelAgency";
            this.labelAgency.Size = new System.Drawing.Size(85, 17);
            this.labelAgency.TabIndex = 7;
            this.labelAgency.Text = "labelAgency";
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(161, 4);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(75, 23);
            this.buttonLogout.TabIndex = 6;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // labelReservations
            // 
            this.labelReservations.AutoSize = true;
            this.labelReservations.Location = new System.Drawing.Point(803, 38);
            this.labelReservations.Name = "labelReservations";
            this.labelReservations.Size = new System.Drawing.Size(91, 17);
            this.labelReservations.TabIndex = 5;
            this.labelReservations.Text = "Reservations";
            // 
            // labelTrips
            // 
            this.labelTrips.AutoSize = true;
            this.labelTrips.Location = new System.Drawing.Point(286, 35);
            this.labelTrips.Name = "labelTrips";
            this.labelTrips.Size = new System.Drawing.Size(40, 17);
            this.labelTrips.TabIndex = 4;
            this.labelTrips.Text = "Trips";
            // 
            // labelAgencies
            // 
            this.labelAgencies.AutoSize = true;
            this.labelAgencies.Location = new System.Drawing.Point(0, 35);
            this.labelAgencies.Name = "labelAgencies";
            this.labelAgencies.Size = new System.Drawing.Size(66, 17);
            this.labelAgencies.TabIndex = 3;
            this.labelAgencies.Text = "Agencies";
            // 
            // tableReservations
            // 
            this.tableReservations.AllowUserToAddRows = false;
            this.tableReservations.AllowUserToDeleteRows = false;
            this.tableReservations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableReservations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnResId,
            this.ColumnResClient,
            this.columnResTrip,
            this.columnResSeats});
            this.tableReservations.Location = new System.Drawing.Point(766, 58);
            this.tableReservations.Name = "tableReservations";
            this.tableReservations.ReadOnly = true;
            this.tableReservations.RowHeadersWidth = 51;
            this.tableReservations.RowTemplate.Height = 24;
            this.tableReservations.Size = new System.Drawing.Size(359, 261);
            this.tableReservations.TabIndex = 2;
            // 
            // columnResId
            // 
            this.columnResId.HeaderText = "Id";
            this.columnResId.MinimumWidth = 6;
            this.columnResId.Name = "columnResId";
            this.columnResId.ReadOnly = true;
            this.columnResId.Visible = false;
            this.columnResId.Width = 10;
            // 
            // ColumnResClient
            // 
            this.ColumnResClient.HeaderText = "Client";
            this.ColumnResClient.MinimumWidth = 6;
            this.ColumnResClient.Name = "ColumnResClient";
            this.ColumnResClient.ReadOnly = true;
            this.ColumnResClient.Width = 60;
            // 
            // columnResTrip
            // 
            this.columnResTrip.HeaderText = "Trip";
            this.columnResTrip.MinimumWidth = 6;
            this.columnResTrip.Name = "columnResTrip";
            this.columnResTrip.ReadOnly = true;
            this.columnResTrip.Width = 125;
            // 
            // columnResSeats
            // 
            this.columnResSeats.HeaderText = "Seats";
            this.columnResSeats.MinimumWidth = 6;
            this.columnResSeats.Name = "columnResSeats";
            this.columnResSeats.ReadOnly = true;
            this.columnResSeats.Width = 40;
            // 
            // tableTrips
            // 
            this.tableTrips.AllowUserToAddRows = false;
            this.tableTrips.AllowUserToDeleteRows = false;
            this.tableTrips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableTrips.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnTripId,
            this.columnTripDestination,
            this.columnTripCompany,
            this.columnTripDepartureTime,
            this.columnTripPrice,
            this.columnTripSeats});
            this.tableTrips.Location = new System.Drawing.Point(168, 58);
            this.tableTrips.Name = "tableTrips";
            this.tableTrips.ReadOnly = true;
            this.tableTrips.RowHeadersWidth = 51;
            this.tableTrips.RowTemplate.Height = 24;
            this.tableTrips.Size = new System.Drawing.Size(592, 261);
            this.tableTrips.TabIndex = 1;
            // 
            // columnTripId
            // 
            this.columnTripId.HeaderText = "Id";
            this.columnTripId.MinimumWidth = 6;
            this.columnTripId.Name = "columnTripId";
            this.columnTripId.ReadOnly = true;
            this.columnTripId.Visible = false;
            this.columnTripId.Width = 30;
            // 
            // columnTripDestination
            // 
            this.columnTripDestination.HeaderText = "Destination";
            this.columnTripDestination.MinimumWidth = 6;
            this.columnTripDestination.Name = "columnTripDestination";
            this.columnTripDestination.ReadOnly = true;
            this.columnTripDestination.Width = 80;
            // 
            // columnTripCompany
            // 
            this.columnTripCompany.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnTripCompany.HeaderText = "Company";
            this.columnTripCompany.MinimumWidth = 6;
            this.columnTripCompany.Name = "columnTripCompany";
            this.columnTripCompany.ReadOnly = true;
            // 
            // columnTripDepartureTime
            // 
            this.columnTripDepartureTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnTripDepartureTime.HeaderText = "Departure time";
            this.columnTripDepartureTime.MinimumWidth = 6;
            this.columnTripDepartureTime.Name = "columnTripDepartureTime";
            this.columnTripDepartureTime.ReadOnly = true;
            // 
            // columnTripPrice
            // 
            this.columnTripPrice.HeaderText = "Price";
            this.columnTripPrice.MinimumWidth = 6;
            this.columnTripPrice.Name = "columnTripPrice";
            this.columnTripPrice.ReadOnly = true;
            this.columnTripPrice.Width = 50;
            // 
            // columnTripSeats
            // 
            this.columnTripSeats.HeaderText = "Seats";
            this.columnTripSeats.MinimumWidth = 6;
            this.columnTripSeats.Name = "columnTripSeats";
            this.columnTripSeats.ReadOnly = true;
            this.columnTripSeats.Width = 50;
            // 
            // listAgencies
            // 
            this.listAgencies.HideSelection = false;
            this.listAgencies.Location = new System.Drawing.Point(0, 58);
            this.listAgencies.Name = "listAgencies";
            this.listAgencies.Size = new System.Drawing.Size(162, 261);
            this.listAgencies.TabIndex = 0;
            this.listAgencies.UseCompatibleStateImageBehavior = false;
            this.listAgencies.View = System.Windows.Forms.View.List;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(467, 184);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(69, 17);
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "Password";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(464, 129);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(73, 17);
            this.labelUsername.TabIndex = 3;
            this.labelUsername.Text = "Username";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(448, 152);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(230, 22);
            this.textBoxUsername.TabIndex = 0;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(449, 207);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(229, 22);
            this.textBoxPassword.TabIndex = 1;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(526, 235);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 2;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // MyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 473);
            this.Controls.Add(this.panelLogin);
            this.Name = "MyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableReservations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableTrips)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView tableReservations;
        private System.Windows.Forms.DataGridView tableTrips;
        private System.Windows.Forms.ListView listAgencies;
        private System.Windows.Forms.Label labelReservations;
        private System.Windows.Forms.Label labelTrips;
        private System.Windows.Forms.Label labelAgencies;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnResId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResClient;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnResTrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnResSeats;
        private System.Windows.Forms.Label labelAgency;
        private System.Windows.Forms.TextBox textBoxBefore;
        private System.Windows.Forms.TextBox textBoxAfter;
        private System.Windows.Forms.ComboBox comboBoxDestination;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label labelPhoneNumber;
        private System.Windows.Forms.Label labelNoSeats;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.Label labelBefore;
        private System.Windows.Forms.Label labelAfter;
        private System.Windows.Forms.TextBox textBoxPhoneNumber;
        private System.Windows.Forms.TextBox textBoxNoSeats;
        private System.Windows.Forms.TextBox textBoxClient;
        private System.Windows.Forms.Button buttonReserve;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTripId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTripDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTripCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTripDepartureTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTripPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTripSeats;
    }
}