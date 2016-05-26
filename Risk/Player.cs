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
        int attackedWith;
        private Territory fortSource;
        private Territory fortTarget;
        private bool isDead;

        public Player() { }

        public Player(String name, Color color, TextBox tb)
        {
            this.name = name;
            this.color = color;
            this.ownedTerrs = new ArrayList();
            this.status = "idle";
            this.tb = tb;
            this.isDead = false;
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

        public void AttackSelect()
        {
            SetStatus("choosing attacker");
            tb.Text += name + " is now attacking." + Environment.NewLine;
        }

        public void Attack()    ///gonna be extremely difficult equations, lots of mapping out, testing, etc.
        {
            status = "attacking";
            tb.Text += name + "'s Terr " + attacker.GetiDNum() + " is attacking " + defender.GetOwner().GetName() + "'s Terr " + defender.GetiDNum() + "." + Environment.NewLine;

            while(attacker.GetTroopNum() > 1 && defender.GetTroopNum() > 0)
            {
                if (attacker.GetTroopNum() == 2)
                    attacker.SetDiceNum(1);
                else if (attacker.GetTroopNum() == 3)
                    attacker.SetDiceNum(2);
                else if (attacker.GetTroopNum() >= 4)
                    attacker.SetDiceNum(3);

                if (defender.GetTroopNum() == 1)
                    defender.SetDiceNum(1);
                else if (defender.GetTroopNum() >= 2)
                    defender.SetDiceNum(2);

                foreach (Die die in attacker.GetDice())
                    die.Roll();
                foreach (Die die in defender.GetDice())
                    die.Roll();

                //put highest rolls at arr beginning for both with sorting algorithm
                //sorting algorithm (attacker)
                bool sorted;
                int p = 1;
                do
                {
                    sorted = true;
                    for (int q = 0; q < attacker.GetDice().Length - p; q++)
                    {
                        if (attacker.GetDice()[q].GetValue() < attacker.GetDice()[q + 1].GetValue())
                        {
                            int temp = attacker.GetDice()[q].GetValue();
                            attacker.GetDice()[q].SetValue(attacker.GetDice()[q + 1].GetValue());
                            attacker.GetDice()[q + 1].SetValue(temp);
                            sorted = false;
                        }
                        p++;
                    }
                } while (!sorted);

                //sorting algorithm (defender)
                p = 1;
                do
                {
                    sorted = true;
                    for (int q = 0; q < defender.GetDice().Length - p; q++)
                    {
                        if (defender.GetDice()[q].GetValue() < defender.GetDice()[q + 1].GetValue())
                        {
                            int temp = defender.GetDice()[q].GetValue();
                            defender.GetDice()[q].SetValue(defender.GetDice()[q + 1].GetValue());
                            defender.GetDice()[q + 1].SetValue(temp);
                            sorted = false;
                        }
                        p++;
                    }
                } while (!sorted);


                ///This is a test
                foreach (Die die in attacker.GetDice())
                    tb.Text += die + ", ";

                tb.Text += Environment.NewLine;

                foreach (Die die in defender.GetDice())
                    tb.Text += die + ", ";

                tb.Text += Environment.NewLine;



                //compare values alongside each arr until one arr get to the end, taking off troops accordingly
                int maxLength;

                if (attacker.GetDice().Length < defender.GetDice().Length)
                    maxLength = attacker.GetDice().Length;
                else
                    maxLength = defender.GetDice().Length;

                for (int i = 0; i < maxLength; i++)
                {
                    if (defender.GetDice()[i].GetValue() < attacker.GetDice()[i].GetValue())
                        defender.SubtractTroops(1);
                    else
                        attacker.SubtractTroops(1);
                }

            }

            attacker.GetBtn().BackColor = attacker.GetOwner().GetColor();

            if(defender.GetTroopNum() < 1)
            {

                //move all troops but 1 from attacker to defender
                Player temp = defender.GetOwner();
                defender.GetOwner().RemoveTerr(defender);
                defender.ChangeOwner(attacker.GetOwner());
                defender.GetOwner().AddTerr(defender);
                defender.GetBtn().BackColor = attacker.GetOwner().GetColor();

                tb.Text += "a: " + attacker.GetTroopNum() + Environment.NewLine + "d: " + defender.GetTroopNum() + Environment.NewLine;///test stuff
                int transferAmt = attacker.GetTroopNum() - 1;
                attacker.SubtractTroops(transferAmt);
                defender.AddTroops(transferAmt);
                tb.Text += "a: " + attacker.GetTroopNum() + Environment.NewLine + "d: " + defender.GetTroopNum() + Environment.NewLine;///test stuff
                                                                                                                                       ///
                if (temp.ownedTerrs.Count == 0)
                    temp.SetIsDead(true);


                RelocateTroops();       ///may be glitchy still

                //set attacker and defender dice to null
                attacker.SetDiceNum(0);
                defender.SetDiceNum(0);

            }
            else
            {
                AttackSelect();
            }



            //To be put after finishing relocating troops:  AttackSelect();
        }

        //let player move troops back to attacker as a option
        //except amount last attacked with
        public void RelocateTroops()        ///It'd make it more convenient if they blinked while this happened
        {
            status = "relocating troops";
            attackedWith = attacker.GetDice().Length;
        }

        public void Fortify()   ///After I finish attack
        {
            status = "fortifying, picking source";
            tb.Text += name + " is now fortifying." + Environment.NewLine;
        }

        public void Transfer()
        {
            status = "transferring";
        }

        public void TakeDraftTroop(int amt)
        {
            draftTroopNum -= amt;
        }

        public void AddTerr(Territory newTerr)
        {
            ownedTerrs.Add(newTerr);
        }

        public void RemoveTerr(Territory remTerr)
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
            if(this.attacker != null)
                this.attacker.GetBtn().BackColor = Color.White;
            
        }
        public void SetDefender(Territory defender) { this.defender = defender; }

        public void SetFortSource(Territory source)
        {
            if (this.fortSource != null)
                this.fortSource.GetBtn().BackColor = color;
            this.fortSource = source;
            if (this.fortSource != null)
                this.fortSource.GetBtn().BackColor = Color.White;
        }
        public void SetFortTarget(Territory target)
        {
            if (this.fortTarget != null)
            {
                this.fortTarget.GetBtn().BackColor = color;
                this.fortTarget.GetBtn().ForeColor = Color.Black;
            }
            this.fortTarget = target;
            if (this.fortTarget != null)
            {
                this.fortTarget.GetBtn().BackColor = Color.Black;
                this.fortTarget.GetBtn().ForeColor = Color.White;
            }
        }

        public void SetIsDead(bool asdf) { this.isDead = asdf; }

        public String GetStatus() { return status; }
        public Color GetColor() { return color; }
        public int GetDraftTroopNum() { return draftTroopNum; }
        public String GetName() { return name; }
        public Territory GetAttacker() { return attacker; }
        public Territory GetDefender() { return defender; }
        public int GetAttackedWith() { return attackedWith; }
        public Territory GetFortSource() { return fortSource; }
        public Territory GetFortTarget() { return fortTarget; }
        public bool GetIsDead() { return isDead; }
    }
}
