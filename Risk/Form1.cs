/* Parker Bedlan
 * Risk Project
 * Started: May 11, 2016
 * 
 * Next Phase: Drafting
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

        Territory[] territories;
        Player[] players;
        Player currentPlayer;

        int playerIndex;
        TextBox tb;

        bool started;

        private void Form1_Load(object sender, EventArgs e)     //essentially the main method, avoid how much you put in
        {
            territories = new Territory[0];
            players = new Player[0];
            currentPlayer = new Player();
            playerIndex = 0;
            tb = textBox1;
            started = false;
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

        private void button3_Click(object sender, EventArgs e)
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

            //Picking
            currentPlayer = players[playerIndex];
            currentPlayer.Pick();

            //Drafting

            
        }

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
                            tb.Text += "Next phase." + Environment.NewLine;
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
            }

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

    public class Territory
    {
        //String name;
        private Button btn;
        private int iDnum;
        private Player owner;
        private int[] neighbors;    //I may want to make it Territory[] rather than by iDnum
        private int troopNum;

        //public Territory() { }

        public Territory( /*Button btn, */ int iDnum, Player owner, int[] neighbors)
        {
            //this.btn = btn;   //not really needed
            this.iDnum = iDnum;
            this.owner = owner;
            this.neighbors = neighbors;
            this.troopNum = 0;
        }

        public void ChangeOwner(Player newOwner)
        {
            this.owner = newOwner;
        }

        public void AddTroops(int additionalTroops)
        {
            this.troopNum = additionalTroops;
        }

        public void SetNeighbors(int[] enteredNeighbors)
        {
            this.neighbors = enteredNeighbors;
        }

        public Player GetOwner() { return owner; }
        public int GetTroopNum() { return troopNum; }

    }

    public class Player
    {
        private ArrayList ownedTerrs;
        private String name;
        private int troopNum;

        private String status;
        Color color;

        private TextBox tb;

        public Player() { }

        public Player(String name, Color color, TextBox tb)
        {
            this.name = name;
            this.color = color;
            this.ownedTerrs = new ArrayList();
            this.status = "idle";
            this.tb = tb;
        }

        public void Pick()
        {
            status = "picking";
            tb.Text += name + " is picking a territory." + Environment.NewLine;
        }

        public void Draft()///Work in progress
        {
            int draftTroopNum = (int)(GetTroopTotal() / 3);
            if (draftTroopNum < 3)
                draftTroopNum = 3;

            //for | PlaceTroop()

        }

        public void PlaceTroop()///Work in progress
        {

        }

        public void AddTerr(Territory newTerr)
        {
            ownedTerrs.Add(newTerr);
        }

        public void ChangeStatus(String newStatus) 
        { 
            this.status = newStatus;
        }

        public int GetTroopTotal()
        {
            int output = 0;
            foreach(Territory terr in ownedTerrs)
            {
                output += terr.GetTroopNum();
            }
            return output;
        }

        public String GetStatus() { return status; }
        public Color GetColor() { return color; }
    }

    public class Game
    {
        private Territory[] territories;
        private Player[] players;

        public void CreateTerrs()
        {

        }
    }
}
