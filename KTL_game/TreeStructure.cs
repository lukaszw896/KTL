using KTL_game.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game
{
    class TreeStructure
    {
        private int ColorsCount { get; set; }
        private List<int> Gameboard { get; set; }
        private int GameLength { get; set; }
        private int SeriesLength { get; set; }
        private int TreeDepth { get; set; }
        private NodeStructure Root { get; set; }
        private bool FirstTreeMove { get; set; }
        private int NumberOfColorChildrenToGet { get; set; }
        public TreeStructure(int subColorsCount, int allColorsCount, int gameLength, int seriesLength, int treeDepth)
        {
            this.GameLength = gameLength;
            InitGameBoard();
            this.SeriesLength = seriesLength;
            this.TreeDepth = treeDepth;
            this.Root = new NodeStructure();
            this.FirstTreeMove = true;
            this.ColorsCount = allColorsCount;
            this.NumberOfColorChildrenToGet = (allColorsCount - subColorsCount) + 1;
        }

        private void InitGameBoard()
        {
            Gameboard = new List<int>();
            for (int i = 0; i < GameLength; i++)
                Gameboard.Add(-1);
        }

        public void FirstRandomInGameplay(int fieldIndex, int color)
        {
            Gameboard[fieldIndex] = color;
        }

        private void MakeFirstMove(List<int> subColorsList, int fieldIndex)
        {
            Root.FieldIndex = fieldIndex;
            Root.CurrentGameBoard = new List<int>(Gameboard);
            var scoreList = new List<int>();
            int totalScore = 0;
            foreach (int color in subColorsList)
            {
                int score = GetMoveScore(color, fieldIndex, new List<int>(Gameboard));
                totalScore += score;
                scoreList.Add(score);
            }
            int avgScore = totalScore / scoreList.Count;
            for (int i = 0; i < subColorsList.Count; i++)
            {
                if (scoreList[i] <= avgScore)
                {
                    NodeStructure childrenNode = new NodeStructure(Root)
                    {
                        CurrentGameBoard = new List<int>(Root.CurrentGameBoard),
                        FieldIndex = Root.FieldIndex,
                        Score = scoreList[i]
                    };
                    childrenNode.CurrentGameBoard[childrenNode.FieldIndex] = subColorsList[i];
                    childrenNode.Color = subColorsList[i];
                    Root.Children.Add(childrenNode);
                }
            }
            foreach(var child in Root.Children)
            {
                CreateTreeRecursive(1, child);
            }
        }

        public int GetMoveColor(List<int> subColorsList, int fieldIndex)
        {
            if (FirstTreeMove)
            {
                MakeFirstMove(subColorsList, fieldIndex);
                FirstTreeMove = false;
                int colorToReturn = ColorReturnByBranch();
                Gameboard[fieldIndex] = colorToReturn;
                return colorToReturn;
            }
            else
            {
                var tmpColorChildList = new List<NodeStructure>();
                foreach(var child in Root.Children)
                {
                    if(child.FieldIndex == fieldIndex)
                    {
                        Root = child;
                        Root.Parent = null;
                        break;
                    }
                }
                foreach(int color in subColorsList)
                {
                    foreach(var child in Root.Children)
                    {
                        if(child.Color == color)
                        {
                            tmpColorChildList.Add(child);
                        }
                    }
                }
                Root.Children = tmpColorChildList;
                Root.Parent = null;
                 AddOneLayerToTree(Root);
                int colorToReturn = ColorReturnByBranch();
                Gameboard[fieldIndex] = colorToReturn;
                return colorToReturn;
            }
        }

        private int ColorReturnByBranch()
        {
            var branchScores = new List<int>();
           
            foreach (var child in Root.Children)
            {
                int branchScore = 0;
                
                CalculateBranchScore(child, ref branchScore);
                branchScores.Add(branchScore);
            }
            int max = 0;
            int currentMaxIndex = -1;
            for (int i = 0; i < branchScores.Count; i++)
            {
                if (i == 0)
                {
                    max = branchScores[i];
                    currentMaxIndex = i;
                }
                else
                {
                    if (branchScores[i] > max)
                    {
                        max = branchScores[i];
                        currentMaxIndex = i;
                    }
                }
            }
            Root = Root.Children[currentMaxIndex];
            Root.Parent = null;
            return Root.Color;
        }

        private void CalculateBranchScore(NodeStructure node, ref int branchScore)
        {
            
            if(node.Children[0].Color == -1)
            {
                foreach(var child in node.Children)
                {
                    CalculateBranchScore(child, ref branchScore);
                }
            }
            else
            {
                for(int i=0;i<node.Children.Count;i++)
                {
                    for(int j=0;j<node.Children.Count-1;j++)
                    {
                        if(node.Children[j].Score > node.Children[j+1].Score)
                        {
                            var tmp = node.Children[j];
                            node.Children[j] = node.Children[j + 1];
                            node.Children[j + 1] = tmp;
                        }
                    }
                }
                for(int i=0;i<NumberOfColorChildrenToGet;i++)
                {
                    branchScore += node.Children[i].Score;
                }
            }
        }

        private void AddOneLayerToTree(NodeStructure node)
        {
            foreach(var child in node.Children)
            {
                if(child.Children.Count==0)
                {
                    CreateTreeRecursive(TreeDepth, child);
                }
                else
                {
                    AddOneLayerToTree(child);
                }
            }
        }

        private void CreateTreeRecursive(int currentTreeDepth, NodeStructure node)
        {
            for (int i = 0; i < GameLength; i++)
            {
                //pięterko bez danych rozdzielające tylko na ilość pozostałych pól w grze
                if(node.CurrentGameBoard[i]==-1)
                {
                    NodeStructure child = new NodeStructure(node)
                    {
                        FieldIndex = i
                    };
                    node.Children.Add(child);
                }
            }
            foreach (var child in node.Children)
            {
                for (int i = 0; i < ColorsCount; i++)
                {
                    NodeStructure grandChild = new NodeStructure(child)
                    {
                        CurrentGameBoard = new List<int>(child.Parent.CurrentGameBoard),
                        FieldIndex = child.FieldIndex
                    };
                    grandChild.Color = i;
                    grandChild.CurrentGameBoard[grandChild.FieldIndex] = i;
                    grandChild.Score = GetMoveScore(i, grandChild.FieldIndex, grandChild.CurrentGameBoard);
                    child.Children.Add(grandChild);
                    if (currentTreeDepth != TreeDepth)
                    {
                        CreateTreeRecursive(currentTreeDepth + 1, grandChild);
                    }
                }
            }
        }

        private int GetMoveScore(int colorIndex, int buttonIndex, List<int> currentBoardState)
        {
            currentBoardState[buttonIndex] = colorIndex;
            int moveScore = 0;
            int i = 0;
            int z = GameLength - 1;
            int k = SeriesLength;
            int jmax = (z - i) / (k - 1) + 1;
            /// amax = (gameLength-1) - (SeriesLength-1) * j  - maximal starting index for 

            for (int j = 1; j < jmax + 1; j++)
            {
                for (int a = 0; a <= ((GameLength - 1) - (j * (SeriesLength - 1))); a++)
                {
                    bool currentButtonClicked = false;
                    int currentSeriesLength = 0;
                    int m;
                    for (m = 0; m < k; m++)
                    {
                        if (currentBoardState[a + m * j] == colorIndex) currentSeriesLength++;
                        else if (currentBoardState[a + m * j] != -1)
                        {
                            break;
                        }
                        if (a + (m) * j == (buttonIndex))
                        {
                            currentButtonClicked = true;
                        }
                    }
                    if (m == k && currentButtonClicked == true)
                    {
                        int sequenceScore = 0;
                        for (int ss = 2; ss < currentSeriesLength + 2; ss++)
                        {
                            sequenceScore += (ss * ss) * 5;
                        }
                        moveScore += sequenceScore;
                    }
                }
            }
            return moveScore;
        }
    }
}
