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
        PlayersSet ps= new PlayersSet() { Player1 = new Player(), Player2 = new Player() };
        public void start()
        {
            Dictionary<char, bool> dResponse = DictionaryValues.PositionManual();
            string pr = "";
            DrawWelcome();
            SetPlayersData();
            do
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine("Captain 1 **"+ps.Player1.Name+" Place ships automatically?Y/N: ");
                ForegroundColor = ConsoleColor.Blue;
                pr = ReadLine();
            } while (pr.Trim() == "" || (pr.ToUpper() != "Y" && pr.ToUpper() != "N"));
            ShipConf player1Navys = new ShipConf(dResponse.GetValueOrDefault(Convert.ToChar(pr.ToUpper())));
            pr = "";
            do
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine("Captain 2 **" + ps.Player2.Name + " Place ships automatically?Y/N: ");
                ForegroundColor = ConsoleColor.Blue;
                pr = ReadLine();
            } while (pr.Trim() == "" || (pr.ToUpper() != "Y" && pr.ToUpper() != "N"));
            ShipConf player2Navis = new ShipConf(dResponse.GetValueOrDefault(Convert.ToChar(pr.ToUpper())));
            Clear();
            DrawWelcome();
            GetPlayersData();
            MapConf.PrintSecondMapHeader();
            MapConf.PrintMap(player1Navys.fireCoordinates, player1Navys, player2Navis, false, true);
            ProcessGame(player1Navys, player2Navis);
        }

        static void DrawWelcome()
        {
            string s = ">>> BATTLESHIP GAME <<<";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            ForegroundColor = ConsoleColor.Green;
            Write(">>> ");
            ForegroundColor = ConsoleColor.White;
            Write("BATTLESHIP GAME");
            ForegroundColor = ConsoleColor.Green;
            Write(" <<<\n\n");
            ForegroundColor = ConsoleColor.White;
        }

        public void SetPlayersData()
        {
            ForegroundColor = ConsoleColor.Green;
            string p1 = "", p2 = "";
            WriteEfect("Welcome to Battle ship game...\n");
            WriteEfect("Please press ENTER to continue...");
            ReadLine();
            do
            {
                WriteEfect("Input Captain 1 name: ");
                ForegroundColor = ConsoleColor.Blue;
                p1 = ReadLine();
            } while (p1.Trim() == "");
            ps.Player1.Name = p1;
            ps.Player1.Win = 0;
            do
            {
                ForegroundColor = ConsoleColor.Green;
                WriteEfect("Input Captain 2 name: ");
                ForegroundColor = ConsoleColor.Blue;
                p2 = ReadLine();
            } while (p2.Trim() == "");
            ps.Player2.Name = p2;
            ps.Player2.Win = 0;
        }

        public void GetPlayersData()
        {
            string s = "Captain: " + ps.Player2.Name + "(wins: " + ps.Player2.Win + ")       Captain: " + ps.Player1.Name + "(wins: " + ps.Player1.Win + ")";
            ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            WriteLine(s + "\n");
        }

        public void WriteEfect(string text)
        {
            for(int i=0; i < text.Length; i++)
            {
                Write(text[i]);
                Thread.Sleep(20);
            }
        }

        void ProcessGame(ShipConf p1N, ShipConf p2N)
        {
            for (int round = 1; round < 101; round++)
            {
                Position position = new Position();

                Dictionary<char, int> dCoordinates = CoordinatesValue.FillDictionary();
                ForegroundColor = ConsoleColor.White;
                WriteLine("Captain **"+ps.Player1.Name+" enter firing position (e.g. A1): ");
                string input = ReadLine();
                position = MapConf.AnalyzeInput(input, dCoordinates);

                if (position.x == -1 || position.y == -1)
                {
                    WriteLine("Invalid coordinates!");
                    round--;
                    continue;
                }

                if (p1N.fireCoordinates.Any(EFP => EFP.x == position.x && EFP.y == position.y))
                {
                    WriteLine("This coordinate already being shot.");
                    round--;
                    continue;
                }

                var index = p1N.fireCoordinates.FindIndex(p => p.x == position.x && p.y == position.y);

                if (index == -1)
                    p1N.fireCoordinates.Add(position);

                Clear();


                p2N.shipCoordinates.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
                p2N.CheckShipStatus(p1N.fireCoordinates);

                DrawWelcome();
                GetPlayersData();
                MapConf.PrintSecondMapHeader();

                MapConf.PrintMap(p1N.fireCoordinates, p1N, p2N, true, false);

                MapConf.Advertisements(p1N, true, ps);
                MapConf.Advertisements(p2N, false, ps);
                if (p2N.allSunk || p1N.allSunk) { break; }

                //TURN TO FIRE PLAYER 2



                input = "";
                ForegroundColor = ConsoleColor.White;
                WriteLine("Captain **" + ps.Player2.Name + " enter firing position (e.g. A1): ");
                input = ReadLine();
                position = MapConf.AnalyzeInput(input, dCoordinates);



                if (position.x == -1 || position.y == -1)
                {
                    WriteLine("Invalid coordinates!");
                    round--;
                    continue;
                }

                if (p2N.fireCoordinates.Any(EFP => EFP.x == position.x && EFP.y == position.y))
                {
                    WriteLine("This coordinate already being shot.");
                    round--;
                    continue;
                }

                var index2 = p2N.fireCoordinates.FindIndex(p => p.x == position.x && p.y == position.y);

                if (index2 == -1)
                    p2N.fireCoordinates.Add(position);

                Clear();

                

                DrawWelcome();
                GetPlayersData();
                MapConf.PrintSecondMapHeader();

                MapConf.PrintMap(p1N.fireCoordinates, p1N, p2N, false, true);

                p1N.shipCoordinates.OrderBy(o => o.x).ThenBy(n => n.y).ToList();
                p1N.CheckShipStatus(p2N.fireCoordinates);

                MapConf.Advertisements(p1N, true, ps);
                MapConf.Advertisements(p2N, false, ps);
                if (p2N.allSunk || p1N.allSunk) { break; }

            }
        }
       
    }
}
