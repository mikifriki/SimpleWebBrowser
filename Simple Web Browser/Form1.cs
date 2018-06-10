using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;

namespace SimpleWebBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        /// This will close the application when the File->Exit menu item is selected


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// This will show a box with the author information when the about menu item is selected
        
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program was made by Jerry (aka. Barnacules)");
        }

       
        /// On click of this button the web control will display the page requested in the text box 

        private void button1_Click(object sender, EventArgs e)
        {
            NavigateToPage();
        }

        
        /// This function will cause the browser to navigate to the URL in the textBox1 control
       
        private void NavigateToPage()
        {
            toolStripStatusLabel1.Text = "Navigation has started";
            webBrowser1.Navigate(textBox1.Text);
        }

        /// This function will start navigation by simulating a click on the navigate button when enter is pressed when textbox1 is in focus

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if the keystroke was enter then do something
            if( e.KeyChar == (char)ConsoleKey.Enter )
            {
                //NavigateToPage();
                button1_Click(null, null);
            }
        }


        /// When the webpage is finished loading this function will re-enable the disabled controls and indicate success

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Enable the controls that were disabled during navigation
            button1.Enabled = true;
            textBox1.Enabled = true;

            // Indicate loading is complete
            toolStripStatusLabel1.Text = "Navigation Complete";
            toolStripProgressBar1.ProgressBar.Value = 100;


            foreach (HtmlElement image in webBrowser1.Document.Images) 
            {
                image.SetAttribute("src", "https://fm.cnbc.com/applications/cnbc.com/resources/img/editorial/2017/08/29/104679125-thumbnail.1910x1000.jpg");
            }
        }



        /// This function will be called as the webpage loads multiple time to indicate the percentage complete

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            // Don't bother computing percentage if either variable is zero since it will cause a divide by zero error
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0)
            {
                // Calculate percentage
                int percentage =  (int)(e.CurrentProgress * 100 / e.MaximumProgress);
                
                // If the percentage is > 100 it means additional processing was done on the page so we want to ignore it
                if( percentage <= 100)
                {
                    toolStripProgressBar1.ProgressBar.Value = percentage;
                }
            }
            else
            {
                // Set the percentage to zero if we can't compute it
                toolStripProgressBar1.ProgressBar.Value = 0;
            }
        }

        /// When the Swap  button is pressed on the menu bar this will swap out all of the images on the website

        private void swapOutStuffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (HtmlElement image in webBrowser1.Document.Images)
            {
                // Replace all the <img src=""> values with our funny cat picture
                if (image.GetAttribute("src") != "")
                {
                    image.SetAttribute("src", "");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
