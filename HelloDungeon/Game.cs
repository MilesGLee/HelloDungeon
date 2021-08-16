using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeon
{
    class Game
    {
        public void Run()
        {
            Console.Title = "Hokus Pokus Cowboys: A text-based rougelite";
            //Runs Main Menu
            MainMenu();
        }

        public class PlayerEntity //Setup of all the variables needed for the player
        {
            public int lvl; //Level of player
            public int ap; //Attack Power of player
            public int armor; //Armor of player, reduces physical damage taken
            public int ward; //Warding power is the same as armor, but for magical damage
            public int EXP; //Current experience of the player
            public int MaxEXP; //Experience needed to level up
            public int crit; //Critical hit chance (doubles damage)
            public int searches; //How many times a player can search
            public List<string> Items = new List<string>(); //List of items player is holding
        }

        public void InitilizePlayer(PlayerEntity player, int LVL, int AP, int ARMOR, int WARD, int EXP, int MAXEXP, int CRIT) 
        {
            player.lvl = LVL;
            player.ap = AP;
            player.armor = ARMOR;
            player.ward = WARD;
            player.EXP = EXP;
            player.MaxEXP = MAXEXP;
            player.crit = CRIT;
        }

        public PlayerEntity Player; //Setup player variable

        public void MainMenu() //Executes the main menu
        {
            Player = new PlayerEntity();
            string command;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Welcome to ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Hokus ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Pokus ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Cowboys");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("! Please type(Ignore brackets []): [Play], [Help], [Quit]");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (command != "Play")
            {
                if (command == "Help")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("The basic way this game progresses is you as the player progress through a total of 10 towns, each containing one sherif boss. To move onto the next town you must slay the sherif. While inside a town you can perform a [Search] action, which can lead to multiple events occuring. You will only be able to search a limited amount of times so be weary.");
                    Console.WriteLine("As most text-based games play, you type in simple yet precise words or phrases into the text entry to perform actions. EX: [Check stats], [Challenge Sherif], [Use Heal]");
                }
                if (command == "Quit")
                {
                    Environment.Exit(0);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            InitilizePlayer(Player, 1, 10, 0, 0, 0, 100, 1);
            Player.searches = 5;
            Stage1();
        }
        public void Stage1() 
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You walk into a dusty town in the middle of nowhere...");
            string command;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (command != "Challenge Sherif")
            {
                if (command == "Check stats") 
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("----------Player Stats----------");
                    Console.WriteLine($"Level: {Player.lvl} [{Player.EXP}/{Player.MaxEXP}]");
                    Console.WriteLine($"Attack Power: {Player.ap}");
                    Console.WriteLine($"Armor: {Player.armor}");
                    Console.WriteLine($"Warding Power: {Player.ward}");
                    Console.WriteLine($"Critical Hit Chance: {Player.crit}");
                    Console.WriteLine("--------------------------------");
                }
                if (command == "Search" && Player.searches > 0) 
                {
                    Random rnd = new Random();
                    int sr = rnd.Next(1, 6);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
        }
    }
}
