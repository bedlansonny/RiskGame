/* Parker Bedlan
 * Risk Project
 * Started: May 11, 2016
 * 
 * Ctrl + F "///" to find Work in Progress
 * 
 * Future additions:
 *      Also, say how many troops are to be put down when drafting
 *      card system
 *      intro form that sets up characters, what happens after winning, etc.
 *          option for random picking of territories at the beginning
 *      after initial picking, more troops need to be included to add onto owned troops (look at risk rules online)
 *      ability to increase amount of troops moved in a click
            also add the ability to take back troops in drafting
 *      blinking when transferring troops after attacking
 *      skip fortifying step if all owned troops only have 1
 *      make dice probability rather than actual dice based
 *      Skip fortifying when impossible
 *      Have Communist symbol, swatstika, flag selection
 *          Soundtrack everytime a territory is taken over or not, based on flag
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
        bool started;

        TextBox tb;

        Button[] btns;

        //initial instantiation of the public variables
        private void Form1_Load(object sender, EventArgs e)
        {
            territories = new Territory[0];
            players = new Player[0];
            currentPlayer = new Player();
            playerIndex = 0;
            started = false;
            tb = textBox1;
            Player.SetTextBox(tb);


        }

        public void CheckForWinner()
        {
            Player winner = null;

            foreach(Player p in players)
            {
                if (!p.GetIsDead())
                {
                    if (winner == null)
                        winner = p;
                    else
                        return;
                }
            }

            MessageBox.Show(winner.GetName() + " won the game!!!!!");

        }

        public void NextPlayer()
        {
            if (playerIndex < players.Length - 1)
                playerIndex++;
            else
            {
                playerIndex = 0;
            }
            currentPlayer = players[playerIndex];
            if (currentPlayer.GetIsDead())
                NextPlayer();

            btnUni.BackColor = currentPlayer.GetColor();

            currentPlayer.Draft();
        }

        private void btnUni_Click(object sender, EventArgs e)
        {
            if (btnUni.Text.Equals("Start Game"))
            {
                if (!started)
                {
                    started = true;

                    btns = new Button[]
                    {
                        button0, button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, 
                        button11, button12, button13, button14, button15, button16, button17, button18, button19, button20, 
                        button21, button22, button23, button24, button25, button26, button27, button28, button29, button30, 
                        button31, button32, button33, button34, button35, button36, button37, button38, button39, button40, button41
                    };
                    Territory.btns = btns;

                    /*
                    Territory brah = new Territory(0, new int[] { 1 });
                    Territory bruh = new Territory(1, new int[] { 0, 2, 4 });
                    Territory bro = new Territory(2, new int[] { 1, 3, 4 });
                    Territory broski = new Territory(3, new int[] { 2, 4, 5 });
                    Territory bruda = new Territory(4, new int[] { 1, 2, 5 });
                    Territory bretheren = new Territory(5, new int[] { 3, 4 });
                     */

                    // Territory t = new Territory(, new int[] {})
                    Territory t0 = new Territory(0, new int[] { 40, 41 });
                    Territory t1 = new Territory(1, new int[] { 2, 4, 30 });
                    Territory t2 = new Territory(2, new int[] { 1, 3, 4, 5 });
                    Territory t3 = new Territory(3, new int[] { 2, 5, 6, 20 });
                    Territory t4 = new Territory(4, new int[] { 1, 2, 5, 7 });
                    Territory t5 = new Territory(5, new int[] { 2, 3, 4, 6, 7, 8 });
                    Territory t6 = new Territory(6, new int[] { 3, 5, 8 });
                    Territory t7 = new Territory(7, new int[] { 4, 5, 8, 9 });
                    Territory t8 = new Territory(8, new int[] { 5, 6, 7, 9 });
                    Territory t9 = new Territory(9, new int[] { 7, 8, 10 });
                    Territory t10 = new Territory(10, new int[] { 9, 11, 12 });
                    Territory t11 = new Territory(11, new int[] { 10, 12, 13 });
                    Territory t12 = new Territory(12, new int[] { 10, 11, 13, 14 });
                    Territory t13 = new Territory(13, new int[] { 11, 12 });
                    Territory t14 = new Territory(14, new int[] { 12, 15, 16, 17, 25, 26 });
                    Territory t15 = new Territory(15, new int[] { 14, 17, 26, 36 });
                    Territory t16 = new Territory(16, new int[] { 14, 17, 18 });
                    Territory t17 = new Territory(17, new int[] { 14, 15, 16, 18, 19, 36 });
                    Territory t18 = new Territory(18, new int[] { 16, 17, 19 });
                    Territory t19 = new Territory(19, new int[] { 17, 18 });
                    Territory t20 = new Territory(20, new int[] { 3, 21, 23 });
                    Territory t21 = new Territory(21, new int[] { 20, 22, 23, 24 });
                    Territory t22 = new Territory(22, new int[] { 21, 24, 26, 27, 34, 36 });
                    Territory t23 = new Territory(23, new int[] { 20, 21, 24, 25 });
                    Territory t24 = new Territory(24, new int[] { 21, 22, 23, 25, 26 });
                    Territory t25 = new Territory(25, new int[] { 14, 23, 24, 26 });
                    Territory t26 = new Territory(26, new int[] { 14, 15, 22, 24, 25, 36 });
                    Territory t27 = new Territory(27, new int[] { 22, 28, 34, 35 });
                    Territory t28 = new Territory(28, new int[] { 27, 29, 31, 33, 35 });
                    Territory t29 = new Territory(29, new int[] { 28, 30, 31 });
                    Territory t30 = new Territory(30, new int[] { 1, 29, 31, 32, 33 });
                    Territory t31 = new Territory(31, new int[] { 28, 29, 30, 33 });
                    Territory t32 = new Territory(32, new int[] { 30, 33 });
                    Territory t33 = new Territory(33, new int[] { 28, 30, 31, 32, 35 });
                    Territory t34 = new Territory(34, new int[] { 22, 27, 35, 36, 37 });
                    Territory t35 = new Territory(35, new int[] { 27, 28, 33, 34, 37, 38 });
                    Territory t36 = new Territory(36, new int[] { 15, 17, 22, 26, 34, 37 });
                    Territory t37 = new Territory(37, new int[] { 34, 35, 36, 38 });
                    Territory t38 = new Territory(38, new int[] { 35, 37, 39 });
                    Territory t39 = new Territory(39, new int[] { 38, 40, 41 });
                    Territory t40 = new Territory(40, new int[] { 0, 39, 41 });
                    Territory t41 = new Territory(41, new int[] { 0, 39, 40 });


                    territories = new Territory[]
                    {
                        t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12,
                        t13, t14, t15, t16, t17, t18, t19, t20, t21, t22, t23,
                        t24, t25, t26, t27, t28, t29, t30, t31, t32, t33, t34,
                        t35, t36, t37, t38, t39, t40, t41
                    };

                    Player player1 = new Player("Nick", Color.Green);
                    Player player2 = new Player("Parker", Color.Red);
                    players = new Player[] { player1, player2 };

                    currentPlayer = players[playerIndex];
                    btnUni.Text = "Picking...";
                    btnUni.BackColor = currentPlayer.GetColor();
                    currentPlayer.Pick();

                }
            }
            else if (btnUni.Text.Equals("Fortify"))
            {
                if (currentPlayer.GetStatus().Equals("choosing attacker") ||
                currentPlayer.GetStatus().Equals("choosing defender") ||
                currentPlayer.GetStatus().Equals("attacking") ||
                currentPlayer.GetStatus().Equals("relocating troops"))
                {
                    if (currentPlayer.GetAttacker() != null)
                        currentPlayer.GetAttacker().GetBtn().BackColor = currentPlayer.GetColor();
                    currentPlayer.SetAttacker(null);
                    currentPlayer.SetDefender(null);
                    currentPlayer.SetStatus("idle");

                    btnUni.Text = "End Turn";

                    currentPlayer.Fortify();
                }
            }
            else if (btnUni.Text.Equals("End Turn"))
            {
                if (currentPlayer.GetStatus().Equals("fortifying, picking source") ||
                currentPlayer.GetStatus().Equals("fortifying, picking target") ||
                currentPlayer.GetStatus().Equals("transferring"))
                {
                    currentPlayer.SetStatus("idle");
                    if (currentPlayer.GetFortSource() != null)
                        currentPlayer.GetFortSource().GetBtn().BackColor = currentPlayer.GetColor();
                    if (currentPlayer.GetFortTarget() != null)
                    {
                        currentPlayer.GetFortTarget().GetBtn().BackColor = currentPlayer.GetColor();
                        currentPlayer.GetFortTarget().GetBtn().ForeColor = Color.Black;
                    }

                    currentPlayer.SetFortSource(null);
                    currentPlayer.SetFortTarget(null);

                    btnUni.Text = "Drafting...";

                    NextPlayer();

                }
            }
        }

        //clicked on territory
        public void DoButtonStuff(int btnNum)
        {

            if(started)
            {
                Territory terr = territories[btnNum];
                Button btn = btns[btnNum];

                if (currentPlayer.GetStatus().Equals("picking"))
                {
                    if (terr.GetOwner() == null)
                    {
                        terr.ChangeOwner(currentPlayer);
                        terr.AddTroops(1);
                        currentPlayer.AddTerr(terr);
                        btn.BackColor = currentPlayer.GetColor();
                        btn.Text = "" + terr.GetTroopNum();
                        currentPlayer.SetStatus("idle");

                        if (AllOwned())
                        {
                            btnUni.Text = "Drafting...";

                            playerIndex = 0;
                            currentPlayer = players[playerIndex];
                            btnUni.BackColor = currentPlayer.GetColor();
                            currentPlayer.Draft();
                        }
                        else
                        {
                            if (playerIndex < players.Length - 1)
                                playerIndex++;
                            else
                                playerIndex = 0;

                            currentPlayer = players[playerIndex];
                            btnUni.BackColor = currentPlayer.GetColor();
                            currentPlayer.Pick();
                        }
                    }
                }
                else if (currentPlayer.GetStatus().Equals("drafting") && terr.GetOwner().Equals(currentPlayer))
                {
                    currentPlayer.TakeDraftTroop(1);
                    terr.AddTroops(1);
                    btn.Text = "" + terr.GetTroopNum();

                    if (currentPlayer.GetDraftTroopNum() == 0)
                    {
                        currentPlayer.SetStatus("idle");

                        btnUni.Text = "Fortify";

                        currentPlayer.AttackSelect();
                    }
                }
                else if ((currentPlayer.GetStatus().Equals("choosing attacker") || 
                          currentPlayer.GetStatus().Equals("choosing defender")) &&
                          terr.GetOwner().Equals(currentPlayer))
                {
                    if (terr.GetTroopNum() < 2)
                        MessageBox.Show("Insufficient troops to attack from there.");
                    else if (terr.Equals(currentPlayer.GetAttacker()))
                    {

                        currentPlayer.GetAttacker().GetBtn().BackColor = currentPlayer.GetColor();
                        currentPlayer.SetAttacker(null);
                        currentPlayer.SetStatus("choosing attacker");
                    }
                    else
                    {
                        currentPlayer.SetAttacker(terr);
                        currentPlayer.SetStatus("choosing defender");
                    }
                }
                else if (currentPlayer.GetStatus().Equals("choosing defender") && !terr.GetOwner().Equals(currentPlayer))
                {

                    if(currentPlayer.GetAttacker().IsNeighbor(terr))
                    {
                        currentPlayer.SetDefender(terr);
                        currentPlayer.Attack();

                        CheckForWinner();
                    }
                    else
                    {

                        currentPlayer.GetAttacker().GetBtn().BackColor = currentPlayer.GetColor();
                        currentPlayer.SetAttacker(null);
                        currentPlayer.SetStatus("choosing attacker");
                    }
                }
                else if (currentPlayer.GetStatus().Equals("relocating troops"))
                {
                    if (!terr.Equals(currentPlayer.GetAttacker()) && !terr.Equals(currentPlayer.GetDefender()))
                    {
                        currentPlayer.AttackSelect();
                    }
                    else if (terr.Equals(currentPlayer.GetDefender()))
                    {
                        if (currentPlayer.GetAttacker().GetTroopNum() == 1)
                        {
                            currentPlayer.AttackSelect();
                            currentPlayer.SetAttacker(terr);
                            currentPlayer.SetStatus("choosing defender");
                        }
                        else
                        {
                            currentPlayer.GetAttacker().SubtractTroops(1);
                            terr.AddTroops(1);
                            currentPlayer.GetAttacker().GetBtn().Text = "" + currentPlayer.GetAttacker().GetTroopNum();
                            terr.GetBtn().Text = "" + terr.GetTroopNum();
                        }
                    }
                    else if (terr.Equals(currentPlayer.GetAttacker()))
                    {
                        if (currentPlayer.GetDefender().GetTroopNum() == currentPlayer.GetAttackedWith())
                        {

                        }
                        else if (currentPlayer.GetDefender().GetTroopNum() > currentPlayer.GetAttackedWith())
                        {
                            currentPlayer.GetDefender().SubtractTroops(1);
                            terr.AddTroops(1);
                            currentPlayer.GetDefender().GetBtn().Text = "" + currentPlayer.GetDefender().GetTroopNum();
                            terr.GetBtn().Text = "" + terr.GetTroopNum();
                        }
                    }
                }
                else if (currentPlayer.GetStatus().Equals("fortifying, picking source") && terr.GetOwner().Equals(currentPlayer) && terr.GetTroopNum() > 1) ///
                {
                    if (terr.Equals(currentPlayer.GetFortSource()))
                    {
                        currentPlayer.GetFortSource().GetBtn().BackColor = currentPlayer.GetColor();
                        currentPlayer.SetFortSource(null);
                        currentPlayer.SetStatus("fortifying, picking source");
                    }
                    else
                    {
                        currentPlayer.SetFortSource(terr);
                        currentPlayer.SetStatus("fortifying, picking target");
                    }

                }
                else if (currentPlayer.GetStatus().Equals("fortifying, picking target"))
                {
                    if (terr.GetOwner().Equals(currentPlayer))
                    {
                        currentPlayer.SetFortTarget(terr);
                        currentPlayer.Transfer();
                        currentPlayer.GetFortSource().SubtractTroops(1);
                        currentPlayer.GetFortTarget().AddTroops(1);
                    }
                    else
                    {

                        currentPlayer.GetFortSource().GetBtn().BackColor = currentPlayer.GetColor();
                        currentPlayer.SetFortSource(null);
                        currentPlayer.SetStatus("fortifying, picking source");
                    }
                }
                else if (currentPlayer.GetStatus().Equals("transferring"))
                {
                    if (!terr.Equals(currentPlayer.GetFortSource()) && !terr.Equals(currentPlayer.GetFortTarget()))
                    {
                        currentPlayer.SetStatus("idle");
                        currentPlayer.GetFortSource().GetBtn().BackColor = currentPlayer.GetColor();
                        currentPlayer.GetFortTarget().GetBtn().BackColor = currentPlayer.GetColor();
                        currentPlayer.GetFortTarget().GetBtn().ForeColor = Color.Black;
                        NextPlayer();
                    }
                    else if (terr.Equals(currentPlayer.GetFortSource()) && currentPlayer.GetFortTarget().GetTroopNum() > 1)
                    {
                        terr.AddTroops(1);
                        currentPlayer.GetFortTarget().SubtractTroops(1);
                    }
                    else if (terr.Equals(currentPlayer.GetFortTarget()) && currentPlayer.GetFortSource().GetTroopNum() > 1)
                    {
                        terr.AddTroops(1);
                        currentPlayer.GetFortSource().SubtractTroops(1);
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

        private void button0_Click(object sender, EventArgs e)
        {
            DoButtonStuff(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoButtonStuff(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoButtonStuff(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DoButtonStuff(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DoButtonStuff(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DoButtonStuff(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DoButtonStuff(6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DoButtonStuff(7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DoButtonStuff(8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DoButtonStuff(9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DoButtonStuff(10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DoButtonStuff(11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DoButtonStuff(12);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DoButtonStuff(13);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DoButtonStuff(14);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DoButtonStuff(15);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            DoButtonStuff(16);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            DoButtonStuff(17);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            DoButtonStuff(18);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DoButtonStuff(19);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            DoButtonStuff(20);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            DoButtonStuff(21);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            DoButtonStuff(22);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            DoButtonStuff(23);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            DoButtonStuff(24);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            DoButtonStuff(25);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            DoButtonStuff(26);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            DoButtonStuff(27);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            DoButtonStuff(28);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            DoButtonStuff(29);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            DoButtonStuff(30);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            DoButtonStuff(31);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            DoButtonStuff(32);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            DoButtonStuff(33);

        }

        private void button34_Click(object sender, EventArgs e)
        {
            DoButtonStuff(34);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            DoButtonStuff(35);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            DoButtonStuff(36);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            DoButtonStuff(37);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            DoButtonStuff(38);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            DoButtonStuff(39);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            DoButtonStuff(40);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            DoButtonStuff(41);
        }



    }

    public class Game
    {
        /*
        public static int[] nAmerTerrs;
        public static int[] sAmerTerrs;
        public static int[] euroTerrs;
        public static int[] africaTerrs;    //14...19
        public static int[] asiaTerrs;  //27...38
        public static int[] austTerrs;  //39,40,41,0
        
        public Game()
        {
            nAmerTerrs = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            sAmerTerrs = new int[] { 10, 11, 12, 13 };
            euroTerrs = new int[] { 20, 21, 22, 23, 24, 25, 26 };

        }
         */
     }

}
