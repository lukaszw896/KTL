using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class Sequence
    {
        public int first_term { get; set; }
        public int step { get; set; }
        public int curr_lengt { get; set; }

        public Sequence()
        {
            this.first_term = -1;
            this.step = -1;
            this.curr_lengt = -1;
        }
        public bool is_still_seq(int index)
        {
            if (SequenceHelper.NTerm(this.first_term,this.step,this.curr_lengt + 1) == index)
                return true;
            else 
                return false;
        }
    }
}
