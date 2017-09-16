using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace virtual_tetris
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        int score = 0;
        int level = 1;
        int interval = 500;

        int rotation_state = 0;
        int next_piece_coming_up = 0;

        Random num_gen = new Random();

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            #region debugging tools
            /*
            if (e.KeyCode == Keys.U)
            {
                timer1.Stop();
            }
            if (e.KeyCode == Keys.I)
            {
                timer1.Start();
            }
            if (e.KeyCode == Keys.O)
            {
                foreach (block item in board)
                {
                    if (item.state == "fixed")
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Black;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "fixed";
                        item.state = "fixed";
                        this.Controls.Add(real_block);
                    }
                }

            }
            if (e.KeyCode == Keys.P)
            {
                foreach (PictureBox item in Controls)
                {
                    Controls.Remove(item);
                }

            }
            */
            #endregion

            if (e.KeyCode == Keys.Space)
            {
                if (level == 1) { score = score + 50; }
                else if (level == 2) { score = score + 75; }
                else if (level == 3) { score = score + 100; }
                else if (level == 4) { score = score + 125; }
                else if (level == 5) { score = score + 150; }
                else if (level == 6) { score = score + 175; }
                else if (level == 7) { score = score + 200; }
                else if (level == 8) { score = score + 225; }
                else if (level == 9) { score = score + 250; }
                else if (level == 10) { score = score + 300; }


                int[] want_to_go_to_x = new int[4];
                int[] want_to_go_to_y = new int[4];
                bool[] all_clear = new bool[4];

                do
                {

                    Array.Clear(want_to_go_to_x, 0, want_to_go_to_x.Length);
                    Array.Clear(want_to_go_to_y, 0, want_to_go_to_y.Length);
                    Array.Clear(all_clear, 0, all_clear.Length);

                    check_if_taken(all_clear, want_to_go_to_x, want_to_go_to_y, 0, -25);

                    if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true)
                    {
                        delete_blocks();
                        draw_new_blocks(want_to_go_to_x, want_to_go_to_y);
                    }

                } while (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true);

                fixate();

                add_piece(next_piece_coming_up);

                next_piece_coming_up = num_gen.Next(0, 7);
                if (next_piece_coming_up == 0) next_piece.Image = virtual_tetris.Properties.Resources.I;
                else if (next_piece_coming_up == 1) next_piece.Image = virtual_tetris.Properties.Resources.S;
                else if (next_piece_coming_up == 2) next_piece.Image = virtual_tetris.Properties.Resources.Z;
                else if (next_piece_coming_up == 3) next_piece.Image = virtual_tetris.Properties.Resources.O;
                else if (next_piece_coming_up == 4) next_piece.Image = virtual_tetris.Properties.Resources.L;
                else if (next_piece_coming_up == 5) next_piece.Image = virtual_tetris.Properties.Resources.J;
                else if (next_piece_coming_up == 6) next_piece.Image = virtual_tetris.Properties.Resources.T;
            }
            if (e.KeyCode == Keys.Left)
            {
                int[] want_to_go_to_x = new int[4];
                int[] want_to_go_to_y = new int[4];
                bool[] all_clear = new bool[4];

                check_if_taken(all_clear, want_to_go_to_x, want_to_go_to_y, 25, 0);

                if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true)
                {
                    delete_blocks();
                    draw_new_blocks(want_to_go_to_x, want_to_go_to_y);
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                int[] want_to_go_to_x = new int[4];
                int[] want_to_go_to_y = new int[4];
                bool[] all_clear = new bool[4];

                check_if_taken(all_clear, want_to_go_to_x, want_to_go_to_y, -25, 0);


                if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true)
                {
                    delete_blocks();
                    draw_new_blocks(want_to_go_to_x, want_to_go_to_y);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                foreach (PictureBox item in this.Controls.OfType<PictureBox>())
                {
                    if (item.Tag.ToString() == "moving-0" && item.BackColor == Color.Orange)
                    {
                        do_the_rotation_thing(-25, 25, 0, 0, 25, -25, 50, -50, 25, -25, 0, 0, -25, 25, -50, 50);
                    }

                    if (item.Tag.ToString() == "moving-0" && item.BackColor == Color.LightBlue)
                    {
                        do_the_rotation_thing(0, 25, 0, 0, -25, 25, -25, 0, 0, -25, 0, 0, 25, -25, 25, 0);
                    }
                    if (item.Tag.ToString() == "moving-0" && item.BackColor == Color.Green)
                    {
                        do_the_rotation_thing(0, 0, 0, 0, 25, 0, 25, 50, 0, 0, 0, 0, -25, 0, -25, -50);
                    }
                    if (item.Tag.ToString() == "moving-0" && item.BackColor == Color.Blue)
                    {
                        do_the_rotation_thing(-25, 25, 0, 0, 25, -25, 0, 50, -25, -25, 0, 0, 25, 25, -50, 0, 25, -25, 0, 0, -25, 25, 0, -50, 25, 25, 0, 0, -25, -25, 50, 0);
                    }
                    if (item.Tag.ToString() == "moving-0" && item.BackColor == Color.Purple)
                    {
                        do_the_rotation_thing(-25, 25, 0, 0, 25, -25, 50, 0, -25, -25, 0, 0, 25, 25, 0, 50, 25, -25, 0, 0, -25, 25, -50, 0, 25, 25, 0, 0, -25, -25, 0, -50);
                    }
                    if (item.Tag.ToString() == "moving-0" && item.BackColor == Color.Yellow)
                    {
                        do_the_rotation_thing(-25, 25, 0, 0, 25, -25, 25, 25, -25, -25, 0, 0, 25, 25, -25, 25, 25, -25, 0, 0, -25, 25, -25, -25, 25, 25, 0, 0, -25, -25, 25, -25);
                    }
                }

            }
            if (e.KeyCode == Keys.Down)
            {
                timer1.Interval = 50;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                timer1.Interval = interval;
            }
        }

        private void do_the_rotation_thing(int a, int b, int c, int d, int e, int f, int g, int h, int aa, int bb, int cc, int dd, int ee, int ff, int gg, int hh)
        {
            int[] want_to_go_to_x = new int[4];
            int[] want_to_go_to_y = new int[4];
            bool[] all_clear = new bool[4];

            if (rotation_state == 0)
            {
                check_if_taken(all_clear, want_to_go_to_x, want_to_go_to_y, a, b, c, d, e, f, g, h);
                if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true) rotation_state++;
            }
            else if (rotation_state == 1)
            {
                check_if_taken(all_clear, want_to_go_to_x, want_to_go_to_y, aa, bb, cc, dd, ee, ff, gg, hh);
                if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true) rotation_state = 0;
            }

            if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true)
            {
                delete_blocks();
                /*
                int i=0;
                foreach (var item in want_to_go_to_x)
                {
                    Console.WriteLine(want_to_go_to_x[i].ToString() + " "+ want_to_go_to_y[i].ToString());
                    i++;
                }*/
                draw_new_blocks(want_to_go_to_x, want_to_go_to_y);
            }
        }
        private void do_the_rotation_thing(int a, int b, int c, int d, int e, int f, int g, int h, int aa, int bb, int cc, int dd, int ee, int ff, int gg, int hh, int aaa, int bbb, int ccc, int ddd, int eee, int fff, int ggg, int hhh, int aaaa, int bbbb, int cccc, int dddd, int eeee, int ffff, int gggg, int hhhh)
        {
            int[] want_to_go_to_x = new int[4];
            int[] want_to_go_to_y = new int[4];
            bool[] all_clear = new bool[4];

            if (rotation_state == 0)
            {
                check_if_taken(all_clear, want_to_go_to_x, want_to_go_to_y, a, b, c, d, e, f, g, h);
                if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true) rotation_state++;
            }
            else if (rotation_state == 1)
            {
                check_if_taken(all_clear, want_to_go_to_x, want_to_go_to_y, aa, bb, cc, dd, ee, ff, gg, hh);
                if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true) rotation_state++;
            }
            else if (rotation_state == 2)
            {
                check_if_taken(all_clear, want_to_go_to_x, want_to_go_to_y, aaa, bbb, ccc, ddd, eee, fff, ggg, hhh);
                if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true) rotation_state++;
            }
            else if (rotation_state == 3)
            {
                check_if_taken(all_clear, want_to_go_to_x, want_to_go_to_y, aaaa, bbbb, cccc, dddd, eeee, ffff, gggg, hhhh);
                if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true) rotation_state = 0;
            }
            //Console.WriteLine(rotation_state.ToString());
            if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true)
            {
                delete_blocks();
                draw_new_blocks(want_to_go_to_x, want_to_go_to_y);
            }
        }
        private void check_if_taken(bool[] condition, int[] destination_x, int[] destination_y, int direction_x, int direction_y)
        {
            foreach (block item in board)
            {
                foreach (block item2 in board)
                {
                    //MessageBox.Show("item.state:" + item.state +"\nitem x" + item.x.ToString() + "\nitem y" + item.y.ToString() + "\nitem2.state:" + item2.state +"\nitem2 x" + item2.x.ToString() + "\nitem2 y" + item2.y.ToString());

                    if (item.state == "moving-0" && ((item.x - direction_x) == item2.x && (item.y - direction_y) == item2.y) && item2.state != "fixed")
                    {
                        condition[0] = true;
                        destination_x[0] = item2.x;
                        destination_y[0] = item2.y;
                    }
                    if (item.state == "moving-1" && ((item.x - direction_x) == item2.x && (item.y - direction_y) == item2.y) && item2.state != "fixed")
                    {
                        condition[1] = true;
                        destination_x[1] = item2.x;
                        destination_y[1] = item2.y;
                    }
                    if (item.state == "moving-2" && ((item.x - direction_x) == item2.x && (item.y - direction_y) == item2.y) && item2.state != "fixed")
                    {
                        condition[2] = true;
                        destination_x[2] = item2.x;
                        destination_y[2] = item2.y;
                    }
                    if (item.state == "moving-3" && ((item.x - direction_x) == item2.x && (item.y - direction_y) == item2.y) && item2.state != "fixed")
                    {
                        condition[3] = true;
                        destination_x[3] = item2.x;
                        destination_y[3] = item2.y;
                    }
                }
            }
        }
        private void check_if_taken(bool[] condition, int[] destination_x, int[] destination_y, int direction_x, int direction_y, int direction_x2, int direction_y2, int direction_x3, int direction_y3, int direction_x4, int direction_y4)
        {
            foreach (block item in board)
            {
                foreach (block item2 in board)
                {
                    //MessageBox.Show("item.state:" + item.state +"\nitem x" + item.x.ToString() + "\nitem y" + item.y.ToString() + "\nitem2.state:" + item2.state +"\nitem2 x" + item2.x.ToString() + "\nitem2 y" + item2.y.ToString());

                    if (item.state == "moving-0" && ((item.x - direction_x) == item2.x && (item.y - direction_y) == item2.y) && item2.state != "fixed")
                    {
                        condition[0] = true;
                        destination_x[0] = item2.x;
                        destination_y[0] = item2.y;
                    }
                    if (item.state == "moving-1" && ((item.x - direction_x2) == item2.x && (item.y - direction_y2) == item2.y) && item2.state != "fixed")
                    {
                        condition[1] = true;
                        destination_x[1] = item2.x;
                        destination_y[1] = item2.y;
                    }
                    if (item.state == "moving-2" && ((item.x - direction_x3) == item2.x && (item.y - direction_y3) == item2.y) && item2.state != "fixed")
                    {
                        condition[2] = true;
                        destination_x[2] = item2.x;
                        destination_y[2] = item2.y;
                    }
                    if (item.state == "moving-3" && ((item.x - direction_x4) == item2.x && (item.y - direction_y4) == item2.y) && item2.state != "fixed")
                    {
                        condition[3] = true;
                        destination_x[3] = item2.x;
                        destination_y[3] = item2.y;
                    }
                }
            }
        }
        private void delete_blocks()
        {
            foreach (block item in board)
            {
                {
                    if (item.state.Contains("moving"))
                    {
                        item.state = "empty";
                        item.color = Color.White;
                    }
                }

            }
        }
        private void draw_new_blocks(int[] destination_x, int[] destination_y)
        {
            int i = 0;
            foreach (PictureBox item in this.Controls.OfType<PictureBox>())
            {
                if (item.Tag.ToString().Contains("moving"))
                {

                    item.Location = new Point(destination_x[i], destination_y[i]);
                    i++;
                }
            }

            for (int j = 0; j < 4; j++)
            {
                foreach (block item in board)
                {
                    if (item.x == destination_x[j] && item.y == destination_y[j])
                    {
                        //Console.WriteLine(destination_x[i].ToString() + " " + destination_y[i].ToString());
                        item.state = "moving-" + j.ToString();
                        //Console.WriteLine("moving-" + i.ToString());

                        //if (j == 3) break;
                        //j++;
                    }
                }
            }





        }

        private void happend_thing(int starting_from, int move_down)
        {


            List<int> want_to_go_to_x = new List<int>();
            List<int> want_to_go_to_y = new List<int>();

            foreach (block item in board)
            {
                if (item.y < (starting_from) && item.state == "fixed")
                {
                    item.state = "empty";
                    want_to_go_to_x.Add(item.x);
                    want_to_go_to_y.Add(item.y);
                }
            }

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (item.Location.Y < (starting_from) && item.Tag.ToString() != "special")
                {
                    item.Location = new Point(item.Location.X, (item.Location.Y + move_down));
                }
            }


            int counter2 = 0;
            for (int i = 0; i < want_to_go_to_x.Count; i++)
            {


                foreach (block item in board)
                {
                    if (item.x == want_to_go_to_x.ElementAt(counter2) && item.y == (want_to_go_to_y.ElementAt(counter2) + move_down))
                    {

                        item.state = "fixed";

                    }
                }



                counter2++;
            }





        }


        private void check_if_line()
        {
            int[] starting_from = new int[4];
            int do_times = 0;
            int[] counter = new int[23];
            for (int i = 0; i < 23; i++)
            {
                foreach (block item in board)
                {
                    if (item.y == (i * 25) && item.state == "fixed")
                    {
                        counter[i]++;
                        if (counter[i] == 10)
                        {
                            starting_from[do_times] = item.y;
                            do_times++;
                        }

                    }
                }
            }

            if (do_times == 1) score = score + 250;
            else if (do_times == 2) score = score + 500;
            else if (do_times == 3) score = score + 1000;
            else if (do_times == 4) score = score + 3000;

            for (int i = 0; i < 23; i++)
            {
                if (counter[i] == 10)
                {
                    foreach (block item in board)
                    {
                        if (item.y == (i * 25))
                        {
                            item.state = "empty";
                            item.color = Color.White;

                            foreach (PictureBox item2 in Controls.OfType<PictureBox>())
                            {
                                if (item2.Location.X == item.x && item2.Location.Y == item.y) { Controls.Remove(item2); }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < do_times; i++)
            {
                happend_thing(starting_from[i], 25);
            }



        }

        private void fixate()
        {
            foreach (PictureBox item in this.Controls.OfType<PictureBox>())
            {
                if (item.Tag.ToString().Contains("moving"))
                {
                    item.Tag = "fixed";
                }

            }
            foreach (block item in board)
            {
                if (item.state.ToString().Contains("moving"))
                {
                    item.state = "fixed";
                }

            }

            check_if_line();

        }
        private void add_piece(int which_one)
        {
            if (which_one == 0)
            {
                foreach (block item in board)
                {
                    if (item.x == 100 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Orange;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-0";
                        item.state = "moving-0";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 125 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Orange;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-1";
                        item.state = "moving-1";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 150 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Orange;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-2";
                        item.state = "moving-2";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 175 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Orange;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-3";
                        item.state = "moving-3";
                        this.Controls.Add(real_block);
                    }
                }
            }
            if (which_one == 1)
            {
                foreach (block item in board)
                {
                    if (item.x == 125 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.LightBlue;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-0";
                        item.state = "moving-0";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 150 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.LightBlue;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-1";
                        item.state = "moving-1";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 100 && item.y == 50)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.LightBlue;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-2";
                        item.state = "moving-2";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 125 && item.y == 50)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.LightBlue;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-3";
                        item.state = "moving-3";
                        this.Controls.Add(real_block);
                    }
                }
            }
            if (which_one == 2)
            {
                foreach (block item in board)
                {
                    if (item.x == 125 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Green;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-0";
                        item.state = "moving-0";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 100 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Green;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-1";
                        item.state = "moving-1";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 125 && item.y == 50)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Green;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-2";
                        item.state = "moving-2";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 150 && item.y == 50)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Green;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-3";
                        item.state = "moving-3";
                        this.Controls.Add(real_block);
                    }
                }
            }
            if (which_one == 3)
            {
                foreach (block item in board)
                {
                    if (item.x == 125 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Red;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-0";
                        item.state = "moving-0";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 150 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Red;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-1";
                        item.state = "moving-1";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 125 && item.y == 50)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Red;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-2";
                        item.state = "moving-2";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 150 && item.y == 50)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Red;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-3";
                        item.state = "moving-3";
                        this.Controls.Add(real_block);
                    }
                }
            }
            if (which_one == 4)
            {
                foreach (block item in board)
                {
                    if (item.x == 100 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Blue;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-0";
                        item.state = "moving-0";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 125 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Blue;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-1";
                        item.state = "moving-1";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 150 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Blue;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-2";
                        item.state = "moving-2";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 100 && item.y == 50)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Blue;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-3";
                        item.state = "moving-3";
                        this.Controls.Add(real_block);
                    }
                }
            }
            if (which_one == 5)
            {
                foreach (block item in board)
                {
                    if (item.x == 100 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Purple;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-0";
                        item.state = "moving-0";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 125 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Purple;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-1";
                        item.state = "moving-1";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 150 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Purple;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-2";
                        item.state = "moving-2";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 150 && item.y == 50)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Purple;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-3";
                        item.state = "moving-3";
                        this.Controls.Add(real_block);
                    }
                }
            }
            if (which_one == 6)
            {
                foreach (block item in board)
                {
                    if (item.x == 100 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Yellow;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-0";
                        item.state = "moving-0";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 125 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Yellow;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-1";
                        item.state = "moving-1";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 150 && item.y == 25)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Yellow;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-2";
                        item.state = "moving-2";
                        this.Controls.Add(real_block);
                    }
                    if (item.x == 125 && item.y == 50)
                    {
                        PictureBox real_block = new PictureBox();
                        real_block.BackColor = Color.Yellow;
                        real_block.Location = new Point(item.x, item.y);
                        real_block.Height = 25;
                        real_block.Width = 25;
                        real_block.Tag = "moving-3";
                        item.state = "moving-3";
                        this.Controls.Add(real_block);
                    }
                }
            }
            rotation_state = 0; //reset rotation

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (item.Tag.ToString().Contains("moving"))
                {
                    foreach (PictureBox item2 in Controls.OfType<PictureBox>())
                    {
                        if (item.Bounds.IntersectsWith(item2.Bounds) && item2.Tag.ToString() == "fixed") { timer1.Stop(); MessageBox.Show("You lose."); Application.Exit(); }
                    }
                }

            }

        }

        block[] board = new block[220];

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = interval;

            int xx = 25;
            int yy = 25;

            for (int i = 0; i < board.Length; i++)
            {
                board[i] = new block();
                board[i].color = Color.White;
                board[i].state = "empty";
                board[i].x = xx;
                board[i].y = yy;

                xx = xx + 25;
                if (xx == 275)
                {
                    xx = 25;
                    yy = yy + 25;
                }
            }
            /*
            PictureBox next_piece = new PictureBox();
            next_piece.BackColor = Color.Black;
            next_piece.Tag = "special";
            next_piece.Location=  new Point(300,50);
            next_piece.Height = 100;
            next_piece.Width = next_piece.Height;
            next_piece.Image = virtual_tetris.Properties.Resources.J;
            Controls.Add(next_piece);
            */
            next_piece_coming_up = num_gen.Next(0, 7);
            if (next_piece_coming_up == 0) next_piece.Image = virtual_tetris.Properties.Resources.I;
            else if (next_piece_coming_up == 1) next_piece.Image = virtual_tetris.Properties.Resources.S;
            else if (next_piece_coming_up == 2) next_piece.Image = virtual_tetris.Properties.Resources.Z;
            else if (next_piece_coming_up == 3) next_piece.Image = virtual_tetris.Properties.Resources.O;
            else if (next_piece_coming_up == 4) next_piece.Image = virtual_tetris.Properties.Resources.L;
            else if (next_piece_coming_up == 5) next_piece.Image = virtual_tetris.Properties.Resources.J;
            else if (next_piece_coming_up == 6) next_piece.Image = virtual_tetris.Properties.Resources.T;






            add_piece(num_gen.Next(0, 7));

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            int[] want_to_go_to_x = new int[4];
            int[] want_to_go_to_y = new int[4];
            bool[] all_clear = new bool[4];

            check_if_taken(all_clear, want_to_go_to_x, want_to_go_to_y, 0, -25);


            if (all_clear[0] == true && all_clear[1] == true && all_clear[2] == true && all_clear[3] == true)
            {
                delete_blocks();
                draw_new_blocks(want_to_go_to_x, want_to_go_to_y);
            }
            else
            {
                fixate();

                add_piece(next_piece_coming_up);

                next_piece_coming_up = num_gen.Next(0, 7);
                if (next_piece_coming_up == 0) next_piece.Image = virtual_tetris.Properties.Resources.I;
                else if (next_piece_coming_up == 1) next_piece.Image = virtual_tetris.Properties.Resources.S;
                else if (next_piece_coming_up == 2) next_piece.Image = virtual_tetris.Properties.Resources.Z;
                else if (next_piece_coming_up == 3) next_piece.Image = virtual_tetris.Properties.Resources.O;
                else if (next_piece_coming_up == 4) next_piece.Image = virtual_tetris.Properties.Resources.L;
                else if (next_piece_coming_up == 5) next_piece.Image = virtual_tetris.Properties.Resources.J;
                else if (next_piece_coming_up == 6) next_piece.Image = virtual_tetris.Properties.Resources.T;


            }
            label_score.Text = score.ToString();
            score++;
            if (timer1.Interval == 50) score++;


            if (score > 5000 && score < 9999) { level = 2; interval = 450; label_level.Text = level.ToString(); }
            else if (score > 10000 && score < 19999) { level = 3; interval = 400; label_level.Text = level.ToString(); }
            else if (score > 20000 && score < 29999) { level = 4; interval = 350; label_level.Text = level.ToString(); }
            else if (score > 30000 && score < 39999) { level = 5; interval = 300; label_level.Text = level.ToString(); }
            else if (score > 40000 && score < 49999) { level = 6; interval = 250; label_level.Text = level.ToString(); }
            else if (score > 50000 && score < 69999) { level = 7; interval = 200; label_level.Text = level.ToString(); }
            else if (score > 70000 && score < 99999) { level = 8; interval = 150; label_level.Text = level.ToString(); }
            else if (score > 100000 && score < 149999) { level = 9; interval = 100; label_level.Text = level.ToString(); }
            else if (score > 150000) { level = 10; interval = 75; label_level.Text = level.ToString(); }


        }


    }

    public class block
    {
        public Color color;
        public string state;
        public int x;
        public int y;
    }
}
