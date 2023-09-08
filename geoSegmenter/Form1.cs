using System;
using System.IO;
using System.Windows.Forms;

namespace geoSegmenter
{
    public partial class Form1 : Form
    {
        //private string[] imageFiles;
        private string[] imageFiles = new string[0]; // Initialize as an empty array
        private int currentIndex = -1;

        private readonly Button openButton;
        private readonly Button previousButton;
        private readonly Button nextButton;

        public Form1()
        {
            InitializeComponent();

            // Create "Open" Button
            /* openButton = new Button
            {
                Text = "Open",
                Location = new Point(10, 10) // Set the button's location
            };
            openButton.Click += OpenButton_Click; // Attach event handler
            Controls.Add(openButton); */

            // Create and configure the "Open" button
            openButton = new Button();
            openButton.Text = "Open";
            openButton.Location = new Point(10, 30); // Set the button's location
            openButton.Click += OpenButton_Click; // Attach event handler
            Controls.Add(openButton);

            // Create "Previous" Button
            previousButton = new Button();
            previousButton.Text = "Previous";
            previousButton.Location = new Point(100, 30); // Set the button's location
            previousButton.Click += PreviousButton_Click; // Attach event handler
            Controls.Add(previousButton);

            // Create "Next" Button
            nextButton = new Button();
            nextButton.Text = "Next";
            nextButton.Location = new Point(200, 30); // Set the button's location
            nextButton.Click += NextButton_Click; // Attach event handler
            Controls.Add(nextButton);

            // Create a MenuStrip
            MenuStrip menuStrip = new MenuStrip();
            this.MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);

            // Create a "File" menu
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            menuStrip.Items.Add(fileMenu);

            // Create an "Exit" item under the "File" menu
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit");
            fileMenu.DropDownItems.Add(exitMenuItem);

            // Subscribe to the "Click" event of the "Exit" item
            exitMenuItem.Click += ExitMenuItem_Click;
        }

        // Event handler for the "Exit" menu item
        private void ExitMenuItem_Click(object? sender, EventArgs e)
        {
            // Exit the application when "Exit" is clicked
            Application.Exit();
        }

        private void OpenButton_Click(object? sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Define an array of image file extensions you want to support
                    string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    //imageFiles = Directory.GetFiles(folderDialog.SelectedPath, "*.jpg");

                    // Get all image files with the specified extensions in the selected folder
                    imageFiles = Directory.GetFiles(folderDialog.SelectedPath)
                                        .Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower()))
                                        .ToArray();


                    if (imageFiles.Length > 0)
                    {
                        currentIndex = 0;
                        ShowCurrentImage();
                    }
                    else
                    {
                        MessageBox.Show("No supported image files found in the selected folder.");
                    }
                }
            }
        }

        private void PreviousButton_Click(object? sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowCurrentImage();
            }
            else
            {
                MessageBox.Show("You are at the first image.");
            }
        }

        private void NextButton_Click(object? sender, EventArgs e)
        {
            if (currentIndex < imageFiles.Length - 1)
            {
                currentIndex++;
                ShowCurrentImage();
            }
            else
            {
                MessageBox.Show("You are at the last image.");
            }
        }

        private void ShowCurrentImage()
        {
            if (currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                pictureBox.Image = new System.Drawing.Bitmap(imageFiles[currentIndex]);
            }
        }
    }
}
