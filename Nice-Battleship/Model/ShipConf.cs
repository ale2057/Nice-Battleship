using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Nice_Battleship.Model
{
    internal class ShipConf
    {
        Random random = new Random();
        private const int carrier = 5; //board spaces 5
        private const int battleship = 4; //board spaces 4
        private const int cruiser = 3; //board spaces 3
        private const int submarine = 3; //board spaces 3
        private const int destroyer = 2; //board spaces 2

        public List<Position> Carrier { get; set; }//5
        public List<Position> Battleship { get; set; }//4
        public List<Position> Cruiser { get; set; }//3
        public List<Position> Submarine { get; set; }//3
        public List<Position> Destroyer { get; set; }//2
        public List<Position> shipCoordinates { get; set; } = new List<Position>();
        public List<Position> fireCoordinates { get; set; } = new List<Position>();


        public bool carrierSunken { get; set; } = false;
        public bool battleshipSunken { get; set; } = false;
        public bool cruiserSunken { get; set; } = false;
        public bool submarineSunken { get; set; } = false;
        public bool destroyerSunken { get; set; } = false;
        public bool allSunk { get; set; } = false;


        public bool checkCarrier { get; set; } = true;
        public bool checkPBattleship { get; set; } = true;
        public bool checkCruiser { get; set; } = true;
        public bool checkSubmarine { get; set; } = true;
        public bool checkDestroyer { get; set; } = true;

        public ShipConf(bool manual)
        {

            Carrier = GeneratePosistion(carrier, shipCoordinates, manual, "CARRIER");
            Battleship = GeneratePosistion(battleship, shipCoordinates, manual, "BATTLESHIP");
            Cruiser = GeneratePosistion(cruiser, shipCoordinates, manual, "CRUISER");
            Submarine = GeneratePosistion(submarine, shipCoordinates, manual, "SUBMARINE");
            Destroyer = GeneratePosistion(destroyer, shipCoordinates, manual, "DESTROYER");
        }

        public List<Position> GeneratePosistion(int size, List<Position> AllPosition, bool Manual, string shipName)
        {
            List<Position> positions = new List<Position>();

            bool IsExist = false;

            do
            {
                if (Manual)
                {
                    if (IsExist)
                    {
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine("Location already taken");
                    }
                    positions = ManualPosition(size, shipName);
                }
                else
                {
                    positions = GeneratePositionRandomly(size);
                }
                
                IsExist = positions.Where(AP => AllPosition.Exists(ShipPos => ShipPos.x == AP.x && ShipPos.y == AP.y)).Any();
            }while (IsExist);
            AllPosition.AddRange(positions);
            return positions;
        }

        public List<Position> GeneratePositionRandomly(int size)
        {
            List<Position> positions = new List<Position>();

            int direction = random.Next(1, size);
            int row = random.Next(1, 11);
            int col = random.Next(1, 11);

            if (direction % 2 != 0)
            {
                if (row - size >= 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row - i;
                        pos.y = col;
                        positions.Add(pos);
                    }
                }
                else // col
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row + i;
                        pos.y = col;
                        positions.Add(pos);
                    }
                }
            }
            else
            {
                if (col - size >= 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row;
                        pos.y = col - i;
                        positions.Add(pos);
                    }
                }
                else // row
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row;
                        pos.y = col + i;
                        positions.Add(pos);
                    }
                }
            }
            return positions;
        }

        public ShipConf CheckShipStatus(List<Position> HitPositions)
        {

            carrierSunken = Carrier.Where(C => !HitPositions.Any(H => C.x == H.x && C.y == H.y)).ToList().Count == 0;
            battleshipSunken = Battleship.Where(B => !HitPositions.Any(H => B.x == H.x && B.y == H.y)).ToList().Count == 0;
            cruiserSunken = Cruiser.Where(D => !HitPositions.Any(H => D.x == H.x && D.y == H.y)).ToList().Count == 0;
            submarineSunken = Submarine.Where(S => !HitPositions.Any(H => S.x == H.x && S.y == H.y)).ToList().Count == 0;
            destroyerSunken = Destroyer.Where(P => !HitPositions.Any(H => P.x == H.x && P.y == H.y)).ToList().Count == 0;


            allSunk = carrierSunken && battleshipSunken && destroyerSunken && submarineSunken && destroyerSunken;
            return this;
        }

        public List<Position> ManualPosition(int size, string shipName)
        {
            ForegroundColor = ConsoleColor.Green;
            string cPos = "";
            Position coorPos;
            Dictionary<char, int> dPosition = DictionaryValues.PositionDictionary();
            Dictionary<char, int> dCoordinates = CoordinatesValue.FillDictionary();
            List<Position> positions = new List<Position>();
            do
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine("Input coordinate for <"+ shipName + "> (e.g. A1): ");
                ForegroundColor = ConsoleColor.Blue;
                cPos = ReadLine();
                coorPos = MapConf.AnalyzeInput(cPos, dCoordinates);
                if (coorPos.x == -1 || coorPos.y == -1)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Invalid coordinates!");
                }
                ForegroundColor = ConsoleColor.Green;
            } while (coorPos.x == -1 || coorPos.y == -1);
            
            int row = coorPos.x;
            int col = coorPos.y;
            string pv = "";
            do
            {
                WriteLine("Direction Ship Horizontal(H), Vertical(V): ");
                ForegroundColor = ConsoleColor.Blue;
                pv = ReadLine();
            } while (pv.Trim() == "" || (pv.ToUpper() != "V" && pv.ToUpper() != "H"));
            Clear();
            int direction = dPosition.GetValueOrDefault(Convert.ToChar(pv.ToUpper()));
            if (direction % 2 != 0)
            {
                if (row - size >= 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row - i;
                        pos.y = col;
                        positions.Add(pos);
                    }
                }
                else // col
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row + i;
                        pos.y = col;
                        positions.Add(pos);
                    }
                }
            }
            else
            {
                if (col - size >= 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row;
                        pos.y = col - i;
                        positions.Add(pos);
                    }
                }
                else // row
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row;
                        pos.y = col + i;
                        positions.Add(pos);
                    }
                    
                }
            }
            return positions;
        }
    }
}
