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
using System.Media;
using System.Threading;

namespace CSharpSimpleGame
{
    public partial class Form1 : Form
    {
        Roulette myRoule;
        public Form1()
        {
            InitializeComponent();
            button4.Enabled = false;
            pictureBox1.SendToBack();
        }

        // Load Gun
        private void button1_Click(object sender, EventArgs e)
        {
            myRoule = new Roulette();
            myRoule.BulletLocation = 1; // Just a value, It will be randomized by the property.
            label1.Text = myRoule.BulletLocation.ToString();
            button4.Enabled = true;
        }

        // Spin
        private void button2_Click(object sender, EventArgs e)
        {
            myRoule.CurrentLocation = myRoule.RandomMyChamper(); // Just a value, It will be randomized by the property.
            label2.Text = myRoule.CurrentLocation.ToString();
            MessageBox.Show("Champer spinned, Be careful!", "Warning!", MessageBoxButtons.OK);
        }

        // Retry
        private void button3_Click(object sender, EventArgs e)
        {
            myRoule = null;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            label1.Text = "";
            label2.Text = "";
        }

        // Fire
        private void button4_Click(object sender, EventArgs e)
        {

            // Run shot sound
            Stream str = Properties.Resources.gunshot;
            SoundPlayer myS = new SoundPlayer(str);
            myS.PlaySync();

            if (myRoule.FireBullet()) // True means you didn't use your 2nd chance
            {
                if (myRoule.CurrentLocation == myRoule.BulletLocation) // You shot the bullet
                {
                    MessageBox.Show("Congratulations! you lived!", "Voylaa!", MessageBoxButtons.OK);
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    button4.Enabled = false;
                }
                else // Didn't shot
                {
                    if (myRoule.CurrentLocation == 6)
                    {
                        myRoule.CurrentLocation = 1;
                    }
                    else
                    {
                        myRoule.CurrentLocation++;
                    }
                }
            }
            else // False means you are dead or winner
            {
                if (myRoule.CurrentLocation == myRoule.BulletLocation) // You shot the bullet
                {
                    MessageBox.Show("Congratulations! you lived!", "Voylaa!", MessageBoxButtons.OK);
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    button4.Enabled = false;
                }
                else
                {
                    MessageBox.Show("You died!", "RIP", MessageBoxButtons.OK);
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    button4.Enabled = false;
                }
            }

            myS.Dispose();

            label1.Text = myRoule.BulletLocation.ToString();
            label2.Text = myRoule.CurrentLocation.ToString();
        }
    }

    class Roulette
    {
        public Roulette()
        {
            this.NumOfTrials = 1;
            MessageBox.Show("Bullet loaded into champer!", "Warning!", MessageBoxButtons.OK);
        }
        private int _bulletLocation;

        // Gets or Sets the location of the current bullet
        public int BulletLocation
        {
            get { return _bulletLocation; }
            set { this._bulletLocation = this.RandomMyBullet(); }
        }

        private int _currentLocation;
        public int CurrentLocation
        {
            get { return _currentLocation; }
            set { this._currentLocation = value; }
        }

        private int _NumOfTrials;
        public int NumOfTrials
        {
            get { return _NumOfTrials; }
            set { this._NumOfTrials = value; }
        }

        private int RandomMyBullet()
        {
            Random myR = new Random();
            return myR.Next(1, 7);
        }

        public int RandomMyChamper()
        {
            Random myR = new Random();
            return myR.Next(1, 7);
        }

        // Fire the bullet, Starts from first place to last place until the bullet is fired.
        public bool FireBullet()
        {
            if (NumOfTrials < 2)
            {
                NumOfTrials++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
