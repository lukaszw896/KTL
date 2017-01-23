using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class SequencesMemory
    {
        public List<List<Sequence>> sequences { get; set; }

        public SequencesMemory(int all_colors)
        {
            this.sequences = new List<List<Sequence>>();
            for (int i = 0; i < all_colors; i++)
                this.sequences.Add(new List<Sequence>());
        }

        public int Update(int selected_number, int color)
        {
            bool add = true;
            for(int i = 0 ; i < this.sequences[color].Count ; i++)
            {
                if(sequences[color][i].step == -1)
                {
                    int tmp_step = selected_number - sequences[color][i].first_term;
                    sequences[color][i].step = tmp_step;
                    if(sequences[color][i].is_still_seq(selected_number))
                    {
                        sequences[color][i].curr_lengt++;
                        add = false;
                    }
                }
                else if (sequences[color][i].step >= 1)
                {
                    if (sequences[color][i].is_still_seq(selected_number))
                    {
                        sequences[color][i].curr_lengt++;
                        add = false;
                    }
                }                
            }
            if (add == true)
            {
                Sequence new_seq = new Sequence();
                new_seq.curr_lengt = 1;
                new_seq.step = -1;
                new_seq.first_term = selected_number;
                sequences[color].Add(new_seq);
                return 1;
            }
            else
                return -1;
        }
    }
}
