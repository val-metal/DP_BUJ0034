using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Game.Maze
{
    public struct ParentInfo
    {
        public int Index { get; set; }
        public Node Parent { get; set; }
        public ParentInfo(Node parent, int index)
        {
            this.Parent = parent;
            this.Index = index;
        }
    }
    public class Node
    {
        public Node()
        {
            this.parentInfo = new List<ParentInfo>();
            Count = 4;
            Walls = new bool[Count];
            for (int i = 0; i < Count; i++)
            {
                Walls[i] = true;
            }
        }
        public bool isFrontier { get; set; }
        public List<ParentInfo> parentInfo { get; set; }
        public bool isStart { get; set; }
        public Point Pos { get; set; }
        public int Count { get; set; }
        public Node Down;
        public Node Up;
        public Node Right;
        public Node Left;
        public bool[] Walls { get; set; }

        public void UnWall(int index)
        {
            Walls[index] = false;
        }

        public bool GetWall(int index)
        {
            return Walls[index];
        }
        public bool WallIsnotDestroyed()
        {
            if (!Walls[0] && !Walls[1] && !Walls[2] && !Walls[3])
            { return false; }
            else
            {
                return true;
            }
        }

        public Node this[int index]
        {
            get
            {
                return SwitchNodeProp(index);
            }
            set
            {
                SwitchNodeProp(index, value);
            }
        }

        private Node SwitchNodeProp(int index, Node value = null)
        {
            switch (index)
            {
                case 0:
                    return ReturnNodeProp(ref Up, value);
                case 1:
                    return ReturnNodeProp(ref Right, value);
                case 2:
                    return ReturnNodeProp(ref Down, value);
                case 3:
                    return ReturnNodeProp(ref Left, value);
            }

            return null;
        }
        private Node ReturnNodeProp(ref Node prop, Node value = null)
        {
            if (value == null)
            {
                return prop;
            }
            else
            {
                prop = value;
                return null;
            }
        }
    }
}
