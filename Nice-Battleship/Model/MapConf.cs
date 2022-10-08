using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Nice_Battleship.Model
{
    internal class MapConf
    {
        public static void PrintMap(List<Position> positions, ShipConf Player1Shipconf, ShipConf Player2ShipConf, bool showPlayer2Ships, bool showPlayer1Ships)
        {
            PrintHeader();
            WriteLine();
            if (!showPlayer2Ships)
                showPlayer2Ships = Player1Shipconf.allSunk;

            List<Position> SortedLFirePositions = positions.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
            List<Position> SortedShipsPositions = Player2ShipConf.shipCoordinates.OrderBy(o => o.x).ThenBy(n => n.y).ToList();

            SortedShipsPositions = SortedShipsPositions.Where(FP => !SortedLFirePositions.Exists(ShipPos => ShipPos.x == FP.x && ShipPos.y == FP.y)).ToList();


            int hitCounter = 0;
            int EnemyshipCounter = 0;
            int myShipCounter = 0;
            int enemyHitCounter = 0;

            int row = 1;
            try
            {
                for (int x = 1; x < 11; x++)
                {
                    for (int y = 1; y < 11; y++)
                    {
                        bool jump = true;

                        if (y == 1)
                        {
                            ForegroundColor = ConsoleColor.Green;
                            if (row < 10)
                            {
                                Write("[" + row + "] ");
                            }
                            else
                            {
                                Write("[" + row + "]");
                            }

                            row++;
                        }

                        if (SortedLFirePositions.Count != 0 && SortedLFirePositions[hitCounter].x == x && SortedLFirePositions[hitCounter].y == y)
                        {

                            if (SortedLFirePositions.Count - 1 > hitCounter)
                                hitCounter++;

                            if (Player2ShipConf.shipCoordinates.Exists(ShipPos => ShipPos.x == x && ShipPos.y == y))
                            {
                                ForegroundColor = ConsoleColor.Red;
                                Write("[*]");
                                jump = false;

                            }
                            else
                            {
                                ForegroundColor = ConsoleColor.Blue;
                                Write("[ ]");
                                jump = false;
                            }

                        }

                        if (jump && showPlayer2Ships && SortedShipsPositions.Count != 0 && SortedShipsPositions[EnemyshipCounter].x == x && SortedShipsPositions[EnemyshipCounter].y == y)

                        {

                            if (SortedShipsPositions.Count - 1 > EnemyshipCounter)
                                EnemyshipCounter++;

                            ForegroundColor = ConsoleColor.DarkGray;
                            Write("[" + (char)127 + "]");
                            jump = false;
                        }

                        if (jump)
                        {
                            ForegroundColor = ConsoleColor.White;
                            Write("[ ]");
                        }


                        PrintShipList(x, y, Player1Shipconf);


                        if (y == 10)
                        {
                            ForegroundColor = ConsoleColor.Magenta;
                            Write("        |||        ");
                            PrintPlayerMap(x, row, Player1Shipconf, Player2ShipConf, ref myShipCounter, ref enemyHitCounter, showPlayer1Ships);
                        }
                    }

                    WriteLine();
                }

            }
            catch (Exception e)
            {
                string error = e.Message.ToString();
            }
        }

        static void PrintPlayerMap(int x, int row, ShipConf Player1Shipconf, ShipConf PcNavyAssets, ref int MyshipCounter, ref int EnemyHitCounter, bool showEnemyShips)
        {
            List<Position> EnemyFirePositions = new List<Position>();
            row--;
            Random random = new Random();
            List<Position> SortedLFirePositions = PcNavyAssets.fireCoordinates.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
            List<Position> SortedLShipsPositions = Player1Shipconf.shipCoordinates.OrderBy(o => o.x).ThenBy(n => n.y).ToList();

            SortedLShipsPositions = SortedLShipsPositions.Where(FP => !SortedLFirePositions.Exists(ShipPos => ShipPos.x == FP.x && ShipPos.y == FP.y)).ToList();


            try
            {
                for (int y = 1; y < 11; y++)
                {
                    bool jump = true;

                    if (y == 1)
                    {
                        ForegroundColor = ConsoleColor.Green;
                        if (row < 10)
                        {
                            Write("[" + row + "] ");
                        }
                        else
                        {
                            Write("[" + row + "]");
                        }
                        row++;
                    }


                    if (SortedLFirePositions.Count != 0 && SortedLFirePositions[EnemyHitCounter].x == x && SortedLFirePositions[EnemyHitCounter].y == y)
                    {

                        if (SortedLFirePositions.Count - 1 > EnemyHitCounter)
                            EnemyHitCounter++;

                        if (Player1Shipconf.shipCoordinates.Exists(ShipPos => ShipPos.x == x && ShipPos.y == y))
                        {

                            ForegroundColor = ConsoleColor.Red;
                            Write("[*]");
                            jump = false;
                        }
                        else
                        {
                            ForegroundColor = ConsoleColor.Blue;
                            Write("[ ]");
                            jump = false;
                        }

                    }

                    if (jump && showEnemyShips && SortedLShipsPositions.Count != 0 && SortedLShipsPositions[MyshipCounter].x == x && SortedLShipsPositions[MyshipCounter].y == y)

                    {

                        if (SortedLShipsPositions.Count - 1 > MyshipCounter)
                            MyshipCounter++;

                        ForegroundColor = ConsoleColor.DarkGray;
                        Write("[" + (char)127 + "]");
                        jump = false;
                    }

                    if (jump)
                    {
                        ForegroundColor = ConsoleColor.White;
                        Write("[ ]");
                    }


                    PrintShipList(x, y, PcNavyAssets);

                }


            }
            catch (Exception e)
            {
                string error = e.Message.ToString();
            }
        }

        public static void PrintHeader()
        {
            ForegroundColor = ConsoleColor.Green;
            Write("[ ]|");
            char column = 'A';
            for (int i = 1; i < 11; i++)
            {
                Write("[" + column + "]");
                column++;
            }



        }

        public static void PrintSecondMapHeader()
        {
            PrintHeader();
            for (int h = 0; h < 33; h++)
            {
                Write(" ");
            }
        }

        static void PrintShipList(int x, int y, ShipConf shipConf)
        {
            if (x == 1 && y == 10)
            {
                ForegroundColor = ConsoleColor.White;
                Write("Ship list:    ");
            }


            if (x == 2 && y == 10)
            {
                if (shipConf.carrierSunken)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Carrier [5]   ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Carrier [5]   ");
                }

            }

            if (x == 3 && y == 10)
            {
                if (shipConf.battleshipSunken)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Battleship [4]");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Battleship [4]");
                }
            }

            if (x == 4 && y == 10)
            {

                if (shipConf.cruiserSunken)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Cruiser [3]   ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Cruiser [3]   ");
                }
            }

            if (x == 5 && y == 10)
            {

                if (shipConf.submarineSunken)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Submarine [3] ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Submarine [3] ");
                }
            }

            if (x == 6 && y == 10)
            {

                if (shipConf.destroyerSunken)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write("Destroyer [2] ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write("Destroyer [2] ");
                }

            }


            if (x > 6 && y == 10)
            {
                for (int i = 0; i < 14; i++)
                {
                    Write(" ");
                }
            }

        }

        public static Position AnalyzeInput(string input, Dictionary<char, int> Coordinates)
        {
            Position pos = new Position();

            char[] inputSplit = input.ToUpper().ToCharArray();


            if (inputSplit.Length < 2 || inputSplit.Length > 4)
            {
                return pos;
            }




            if (Coordinates.TryGetValue(inputSplit[0], out int value))
            {
                pos.y = value;
            }
            else
            {
                return pos;
            }


            if (inputSplit.Length == 3)
            {

                if (inputSplit[1] == '1' && inputSplit[2] == '0')
                {
                    pos.x = 10;
                    return pos;
                }
                else
                {
                    return pos;
                }

            }


            if (inputSplit[1] - '0' > 9)
            {
                return pos;
            }
            else
            {
                pos.x = inputSplit[1] - '0';
            }

            return pos;
        }

        public static void Advertisements(ShipConf shipConf, bool isMyShip, PlayersSet playerSet)
        {

            string title = isMyShip ? playerSet.Player1.Name : playerSet.Player2.Name;


            if (shipConf.checkCarrier && shipConf.carrierSunken)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sunken", title, nameof(shipConf.Carrier));
                shipConf.checkCarrier = false;
            }

            if (shipConf.checkPBattleship && shipConf.battleshipSunken)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sunken", title, nameof(shipConf.Battleship));
                shipConf.checkPBattleship = false;
            }

            if (shipConf.checkCruiser && shipConf.cruiserSunken)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sunken", title, nameof(shipConf.Cruiser));
                shipConf.checkCruiser = false;
            }

            if (shipConf.checkSubmarine && shipConf.submarineSunken)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sunken", title, nameof(shipConf.Submarine));
                shipConf.checkSubmarine = false;
            }

            if (shipConf.checkDestroyer && shipConf.destroyerSunken)
            {
                ForegroundColor = ConsoleColor.DarkRed;
                WriteLine("{0} {1} is sunken", title, nameof(shipConf.Destroyer));
                shipConf.checkDestroyer = false;
            }

        }
    }
}
