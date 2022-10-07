using Nice_Battleship.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Nice_Battleship
{
    internal class WorkFlow
    {
        public void start()
        {
            bool isShowShips = false;//show enemy ships position
            ShipConf player1Navys = new ShipConf();
            ShipConf pcNavys = new ShipConf();
            Dictionary<char, int> Coordinates = CoordinatesValue.FillDictionary();

            PrintMap(player1Navys.fireCoordinates, player1Navys, pcNavys, isShowShips);
            //DrawWelcome();
        }

       static void DrawWelcome()
        {
            ForegroundColor = ConsoleColor.Green;
            Write(">>> ");
            ForegroundColor = ConsoleColor.White;
            Write("BATTLESHIP GAME");
            ForegroundColor = ConsoleColor.Green;
            Write(" <<<");
            ForegroundColor = ConsoleColor.White;
        }
        static void PrintMap(List<Position> positions, ShipConf Player1Shipconf, ShipConf PcNavyAssets, bool showEnemyShips)
        {
            
            WriteLine();
            if (!showEnemyShips)
                showEnemyShips = Player1Shipconf.allSunk;

            List<Position> SortedLFirePositions = positions.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
            List<Position> SortedShipsPositions = PcNavyAssets.shipCoordinates.OrderBy(o => o.x).ThenBy(n => n.y).ToList();

            SortedShipsPositions = SortedShipsPositions.Where(FP => !SortedLFirePositions.Exists(ShipPos => ShipPos.x == FP.x && ShipPos.y == FP.y)).ToList();

            int row = 1;
            try
            {
                for (int x = 1; x < 11; x++)
                {
                    for (int y = 1; y < 11; y++)
                    {
                        bool jump = true;

                        #region row indicator
                        if (y == 1)
                        {
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
                        #endregion

                        if (jump)
                        {
                            Write("[ ]");
                        }

                        if (y == 10)
                        {
                            Write("      ");
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
    }
}
