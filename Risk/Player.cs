using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Risk
{
    public class Player
    {
        private ArrayList ownedTerrs;
        private String name;
        private int draftTroopNum;
        private String status;
        Color color;
        private TextBox tb;
        private Territory attacker;
        private Territory defender;

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

        public void Draft()
        {
            draftTroopNum = (int)(GetTroopTotal() / 3);         ///you'll have to incorporate region ownership bonuses
            if (draftTroopNum < 3)
                draftTroopNum = 3;

            status = "drafting";
            tb.Text += name + " is drafting his troops." + Environment.NewLine;

        }

        public void AttackSelect()  ///
        {
            status = "choosing attacker";
            tb.Text += name + " is now attacking." + Environment.NewLine;
        }

        public void Attack()    ///uses state variables attacker and defender
        {
            tb.Text += name + "'s Terr " + attacker.GetiDNum() + " is attacking " + defender.GetOwner().GetName() + "'s Terr " + defender.GetiDNum() + "." + Environment.NewLine;

        }

        public void Fortify()   ///After I finish attack
        {
            status = "fortifying";
            tb.Text += name + " is now fortifying." + Environment.NewLine;
        }

        public void TakeDraftTroop(int amt)
        {
            draftTroopNum -= amt;
        }

        public void AddTerr(Territory newTerr)
        {
            ownedTerrs.Add(newTerr);
        }

        public void RemoteTerr(Territory remTerr)
        {
            ownedTerrs.Remove(remTerr);
        }

        public int GetTroopTotal()
        {
            int output = 0;
            foreach (Territory terr in ownedTerrs)
            {
                output += terr.GetTroopNum();
            }
            return output;
        }

        public void SetStatus(String newStatus) { this.status = newStatus; }
        public void SetAttacker(Territory attacker)
        {
            if(this.attacker != null)
                this.attacker.GetBtn().BackColor = color;
            this.attacker = attacker;
            this.attacker.GetBtn().BackColor = Color.White;
            
        }
        public void SetDefender(Territory defender) { this.defender = defender; }

        public String GetStatus() { return status; }
        public Color GetColor() { return color; }
        public int GetDraftTroopNum() { return draftTroopNum; }
        public String GetName() { return name; }
        public Territory GetAttacker() { return attacker; }
        public Territory GetDefender() { return defender; }
    }
}
