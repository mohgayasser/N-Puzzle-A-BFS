using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npuzzle
{
    class Node_BFS_
    {
        public List<Node_BFS_> children = new List<Node_BFS_>();
        public Node_BFS_ parent;
        public int[] puzzle = new int[9];
        public int index0 = 8;
        public int col = 3;
        public Node_BFS_(int[] p)
        {
            setpuzzle(p);
        }
        public void setpuzzle(int[] p)
        {
            for (int i = 0; i < puzzle.Length; i++)
            {
                this.puzzle[i] = p[i];
                if (p[i] == 0)
                {
                    index0 = i;
                }
            }
        }
        // get the index of the 0 and get the possible children  
        public void ExpandNode()
        {
           
            MoveToRight(puzzle, index0);
            MoveToLeft(puzzle, index0);
            MoveToUp(puzzle, index0);
            MoveToDown(puzzle, index0);
        }
        //i index of blankcell
        public void MoveToRight(int[] p, int i)
        {
            if (i % col < col - 1)
            {
                int[] pc = new int[9];
                CopyPuzzle(pc, p);

                int temp = pc[i + 1];
                pc[i + 1] = pc[i];
                pc[i] = temp;

                Node_BFS_ child = new Node_BFS_(pc);
                children.Add(child);
                child.parent = this;
            }
        }
        public void MoveToLeft(int[] p, int i)
        {
            if (i % col > 0)
            {
                int[] pc = new int[9];
                CopyPuzzle(pc, p);

                int temp = pc[i - 1];
                pc[i - 1] = pc[i];
                pc[i] = temp;

                Node_BFS_ child = new Node_BFS_(pc);
                children.Add(child);
                child.parent = this;
            }
        }
        public void MoveToUp(int[] p, int i)
        {
            if (i - col >= 0)
            {
                int[] pc = new int[9];
                CopyPuzzle(pc, p);

                int temp = pc[i - 3];
                pc[i - 3] = pc[i];
                pc[i] = temp;

                Node_BFS_ child = new Node_BFS_(pc);
                children.Add(child);
                child.parent = this;
            }
        }
        public void MoveToDown(int[] p, int i)
        {
            if (i + col < puzzle.Length)
            {
                int[] pc = new int[9];
                CopyPuzzle(pc, p);

                int temp = pc[i + 3];
                pc[i + 3] = pc[i];
                pc[i] = temp;

                Node_BFS_ child = new Node_BFS_(pc);
                children.Add(child);
                child.parent = this;
            }
        }
        public void PrintPuzzle()
        {
            Console.WriteLine();
            int m = 0;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write(puzzle[m] + " ");
                    m++;
                }
                Console.WriteLine();
            }
        }
        public bool issamepuzzle(int[] p)
        {
            bool samepuzzle = true;
            for (int i = 0; i < p.Length; i++)
            {
                if (puzzle[i] != p[i])
                {
                    samepuzzle = false;
                    break;
                }
            }
            return samepuzzle;
        }
        public void CopyPuzzle(int[] a, int[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                a[i] = b[i];
            }
        }
        public bool goaltest() //O(n^2) 
        {
            bool isgoal = true;
            int m = puzzle[0];
            //check that this puzzle is not the goal 
            for (int i = 1; i < puzzle.Length - 1; i++)
            {
                if (m > puzzle[i])
                {
                    return isgoal = false;
                }
                m = puzzle[i];

            }
            if (isgoal && puzzle[8] == 0)
            {
                isgoal = true;

            }
            else isgoal = false;
            return isgoal;
        }
       

    }
}


