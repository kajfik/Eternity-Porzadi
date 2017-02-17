using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Eternity_Porzadi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static int szyrzka = 1360, wyszka = 705;
        public static int iloscKostek = 256;
        public static int porzadi = 1;
        public static int szyrzkaPola = 31;
        public static int wyszkaPola = 16;
        public int[,] xypole = new int[100, 100];
        public static Bitmap bmp = new Bitmap(1, 1);
        public int iloscDanychKostek = 0;
        public Graphics g;
        int poKliknyciu = 4;
        bool start = false;

        public void namalujMrzizke()
        {
            for (int a = 1; a <= wyszkaPola - 1; a++)
            {
                for (int b = 0; b < szyrzka; b++)
                {
                    bmp.SetPixel(b, a * wyszka / wyszkaPola, Color.Black);
                }
            }
            for (int a = 1; a <= szyrzkaPola - 1; a++)
            {
                for (int b = 0; b < wyszka; b++)
                {
                    bmp.SetPixel(a * szyrzka / szyrzkaPola, b, Color.Black);
                }
            }
            pictureBox1.Image = bmp;
        }

        public void load()
        {
            szyrzkaPola = Convert.ToInt32(numericUpDown1.Value);
            wyszkaPola = Convert.ToInt32(numericUpDown2.Value);
            pictureBox1.Width = szyrzkaPola * 34;
            pictureBox1.Height = wyszkaPola * 34;
            szyrzka = pictureBox1.Width;
            wyszka = pictureBox1.Height;
            bmp = new Bitmap(szyrzka, wyszka);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            namalujMrzizke();
            if (Zaklad.Checked)
            { xypole[7, 8] = -1; }
            else { xypole[7, 8] = 0; }
            if (Napowieda1.Checked)
            { xypole[2, 2] = -1; }
            else { xypole[2, 2] = 0; }
            if (Napowieda2.Checked)
            { xypole[13, 2] = -1; }
            else { xypole[13, 2] = 0; }
            if (Napowieda3.Checked)
            { xypole[2, 13] = -1; }
            else { xypole[2, 13] = 0; }
            if (Napowieda4.Checked)
            {xypole[13, 13] = -1;}
            else { xypole[13, 13] = 0; }
            namalujXYpole();
            pictureBox1.Image = bmp;
            if (!start)
            {
                button4.BackColor = Color.Tomato;
                start = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load();
        }

        public void namalujXYpole()
        {
            for (int a = 0; a < szyrzkaPola; a++)
            {
                for (int b = 0; b < wyszkaPola; b++)
                {
                    if (xypole[a, b] != 0)
                    {
                        g.DrawString(Convert.ToString(xypole[a, b]), DefaultFont, Brushes.Black, new PointF(a * (szyrzka / szyrzkaPola) + 10, b * (wyszka / wyszkaPola) + 12));
                    }
                }
            }
        }

        bool mouseDown = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            if (poKliknyciu == 4)
            {
                if (mouseDown && xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] == 0)
                {
                    xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] = porzadi;
                    porzadi++;
                }
            }
                //wymaz
            else if (poKliknyciu == 7)
            {
                if (xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] != -1)
                {
                    int max = 0;
                    for (int a = 0; a < szyrzkaPola; a++)
                    {
                        for (int b = 0; b < wyszkaPola; b++)
                        {
                            if (xypole[a, b] > max)
                            {
                                max = xypole[a, b];
                            }
                        }
                    }
                    if (xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] == max)
                    {
                        porzadi = max;
                    }
                    xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] = 0;
                }
            }
            else if (poKliknyciu == 8)
            {
                if (xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] != -1 &&
                    xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] != 0)
                {
                    if (xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] == porzadi - 1)
                    {
                        porzadi--;
                    }
                    xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] -= 1;
                }
            }
            else if (poKliknyciu == 9)
            {
                if (xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] != -1)
                {
                    if (xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] == porzadi - 1)
                    {
                        porzadi++;
                    }
                    xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] += 1;
                }
            }
            g.Clear(Color.White);
            namalujMrzizke();
            namalujXYpole();
            pictureBox1.Image = bmp;

        }
        //resetuj
        private void button2_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < szyrzkaPola; a++)
            {
                for (int b = 0; b < wyszkaPola; b++)
                {
                    xypole[a, b] = 0;
                }
            }
            if (Zaklad.Checked)
            { xypole[7, 8] = -1; }
            else { xypole[7, 8] = 0; }
            if (Napowieda1.Checked)
            { xypole[2, 2] = -1; }
            else { xypole[2, 2] = 0; }
            if (Napowieda2.Checked)
            { xypole[13, 2] = -1; }
            else { xypole[13, 2] = 0; }
            if (Napowieda3.Checked)
            { xypole[2, 13] = -1; }
            else { xypole[2, 13] = 0; }
            if (Napowieda4.Checked)
            { xypole[13, 13] = -1; }
            else { xypole[13, 13] = 0; }
            g.Clear(Color.White);
            namalujMrzizke();
            namalujXYpole();
            pictureBox1.Image = bmp;
            porzadi = 1;
        }
        //zapisz
        private void button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText("C:\\Eternity\\" + textBox1.Text + ".txt", String.Empty);
            using (System.IO.StreamWriter file2 = new System.IO.StreamWriter("C:\\Eternity\\" + textBox1.Text + ".txt", true))
            {
                for (int m = 0; m < wyszkaPola; m++)
                {
                    for (int n = 0; n < szyrzkaPola; n++)
                    {
                        file2.Write(xypole[n, m] >= 100 ? " " : xypole[n, m] >= 10 ? "  " : "   ");
                        file2.Write(xypole[n, m] >= 0 ? xypole[n, m] : 0);
                    }
                    file2.WriteLine();
                }
                file2.Close();
            }
        }
        //naczti
        private void button3_Click(object sender, EventArgs e)
        {
            porzadi = 1;
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Eternity\\" + textBox2.Text + ".txt");
            for (int a = 0; a < wyszkaPola; a++)
            {
                for (int b = 0; b < szyrzkaPola; b++)
                {
                    int czislo = 0;
                    file.Read();
                    char c = (char)file.Read();
                    if (c != (char)' ')
                    {
                        czislo += (Convert.ToInt32(c) - 48) * 100;
                    }
                    c = (char)file.Read();
                    if (c != (char)' ')
                    {
                        czislo += (Convert.ToInt32(c) - 48) * 10;
                    }
                    c = (char)file.Read();
                    if (c != (char)' ')
                    {
                        czislo += Convert.ToInt32(c) - 48;
                        
                    }
                    xypole[b, a] = czislo;
                    if(czislo != 0)
                    {
                        porzadi++;
                    }
                }
                file.Read();
                file.Read();
            }
            file.Close();
            if (Zaklad.Checked)
            { xypole[7, 8] = -1; }
            if (Napowieda1.Checked)
            { xypole[2, 2] = -1; }
            if (Napowieda2.Checked)
            { xypole[13, 2] = -1; }
            if (Napowieda3.Checked)
            { xypole[2, 13] = -1; }
            if (Napowieda4.Checked)
            { xypole[13, 13] = -1; }
            g.Clear(Color.White);
            namalujMrzizke();
            namalujXYpole();
            pictureBox1.Image = bmp;
        }

        //wymaz
        private void button7_Click(object sender, EventArgs e)
        {
            poKliknyciu = 7;
            button7.BackColor = Color.Tomato;
            button4.BackColor = button1.BackColor;
            button8.BackColor = button1.BackColor;
            button9.BackColor = button1.BackColor;
        }
        //dowej nowe
        private void button4_Click(object sender, EventArgs e)
        {
            poKliknyciu = 4;
            button4.BackColor = Color.Tomato;
            button7.BackColor = button1.BackColor;
            button8.BackColor = button1.BackColor;
            button9.BackColor = button1.BackColor;
        }
        //zmjynsz jedno
        private void button8_Click(object sender, EventArgs e)
        {
            poKliknyciu = 8;
            button8.BackColor = Color.Tomato;
            button7.BackColor = button1.BackColor;
            button4.BackColor = button1.BackColor;
            button9.BackColor = button1.BackColor;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            poKliknyciu = 9;
            button9.BackColor = Color.Tomato;
            button7.BackColor = button1.BackColor;
            button4.BackColor = button1.BackColor;
            button8.BackColor = button1.BackColor;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            szyrzkaPola = Convert.ToInt32(numericUpDown1.Value);
            wyszkaPola = Convert.ToInt32(numericUpDown2.Value);
            pictureBox1.Width = szyrzkaPola * 34;
            pictureBox1.Height = wyszkaPola * 34;
            szyrzka = pictureBox1.Width;
            wyszka = pictureBox1.Height;
            bmp = new Bitmap(szyrzka, wyszka);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            namalujMrzizke();
            if (Zaklad.Checked)
            { xypole[7, 8] = -1; }
            else { xypole[7, 8] = 0; }
            if (Napowieda1.Checked)
            { xypole[2, 2] = -1; }
            else { xypole[2, 2] = 0; }
            if (Napowieda2.Checked)
            { xypole[13, 2] = -1; }
            else { xypole[13, 2] = 0; }
            if (Napowieda3.Checked)
            { xypole[2, 13] = -1; }
            else { xypole[2, 13] = 0; }
            if (Napowieda4.Checked)
            { xypole[13, 13] = -1; }
            else { xypole[13, 13] = 0; }
            namalujXYpole();
            pictureBox1.Image = bmp;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && e.X >= 0 && e.X < pictureBox1.Width && e.Y >= 0 && e.Y < pictureBox1.Height)
            {
                if (poKliknyciu == 4)
                {
                    if (xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] == 0)
                    {
                        xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] = porzadi;
                        porzadi++;
                    }
                }
                //wymaz
                else if (poKliknyciu == 7)
                {
                    if (xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] != -1)
                    {
                        int max = 0;
                        for (int a = 0; a < szyrzkaPola; a++)
                        {
                            for (int b = 0; b < wyszkaPola; b++)
                            {
                                if (xypole[a, b] > max)
                                {
                                    max = xypole[a, b];
                                }
                            }
                        }
                        if (xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] == max)
                        {
                            xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] = 0;
                            int max2 = 0;
                            for (int a = 0; a < szyrzkaPola; a++)
                            {
                                for (int b = 0; b < wyszkaPola; b++)
                                {
                                    if (xypole[a, b] > max2)
                                    {
                                        max2 = xypole[a, b];
                                    }
                                }
                            }
                            porzadi = max2 + 1;
                        }
                        else
                        {
                            xypole[e.X / (szyrzka / szyrzkaPola), e.Y / (wyszka / wyszkaPola)] = 0;
                        }
                    }
                }
                g.Clear(Color.White);
                namalujMrzizke();
                namalujXYpole();
                pictureBox1.Image = bmp;
                pictureBox1.Update();
            }
        }

        private void Zaklad_CheckedChanged(object sender, EventArgs e)
        {
            load();
        }

        private void Napowieda1_CheckedChanged(object sender, EventArgs e)
        {
            load();
        }

        private void Napowieda2_CheckedChanged(object sender, EventArgs e)
        {
            load();
        }

        private void Napowieda3_CheckedChanged(object sender, EventArgs e)
        {
            load();
        }

        private void Napowieda4_CheckedChanged(object sender, EventArgs e)
        {
            load();
        }
    }
}
