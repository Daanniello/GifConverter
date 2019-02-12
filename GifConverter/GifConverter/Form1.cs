using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GifConverter
{
    public partial class Form1 : Form
    {
        private Color color;
        private Image image;

        public Form1()
        {
            InitializeComponent();
        }

        //Convert
        private void button1_Click(object sender, EventArgs e)
        {
            
            

            Converter converter = new Converter(image, color);
            pictureBox1.Image = converter.GetDisplayImage();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            color = colorDialog1.Color;
            label1.BackColor = color;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
          
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                    image = Image.FromFile(openFileDialog1.FileName);
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
            var displayImage = image;
            pictureBox1.Image = displayImage;
        }
    }
}
