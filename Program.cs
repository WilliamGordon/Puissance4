using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuissanceQuatre
{
    class Program
    {
        //////////////
        //GAME SETUP//
        //////////////

        public static int[,,] CreateTable()
        {
            int[,,] table = new int[6, 7, 2];
            return table;
        }

        public class Player
        {
            public string whichPlayer;

            public Player()
            {
                this.whichPlayer = "playerOne";
            }
        }


        ////////////////
        //GAME DISPLAY//
        ////////////////

        public static void printTable(int[,,] table, Player player)
        {
            var title = new[]
            {
                    @"      $$$$$$$\            $$\                                                                   $$\   $$\     ",
                    @"      $$  __$$\           \__|                                                                  $$ |  $$ |    ",
                    @"      $$ |  $$ |$$\   $$\ $$\  $$$$$$$\  $$$$$$$\  $$$$$$\  $$$$$$$\   $$$$$$$\  $$$$$$\        $$ |  $$ |    ",
                    @"      $$$$$$$  |$$ |  $$ |$$ |$$_____  |$$_____|   \____$$\ $$  __$$\ $$  _____ |$$ __$$\       $$$$$$$$ |    ",
                    @"      $$  ____ /$$ |  $$ |$$ |\$$$$$$\  \$$$$$$\   $$$$$$$ |$$ |  $$ |$$ /      $$$$$$$$ |      \_____$$ |    ",
                    @"      $$ |      $$ |  $$ |$$ | \____$$\  \____$$\ $$  __$$ |$$ |  $$ |$$ |      $$   ____|            $$ |    ",
                    @"      $$ |      \$$$$$$  |$$ |$$$$$$$  |$$$$$$$  |\$$$$$$$ |$$ |  $$ |\$$$$$$$\ \$$$$$$$\             $$ |    ",
                    @"      \__|       \______/ \__|\_______/ \_______/  \_______|\__|  \__| \_______| \_______|            \__|    ",
                    @"                                                                                                              ",
            };

            Console.SetCursorPosition(5, 0);
            Console.WindowWidth = 110;
            Console.WindowHeight = 40;
            Console.WriteLine("\n\n");
            foreach (string line in title)
                Console.WriteLine(line);

            if (player.whichPlayer == "playerOne")
            {
                Console.SetCursorPosition(45, 12);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("PLAYER ONE TURN");
                Console.ResetColor();

            }
            else if (player.whichPlayer == "playerTwo")
            {
                Console.SetCursorPosition(45, 12);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("PLAYER TWO TURN");
                Console.ResetColor();
            }


            int counter = 14;
            for (int i = 0; i < table.GetLength(0); i++)
            {
                Console.SetCursorPosition(20, counter);
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (table[i, j, 1] == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("|       |");
                        Console.ResetColor();
                    }
                    else if (table[i, j, 1] == -1)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("|       |");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("|       |", table[i, j, 0]);
                    }
                }
                counter += 4;
            }
        }

        ////////////
        //GAMEPLAY//
        ////////////


        public static int[] CalculteCoordinate(int x, int y)
        {
            x = (x - 24) / 9;
            y = (y - 14) / 4;
            int[] coordinate = new int[2] { y, x };
            return coordinate;
        }


        public static void Selected(int[,,] table, int[] coordinate, Player player)
        {
            if (coordinate[0] == 5 || table[coordinate[0] + 1, coordinate[1], 1] != 0)
            {
                if (player.whichPlayer == "playerOne" && table[coordinate[0], coordinate[1], 1] == 0)
                {
                    table[coordinate[0], coordinate[1], 1] = 1;
                    player.whichPlayer = "playerTwo";
                }
                else if (player.whichPlayer == "playerTwo" && table[coordinate[0], coordinate[1], 1] == 0)
                {
                    table[coordinate[0], coordinate[1], 1] = -1;
                    player.whichPlayer = "playerOne";
                }
            }
        }

        public static int[] HasWon(int[,,] table, int[] coordonate)
        {
            int y = coordonate[0];
            int x = coordonate[1];

            int[] info = new int[2]; // 1 = player 1 (blue) | -1 = player 2 (red) || 1 = WON | -1 = LOST ||

            if (y + 3 <= 5 && table[y, x, 1] + table[y + 1, x, 1] + table[y + 2, x, 1] + table[y + 3, x, 1] == 4)   //VERTICAL DOWN - PLAYER 1
            {
                info[0] = 1; info[1] = 1;
            }
            else if (y + 3 <= 5 && table[y, x, 1] + table[y + 1, x, 1] + table[y + 2, x, 1] + table[y + 3, x, 1] == -4) //VERTICAL DOWN - PLAYER 2
            {
                info[0] = 1; info[1] = -1;
            }
            else if (x + 3 <= 6 && table[y, x, 1] + table[y, x + 1, 1] + table[y, x + 2, 1] + table[y, x + 3, 1] == 4) //HORIZONTAL RIGHT - PLAYER 1
            {
                info[0] = 1; info[1] = 1;
            }
            else if (x + 3 <= 6 && table[y, x, 1] + table[y, x + 1, 1] + table[y, x + 2, 1] + table[y, x + 3, 1] == -4) //HORIZONTAL RIGHT - PLAYER 2
            {
                info[0] = 1; info[1] = -1;
            }
            else if (x - 3 >= 0 && table[y, x, 1] + table[y, x - 1, 1] + table[y, x - 2, 1] + table[y, x - 3, 1] == 4) //HORIZONTAL LEFT - PLAYER 1
            {
                info[0] = 1; info[1] = 1;
            }
            else if (x - 3 >= 0 && table[y, x, 1] + table[y, x - 1, 1] + table[y, x - 2, 1] + table[y, x - 3, 1] == -4) //HORIZONTAL LEFT - PLAYER 2
            {
                info[0] = 1; info[1] = -1;
            }
            else if (x + 3 <= 6 && y + 3 <= 5 && table[y, x, 1] + table[y + 1, x + 1, 1] + table[y + 2, x + 2, 1] + table[y + 3, x + 3, 1] == 4) // DIAGONAL RIGHT - PLAYER 1 
            {
                info[0] = 1; info[1] = 1;
            }
            else if (x + 3 <= 6 && y + 3 <= 5 && table[y, x, 1] + table[y + 1, x + 1, 1] + table[y + 2, x + 2, 1] + table[y + 3, x + 3, 1] == -4) // DIAGONAL RIGHT - PLAYER 2
            {
                info[0] = 1; info[1] = -1;
            }
            else if (x - 3 >= 0 && y + 3 <= 5 && table[y, x, 1] + table[y + 1, x - 1, 1] + table[y + 2, x - 2, 1] + table[y + 3, x - 3, 1] == 4) // DIAGONAL LEFT - PLAYER 1
            {
                info[0] = 1; info[1] = 1;
            }
            else if (x - 3 >= 0 && y + 3 <= 5 && table[y, x, 1] + table[y + 1, x - 1, 1] + table[y + 2, x - 2, 1] + table[y + 3, x - 3, 1] == -4) // DIAGONAL LEFT - PLAYER 2
            {
                info[0] = 1; info[1] = -1;
            }
            else
            {
                info[0] = 0; info[1] = 0;
            }
            return info;
        }


        static void Main(string[] args)
        {
            Player player = new Player();
            int[,,] table = new int[6, 7, 2];


            //////////////
            //NAVIGATION//
            //////////////


            int x = 24;
            int y = 34;
            ConsoleKey key = new ConsoleKey();

            while (true)
            {
                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (x > 24) { x -= 9; }
                        break;
                    case ConsoleKey.RightArrow:
                        if (x < 78) { x += 9; }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y > 14) { y -= 4; }
                        break;
                    case ConsoleKey.DownArrow:
                        if (y < 34) { y += 4; }
                        break;
                    case ConsoleKey.Enter:

                        //

                        int[] coordinate = CalculteCoordinate(x, y);

                        if (player.whichPlayer == "playerOne" && (table[coordinate[0], coordinate[1], 1] != -1 && table[coordinate[0], coordinate[1], 1] != 1))
                        {
                            Selected(table, CalculteCoordinate(x, y), player);
                        }
                        else if (player.whichPlayer == "playerTwo" && (table[coordinate[0], coordinate[1], 1] != -1 && table[coordinate[0], coordinate[1], 1] != 1))
                        {
                            Selected(table, CalculteCoordinate(x, y), player);
                        }
                        break;
                }

                printTable(table, player);
                Console.SetCursorPosition(x, y);

                if (HasWon(table, CalculteCoordinate(x, y))[0] == 1 && HasWon(table, CalculteCoordinate(x, y))[1] == 1)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Blue;
                    var title = new[]
                    {
                            @"       _______   __       __    __  ________        __      __    ______   __    __   __  __  __      ",
                            @"      |       \ |  \     |  \  |  \|        \      |  \  _ |  \  /      \ |  \  |  \ |  \|  \|  \     ",
                            @"      | $$$$$$$\| $$     | $$  | $$| $$$$$$$$      | $$ / \ | $$|  $$$$$$\| $$\ | $$ | $$| $$| $$     ",
                            @"      | $$__ /$$| $$     | $$  | $$| $$__          | $$/  $\| $$| $$  | $$| $$$\| $$ | $$| $$| $$     ",
                            @"      | $$    $$| $$     | $$  | $$| $$  \         | $$  $$$\ $$| $$  | $$| $$$$\ $$ | $$| $$| $$     ",
                            @"      | $$$$$$$\| $$_____| $$  | $$| $$$$$         | $$ $$\$$\$$| $$  | $$| $$\$$ $$  \$$ \$$ \$$     ",
                            @"      | $$__ /$$| $$_____| $$__/ $$| $$_____       | $$$$  \$$$$| $$__/ $$| $$ \$$$$  __  __  __      ",
                            @"      | $$    $$| $$     \\$$    $$| $$     \      | $$$    \$$$ \$$    $$| $$  \$$$ |  \|  \|  \     ",
                            @"       \$$$$$$$  \$$$$$$$$ \$$$$$$  \$$$$$$$$       \$$      \$$  \$$$$$$  \$$   \$$  \$$ \$$ \$$     ",
                            @"                                                                                                      ",
                    };

                    Console.SetCursorPosition(5, 0);
                    Console.WindowWidth = 110;
                    Console.WindowHeight = 40;
                    Console.WriteLine("\n\n");
                    foreach (string line in title)
                        Console.WriteLine(line);

                    Console.ResetColor();
                    Console.ReadKey();

                    break;
                }
                else if (HasWon(table, CalculteCoordinate(x, y))[0] == 1 && HasWon(table, CalculteCoordinate(x, y))[1] == -1)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Red;
                    var title = new[]
            {
                    @"       _______   ________  _______             __      __    ______   __    __   __  __  __      ",
                    @"      |       \ |        \|       \           |  \  _ |  \  /      \ |  \  |  \ |  \|  \|  \     ",
                    @"      | $$$$$$$\| $$$$$$$$| $$$$$$$\          | $$ / \ | $$|  $$$$$$\| $$\ | $$ | $$| $$| $$     ",
                    @"      | $$__| $$| $$__    | $$  | $$          | $$/  $\| $$| $$  | $$| $$$\| $$ | $$| $$| $$     ",
                    @"      | $$    $$| $$  \   | $$  | $$          | $$  $$$\ $$| $$  | $$| $$$$\ $$ | $$| $$| $$     ",
                    @"      | $$$$$$$\| $$$$$   | $$  | $$          | $$ $$\$$\$$| $$  | $$| $$\$$ $$  \$$ \$$ \$$     ",
                    @"      | $$  | $$| $$_____ | $$__/ $$          | $$$$  \$$$$| $$__/ $$| $$ \$$$$  __  __  __      ",
                    @"      | $$  | $$| $$     \| $$    $$          | $$$    \$$$ \$$    $$| $$  \$$$ |  \|  \|  \     ",
                    @"       \$$   \$$ \$$$$$$$$ \$$$$$$$            \$$      \$$  \$$$$$$  \$$   \$$  \$$ \$$ \$$     ",
                    @"                                                                                                 ",
            };

                    Console.SetCursorPosition(5, 0);
                    Console.WindowWidth = 110;
                    Console.WindowHeight = 40;
                    Console.WriteLine("\n\n");
                    foreach (string line in title)
                        Console.WriteLine(line);

                    Console.ResetColor();
                    Console.ReadKey();

                    break;
                }
            }
            Console.WriteLine(player.whichPlayer);
        }
    }
}
