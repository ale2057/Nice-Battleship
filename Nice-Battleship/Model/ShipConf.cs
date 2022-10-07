using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public int stepsTaken { get; set; } = 0;

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

        public List<Position> GeneratePosistion(int size, List<Position> AllPosition)
        {
            List<Position> positions = new List<Position>();

            bool IsExist = false;

            do
            {
                positions = GeneratePositionRandomly(size);
                IsExist = positions.Where(AP => AllPosition.Exists(ShipPos => ShipPos.x == AP.x && ShipPos.y == AP.y)).Any();
            }
            while (IsExist);

            AllPosition.AddRange(positions);


            return positions;
        }

        public List<Position> GeneratePositionRandomly(int size)
        {
            List<Position> positions = new List<Position>();

            int direction = random.Next(1, size);//odd for horizontal and even for vertical
                                                 //pick row and column
            int row = random.Next(1, 11);
            int col = random.Next(1, 11);

            if (direction % 2 != 0)
            {
                //left first, then right
                if (row - size > 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        Position pos = new Position();
                        pos.x = row - i;
                        pos.y = col;
                        positions.Add(pos);
                    }
                }
                else // row
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
                //top first, then bottom
                if (col - size > 0)
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

        public ShipConf()
        {

            Carrier = GeneratePosistion(carrier, shipCoordinates);
            Battleship = GeneratePosistion(battleship, shipCoordinates);
            Destroyer = GeneratePosistion(cruiser, shipCoordinates);
            Submarine = GeneratePosistion(submarine, shipCoordinates);
            Destroyer = GeneratePosistion(destroyer, shipCoordinates);
        }
    }
}
