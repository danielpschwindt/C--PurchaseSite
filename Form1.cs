// Programamar: Danny Schwindt
// Assignment: Assignment 3
// I
using System;
using System.Windows.Forms;
using System.IO; // Access to streamreader.

namespace Schwindt_3
{
    public partial class Form1 : Form
    {
        // Declare class level constants
        private const decimal STORE_PICK_UP_RATE = 0m;
        private const decimal HOME_DELIVERY_RATE = 7.50m;
        private const decimal SINGLE_BALLOON = 9.95m;
        private const decimal HALF_DOZEN_BALLOON = 35.95m;
        private const decimal DOZEN_BALLOONS = 65.95m;
        private const decimal PESONALIZED_MESSAGE = 2.50m;
        private const decimal EXTRAS = 9.50m;
        private const decimal TAX_RATE = 0.07m;

        // Declare class level variables for total levels
        private decimal subtotalPrice = 0m;
        private decimal taxAmount = 0m;
        private decimal orderTotal = 0m;

        
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Call Custom Method
            PopulateBoxes();

            // Display current date
            dateMaskedTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
            
            // radio buttons checked at start up
            storePickUpRadioButton.Checked = true;
            singleRadioButton.Checked = true;

            // set default selection
            titleComboBox.SelectedItem = "";
            occasionComboBox.SelectedItem = "Birthday";
        }
        private void PopulateBoxes()
        {
            try
            {
                StreamReader inputFile; // declare object

                inputFile = File.OpenText("Occasions.txt");
                while (!inputFile.EndOfStream) // Verify that more data exist
                {
                    // Read a line from input file and add it to combo box
                    occasionComboBox.Items.Add(inputFile.ReadLine());
                }
                inputFile.Close(); // Close input file and add it to list box

                inputFile = File.OpenText("Extras.txt"); // open file
                while (!inputFile.EndOfStream) //verify that more data exist
                {
                    // read a line from input file and add it to list box
                    extrasListBox.Items.Add(inputFile.ReadLine());
                }
                inputFile.Close(); // close input file
            }
          
            catch (Exception ex)
            {
                // display meesage if error occurs
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void UpdateTotals()
        {
            decimal tempPrice = 0.00m;

            //Calculate
            if (homeDeliveryRadioButton.Checked)
            {
                tempPrice = tempPrice + HOME_DELIVERY_RATE;

            }
            if (singleRadioButton.Checked)
            {
                tempPrice = tempPrice + SINGLE_BALLOON;
            }
            if(halfDozenRadioButton.Checked)
            {
                tempPrice = tempPrice + HALF_DOZEN_BALLOON;
            }
            if (dozenRadioButton.Checked)
            {
                tempPrice = tempPrice + DOZEN_BALLOONS;
            }
            for (int count = 0; count < extrasListBox.Items.Count; count++)
            {
                // Use get selected method 
                if (extrasListBox.GetSelected(count))
                {
                    tempPrice = tempPrice + EXTRAS;
                }
            }
            if (messageCheckBox.Checked)
            {
                tempPrice = tempPrice + PESONALIZED_MESSAGE;
                personalizedMessageTextBox.Enabled = true;
            }
            else
            {
                personalizedMessageTextBox.Enabled = false;
            }

            subtotalPrice = tempPrice;
            taxAmount = subtotalPrice + TAX_RATE;
            orderTotal = subtotalPrice + taxAmount;
            subTotalLabel.Text = subtotalPrice.ToString("c");
            taxAmountLabel.Text = taxAmount.ToString("c");
            orderTotalLabel.Text = orderTotal.ToString("c");

        }


        private void ResetForm()
        {
            // reset form
            titleComboBox.Text = "";
            firstNameTextBox.Text = "";
            lastNameTextbox.Text = "";
            streetTextBox.Text = "";
            cityTextBox.Text = "";
            stateTextBox.Text = "";
            zipCodeMaskedTextBox.Text = "00000";
            phoneMakedTextBox.Text = "(999)-000-0000";
            dateMaskedTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
            storePickUpRadioButton.Checked = true;
            singleRadioButton.Checked = true;
            occasionComboBox.Text = "";
            messageCheckBox.Checked = false;
            personalizedMessageTextBox.Enabled = false;
            titleComboBox.Focus(); 
            // list box!!!!
       
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ResetForm(); // call custom method to clear form
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Declare variable to hold user selection in dialog box
            DialogResult selection;
            selection = MessageBox.Show("are you sure you wish to quit?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // take appropriate action based on user selection in dialog box
            if (selection == DialogResult.Yes)
            {
                // close the form
                this.Close();
            }
        }

        private void storePickUpRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void homeDeliveryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void singleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void halfDozenRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void dozenRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void extrasListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void displaySummaryButton_Click(object sender, EventArgs e)
        {
            if (firstNameTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Name rquired to complete form", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (lastNameTextbox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Last name is required to complete thsi form", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (!phoneMakedTextBox.MaskFull)
            {
                MessageBox.Show("Bonnie's Balloons Order Summary" + "\n" +
                    "Customer Name:" + titleComboBox.Text + firstNameTextBox.Text + lastNameTextbox.Text + "\n" +
                    "Customer Address:" + streetTextBox.Text + cityTextBox.Text + stateTextBox.Text + "\n" +
                    "Customer Phone:" + phoneMakedTextBox.Text + "\n" +
                    "Delivery Date:" + dateMaskedTextBox.Text + "\n");

            }
        }
    }
}
