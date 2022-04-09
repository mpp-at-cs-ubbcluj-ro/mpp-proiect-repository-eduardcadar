using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client
{
    public partial class TabMain : UserControl
    {
        private IServices _srv;
        public TabMain()
        {
            InitializeComponent();
        }
        public void Initialize(Agency agency, Service srv)
        {
            _srv = srv;
            labelAgency.Text = "Agency: " + agency;
            listAgencies.Items.Clear();

            foreach (Agency a in this._srv.getAllAgencies())
                listAgencies.Items.Add(a.ToString());

            ReloadTrips();
            ReloadDestinationComboBox();
            ReloadReservations();
        }

        private void ReloadTrips()
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
                trips = _srv.getAllTrips();
            else
                trips = _srv.getTouristAttractionTrips(comboBoxDestination.SelectedItem.ToString(), timeAfter, timeBefore);

            tableTrips.Rows.Clear();
            foreach (TripDTO t in this._srv.getTripDTOs(trips))
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

        private void ReloadDestinationComboBox()
        {
            HashSet<string> destinations = new HashSet<string>();
            foreach (Trip t in this._srv.getAllTrips())
                destinations.Add(t.TouristAttraction);
            foreach (string d in destinations)
                comboBoxDestination.Items.Add(d);
        }

        private void ReloadReservations()
        {
            tableReservations.Rows.Clear();
            foreach (Reservation r in this._srv.getAllReservations())
            {
                DataGridViewRow tableRow = tableReservations.Rows[tableReservations.Rows.Add()];
                tableRow.Cells["columnResId"].Value = r.getId();
                tableRow.Cells["columnResClient"].Value = r.Client;
                tableRow.Cells["columnResTrip"].Value = r.Trip;
                tableRow.Cells["columnResSeats"].Value = r.Seats;
            }
        }

        private void ButtonLogout_Click(object sender, EventArgs e)
        {
            var parent = this.Parent as MyForm;
            parent.SetLoginTab();
        }

        private void ComboBoxDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadTrips();
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            ReloadTrips();
        }

        private void ButtonReserve_Click(object sender, EventArgs e)
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
            Trip trip = _srv.getTrip(idTrip);
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
            try
            { 
                _srv.saveReservation(client, trip, phoneNumber, seats);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            ReloadReservations();
            ReloadTrips();
        }
    }
}
