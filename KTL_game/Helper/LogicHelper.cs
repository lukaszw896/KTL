using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class LogicHelper
    {
        public int game_length { get; set; }
        public int all_colors { get; set; }
        public int random_colors { get; set; }
        public int seq_length { get; set; }
        public int deep_search { get; set; }
        public List<Plate> game_state { get; set; }
        SequencesMemory sequences { get; set; }
        public int free_plates { get; set; }
        Scenario scenariusz { get; set; }
        public bool first_time { get; set; }
        public List<List<int>> all_posssible_colors{ get; set; }


        public LogicHelper()
        {
            this.game_length = -1;
            this.all_colors = -1;
            this.random_colors = -1;
            this.seq_length = -1;
            this.deep_search = -1;
            this.game_state = new List<Plate>();
            this.sequences = new SequencesMemory(1);
            this.free_plates = -1;
            this.scenariusz = new Scenario();
            this.first_time = true;
            this.all_posssible_colors = new List<List<int>>();
        }

        public LogicHelper(int _game_length, int _all_colors, int _random_colors, int _seq_length, int _deep_search, int _free_plates)
        {
            this.game_length = -1;
            this.all_colors = -1;
            this.random_colors = -1;
            this.seq_length = -1;
            this.deep_search = -1;
            this.game_state = new List<Plate>();
            this.free_plates = _free_plates;
            this.first_time = true;
            this.sequences = new SequencesMemory(_all_colors);
            this.all_posssible_colors = SequenceHelper.GenerateColors(this.all_colors, this.random_colors);
        }
        public void prepareMemory()
        {
            this.sequences = new SequencesMemory(this.all_colors);
        }

        public int chooseColor (int selected_number, List<int> random_colors)
        {
            int answ_color = -1;
            if (this.first_time == true)
            {
                this.scenariusz = new Scenario(0, 0, this.game_state, 1, this.deep_search, this.sequences, this.all_colors, this.random_colors, this.free_plates);
                this.scenariusz.MakeMove(selected_number, random_colors, this.all_posssible_colors);
                this.first_time = false;
            }
            else
            {
                Scenario tmp_scenario = new Scenario();
                for (int i = 0; i < this.scenariusz.children.Count(); i++)
                {
                    if (this.scenariusz.children[i].choosen_number == selected_number)
                        tmp_scenario = this.scenariusz.children[i];
                }
                this.scenariusz = new Scenario();
                this.scenariusz = tmp_scenario;
                this.scenariusz.MakeMove(selected_number, random_colors, this.all_posssible_colors);
            }
            return answ_color;
        }
    }
}
