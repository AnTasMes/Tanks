using System;

namespace TanksFixed
{
    class Program
    {
        static void Main(string[] args)
        {
            Tank t1 = new Tank(3, 2);
            Tank t2 = new Tank(3, 5);
            Tank t3 = new Tank(5, 2);

            Board board1 = new Board(10, 10);
            board1.AddTanks(t1, t2, t3);
            board1.PrintBoard();

            while(board1.Tanks.Count > 1)
            {
                string response;
                int tankID;
                do
                {
                    Console.WriteLine("Do you wish to give a tank a point? (Y / N)");
                    response = Console.ReadLine();
                    if (response == "N" || response == "n")
                        break;
                    Console.WriteLine("Which tank will it be? (Tank number)");
                    tankID = Convert.ToInt32(Console.ReadLine());
                    board1.AssignPoints(tankID);
                        
                } while (response == "Y" || response == "y");
                board1.PrintBoard();

                board1.Tanks.ForEach(tank =>
                {
                    if (tank.IsAlive())
                    {
                        int moves = tank.Moves;
                        while (moves > 0)
                        {
                            Console.WriteLine("CURRENT ATTACKER");
                            Console.WriteLine(tank);
                            Console.WriteLine("Number of moves: " + moves);
                            Console.WriteLine("\nWhat is your command? (move, shoot, give, pass)");
                            response = Console.ReadLine();
                            response = response.Trim(' ');
                            switch (response)
                            {
                                case "move":
                                    do
                                    {
                                        Console.WriteLine("Where do you wish to move? (r, l, u, d, ur, ul, dr, dl)");
                                        response = Console.ReadLine();
                                        response = response.Trim(' ');
                                    } while (!tank.Move(response, board1));
                                    moves--;
                                    break;
                                case "shoot":
                                    int damage = 0;
                                    if (tank.NoPoints <= 0)
                                    {
                                        Console.WriteLine("You dont have any points");
                                        break;
                                    }                                        
                                    do
                                    {
                                        Console.WriteLine("Which tank are you attacking? (Tank number)");
                                        tankID = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("What is the damage? (Points)");
                                        damage = Convert.ToInt32(Console.ReadLine());
                                    } while (!tank.Shoot(tankID, damage, board1));
                                    moves--;
                                    break;
                                case "give":
                                    int points = 0;
                                    do
                                    {
                                        Console.WriteLine("Who will recieve your points? (Tank number)");
                                        tankID = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("How many points are you giving? (Number)");
                                        points = Convert.ToInt32(Console.ReadLine());
                                    } while (tank.Give(tankID, points, board1));
                                    moves--;
                                    break;
                                case "pass":
                                    Console.WriteLine("You passed the round, but saved half of your moves for another round");
                                    tank.Moves += Convert.ToInt32(Math.Round(moves*0.5));
                                    moves = 0;
                                    break;
                                default:
                                    Console.WriteLine("Invalid command");
                                    break;
                            }
                        }
                    }
                    board1.PrintBoard();
                });
                board1.RemoveDead();
            }
        }
    }
}
