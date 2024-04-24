using DP_BUJ0034.Game;
using DP_BUJ0034.Game.Maze;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine.Generator
{
    public class PathTwoDifficulty : IGenerator
    {
        public Paths generatePath(Points start, Points end, float width, float height)
        {
            
            float width_offset = width / 16;
            float height_offset = (height /9)/2;
            width = width/16*15;
            height = height / 9 * 8;
            Paths path = new Paths();
            Dots dot = new Dots(0,0);
            Prim prim=new Prim(16,9);
            int count = 0;
            int regenerate = 0;
            prim.Generate();
            int startx = (int)(start.x/(width / 16));
            Node[,] mazeNodes = prim.ConvertToListTo2DArray(prim.Nodes);
            int starty = (int)(start.y/(height / 9));
            int endy = (int)(end.y / (height / 9));
            Node startNode = mazeNodes[2, starty];
            Node endNode = mazeNodes[15, endy];
            List<Node> maze_path = FindPath(mazeNodes,startNode,endNode);
            path.dot.Add(new Dots(width / 16 * startx, height / 9 * starty));
            for (int i = 0; i < maze_path.Count; i++)
            { 
                if (i != 0 && maze_path[i].Pos.Y== maze_path[i - 1].Pos.Y && regenerate<3)
                {
                    count++;
                   
                }
                else
                {
                    count = 0;
                }
                if (count == 5)
                {
                    

                }

                float x = (width/16*(float)maze_path[i].Pos.X) + width_offset;
                float y = (height/9*(float)maze_path[i].Pos.Y)+height_offset;
                dot = new Dots(x,y);
                path.dot.Add(dot);
            }
       
            return path;

        }
        
        public List<Node> FindPath(Node[,] maze, Node start, Node end)
        {
            HashSet<Node> visited = new HashSet<Node>();
            Dictionary<Node, Node> parentMap = new Dictionary<Node, Node>();
            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                if (current == end)
                {
                    List<Node> path = new List<Node>();
                    while (current != null)
                    {
                        path.Add(current);
                        current = parentMap.ContainsKey(current) ? parentMap[current] : null;
                    }
                    path.Reverse();
                    return path;
                }

                foreach (Node neighbor in GetNeighbors(current, maze))
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        parentMap[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            // No path found
            return null;
        }

        private IEnumerable<Node> GetNeighbors(Node node, Node[,] maze)
        {
            List<Node> neighbors = new List<Node>();

            if (node.Up != null && !node.GetWall(0)) // Up
                neighbors.Add(node.Up);
            if (node.Right != null && !node.GetWall(1)) // Right
                neighbors.Add(node.Right);
            if (node.Down != null && !node.GetWall(2)) // Down
                neighbors.Add(node.Down);
            if (node.Left != null && !node.GetWall(3)) // Left
                neighbors.Add(node.Left);

            return neighbors;
        }
    }
}
