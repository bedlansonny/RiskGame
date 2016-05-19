/* Parker Bedlan
 * Risk Project
 * Started: May 11, 2016
 * Current issue:
 * Idk how I would transition from player to player in picking effectively. Just switching the statuses leads to cycling
 * through all the way to the end. I need to find a way, either in DoButtonStuff or button3_Click that will allow for 
 * some trigger to move onto the next player's picking in the process.
 * 
 * 5/15/16: I feel that recursion would be the best solution to the button-triggered pause.
 *          Sketch it out on a whiteboard with the methods: DoButtonStuff, Pick, StartButton[button3], and Testing Method[a new one if neccessary]
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

        private void Form1_Load(object sender, EventArgs e)     //essentially the main method, avoid how much you put in
        {
            territories = new Territory[0];
            players = new Player[0];
            currentPlayer = new Player();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "sup bro";  //just a test

            Territory brah = new Territory(btn0, 0, null, new int[] { 1 });
            Territory bruh = new Territory(btn1, 1, null, new int[] { 0, 2 });
            Territory bro = new Territory(btn2, 2, null, new int[] { 1 });
            territories = new Territory[] { brah, bruh, bro };

            Player player1 = new Player("Parker", Color.Red);
            Player player2 = new Player("Billy", Color.Blue);
            players = new Player[] { player1, player2 };

            /*for (int i = 0; i < players.Length; i++)
            {
                currentPlayer = players[i];

                do
                {
                    currentPlayer.Pick();
                } while (currentPlayer.GetStatus().Equals("picking"));    ///this do-while loop messes stuff up
            }*/

            currentPlayer = players[0];
            currentPlayer.Pick();
            //////////how do I end Pick()??????????????
            
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            doButtonStuff(0);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            doButtonStuff(1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            doButtonStuff(2);
        }

        public void doButtonStuff(int btnNum)
        {
            Territory terr = territories[btnNum];

            if (currentPlayer.GetStatus().Equals("picking"))
            {
                if (terr.GetOwner() == null)  //player.pick()
                {
                    terr.ChangeOwner(currentPlayer);
                    terr.AddTroops(1);
                    currentPlayer.AddTerr(terr);
                    btn0.BackColor = currentPlayer.GetColor();
                    btn0.Text = "" + terr.GetTroopNum();
                    currentPlayer.ChangeStatus("idle"); /////////ends the pick method?? (maybe?)
                }
            }
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

        public Territory(Button btn, int iDnum, Player owner, int[] neighbors)
        {
            this.btn = btn;
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

        public Player() { }

        public Player(String name, Color color)
        {
            this.name = name;
            this.color = color;
            this.ownedTerrs = new ArrayList();
            this.status = "idle";
        }

        public void Pick()
        {
            status = "picking";
            //display a message saying name + " is now picking their territory."
            /////////////////////////still kinda iffy
        }

        public void Draft(int troopNum)
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
