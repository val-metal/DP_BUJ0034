using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Game.Maze
{
    public class Prim
    {
        public Node[,] ConvertToListTo2DArray(List<List<Node>> nodeList)
        {
            int width = nodeList.Count;
            int height = nodeList[0].Count;

            Node[,] nodeArray = new Node[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodeArray[x, y] = nodeList[x][y];
                }
            }

            return nodeArray;
        }

        public List<List<Node>> Nodes = new List<List<Node>>();
        public Prim(int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                List<Node> nX = new List<Node>();
                for (int y = 0; y < height; y++)
                {
                    Node nY = new Node();
                    nY.Pos = new Point(x, y);
                    if (y > 0)
                    {
                        nY.Up = nX[y - 1];
                        nX[y - 1].Down = nY;
                    }
                    if (x > 0)
                    {
                        nY.Left = Nodes[x - 1][y];
                        Nodes[x - 1][y].Right = nY;
                    }
                    nX.Add(nY);
                }
                Nodes.Add(nX);
            }
        }

        public void Generate()
        {
            int total = this.Nodes.Count * this.Nodes[0].Count;
            int visited = 1;

            List<Node> frontier = new List<Node>();
            Random r = new Random();

            Node current = this.Nodes[(int)(r.NextDouble() * 10) % this.Nodes.Count][(int)(r.NextDouble() * 50) % this.Nodes[0].Count];
            current.isStart = true;
            for (int i = 0; i < current.Count; i++)
            {
                if (current[i] != null)
                {
                    current[i].parentInfo.Add(new ParentInfo(current, i));
                    frontier.Add(current[i]);
                    current[i].isFrontier = true;
                }
            }

            while (frontier.Count > 0)
            {
                current = frontier[(int)(r.NextDouble() * 10) % frontier.Count];
                current.isFrontier = false;
                frontier.Remove(current);

                ParentInfo parentInfo = current.parentInfo[(int)(r.NextDouble() * 15) % current.parentInfo.Count];

                parentInfo.Parent.UnWall(parentInfo.Index);
                current.UnWall((parentInfo.Index + 2) % 4);

                for (int i = 0; i < current.Count; i++)
                {
                    if (current[i] != null && current[i].parentInfo.Count == 0 && current[i].WallIsnotDestroyed())
                    {
                        frontier.Add(current[i]);
                        current[i].parentInfo.Add(new ParentInfo(current, i));
                        current[i].isFrontier = true;
                    }
                }
                visited++;
            }
        }

    }
}
