using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class Plate
    {
        public bool is_checked { get; set; }
        public int color { get; set; }

        public Plate()
        {
            this.is_checked = false;
            this.color = -1;
        }

        public Plate(int _color)
        {
            this.is_checked = false;
            this.color = _color;
        }
    }
}
