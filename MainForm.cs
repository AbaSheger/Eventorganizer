using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EventOrganizer
{
    /// <summary>
    /// Main form for the Event Organizer application.
    /// </summary>
    public partial class MainForm : Form
    {
        // Instance variable for managing the event
        private EventManager eventManager;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
        }

        /// <summary>
        /// Initialize GUI components and logic.
        /// </summary>
        private void InitializeGUI()
        {
            eventManager = new EventManager();
            FormatCountryNamesInComboBox(); // Populate ComboBox with countries
            grpAddParticipant.Enabled = false; // Disable participant-related inputs initially
        }

        /// <summary>
        /// Populate the ComboBox with formatted country names.
        /// </summary>
        private void FormatCountryNamesInComboBox()
        {
            var countriesList = Enum.GetValues(typeof(Countries))
                                     .Cast<Countries>()
                                     .Select(c => c.ToString().Replace('_', ' '))
                                     .ToList();

            comboBoxCountry.DataSource = countriesList;
        }

        /// <summary>
        /// Update the GUI after changes in the event or participants.
        /// </summary>
        private void UpdateGUI()
        {
            listBoxParticipants.Items.Clear();

            List<string> participantInfoList = eventManager.Participants.GetParticipantsInfo();
            foreach (string participantInfo in participantInfoList)
            {
                listBoxParticipants.Items.Add(participantInfo);
            }

            lblNumberOfParticipants.Text = eventManager.Participants.Count().ToString();
            lblTotalCost.Text = eventManager.CalculateTotalCost().ToString("C");
            lblTotalFees.Text = eventManager.CalculateTotalFees().ToString("C");
            lblSurplusDeficit.Text = eventManager.CalculateSurplusDeficit().ToString("C");
        }

        /// <summary>
        /// Read participant input from the form and return a Participant object.
        /// </summary>
        /// <returns>A Participant object if input is valid; otherwise, null.</returns>
        private Participant ReadParticipantInput()
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string street = txtStreet.Text;
            string city = txtCity.Text;
            string zipCode = txtZipCode.Text;

            if (comboBoxCountry.SelectedItem == null)
            {
                MessageBox.Show("Please select a country.");
                return null;
            }

            Countries selectedCountry = (Countries)Enum.Parse(typeof(Countries), comboBoxCountry.SelectedItem.ToString().Replace(' ', '_'));

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("First name, last name, and city cannot be empty.");
                return null;
            }

            Address address = new Address(street, city, zipCode, selectedCountry);
            return new Participant(firstName, lastName, address);
        }

        /// <summary>
        /// Clear input fields after adding or editing a participant.
        /// </summary>
        private void ClearParticipantInputFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtStreet.Clear();
            txtCity.Clear();
            txtZipCode.Clear();
            comboBoxCountry.SelectedIndex = -1;
        }

        /// <summary>
        /// Create Event button click handler.
        /// </summary>
        private void btnCreateEvent_Click(object sender, EventArgs e)
        {
            string eventTitle = txtEventTitle.Text;
            double costPerParticipant = double.TryParse(txtCostPerParicipant.Text, out double cost) ? cost : 0.0;
            double feePerParticipant = double.TryParse(txtFeePerParticipant.Text, out double fee) ? fee : 0.0;

            if (string.IsNullOrWhiteSpace(eventTitle))
            {
                MessageBox.Show("Event title cannot be empty.");
                return;
            }

            eventManager.Title = eventTitle;
            eventManager.CostPerParticipant = costPerParticipant;
            eventManager.FeePerParticipant = feePerParticipant;

            grpAddParticipant.Enabled = true; // Enable participant-related inputs
            MessageBox.Show("Event created successfully!");
        }

        /// <summary>
        /// Add Participant button click handler.
        /// </summary>
        private void btnAddParticipant_Click(object sender, EventArgs e)
        {
            Participant participant = ReadParticipantInput();
            if (participant == null)
            {
                return; // Validation failed
            }

            eventManager.Participants.AddParticipant(participant);
            UpdateGUI();
            MessageBox.Show("Participant added successfully!");
            ClearParticipantInputFields();
        }

        /// <summary>
        /// Change Participant button click handler.
        /// </summary>
        private void btnChange_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxParticipants.SelectedIndex;

            // Validate that a participant is selected
            if (selectedIndex < 0)
            {
                MessageBox.Show("Please select a participant to edit.");
                return;
            }

            // Validate and save updated details
            Participant updatedParticipant = ReadParticipantInput();
            if (updatedParticipant == null)
            {
                return; // Validation failed
            }

            // Update the participant in the list
            eventManager.Participants.EditParticipant(selectedIndex, updatedParticipant);

            // Refresh the GUI
            UpdateGUI();
            MessageBox.Show("Participant updated successfully!");
        }

        /// <summary>
        /// Delete Participant button click handler.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxParticipants.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Please select a participant to delete.");
                return;
            }

            eventManager.Participants.RemoveParticipant(selectedIndex);
            UpdateGUI();
            MessageBox.Show("Participant deleted successfully!");
        }

        /// <summary>
        /// ListBox Participants selected index changed handler.
        /// </summary>
        private void listBoxParticipants_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBoxParticipants.SelectedIndex;

            if (selectedIndex < 0)
            {
                // No participant selected, clear the input fields
                ClearParticipantInputFields();
                return;
            }

            // Retrieve and display the selected participant's details
            Participant selectedParticipant = eventManager.Participants.GetParticipantAt(selectedIndex);
            txtFirstName.Text = selectedParticipant.FirstName;
            txtLastName.Text = selectedParticipant.LastName;
            txtStreet.Text = selectedParticipant.Address.Street;
            txtCity.Text = selectedParticipant.Address.City;
            txtZipCode.Text = selectedParticipant.Address.ZipCode;
            comboBoxCountry.SelectedItem = selectedParticipant.Address.Country.ToString().Replace('_', ' ');
        }
    }
}


