using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npuzzle
{
    class BFS_Search
    { 
        public BFS_Search()
        {

        }
        //open list : visited nodes (white or grey )
        //closed list : expored Node_BFS_(which its children added to the openlist)(black ) 
        static int count = 0;
        public List<Node_BFS_> BreadthFirstSearch(Node_BFS_ root)
        {
            List<Node_BFS_> PathToSolution = new List<Node_BFS_>();
            List<Node_BFS_> OpenList = new List<Node_BFS_>();
            List<Node_BFS_> ClosedList = new List<Node_BFS_>();

            OpenList.Add(root);
            bool goalfound = false;
            while (OpenList.Count > 0 && !goalfound)//O(V)
            {
                Node_BFS_ currentNode = OpenList[0];
                ClosedList.Add(currentNode);
                OpenList.RemoveAt(0);
                // put all the childs of teh current Node_BFS_ in children list
                currentNode.ExpandNode(); //O(N^2)

                //currentNode.PrintPuzzle(); // infinite loooooooop 
                for (int i = 0; i < currentNode.children.Count; i++)//O(N^2) as the maximum no of childs = 4
                {
                    Node_BFS_ currentchild = currentNode.children[i];
                    if (currentchild.goaltest())// goaltest O(N^2)
                    {
                        count++;
                        Console.WriteLine("Goal Found...");
                        goalfound = true;
                        //trace path  to root Node_BFS_
                        PathTrace(PathToSolution, currentchild);//O(E)

                    }
                    //some Node_BFS_ doesnot visited 
                    if(!OpenList.Contains(currentchild) && !ClosedList.Contains(currentchild))
                    {
                        Console.WriteLine("Goal NOOOT Found...");
                        OpenList.Add(currentchild);
                        count++;
                    }
                    
                }
            }
            return PathToSolution;
        }
        public void PathTrace(List<Node_BFS_> path, Node_BFS_ n)
        {

            Console.WriteLine("# Of Passing nodes   :  " + count);
            Console.WriteLine("Tracing Path...");
            Node_BFS_ current = n;
            path.Add(n);
            while (current.parent != null) //O(E)
            {
                current = current.parent;
                path.Add(current);
            }
        }
       /* public static bool contains(List<Node_BFS_> list, Node_BFS_ c)//O(V)
        {
            bool contains = false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].issamepuzzle(c.puzzle))
                {
                    contains = true;
                }
            }
            return contains;
        }*/
    }
}


