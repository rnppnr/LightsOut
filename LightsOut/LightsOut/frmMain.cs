using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class frmMain : Form
    {
        private Random rnd = new Random();
        private PictureBox[,] cellArray = new PictureBox[5, 5];

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    cellArray[row, col] = new PictureBox();
                    cellArray[row, col].Tag = row + "," + col;
                    cellArray[row, col].Width = 98;
                    cellArray[row, col].Height = 98;
                    cellArray[row, col].Left = (col * 100) + 10;
                    cellArray[row, col].Top = (row * 100) + 10;
                    cellArray[row, col].BackColor = Color.DarkGreen;
                    cellArray[row, col].MouseClick += new MouseEventHandler(cellArray_Click);
                    this.Controls.Add(cellArray[row, col]);
                }
            }

            foreach (var item in this.Controls)
            {
                if (item is PictureBox)
                {
                    PictureBox pb = (PictureBox)item;
                    pb.BackColor = (rnd.Next(0, 100) < 90) ? Color.DarkGreen : Color.Green;
                }
            }
        }

        private void cellArray_Click(object sender, MouseEventArgs e)
        {
            int row = 0,
                col = 0;

            foreach (var item in this.Controls)
            {
                if (item is PictureBox)
                {
                    PictureBox ca = (PictureBox)item;
                    if (ca.Bounds.Contains(PointToClient(MousePosition)))
                    {                        
                        string[] cell = ca.Tag.ToString().Split(',');
                        row = Int16.Parse(cell[0]);
                        col = Int16.Parse(cell[1]);
                        ca.BackColor = Color.Green;
                        ToggleColours(row, col);
                    }                    
                }                
            }
        }

        private void ToggleColours(int row, int col)
        {
            try
            {
                if (row > 0)
                    cellArray[row - 1, col].BackColor = (cellArray[row, col].BackColor == Color.Green) ? Color.DarkGreen : Color.Green;
                if (row < 4)
                    cellArray[row + 1, col].BackColor = (cellArray[row, col].BackColor == Color.Green) ? Color.DarkGreen : Color.Green;
                if (col > 0)
                    cellArray[row, col - 1].BackColor = (cellArray[row, col].BackColor == Color.Green) ? Color.DarkGreen : Color.Green;
                if (col < 4)
                    cellArray[row, col + 1].BackColor = (cellArray[row, col].BackColor == Color.Green) ? Color.DarkGreen : Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
