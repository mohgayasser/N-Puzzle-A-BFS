using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
//using static Npuzzle.newqueue;

namespace Npuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            //[M1]
            //FileStream f = new FileStream("8 Puzzle (1).txt", FileMode.Open, FileAccess.Read); //8
             FileStream f = new FileStream("8 Puzzle (2).txt", FileMode.Open, FileAccess.Read);//20
            // FileStream f = new FileStream("8 Puzzle (3).txt", FileMode.Open, FileAccess.Read);//14
            //  FileStream f = new FileStream("15 Puzzle - 1.txt", FileMode.Open, FileAccess.Read);//5
            //   FileStream f = new FileStream("24 Puzzle 1.txt", FileMode.Open, FileAccess.Read);//11
            //FileStream f = new FileStream("24 Puzzle 2.txt", FileMode.Open, FileAccess.Read);//24


            //[M2] 
            //Sample Test_II  Unsolvable Puzzles

            // FileStream f = new FileStream("15 Puzzle - Case 3.txt", FileMode.Open, FileAccess.Read);
            // FileStream f = new FileStream("15 Puzzle - Case 2.txt", FileMode.Open, FileAccess.Read);
           // FileStream f = new FileStream("8 Puzzle(3) - Case 1.txt", FileMode.Open, FileAccess.Read);
            //    FileStream f = new FileStream("8 Puzzle(2) - Case 1.txt", FileMode.Open, FileAccess.Read);
            // FileStream f = new FileStream("8 Puzzle - Case 1.txt", FileMode.Open, FileAccess.Read);

            //Manhattan only
            // FileStream f = new FileStream("15 Puzzle 1.txt", FileMode.Open, FileAccess.Read); //46 Move
            // FileStream f = new FileStream("15 Puzzle 3.txt", FileMode.Open, FileAccess.Read); // moves = 38
            // FileStream f = new FileStream("15 Puzzle 4.txt", FileMode.Open, FileAccess.Read); //movements = 44
          //  FileStream f = new FileStream("15 Puzzle 5.txt", FileMode.Open, FileAccess.Read); //movements =  45


            //Hamming &Manhattan

            // FileStream f = new FileStream("50 Puzzle.txt", FileMode.Open, FileAccess.Read);  // movements = 18 
           // FileStream f = new FileStream("99 Puzzle - 1.txt", FileMode.Open, FileAccess.Read);  // movements = 18
                                                                                                 //  FileStream f = new FileStream("99 Puzzle - 2.txt", FileMode.Open, FileAccess.Read);  //movements = 38
                // FileStream f = new FileStream("9999 Puzzle.txt", FileMode.Open, FileAccess.Read);  //movements = 4


            //V..Larg
           // FileStream f = new FileStream("TEST.txt", FileMode.Open, FileAccess.Read);  //Movements = 56  

            //complete unsolvabule puzzle

            // FileStream f = new FileStream("9999 Puzzle (2).txt", FileMode.Open, FileAccess.Read); 
            // FileStream f = new FileStream("99 Puzzle - Unsolvable Case 2.txt", FileMode.Open, FileAccess.Read); 
            // FileStream f = new FileStream("99 Puzzle - Unsolvable Case 1.txt", FileMode.Open, FileAccess.Read);
            // FileStream f = new FileStream("15 Puzzle 1 - Unsolvable.txt", FileMode.Open, FileAccess.Read);  

            StreamReader sr = new StreamReader(f);
            int N = int.Parse(sr.ReadLine());
            int[,] matrix = new int[N, N];
            int i0 = -1, j0 = -1;
            string firstline = sr.ReadLine();
            for (int i = 0; i < N; i++)
            {
                string[] s;
                if (firstline != "")
                {
                    s = firstline.Split(' ');
                    firstline = "";
                }
                else
                    s = sr.ReadLine().Split(' ');
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = int.Parse(s[j]);
                }
            }
            int[] arr1D = new int[N * N];
            int p = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        i0 = i;
                        j0 = j;
                    }
                    arr1D[p] = matrix[i, j];
                    p++;
                }
            }
            sr.Close();
            f.Close();
            Console.WriteLine("Do you want to run A* OR BFS ?? [1]A* // [2]BFS");
            int c = int.Parse(Console.ReadLine());
            switch (c)
            {
                case 1:
                    int heuristic_value = 0;

                    Stopwatch stopwatch = new Stopwatch();
                    // Begin timing
                    stopwatch.Start();

                    System.Threading.Thread.Sleep(500);

                    if (isSolvable(N, arr1D))
                    {
                        Console.WriteLine("Solvable");
                  
                        Console.Write("Total # of movements = ");

                        int hamming_value = Hamming(arr1D, N);
                        int Manhatten_value = Manhatten_fun(matrix, N);
                        heuristic_value = Manhatten_value;
                        //heuristic_value = hamming_value;
                        node newnode = new node(matrix, N, i0, j0, heuristic_value, 0);
                        node n = astar(newnode);

                        if (N == 3)
                        {
                            getpath(n);
                        }


                    }
                    else
                    {
                        Console.WriteLine("Not Solvable");
                    }

                    // Stop timing
                    stopwatch.Stop();

                    Console.WriteLine("Time : {0}", stopwatch.Elapsed);
                    break;
                case 2:

                    Stopwatch stopwatch2 = new Stopwatch();
                    // Begin timing
                    stopwatch2.Start();

                    System.Threading.Thread.Sleep(500);


                    Node_BFS_ root = new Node_BFS_(arr1D);
                    BFS_Search bfs = new BFS_Search();
                    List<Node_BFS_> solution = bfs.BreadthFirstSearch(root);

                    if (solution.Count > 0)
                    {
                        solution.Reverse();
                        for (int i = 0; i < solution.Count; i++)
                        {

                            solution.ElementAt(i).PrintPuzzle();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Path to solution to found...");
                    }

                    stopwatch2.Stop();

                    Console.WriteLine("Time : {0}", stopwatch2.Elapsed);

                    break;
            }
            Console.Read();
        }


        public static void printpath(int[,] arr, int N)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(arr[i, j]);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static bool getpath(node start)
        {
            if (start == null)
            {
                return false;
            }

            getpath(start.parent);
            printpath(start.borad, start.N);
            return true;
        }

        static priorityqueue pq;

        public static node astar(node start)
        {
            pq = new priorityqueue();
            node root = start;
            pq.enqueue(root);
            while (!pq.empty())
            {
                node top = pq.dequeue();
                if (top.H == 0)
                {
                    Console.WriteLine(top.Level);
                    return top;
                }
                top.getadj();

                for (int i = 0; i < top.adj.Count(); i++)
                {
                    node front = top.adj[i];
                    front.F = front.Level + front.H;
                    pq.enqueue(front);
                }

            }
            return null;
        }
        static int Manhatten_fun(int[,] source, int N)
        {
            int m = 0;
            int C = 0;
            int R = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (source[i, j] == 0) continue;
                    else if (source[i, j] != ((i * N + j) + 1))
                    {
                        C = ((source[i, j] - 1) % N);
                        R = (source[i, j] - 1) / N;
                        int right_colume = C,
                         right_row = R;
                        m += Math.Abs(right_row - i) + Math.Abs(right_colume - j);
                    }
                }
            }
            return m;
        }
        static int Hamming(int[] source, int N)
        {
            int count = 0;
            for (int i = 0; i < N * N; i++)
            {
                int index = i + 1;
                if (source[i] == 0) continue;

                else if (source[i] != index)
                {
                    count++;
                }
            }
            return count;
        }
        static bool isSolvable(int n, int[] arr)
        {
            int noOFinversions = 0;
            int spaceIndex = -1;
            //compare from first cell to the pre last
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] == 0)
                {
                    spaceIndex = i / n;
                    continue;
                }
                //compare with the cell after i cell till the last cell 
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] == 0)
                    {
                        continue;
                    }
                    else if (arr[i] > arr[j])
                    {
                        noOFinversions++;
                    }
                }
            }
            if (n % 2 != 0 && noOFinversions % 2 == 0)
            {
                return true;
            }
            else if (n % 2 == 0 && noOFinversions % 2 != 0 && spaceIndex % 2 == 0)
            {
                return true;
            }
            else if (n % 2 == 0 && noOFinversions % 2 == 0 && spaceIndex % 2 != 0)
            {
                return true;
            }
            return false;
        }

    }
}

