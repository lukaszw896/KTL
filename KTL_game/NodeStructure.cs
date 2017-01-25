using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game
{
    class NodeStructure
    {
        public NodeStructure Parent { get; set; }
        public List<NodeStructure> Children { get; set; }
        public int Score { get; set; }
        public int Color { get; set; }
        public int FieldIndex { get; set; }
        public List<int> CurrentGameBoard { get; set; }
        public NodeStructure()
        {
            Color = -1;
            this.Parent = null;
            this.Children = new List<NodeStructure>();
        }
        public NodeStructure(NodeStructure parent)
        {
            Color = - 1;
            this.Parent = parent;
            this.Children = new List<NodeStructure>();
        }
    }
}
