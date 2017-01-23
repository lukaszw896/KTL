using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class Scenario
    {
        SequencesMemory current_sequences { get; set; }
        List<Plate> game_state { get; set; }
        int alfa { get; set; }
        int beta { get; set; }
        public List<Scenario> children { get; set; }
        int current_depth { get; set; }
        int max_depth { get; set; }
        int all_colors { get; set; }
        int random_colors { get; set; }
        int free_plates { get; set; }
        public int choosen_number { get; set; }
        List<int> random_choosen_colors { get; set; }

        public Scenario()
        {
            this.game_state = new List<Plate>();
            this.alfa = 0;
            this.beta = 0;
            this.children = new List<Scenario>();
            this.current_depth = -1;
            this.max_depth = -1;
            this.current_sequences = new SequencesMemory(1);
            this.all_colors = -1;
            this.random_colors = -1;
            this.free_plates = -1;
            this.choosen_number = -1;
            this.random_choosen_colors = new List<int>();

        }

        public Scenario(int _alfa, int _beta, List<Plate> _game_state, int _current_depth, int _max_depth, SequencesMemory _memory, int _all_colors, int _random_colors, int _free_plates)
        {
            this.game_state = _game_state;
            this.alfa = _alfa;
            this.beta = _beta;
            this.children = new List<Scenario>();
            this.current_depth = _current_depth;
            this.max_depth = _max_depth;
            this.current_sequences = new SequencesMemory(1);
            this.current_sequences = _memory;
            this.all_colors = _all_colors;
            this.random_colors = _random_colors;
            this.free_plates = _free_plates;
        }
        public void MakeMove(int selected_number, List<int> random_colors, List<List<int>> all_posssible_colors)
        {
            this.choosen_number = selected_number;
            if (current_depth <= max_depth)
            {
                if (this.children.Count < 1)
                {
                    for (int i = 0; i < random_colors.Count; i++)
                    {
                        List<Plate> temp_game_state = this.game_state;
                        temp_game_state[selected_number].is_checked = true;
                        temp_game_state[selected_number].color = random_colors[i];
                        SequencesMemory temp_memory = new SequencesMemory(this.all_colors);
                        for (int w = 0; w < this.all_colors; w++)
                        {
                            List<Sequence> tmpseqlist = new List<Sequence>();
                            for(int ww = 0 ; ww < this.current_sequences.sequences[w].Count ; ww ++)
                                tmpseqlist.Add(this.current_sequences.sequences[w][ww]);
                            temp_memory.sequences[w] = tmpseqlist;

                        }
                        int answ = temp_memory.Update(selected_number, random_colors[i]);
                        //calc alfa, beta
                        int tmp_alfa = this.alfa, tmp_beta = this.beta;
                        if (answ < 0)
                            tmp_alfa++;
                        else
                            tmp_beta++;

                        if (tmp_alfa <= tmp_beta)
                        {

                            Scenario temp_scenario = new Scenario(tmp_alfa, tmp_beta, temp_game_state, this.current_depth++, this.max_depth, temp_memory, this.all_colors, this.random_colors, this.free_plates - 1);
                            temp_scenario.random_choosen_colors = random_colors;
                            this.children.Add(temp_scenario);

                            int counter = 0;
                            for (int p = 0; p < this.free_plates; p++)
                            {
                                for (int j = counter; j < game_state.Count; j++)
                                    if (this.game_state[j].is_checked == false)
                                    {
                                        int new_selected_number = j;
                                        counter = j;

                                        /*
                                         *  dla tego new_selected_numer znajduje WSZYSTKIE mozliwe kombinacje losowych kolorow ?
                                         *  czy po prostu dla kazdego koloru...
                                         *  List<int> tmp_rnd_col = SequenceHelper.randColors(this.all_colors, this.random_colors);
                                         */
                                        for (int c = 0; c < all_posssible_colors.Count ; c++)
                                        {
                                            this.children[this.children.Count - 1].MakeMove(new_selected_number, all_posssible_colors[c], all_posssible_colors);
                                        }
                                    }
                            }
                        }
                    }
                }
                else
                {
                    for (int q = 0; q < this.children.Count; q++)
                    {
                        this.children[q].MakeMove(this.children[q].choosen_number, this.children[q].random_choosen_colors, all_posssible_colors);
                    }
                }
            }
            else
            {
                /* zwrot  wyniku
                 * jade po drzewie. i wybieram takie gdzie jest najwieksze beta - alfa 
                 */
            }
        }
    }
}
