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
        private Territory attacker;
        private Territory defender;
        int attackedWith;
        private Territory fortSource;
        private Territory fortTarget;
        private bool isDead;

        static TextBox tb;

        public Player() { }

        public Player(String name, Color color)
        {
            this.name = name;
            this.color = color;
            this.ownedTerrs = new ArrayList();
            this.status = "idle";
            this.isDead = false;
        }

        public static void SetTextBox(TextBox tb2)
        {
            tb = tb2;
        }

        public void Pick()
        {
            status = "picking";
        }

        public void Draft()
        {

            draftTroopNum = (int)(GetTroopTotal() / 3);         ///you'll have to incorporate region ownership bonuses
            if (draftTroopNum < 3)
                draftTroopNum = 3;

            int[] iDarr = new int[ownedTerrs.Count];
            Territory[] ownedTerrArr = (Territory[]) ownedTerrs.ToArray(typeof(Territory));
            for (int i = 0; i < ownedTerrs.Count; i++)
            {
                iDarr[i] = ownedTerrArr[i].GetiDNum();
            }

            ////call bools of territories to add bonuses
            if (ownsNorthAmerica(iDarr))
                draftTroopNum += 5;
            if (ownsSouthAmerica(iDarr))
                draftTroopNum += 2;
            if (ownsEurope(iDarr))
                draftTroopNum += 5;
            if (ownsAfrica(iDarr))
                draftTroopNum += 3;
            if (ownsAsia(iDarr))
                draftTroopNum += 7;
            if (ownsAustralia(iDarr))
                draftTroopNum += 2;

            tb.Text = "" + draftTroopNum;

            status = "drafting";

        }

        public void AttackSelect()
        {
            SetStatus("choosing attacker");
        }

        public void Attack()            ///may be a little sketchy, just removed die class
        {
            status = "attacking";

            int attackerDice = 0;
            int defenderDice = 0;
            Random rnd = new Random();

            //while loop




            
            while(attacker.GetTroopNum() > 1 && defender.GetTroopNum() > 0)
            {
                if (attacker.GetTroopNum() == 2)
                    attackerDice = 1;
                else if (attacker.GetTroopNum() == 3)
                    attackerDice = 2;
                else if (attacker.GetTroopNum() >= 4)
                    attackerDice = 3;

                if (defender.GetTroopNum() == 1)
                    defenderDice = 1;
                else if (defender.GetTroopNum() >= 2)
                    defenderDice = 2;

                if (attackerDice == 1 && defenderDice == 1)
                {
                    if (rnd.Next(1, 101) <= 42)
                        defender.SubtractTroops(1);
                    else
                        attacker.SubtractTroops(1);
                }
                else if (attackerDice == 2 && defenderDice == 1)
                {
                    if (rnd.Next(1, 101) <= 42)
                        attacker.SubtractTroops(1);
                    else
                        defender.SubtractTroops(1);
                }
                else if (attackerDice == 3 && defenderDice == 1)
                {
                    if (rnd.Next(1, 101) <= 66)
                        defender.SubtractTroops(1);
                    else
                        attacker.SubtractTroops(1);
                }
                else if (attackerDice == 1 && defenderDice == 2)
                {
                    if (rnd.Next(1, 5) == 1)
                        defender.SubtractTroops(1);
                    else
                        attacker.SubtractTroops(1);
                }
                else if (attackerDice == 2 && defenderDice == 2)
                {
                    int randNum = rnd.Next(1, 101);

                    if (randNum <= 23)
                        defender.SubtractTroops(2);
                    else if (randNum <= 68)
                        attacker.SubtractTroops(2);
                    else
                    {
                        attacker.SubtractTroops(1);
                        defender.SubtractTroops(1);
                    }
                }
                else if (attackerDice == 3 && defenderDice == 2)
                {
                    int randNum = rnd.Next(1, 101);

                    if (randNum <= 37)
                        defender.SubtractTroops(2);
                    else if (randNum <= 66)
                        attacker.SubtractTroops(2);
                    else
                    {
                        attacker.SubtractTroops(1);
                        defender.SubtractTroops(1);
                    }
                }
                /*
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
                */

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

                int transferAmt = attacker.GetTroopNum() - 1;
                attacker.SubtractTroops(transferAmt);
                defender.AddTroops(transferAmt);

                if (temp.ownedTerrs.Count == 0)
                    temp.SetIsDead(true);

                attackedWith = attackerDice;
                RelocateTroops();

            }
            else
            {
                AttackSelect();
            }

             
        }

        //let player move troops back to attacker as a option
        //except amount last attacked with
        public void RelocateTroops()        ///It'd make it more convenient if they blinked while this happened
        {
            status = "relocating troops";
        }

        public void Fortify()
        {
            status = "fortifying, picking source";
        }

        public void Transfer()
        {
            status = "transferring";
        }

        public void TakeDraftTroop(int amt)
        {
            draftTroopNum -= amt;
            tb.Text = "" + draftTroopNum;
        }

        public void AddTerr(Territory newTerr)
        {
            ownedTerrs.Add(newTerr);
        }

        public void RemoveTerr(Territory remTerr)
        {
            ownedTerrs.Remove(remTerr);
        }

        public bool ownsNorthAmerica(int[] iDarr)   ///
        {
            for(int i = 1; i <=9; i++)
            {
                if (!iDarr.Contains(i))
                    return false;
            }
            return true;
        }

        public bool ownsSouthAmerica(int[] iDarr)
        {
            for (int i = 10; i <= 13; i++)
            {
                if (!iDarr.Contains(i))
                    return false;
            }
            return true;
        }

        public bool ownsEurope(int[] iDarr)
        {
            for (int i = 20; i <= 26; i++)
            {
                if (!iDarr.Contains(i))
                    return false;
            }
            return true;
        }

        public bool ownsAfrica(int[] iDarr)
        {
            for (int i = 14; i <= 19; i++)
            {
                if (!iDarr.Contains(i))
                    return false;
            }
            return true;
        }

        public bool ownsAsia(int[] iDarr)
        {
            for (int i = 27; i <= 38; i++)
            {
                if (!iDarr.Contains(i))
                    return false;
            }
            return true;
        }

        public bool ownsAustralia(int[] iDarr)
        {
            for (int i = 39; i <= 41; i++)
            {
                if (!iDarr.Contains(i))
                    return false;
            }

            if (!iDarr.Contains(0))
                return false;

            return true;
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
