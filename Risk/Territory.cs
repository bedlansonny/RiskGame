using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Risk
{
    public class Territory
    {
        //String name;
        private Button btn;
        private int iDnum;
        private Player owner;
        private int[] neighbors;    //I may want to make it Territory[] rather than by iDnum
        private int troopNum;

        public static Button[] btns;    //not traditional, experimental to shorten constructor parameters

        Die[] dice;

        //public Territory() { }

        public Territory(int iDnum, int[] neighbors)
        {
            this.btn = btns[iDnum];
            this.iDnum = iDnum;
            this.owner = null;
            this.neighbors = neighbors;
            this.troopNum = 0;
        }

        public void ChangeOwner(Player newOwner)
        {
            this.owner = newOwner;
        }

        public void AddTroops(int additionalTroops)
        {
            this.troopNum += additionalTroops;
            btn.Text = "" + this.troopNum;
        }

        public void SubtractTroops(int subractedTroops)
        {
            this.troopNum -= subractedTroops;
            btn.Text = "" + this.troopNum;
        }

        public void SetNeighbors(int[] enteredNeighbors)
        {
            this.neighbors = enteredNeighbors;
        }

        public bool IsNeighbor(Territory otherTerr)
        {
            return neighbors.Contains(otherTerr.GetiDNum());
        }

        public void SetDiceNum(int diceNum)
        {
            dice = new Die[diceNum];
            for (int i = 0; i < dice.Length; i++)
                dice[i] = new Die();
        }

        public Player GetOwner() { return owner; }
        public int GetTroopNum() { return troopNum; }
        public int GetiDNum() { return iDnum; }
        public Button GetBtn() { return btn; }
        public Die[] GetDice() { return dice; }
    }
}
