using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk
{
    public class Territory
    {
        //String name;
        //private Button btn;       //not really needed
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
            this.troopNum += additionalTroops;
        }

        public void SetNeighbors(int[] enteredNeighbors)
        {
            this.neighbors = enteredNeighbors;
        }

        public Player GetOwner() { return owner; }
        public int GetTroopNum() { return troopNum; }

    }
}
