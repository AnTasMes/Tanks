using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanksFixed
{
    class Board
    {
        private int _xSize, _ySize;
        private string[,] boardMatrix;
        private List<Tank> _tanks;

        public Board(int xSize, int ySize)
        {
            XSize = xSize;
            YSize = ySize;
            Tanks = new List<Tank>();
            boardMatrix = new string[xSize, ySize];
            ResetBoard();
        }

        public int XSize { get => _xSize; set => _xSize = value; }
        public int YSize { get => _ySize; set => _ySize = value; }
        internal List<Tank> Tanks { get => _tanks; set => _tanks = value; }

        public void RemoveDead()
        {
            _tanks.RemoveAll(
                delegate (Tank t)
                {
                    return t.NoLives == 0;
                });
        }

        public void AddTanks(params Tank[] t)
        {
            int c = 1;
            foreach(Tank item in t)
            {
                Tanks.Add(item);
                item.Id = c++;
            }
        }

        public bool FindAt(int x, int y)
        {
            foreach (Tank tank in _tanks)
            {
                if (tank.XCord == x && tank.YCord == y && tank.IsAlive())
                    return true;
            }
            return false;
        }

        public bool FindID(int ID)
        {
            return _tanks.Exists(delegate (Tank t)
            {
                return t.Id == ID;
            });
        }


        public bool AssignPoints(int ID)
        {
            Tank tank = _tanks.Find(delegate (Tank t)
            {
                return t.Id == ID;
            });
            try
            {
                tank.NoPoints += 1;
                Console.WriteLine("Points assigned to tank "+ID);
                Console.WriteLine("Current points: "+tank.NoPoints);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid ID");
                return false;
            }
        }

        public bool InRange(int mainID, int targetID, out Tank targetT)
        {
            Tank mainT = _tanks.Find(
                delegate (Tank t)
                {
                    return t.Id == mainID;
                });

            targetT = _tanks.Find(
                delegate (Tank t)
                {
                    return t.Id == targetID;
                });
            try
            {
                if (Math.Abs(targetT.XCord - mainT.XCord) <= mainT.Range && Math.Abs(targetT.YCord - mainT.YCord) <= mainT.Range)
                {
                    return true;
                }
                    
            }
            catch
            {
                return false;
            }
            return false;
        }

        public void PrintBoard()
        {
            Console.Clear();
            ResetBoard();
            try
            {
                _tanks.ForEach(tank =>
                {
                    if (tank.IsAlive()) 
                    {
                        boardMatrix[tank.YCord, tank.XCord] = " " + tank.Id + " ";
                        Console.WriteLine(tank);
                    }
                        
                });
            }
            catch
            {
                Console.WriteLine("Some tanks are out of bounds");
                return;
            }
            //Printing the lables
            Console.Write("   ");
            for (int i = 0; i < YSize; i++)
            {
                Console.Write(" " + i + " ");
            }
            Console.Write("\n   ");
            for (int i = 0; i < YSize; i++)
            {
                Console.Write("___");
            }
            Console.WriteLine("");

            //Printing the inner table
            for (int i = 0; i < XSize; i++)
            {
                Console.Write(i + " |");
                for (int j = 0; j < YSize; j++)
                {
                    Console.Write(boardMatrix[i, j]);
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
            for (int i = 0; i < YSize; i++)
            {
                Console.Write("____");
            }
            Console.WriteLine("\n");
            


        }

        public void ResetBoard()
        {
            for (int i = 0; i < XSize; i++)
            {
                for (int j = 0; j < YSize; j++)
                {
                    boardMatrix[i, j] = " . ";
                }
            }
        }
    }
}
