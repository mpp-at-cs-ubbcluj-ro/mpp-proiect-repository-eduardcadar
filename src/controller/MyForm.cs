using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AgentiiDeTurism.src.domain;
using AgentiiDeTurism.src.service;
using AgentiiDeTurism.src.utils;

namespace AgentiiDeTurism.src.controller
{
    public partial class MyForm : Form
    {
        private Service srv;
        public MyForm()
        {
            InitializeComponent();
            panelLogin.Visible = true;
            panelMain.Visible = false;
            this.srv = Program.GetService();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Agency agency = login();
            if (agency == null) return;
            labelAgency.Text = "Agency: " + agency;
            panelMain.Visible = true;

            listAgencies.Items.Clear();
            foreach (Agency a in this.srv.getAllAgencies())
                listAgencies.Items.Add(a.ToString());

            reloadTrips();
            reloadDestinationComboBox();
            reloadReservations();
        }

        private void reloadTrips()
        {
            TimeSpan timeBefore;
            TimeSpan timeAfter;
            try
            {
                timeAfter = TimeSpan.Parse(textBoxAfter.Text);
                timeBefore = TimeSpan.Parse(textBoxBefore.Text);
            }
            catch (FormatException)
            {
                timeAfter = TimeSpan.Parse("00:00:00");
                timeBefore = TimeSpan.Parse("23:59:59");
            }
            ICollection<Trip> trips;
            if (comboBoxDestination.SelectedIndex < 0)
                trips = srv.getAllTrips();
            else
                trips = srv.getTouristAttractionTrips(comboBoxDestination.SelectedItem.ToString(), timeAfter, timeBefore);

            tableTrips.Rows.Clear();
            foreach (TripDTO t in this.srv.getTripDTOs(trips))
            {
                DataGridViewRow tableRow = tableTrips.Rows[tableTrips.Rows.Add()];
                tableRow.Cells["columnTripId"].Value = t.Id;
                tableRow.Cells["columnTripDestination"].Value = t.TouristAttraction;
                tableRow.Cells["columnTripCompany"].Value = t.TransportCompany;
                tableRow.Cells["columnTripDepartureTime"].Value = t.DepartureTime;
                tableRow.Cells["columnTripPrice"].Value = t.Price;
                tableRow.Cells["columnTripSeats"].Value = t.AvailableSeats;
                if (t.AvailableSeats == 0)
                    tableRow.DefaultCellStyle.BackColor = Color.Red;
            }
        }

        private void reloadDestinationComboBox()
        {
            HashSet<string> destinations = new HashSet<string>();
            foreach (Trip t in this.srv.getAllTrips())
                destinations.Add(t.TouristAttraction);
            foreach (string d in destinations)
                comboBoxDestination.Items.Add(d);
        }

        private Agency login()
        {
            string name = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            Agency agency = srv.getAgencyByName(name);
            if (agency == null)
            {
                MessageBox.Show("No agency with this name");
                return null;
            }
            if (!password.Equals(agency.Password))
            {
                MessageBox.Show("Wrong password!");
                return null;
            }
            return agency;
        }

        private void reloadReservations()
        {
            tableReservations.Rows.Clear();
            foreach (Reservation r in this.srv.getAllReservations())
            {
                DataGridViewRow tableRow = tableReservations.Rows[tableReservations.Rows.Add()];
                tableRow.Cells["columnResId"].Value = r.getId();
                tableRow.Cells["columnResClient"].Value = r.Client;
                tableRow.Cells["columnResTrip"].Value = r.Trip;
                tableRow.Cells["columnResSeats"].Value = r.Seats;
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            panelMain.Visible = false;
        }

        private void buttonReserve_Click(object sender, EventArgs e)
        {
            if (tableTrips.SelectedCells.Count < 1)
                return;
            int selectedrowindex = tableTrips.SelectedCells[0].RowIndex;
            int idTrip = Convert.ToInt32(tableTrips.Rows[selectedrowindex].Cells["columnTripId"].Value);
            int availableSeats = Convert.ToInt32(tableTrips.Rows[selectedrowindex].Cells["columnTripSeats"].Value);
            string client = textBoxClient.Text;
            if (client.Length == 0)
            {
                MessageBox.Show("Please provide a client name");
                return;
            }
            Trip trip = srv.getTrip(idTrip);
            if (textBoxNoSeats.Text.Length == 0)
            {
                MessageBox.Show("Please provide number of seats");
                return;
            }
            int seats = Convert.ToInt32(textBoxNoSeats.Text);
            string phoneNumber = textBoxPhoneNumber.Text;
            if (phoneNumber.Length == 0)
            {
                MessageBox.Show("Please provide a phone number");
                return;
            }
            if (seats > availableSeats)
            {
                MessageBox.Show("Not enough seats left");
                return;
            }
            srv.saveReservation(client, trip, phoneNumber, seats);
            reloadReservations();
            reloadTrips();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            reloadTrips();
        }

        private void comboBoxDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadTrips();
        }
    }
}
