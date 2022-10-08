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
            bool isShowShips = true;
            string pr = "";
            DrawWelcome();
            SetPlayersData();
            do
            {
                WriteLine("Captain 1 "+ps.Player1.Name+" Place ships automatically?Y/N: ");
                pr = ReadLine();
            } while (pr.Trim() == "" || (pr.ToUpper() != "Y" && pr.ToUpper() != "N"));
            ShipConf player1Navys = new ShipConf(dResponse.GetValueOrDefault(Convert.ToChar(pr.ToUpper())));
            pr = "";
            do
            {
                WriteLine("Captain 1 " + ps.Player2.Name + " Place ships automatically?Y/N: ");
                pr = ReadLine();
            } while (pr.Trim() == "" || (pr.ToUpper() != "Y" && pr.ToUpper() != "N"));
            ShipConf player2Navis = new ShipConf(dResponse.GetValueOrDefault(Convert.ToChar(pr.ToUpper())));
            Clear();
            DrawWelcome();
            GetPlayersData();
            MapConf.PrintSecondMapHeader();
            MapConf.PrintMap(player1Navys.fireCoordinates, player1Navys, player2Navis, isShowShips);
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
            WriteEfect("Please press any button to coninue...");
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
            string s = "Captain 1: " + ps.Player1.Name + "(wins: " + ps.Player1.Win + ")       Captain 2: " + ps.Player2.Name + "(" + ps.Player2.Win + ")";
            ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            WriteLine(s);
        }

        public void WriteEfect(string text)
        {
            for(int i=0; i < text.Length; i++)
            {
                Write(text[i]);
                Thread.Sleep(20);
            }
        }
       
    }
}
