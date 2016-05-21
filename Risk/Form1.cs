/* Parker Bedlan
 * Risk Project
 * Started: May 11, 2016
 * 
 * Next Phase: Attacking
 *      Ctrl + F "///" to find Work in Progress
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
using System.Collections;

namespace Risk
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        //public variables
        Territory[] territories;
        Player[] players;
        Player currentPlayer;

        int playerIndex;
        TextBox tb;

        bool started;

        //initial instantiation of the public variables
        private void Form1_Load(object sender, EventArgs e)
        {
            territories = new Territory[0];
            players = new Player[0];
            currentPlayer = new Player();
            playerIndex = 0;
            tb = textBox1;
            started = false;
        }

        //start button
        private void btnStart_Click(object sender, EventArgs e)
        {
            if(!started)
            {
                started = true;
                tb.Text += "Starting the game..." + Environment.NewLine;

                Territory brah = new Territory(0, null, new int[] { 1 });
                Territory bruh = new Territory(1, null, new int[] { 0, 2, 4 });
                Territory bro = new Territory(2, null, new int[] { 1, 3, 4 });
                Territory broski = new Territory(3, null, new int[] { 2, 4, 5 });
                Territory bruda = new Territory(4, null, new int[] { 1, 2, 5 });
                Territory bretheren = new Territory(5, null, new int[] { 3, 4 });
                territories = new Territory[] { brah, bruh, bro, broski, bruda, bretheren };

                Player player1 = new Player("Parker", Color.Red, tb);
                Player player2 = new Player("Billy", Color.Blue, tb);
                Player player3 = new Player("Henry", Color.Green, tb);
                players = new Player[] { player1, player2, player3 };

                //Picking (all players, in a circle)
                currentPlayer = players[playerIndex];
                currentPlayer.Pick();

            }
        }

        private void btnBeginTurn_Click(object sender, EventArgs e)
        {
            playerIndex = 0;
            currentPlayer = players[playerIndex];
            currentPlayer.Draft();
        }

        //clicked on territory
        public void DoButtonStuff(int btnNum, Button btn)
        {

            if(started)
            {
                Territory terr = territories[btnNum];

                if (currentPlayer.GetStatus().Equals("picking"))
                {
                    if (terr.GetOwner() == null)
                    {
                        terr.ChangeOwner(currentPlayer);
                        terr.AddTroops(1);
                        currentPlayer.AddTerr(terr);
                        btn.BackColor = currentPlayer.GetColor();
                        btn.Text = "" + terr.GetTroopNum();
                        currentPlayer.ChangeStatus("idle");

                        if (AllOwned())
                        {
                            tb.Text += "Drafting phase." + Environment.NewLine;
                            playerIndex = 0;
                            currentPlayer = players[playerIndex];
                            currentPlayer.Draft();
                        }
                        else
                        {
                            if (playerIndex < players.Length - 1)
                                playerIndex++;
                            else
                                playerIndex = 0;

                            currentPlayer = players[playerIndex];
                            currentPlayer.Pick();
                        }
                    }
                }
                else if (currentPlayer.GetStatus().Equals("drafting"))
                {
                    currentPlayer.TakeDraftTroop(1);
                    terr.AddTroops(1);
                    btn.Text = "" + terr.GetTroopNum();

                    if (currentPlayer.GetDraftTroopNum() == 0)
                    {
                        currentPlayer.ChangeStatus("idle");

                        tb.Text += "Attacking phase." + Environment.NewLine;    ///
                    }


                }
            }

        }

        public bool AllOwned()
        {
            for (int i = 0; i < territories.Length; i++)
            {
                if (territories[i].GetOwner() == null)
                    return false;
            }
            return true;
        }










        //btn event handlers, not important
        private void btn0_Click(object sender, EventArgs e)
        {
            DoButtonStuff(0, btn0);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            DoButtonStuff(1, btn1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            DoButtonStuff(2, btn2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            DoButtonStuff(3, btn3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            DoButtonStuff(4, btn4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            DoButtonStuff(5, btn5);
        }

    }
}
