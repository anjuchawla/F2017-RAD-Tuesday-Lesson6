/*
 * Name : Anju Chawla
 * Date : November, 2017
 * Purpose: To allow the user to select coffee and syrup flavours.
 * New coffee flavours can be added and old ones removed. 
 * The entire coffee list can be cleared and number of coffee flavours can be displayed.
 * Can print all available coffee flavours and only selected ones too.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson6
{
    public partial class frmCoffeeSyrup : Form
    {
        public frmCoffeeSyrup()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Close/terminate the application
        /// </summary>
        /// <param name="sender">The sender of the event-the Exit menu item</param>
        /// <param name="e">The event arguments</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void printAllDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Handle printing and print previews when printing all flavours.

            Font printFont = new Font("Arial", 12);
            float lineHeightFloat = printFont.Height + 2;
            float horizontalPrintLocationFloat = e.MarginBounds.Left;
            float verticalPrintLocationFloat = e.MarginBounds.Top;
            string printLineString;

            //Print the heading
            Font headingFont = new Font("Arial", 14, FontStyle.Bold);
            e.Graphics.DrawString("Coffee Flavours", headingFont,
                Brushes.Black, horizontalPrintLocationFloat,
                verticalPrintLocationFloat);

            // Loop through the entire list.
            //for (int listIndexInteger = 0; listIndexInteger <= CoffeeComboBox.Items.Count - 1; listIndexInteger++)
            foreach (Object flavor in cboCoffee.Items)
            {
                //increment the  Y position for the next line.
                verticalPrintLocationFloat += lineHeightFloat;

                //Set up a line
                //PrintLineString = CoffeeComboBox.Items[ListIndexInteger].ToString();
                printLineString = flavor.ToString();
                //Send the line to the graphics page object.
                e.Graphics.DrawString(printLineString, printFont,
                    Brushes.Black, horizontalPrintLocationFloat,
                    verticalPrintLocationFloat);
            } // end for
        }


        private void printSelectedDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Handle printing and print previews when printing selected items.

            Font printFont = new Font("Arial", 12);
            Font headingFont = new Font("Arial", 14, FontStyle.Bold);
            float lineHeightFloat = printFont.Height + 2;
            float horizontalPrintLocationFloat = e.MarginBounds.Left;
            float verticalPrintLocationFloat = e.MarginBounds.Top;
            string printLineString;

            //Set up and display heading lines
            printLineString = "Print Selected Item";
            e.Graphics.DrawString(printLineString, headingFont,
                Brushes.Black, horizontalPrintLocationFloat,
                verticalPrintLocationFloat);
            printLineString = "by Anju Chawla";
            verticalPrintLocationFloat += lineHeightFloat;
            e.Graphics.DrawString(printLineString, headingFont,
                Brushes.Black, horizontalPrintLocationFloat,
                verticalPrintLocationFloat);

            // Leave a blank line between the heading and detail line.
            verticalPrintLocationFloat += lineHeightFloat * 2;
            // Set up the selected line.
            printLineString = "Coffee: " + cboCoffee.Text +
                "     Syrup: " + lstSyrup.Text;
            // Send the line to the graphics page object.
            e.Graphics.DrawString(printLineString, printFont,
                Brushes.Black, horizontalPrintLocationFloat,
                  verticalPrintLocationFloat);

        }
        /// <summary>
        /// Displays information about the application and the company
        /// </summary>
        /// <param name="sender">The About menu item</param>
        /// <param name="e">Event Arguments</param>
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            frmAbout information = new frmAbout();
            information.ShowDialog();

        }
        /// <summary>
        /// Allow the user to add a new coffee flavour to the list, if it does not exist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddCoffeeFlavour_Click(object sender, EventArgs e)
        {
            bool itemFound;
            int position;

            //if the user has provided a flavour
            if (cboCoffee.Text.Trim() != String.Empty)
            {
                itemFound = CheckFlavour(cboCoffee.Text.Trim(), out position);
                if (itemFound)//flavour already present
                {
                    MessageBox.Show("Duplicate Flavour cannot be added.", "Add Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboCoffee.Focus();
                }
                else
                {
                    //add the flavour
                    cboCoffee.Items.Add(cboCoffee.Text);
                    cboCoffee.Text = String.Empty;
                }

            }
            else
            {
                MessageBox.Show("Enter a coffee flavour to add", "Missing Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboCoffee.Focus();

            }

        }
        /// <summary>
        /// This method searches for a string/flavour in the coffee list
        /// </summary>
        /// <param name="text">The string/flavour to be searched</param>
        /// <param name="position">The position where the string was found, if found</param>
        /// <returns>true if text was found, false otherwise</returns>
        private bool CheckFlavour(string text, out int position)
        {
            position = -1;

            // for(int i =0; i < cboCoffee.Items.Count; i++)
            foreach (Object item in cboCoffee.Items)
            {
                position++;
                if (text.Equals(item.ToString().Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;


                }


            }

            return false;
        }
        /// <summary>
        /// 
        /// Remove an existing flavour from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRemoveCoffeeFlavour_Click(object sender, EventArgs e)
        {
            bool itemFound;
            int position;

            //user types in a flavour to remove
            if (cboCoffee.SelectedIndex == -1 && cboCoffee.Text.Trim() != String.Empty)
            {
                itemFound = CheckFlavour(cboCoffee.Text.Trim(), out position);
                //if flavour not found
                if (!itemFound)
                {
                    MessageBox.Show("Cannot find the flavour to remove", "Remove Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    cboCoffee.Focus();

                }
                else
                {
                    //remove the flavour
                    cboCoffee.Items.RemoveAt(position);
                    MessageBox.Show("Coffee flavour removed", "Remove Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboCoffee.Text = "";


                }

            }
            else
            {
                if (cboCoffee.SelectedIndex == -1)//no selection made
                {
                    MessageBox.Show("Please select or enter the coffee flavour to remove first", "Remove Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboCoffee.Focus();
                }

                else
                {
                    cboCoffee.Items.RemoveAt(cboCoffee.SelectedIndex);
                    // cboCoffee.Items.Remove(cboCoffee.SelectedItem);
                    //cboCoffee.Items.Remove(cboCoffee.Items[cboCoffee.SelectedIndex]);
                    MessageBox.Show("Coffee flavour removed", "Remove Successful",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboCoffee.Text = "";

                }

            }





        }

        private void cboCoffee_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Hello");
            //cboCoffee.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void tsmiPrintSelected_Click(object sender, EventArgs e)
        {
            if (lstSyrup.SelectedIndex == -1)
            {
                lstSyrup.SelectedIndex = 0;
            }
            if (cboCoffee.SelectedIndex != -1)
                printSelectedDocument.Print();
            else
            {
                MessageBox.Show("Please select a coffee flavour", "Print Selection Incomplete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboCoffee.Focus();
            }
        }

        private void tsmiPreviewSelected_Click(object sender, EventArgs e)
        {
            if (lstSyrup.SelectedIndex == -1)
            {
                lstSyrup.SelectedIndex = 0;
            }
            if (cboCoffee.SelectedIndex != -1)
            {
                printPreviewDialog1.Document = printSelectedDocument;
                printPreviewDialog1.ShowDialog();
            }

            else
            {
                MessageBox.Show("Please select a coffee flavour", "Print Selection Incomplete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboCoffee.Focus();
            }

        }

        private void tsmiPrintAll_Click(object sender, EventArgs e)
        {
            printAllDocument.Print();
        }

        private void tsmiPreviewAll_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printAllDocument;
            printPreviewDialog1.ShowDialog();
        }

        private void tsmiClearAll_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Do you want to remove all flavours?",
                "Clear Coffee Flavours", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);

            if (confirm == DialogResult.Yes)
                cboCoffee.Items.Clear();
        }

        private void tsmiCountCoffeeFlavours_Click(object sender, EventArgs e)
        {
            string message = "The number of coffee flavours available is " + cboCoffee.Items.Count;
            MessageBox.Show(message, "Coffee Flavours", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
