using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanksFixed
{
    class Tank
    {
        private int _noLives, _noPoints;
        private int _xCord, _yCord;
        private int _id;
        private int _range;
        private int _moves;

        
        public int NoPoints { get => _noPoints; set => _noPoints = value; }
        public int XCord { get => _xCord; set => _xCord = value; }
        public int YCord { get => _yCord; set => _yCord = value; }
        public int Id { get => _id; set => _id = value; }
        public int Range { get => _range; set => _range = value; }
        public int NoLives
        {
            get => _noLives; set
            {
                if (value >= 0)
                {
                    _noLives = value;
                }
                else
                {
                    _noLives = 0;
                }
            }
        }
        public int Moves { get => _moves; set
            {
                if (value >= 0)
                {
                    _moves = value;
                }
                else
                {
                    _moves = 0;
                }
            }
        }

        public Tank(int x, int y)
        {
            _noLives = 3;
            _noPoints = 4;
            _xCord = x;
            _yCord = y;
            _range = 2;
            _moves = 2;
        }

        public bool Move(string direction, Board boardObj)
        {
            int x = XCord;
            int y = YCord;
            //r l u p ur ul dr dl
            switch (direction)
            {
                case "r":
                    x += 1;
                    break;
                case "l":
                    x -= 1;
                    break;
                case "u":
                    y -= 1;
                    break;
                case "d":
                    y += 1;
                    break;
                case "ur":
                    x += 1;
                    y -= 1;
                    break;
                case "ul":
                    x -= 1;
                    y -= 1;
                    break;
                case "dr":
                    x += 1;
                    y += 1;
                    break;
                case "dl":
                    x -= 1;
                    y += 1;
                    break;
                default:
                    Console.WriteLine(" - Position must be: r, l, u, d, ur, ul, dr, dl");
                    return false;
            }

            if (x >= boardObj.XSize || y >= boardObj.YSize || x < 0 || y < 0 || boardObj.FindAt(x, y))
            {
                Console.WriteLine(" - Cannot move to this position");

                return false;
            }
            else
            {
                XCord = x;
                YCord = y;

                boardObj.PrintBoard();
                return true;
            }
        }

        public bool Shoot(int tankID, int damage, Board boardObj)
        {
            if(tankID == Id)
            {
                Console.WriteLine(" - You cannot shoot your own tank");
                return false;
            }
            if (damage > NoPoints)
            {
                Console.WriteLine(" - You dont have enough points");
                return false;
            }
            ;
            bool isInRange = boardObj.InRange(Id, tankID, out Tank tank);
            if (isInRange)
            {
                if(damage>tank.NoLives)
                    NoPoints -= tank.NoLives;
                NoPoints -= damage;
                tank.NoLives -= damage;
                boardObj.PrintBoard();
                Console.WriteLine(" - Successfully shot tank: "+tank.Id);

                return true;
            }
            return false;
        }

        public bool Give(int tankID, int points, Board boardObj)
        {
            if (tankID == Id)
            {
                Console.WriteLine("You cannot give points to yourself");
                return false;
            }
            if (points > NoPoints)
            {
                Console.WriteLine("You dont have enough points");
                return false;
            }
            bool isInRange = boardObj.InRange(Id, tankID, out Tank tank);
            if (isInRange)
            {
                tank.NoPoints += points;
                NoPoints -= points;
                return true;
            }
            return false;
        }

        public bool IsAlive()
        {
            if (_noLives > 0)
                return true;
            return false;
        }

        public override string ToString()
        {
            return "TANK ==> ID: " + Id + " ; Lives: " + NoLives + " ; Points: " + NoPoints + " ; (X,Y) = (" + XCord + "," + YCord + ")";
        }
    }
}
