using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk
{
    public class Die
    {
        Random rnd;
        int value;
        
        public Die()
        {
            rnd = new Random();
            value = 0;
        }

        public void Roll()
        {
            value = rnd.Next(1, 7);
        }

        public void Reset()
        {
            value = 0;
        }

        public int GetValue()
        {
            return value;
        }

        public void SetValue(int newVal)
        {
            value = newVal;
        }
    }
}
