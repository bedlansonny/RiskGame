/* Parker Bedlan
 * Risk Project
 * Started: May 11, 2016
 * 
 * Ctrl + F "///" to find Work in Progress
 * 
 * Future addtions:
 *      make the start, attack, fortify, and end turn buttons are the same button
 *      fortifying only if territories are connected
 *      card system
 *      ability to increase amount of troops moved in a click
            also add the ability to take back troops in drafting
 *      blinking when transferring troops after attacking
 *      skip fortifying step if all owned troops only have 1
 *      actually impliment the Game class somehow
 * 
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

        Button[] btns;

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

        public void NextPlayer()    ///
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
            currentPlayer.Draft();
        }

        private void btnClear_Click(object sender, EventArgs e) ////experimental
        {
            tb.Text = "";
        }

        private void btnUni_Click(object sender, EventArgs e)
        {
            if (btnUni.Text.Equals("Start Game"))
            {
                if (!started)
                {
                    started = true;
                    tb.Text += "Starting the game..." + Environment.NewLine;

                    btns = new Button[] { btn0, btn1, btn2, btn3, btn4, btn5 };     //needs to change based on amt of terr btns
                    Territory.btns = btns;

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
                    Game game = new Game(players);      ///wut

                    currentPlayer = players[playerIndex];
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
                        currentPlayer.GetAttacker().GetBtn().BackColor = currentPlayer.GetColor(); ////
                    currentPlayer.SetAttacker(null);
                    currentPlayer.SetDefender(null);
                    currentPlayer.SetStatus("idle");

                    btnUni.Text = "End Turn";

                    currentPlayer.Fortify();
                }
                else  ///just a test
                {
                    tb.Text += "ERROR:" + currentPlayer.GetStatus() + Environment.NewLine;
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
                else   ///just a test
                {
                    tb.Text += "ERROR:" + currentPlayer.GetStatus() + Environment.NewLine;
                }
            }
        }

        //start button
        private void btnStart_Click(object sender, EventArgs e)
        {
            if(!started)
            {
                started = true;
                tb.Text += "Starting the game..." + Environment.NewLine;

                btns = new Button[] { btn0, btn1, btn2, btn3, btn4, btn5 };     //needs to change based on amt of terr btns
                Territory.btns = btns;

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
                Game game = new Game(players);      ///wut

                currentPlayer = players[playerIndex];
                currentPlayer.Pick();

            }
        }

        private void btnStopAttacking_Click(object sender, EventArgs e)
        {
            if (currentPlayer.GetStatus().Equals("choosing attacker") ||
                currentPlayer.GetStatus().Equals("choosing defender") ||
                currentPlayer.GetStatus().Equals("attacking") ||
                currentPlayer.GetStatus().Equals("relocating troops"))
            {
                if (currentPlayer.GetAttacker() != null)
                    currentPlayer.GetAttacker().GetBtn().BackColor = currentPlayer.GetColor(); ////
                currentPlayer.SetAttacker(null);
                currentPlayer.SetDefender(null);
                currentPlayer.SetStatus("idle");
                currentPlayer.Fortify();
            }
            else  ///just a test
            {
                tb.Text += "ERROR:" + currentPlayer.GetStatus() + Environment.NewLine;
            }
        }

        private void btnEndTurn_Click(object sender, EventArgs e)
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

                NextPlayer();

            }
            else   ///just a test
            {
                tb.Text += "ERROR:" + currentPlayer.GetStatus() + Environment.NewLine;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            started = true;

            btns = new Button[] { btnTest0, btnTest1 };
            Territory.btns = btns;

            Player p1 = new Player("Joe", Color.DarkViolet, tb);
            Player p2 = new Player("Bob", Color.DarkTurquoise, tb);
            players = new Player[] { p1, p2 };
;
            Territory test0 = new Territory(0, null, new int[] { 1 });
            Territory test1 = new Territory(1, null, new int[] { 0 });
            territories = new Territory[] { test0, test1 };

            currentPlayer = players[playerIndex];
            currentPlayer.Pick();


        }

        //clicked on territory
        public void DoButtonStuff(int btnNum)
        {

            tb.Text += currentPlayer.GetName() + ": " + currentPlayer.GetStatus() + Environment.NewLine;     ///just a test

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
                else if (currentPlayer.GetStatus().Equals("drafting") && terr.GetOwner().Equals(currentPlayer))
                {
                    currentPlayer.TakeDraftTroop(1);
                    terr.AddTroops(1);
                    btn.Text = "" + terr.GetTroopNum();

                    if (currentPlayer.GetDraftTroopNum() == 0)
                    {
                        currentPlayer.SetStatus("idle");

                        tb.Text += "Attacking phase." + Environment.NewLine;

                        ///
                        btnUni.Text = "Fortify";

                        currentPlayer.AttackSelect();
                    }
                }
                else if ((currentPlayer.GetStatus().Equals("choosing attacker") || 
                          currentPlayer.GetStatus().Equals("choosing defender")) &&
                          terr.GetOwner().Equals(currentPlayer))
                {
                    if (terr.GetTroopNum() < 2)
                        tb.Text += "Insufficient troops to attack from there." + Environment.NewLine;
                    else if(terr.Equals(currentPlayer.GetAttacker()))
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

                        CheckForWinner();  ///////////////
                    }
                    else
                    {

                        currentPlayer.GetAttacker().GetBtn().BackColor = currentPlayer.GetColor();
                        currentPlayer.SetAttacker(null);
                        currentPlayer.SetStatus("choosing attacker");
                    }
                }
                else if (currentPlayer.GetStatus().Equals("relocating troops")) ///will be tested later**************
                {
                    if (!terr.Equals(currentPlayer.GetAttacker()) && !terr.Equals(currentPlayer.GetDefender()))
                    {
                        currentPlayer.AttackSelect();
                    }
                    else if (terr.Equals(currentPlayer.GetDefender()))
                    {
                        if (currentPlayer.GetAttacker().GetTroopNum() == 1) ////needs to jump to turning white and whatnot
                        {
                            tb.Text += "potato" + Environment.NewLine;    ///experimental
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
                else if (currentPlayer.GetStatus().Equals("transferring"))  ///
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








        private void btnTest0_Click(object sender, EventArgs e)
        {
            DoButtonStuff(0);
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            DoButtonStuff(1);
        }

        //btn event handlers, not important
        private void btn0_Click(object sender, EventArgs e)
        {
            DoButtonStuff(0);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            DoButtonStuff(1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            DoButtonStuff(2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            DoButtonStuff(3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            DoButtonStuff(4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            DoButtonStuff(5);
        }

    }

    public class Game
    {
        public static Player[] players;

        public Game(Player[] players2)
        {
            players = players2;
        }
    }
}
