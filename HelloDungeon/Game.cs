using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeon
{
    class Game
    {
        //Runtime Variables
        public int nearbyItem;
        public int stage;
        public bool inCombat;
        public int maxSearches;
        public bool beatenSheriff = false; //if player has beaten boss
        public List<string> allSpells = new List<string>();
        public bool shopType;
        public string shopSlot1;
        public string shopSlot2;
        public string shopSlot3;
        public int shopCost;
        public bool distracting;
        public bool houdini;

        public void Run()
        {
            ListSpells();
            Console.Title = "Hocus Pocus Cowboys: A text-based roguelite";
            MainMenu();
        }

        //Player Variables
        public class Player
        {
            public int lvl; //player level
            public int cash; //Player money
            public int hp; //player hp
            public int maxHP; //player hp
            public int atk; //player attack power
            public int arm; //player armor
            public int ward; //player magic armor
            public int exp; //current player xp
            public int maxexp; //player xp needed to level up
            public int crit; //player critical hit chance
            public int searches; //amount of times player can search
            public int luck; //chance manipulating stat
            public int casts; 
            public int maxcasts; 
            public List<string> Items = new List<string>(); //Item List
            public List<string> Spells = new List<string>(); //Spell list
        } //Da player class, what else you expect?

        public Player player = new Player();
        public void InitializePlayer(int LVL, int CASH, int HP, int MAXHP, int ATK, int ARM, int WARD, int EXP, int MAXEXP, int CRIT, int SEARCHES, int LUCK, int CASTS, int MAXCASTS)
        {
            player.lvl = LVL;
            player.hp = HP;
            player.maxHP = MAXHP;
            player.atk = ATK;
            player.arm = ARM;
            player.ward = WARD;
            player.exp = EXP;
            player.maxexp = MAXEXP;
            player.crit = CRIT;
            player.searches = SEARCHES;
            player.luck = LUCK;
            player.cash = CASH;
            player.casts = CASTS;
            player.maxcasts = MAXCASTS;
        } //Setup player stats
        //Entity Variables
        public class Entity
        {
            public string name;
            public int level;
            public int health;
            public int attack;
            public int armor;
            public bool boss;
        }
        public Entity currEnemy = new Entity();
        public void CreateEntity(string NAME, int LEVEL, int HEALTH, int ATTACK, int ARMOR, bool BOSS)
        {
            currEnemy.name = NAME;
            currEnemy.level = LEVEL;
            currEnemy.health = HEALTH;
            currEnemy.attack = ATTACK;
            currEnemy.armor = ARMOR;
            currEnemy.boss = BOSS;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"You've encountered a level {currEnemy.level} {currEnemy.name}");
            inCombat = true;
        } //Streamlined proccess of making enemies 
        //Public Runtime Voids
        public void ListSpells() 
        {
            allSpells.Add("Distracting Tumbleweed");
            allSpells.Add("Houdini Rounds");
            allSpells.Add("Summon Mead");
        } //Add spells to AllSpells list
        public void TextHelp()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("As stated this is a text based roguelite! To play this game you will have to input simple yet precise phrases into the command prompt to perform actions.");
            Console.WriteLine("These actions include (Remember it is case sensitive): 'Search', 'Check stats', 'Challenge Sheriff', 'Attack', 'Collect', 'Buy #'");
            Console.WriteLine("The story is as follows: This universe takes place in a fantasy western, where cowboy vigilate use what they call 'Hocus Pocus'(Magic). You, as a new sheriff in town, must make your way through 10 towns. Each town contains a Sheriff boss you must defeat in order to move onto the next stage. You are able to search each area a limited amount of times for a variety of events to happen. If your health depletes to 0, you die and the run is over.");
        } //List of commands and general help
        public void CheckStats()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("----------Player Stats----------");
            Console.WriteLine($"Level: {player.lvl} ({player.exp}/{player.maxexp})");
            Console.WriteLine($"Cash: ${player.cash}");
            Console.WriteLine($"Health: {player.hp}");
            Console.WriteLine($"Attack Power: {player.atk}");
            Console.WriteLine($"Armor: {player.arm}");
            Console.WriteLine($"Warding Power: {player.ward}");
            Console.WriteLine($"Critical Hit Chance: {player.crit}");
            Console.WriteLine("----------Player Items----------");
            Console.WriteLine($"!> You have [{player.Items.Count}] items.");
            foreach (string A in player.Items)
            {
                Console.WriteLine($"[{player.Items.FindIndex(0, player.Items.Count, A.StartsWith)}]:{A}");
            }
            Console.WriteLine("----------Player Spells---------");
            Console.WriteLine($"!> You know [{player.Spells.Count}] cantrips. And have {player.casts} casts remaining");
            foreach (string A in player.Spells)
            {
                Console.WriteLine($"[{player.Spells.FindIndex(0, player.Spells.Count, A.StartsWith)}]:{A}");
            }
            Console.WriteLine("--------------------------------");

        } //Prints players stats
        public void SearchGame() 
        {
            nearbyItem = 0;
            player.searches -= 1;
            Random rnd = new Random();
            int roll = rnd.Next(1, 200); // creates a number between 1 and 200
            int roll2 = rnd.Next(1, 3); // creates a number between 1 and 2
            if (roll > (100 - player.ward) && roll2 == 1) 
            {
                int itemRoll = rnd.Next(1, 9);
                if (itemRoll == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have searched and found an item! It looks to be a Focus Lens.");
                    nearbyItem = itemRoll;
                }
                if (itemRoll == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have searched and found an item! It looks to be a Lead Lined Stetson.");
                    nearbyItem = itemRoll;
                }
                if (itemRoll == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have searched and found an item! It looks to be a infused flask");
                    nearbyItem = itemRoll;
                }
                if (itemRoll == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have searched and found an item! It looks to be an Extra boot spur.");
                    nearbyItem = itemRoll;
                }
                if (itemRoll == 5)
                {
                    int chance = rnd.Next(1, 101);
                    if (chance > 20 && player.luck > 0)
                    {
                        int rerolls = player.luck;
                        while (rerolls != 0 && chance > 20)
                        {
                            chance = rnd.Next(1, 101);
                            rerolls--;
                        }
                    }
                    if (chance < 20)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("You have searched and found an item! ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Oh whats this!? It appears to be a hidden map!");
                        nearbyItem = itemRoll;
                    }
                    else
                    {
                        int randItem = rnd.Next(1, 5);
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (randItem == 1)
                            Console.WriteLine("You have searched and found an item! It looks to be a Focus Lens.");
                        if (randItem == 2)
                            Console.WriteLine("You have searched and found an item! It looks to be a Lead Lined Stetson.");
                        if (randItem == 3)
                            Console.WriteLine("You have searched and found an item! It looks to be a Infused Flask.");
                        if (randItem == 4)
                            Console.WriteLine("You have searched and found an item! It looks to be an Extra boot spur.");
                        nearbyItem = randItem;
                    }
                }
                if (itemRoll == 6)
                {
                    int chance = rnd.Next(1, 101);
                    if (chance > 20 && player.luck > 0)
                    {
                        int rerolls = player.luck;
                        while (rerolls != 0 && chance > 20)
                        {
                            chance = rnd.Next(1, 101);
                            rerolls--;
                        }
                    }
                    if (chance < 20)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("You have searched and found an item! ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Oh woulda lookit here! You found a lucky coin!");
                        nearbyItem = itemRoll;
                    }
                    else
                    {
                        int randItem = rnd.Next(1, 5);
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (randItem == 1)
                            Console.WriteLine("You have searched and found an item! It looks to be a Focus Lens.");
                        if (randItem == 2)
                            Console.WriteLine("You have searched and found an item! It looks to be a Lead Lined Stetson.");
                        if (randItem == 3)
                            Console.WriteLine("You have searched and found an item! It looks to be a Infused Flask.");
                        if (randItem == 4)
                            Console.WriteLine("You have searched and found an item! It looks to be an Extra boot spur.");
                        nearbyItem = randItem;
                    }
                }
                if (itemRoll == 7) 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have searched and found an item! It looks to be a Warding Stone.");
                    nearbyItem = itemRoll;
                }
                if (itemRoll == 8)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have searched and found an item! It looks to be a Tome Page.");
                    nearbyItem = itemRoll;
                }
            } //Player found an item event
            if (roll > (100 - player.ward) && roll2 == 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("You turn a courner and encounter a spellsmith, offering you goods...");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[Spellsmith]: Howdy outsider. I have many a spells for you to pick from...");
                int spellRoll = rnd.Next(0, (allSpells.Count));
                int spellRoll2 = rnd.Next(0, (allSpells.Count));
                int spellRoll3 = rnd.Next(0, (allSpells.Count));
                string slot1 = allSpells[spellRoll];
                string slot2 = allSpells[spellRoll2];
                string slot3 = allSpells[spellRoll3];
                shopCost = 60;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"1:{slot1} ${shopCost}, 2:{slot2} ${shopCost}, 3:{slot3} ${shopCost}");
                shopType = false;
                shopSlot1 = slot1;
                shopSlot2 = slot2;
                shopSlot3 = slot3;
            } //Player encounters a spellsmith
            if (roll < (100 - player.ward)) 
            {
                int lvl = rnd.Next(stage, (stage * 3));
                if (stage == 1)
                    CreateEntity("Theif", lvl, 20 + (10 * lvl), 7 + (2 * lvl), 3 + (2 * lvl), false);
                if (stage == 2)
                    CreateEntity("Zombie", lvl, 20 + (10 * lvl), 10 + (2 * lvl), 3 + (2 * lvl), false);
                if (stage == 3)
                    CreateEntity("Amalgamation", lvl, 20 + (10 * lvl), 12 + (2 * lvl), 3 + (2 * lvl), false);
                if (stage == 4)
                    CreateEntity("Fire Spirit", lvl, 20 + (10 * lvl), 14 + (2 * lvl), 3 + (2 * lvl), false);
                if (stage == 5)
                    CreateEntity("Flying Wyvern", lvl, 20 + (10 * lvl), 16 + (2 * lvl), 3 + (2 * lvl), false);
                if (stage == 6)
                    CreateEntity("Ice Golem", lvl, 20 + (10 * lvl), 19 + (2 * lvl), 3 + (2 * lvl), false);
                if (stage == 7)
                    CreateEntity("Conduit", lvl, 20 + (10 * lvl), 22 + (2 * lvl), 3 + (2 * lvl), false);
                if (stage == 8)
                    CreateEntity("Dark Entity", lvl, 20 + (10 * lvl), 24 + (2 * lvl), 3 + (2 * lvl), false);
                if (stage == 9)
                    CreateEntity("Vampire", lvl, 20 + (10 * lvl), 26 + (2 * lvl), 3 + (2 * lvl), false);
                if (stage == 10)
                    CreateEntity("TZAFFICDV", lvl, 20 + (10 * lvl), 30 + (2 * lvl), 3 + (2 * lvl), false);
            } //Player encounters enemy event
        } //Random event selection
        public void CollectItem()
        {
            if (nearbyItem == 1)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You collected a Focus Lens!");
                player.Items.Add("Focus Lens");
                player.crit += 10;
                nearbyItem = 0;
            } //Focus lens
            if (nearbyItem == 2)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You collected a Lead Lined Stetson!");
                player.Items.Add("Lead Lined Stetson");
                player.arm += 2;
                nearbyItem = 0;
            } //Lead Lined Stetson
            if (nearbyItem == 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You collected a Infused Flask!");
                player.Items.Add("Infused Flask");
                nearbyItem = 0;
            } //Infused Flask
            if (nearbyItem == 4)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You collected an Extra boot spur!");
                player.Items.Add("Extra boot spur");
                player.atk += 5;
                nearbyItem = 0;
            } //Boot spur
            if (nearbyItem == 5)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You collected a Hidden Map");
                player.Items.Add("Hidden Map");
                maxSearches += 1;
                nearbyItem = 0;
            } //Hidden Map
            if (nearbyItem == 6)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You picked up a lucky coin");
                player.Items.Add("Lucky Coin");
                player.luck += 1;
                nearbyItem = 0;
            } //Lucky coin
            if (nearbyItem == 7) 
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You collected a Warding Stone!");
                player.Items.Add("Warding Stone");
                player.ward += 10;
                nearbyItem = 0;
            } //Warding Stone
            if (nearbyItem == 8)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You collected a Tome Page!");
                player.Items.Add("Tome Page");
                player.maxcasts += 1;
                nearbyItem = 0;
            } //Tome Page
        } //Collecting found items
        public void BuyItem(int slot) 
        {
            if(slot == 1 && player.Spells.Contains(shopSlot1) == false)
                player.cash -= shopCost;
            if (slot == 2 && player.Spells.Contains(shopSlot2) == false)
                player.cash -= shopCost;
            if (slot == 3 && player.Spells.Contains(shopSlot3) == false)
                player.cash -= shopCost;
            if (slot == 1)
                player.Spells.Add(shopSlot1);
            if (slot == 2)
                player.Spells.Add(shopSlot2);
            if (slot == 3)
                player.Spells.Add(shopSlot3);
            shopSlot1 = null;
            shopSlot2 = null;
            shopSlot3 = null;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[Spellsmith]: Alright enough! Be gone with you...");
        } //Player buys items from shops
        public void AttackEntity(Entity Victim)
        {
            Random rnd = new Random();
            int critRoll = rnd.Next(1, 101);
            if (critRoll > player.crit && player.luck > 0)
            {
                int rerolls = player.luck;
                while (rerolls != 0 && critRoll > player.crit)
                {
                    critRoll = rnd.Next(1, 101);
                    rerolls--;
                }
            }
            if (houdini == true)
            {
                critRoll = 0;
            }
            if (critRoll < player.crit)
            {
                int critAtk = player.atk * 2;
                if (Victim.armor < critAtk)
                {
                    Victim.health -= critAtk;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Bingo! You've hit a perfectly lined shot for a critical strike! The enemy has {Victim.health} health left.");
                }
            }
            else
            {
                if (Victim.armor < player.atk)
                {
                    Victim.health -= player.atk;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You hip-fire your revolver at your foe. The enemy has {Victim.health} health left.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"The enemy merely shrugged off your attack");
                }
            }
            if (Victim.health <= 0 && Victim.boss == false)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{Victim.name} Defeated! You've gained {(Victim.level + stage) * 20} Exp! and you've collected ${(stage * 10) + Victim.level}");
                player.exp += ((Victim.level + stage) * 20);
                player.cash += (stage * 10) + Victim.level;
                inCombat = false;
                if (player.exp >= player.maxexp)
                {
                    player.lvl += 1;
                    player.exp = 0;
                    player.maxexp = 100 * player.lvl;
                    player.maxHP += 10 * player.lvl;
                    player.hp = player.maxHP;
                    player.atk += 10;
                    player.arm += 1;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"You've leveled up! You can feel yourself growing stronger... Level {player.lvl} acquired!");
                }
            }
            if (Victim.health <= 0 && Victim.boss == true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Boss {Victim.name} Defeated! You've gained {(Victim.level + stage) * 20} Exp! and you've collected ${(stage * 10) + Victim.level}. And proceed to the next town...");
                player.exp += ((Victim.level + stage) * 20);
                player.cash += (stage * 10) + Victim.level;
                inCombat = false;
                if (player.exp >= player.maxexp)
                {
                    player.lvl += 1;
                    player.exp = 0;
                    player.maxexp = 100 * player.lvl;
                    player.maxHP += 10 * player.lvl;
                    player.hp = player.maxHP;
                    player.atk += 10;
                    player.arm += 1;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"You've leveled up! You can feel yourself growing stronger... Level {player.lvl} acquired!");
                }
                beatenSheriff = true;
            }
            houdini = false;
        } //When player attacks and entity
        public void AttackPlayer()
        {
            int damageTaken;
            int postHP;
            int preHP;
            preHP = player.hp;
            postHP = player.hp - (currEnemy.attack - player.arm);
            damageTaken = preHP - postHP;
            if (distracting == false)
            {
                if (player.arm < damageTaken)
                {
                    player.hp -= damageTaken;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"The {currEnemy.name} lashes out at you dealing {damageTaken} damage to you.");
                    if (player.hp < 50 && player.Items.Contains("Infused Flask"))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($"After taking a heavy blow you swifty chug your Infused Flask");
                        player.hp += 50;
                        player.Items.Remove("Infused Flask");
                    }
                    if (player.hp <= 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"You have died... You made it a total of {stage} stages. Better luck next time");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("You were able to nimbly avoid the enemies attack.");
                }
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The {currEnemy.name} tries to attack you, but misses due to a tumbleweed rolling by that caught its eye");
                distracting = false;
            }
        } //When the player is attacked
        public void UseSpell(string spell) 
        {
            if (spell == allSpells[0]) 
            {
                distracting = true;
                player.casts -= 1;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Distracting Tumbleweed has been successfully casted, the next attack against you will surley miss");
            }
            if (spell == allSpells[1])
            {
                houdini = true;
                player.casts -= 1;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Houdini Rounds has been successfully casted, your next attack has a 100% chance to critical strike");
            }
            if (spell == allSpells[2])
            {
                player.hp += 50;
                if (player.hp > player.maxHP)
                    player.hp = player.maxHP;
                player.casts -= 1;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Summon Mead has been successfully casted, you got a nice swig from your mead, you healed for 50 health");
            }
        } //For casting spells

        //Scene Voids
        public void MainMenu()
        {
            string command;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Welcome to ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Hocus ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Pocus ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Cowboys");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("! Made by Miles Lee");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Please type one of these without the brackets: [Play], [Help], [Quit]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine(); //Get players input

            while (command != "Play")
            {
                if (command == "Help")
                {
                    TextHelp();
                }
                if (command == "Quit")
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Environment.Exit(0);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            maxSearches = 5; //set plays searches
            InitializePlayer(1, 100, 100, 100, 10, 0, 0, 0, 100, 1, maxSearches, 0, 3, 3); // Set players stats
            Console.Clear();
            Stage1();
        }
        public void Stage1()
        {
            //These next four lines are the setup for each stage
            beatenSheriff = false;
            player.casts = player.maxcasts;
            player.searches = maxSearches;
            stage = 1;
            bool damageCheck = false;
            Console.Title = "Hocus Pocus Cowboys: Sandthorough";
            string command;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You arrive in a dusty town in the middle of a desert... What do you do...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (beatenSheriff == false)
            {
                //Casting Spells
                if (command == $"Cast {allSpells[0]}" && player.casts > 0 && player.Spells.Contains(allSpells[0]))
                    UseSpell(allSpells[0]);
                if (command == $"Cast {allSpells[1]}" && player.casts > 0 && player.Spells.Contains(allSpells[1]))
                    UseSpell(allSpells[1]);
                if (command == $"Cast {allSpells[2]}" && player.casts > 0 && player.Spells.Contains(allSpells[2]))
                    UseSpell(allSpells[2]);
                //Buying items from shop
                if (inCombat == false && command == "Buy 1" && shopSlot1 != null && player.cash >= shopCost)
                    BuyItem(1);
                if (inCombat == false && command == "Buy 2" && shopSlot2 != null && player.cash >= shopCost)
                    BuyItem(2);
                if (inCombat == false && command == "Buy 3" && shopSlot3 != null && player.cash >= shopCost)
                    BuyItem(3);
                //Fight the boss
                if (inCombat == false && command == "Challenge Sheriff")
                    CreateEntity("Sheriff of Sandthorough", 7, 150, 10, 0, true);
                //Attack an enemy
                if (inCombat == true && command == $"Attack")
                {
                    AttackEntity(currEnemy);
                    damageCheck = false;
                }
                if (command == "Check stats")
                    CheckStats();
                if (command == "Help")
                    TextHelp();
                //Search and collect items
                if (command == "Search" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Search" && player.searches <= 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You cannot search anymore");
                }
                if (command == "Search" && player.searches > 0 && inCombat == false)
                    SearchGame();
                if (command == "Collect" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Collect" && nearbyItem == 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No items to collect");
                }
                if (command == "Collect" && nearbyItem != 0 && inCombat == false)
                    CollectItem();
                //make sure enemies attack player at the end of a round
                if (inCombat == true && damageCheck == false)
                {
                    AttackPlayer();
                    damageCheck = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            Console.Clear();
            Stage2();
        }
        public void Stage2()
        {
            beatenSheriff = false;
            player.searches = maxSearches;
            player.casts = player.maxcasts;
            stage = 2;
            bool damageCheck = false;
            Console.Title = "Hocus Pocus Cowboys: Rottenburgh";
            string command;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("You arrive in a seemingly abandoned town... What do you do...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (beatenSheriff == false)
            {
                if (command == $"Cast {allSpells[0]}" && player.casts > 0 && player.Spells.Contains(allSpells[0]))
                    UseSpell(allSpells[0]);
                if (command == $"Cast {allSpells[1]}" && player.casts > 0 && player.Spells.Contains(allSpells[1]))
                    UseSpell(allSpells[1]);
                if (command == $"Cast {allSpells[2]}" && player.casts > 0 && player.Spells.Contains(allSpells[2]))
                    UseSpell(allSpells[2]);
                if (inCombat == false && command == "Buy 1" && shopSlot1 != null && player.cash >= shopCost)
                    BuyItem(1);
                if (inCombat == false && command == "Buy 2" && shopSlot2 != null && player.cash >= shopCost)
                    BuyItem(2);
                if (inCombat == false && command == "Buy 3" && shopSlot3 != null && player.cash >= shopCost)
                    BuyItem(3);
                if (inCombat == false && command == "Challenge Sheriff")
                    CreateEntity("Undead Sheriff of Rottenburgh", 10, 200, 25, 5, true);
                if (inCombat == true && command == $"Attack")
                {
                    AttackEntity(currEnemy);
                    damageCheck = false;
                }
                if (command == "Check stats")
                    CheckStats();
                if (command == "Help")
                    TextHelp();
                if (command == "Search" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Search" && player.searches <= 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You cannot search anymore");
                }
                if (command == "Search" && player.searches > 0 && inCombat == false)
                    SearchGame();
                if (command == "Collect" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Collect" && nearbyItem == 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No items to collect");
                }
                if (command == "Collect" && nearbyItem != 0 && inCombat == false)
                    CollectItem();
                if (inCombat == true && damageCheck == false)
                {
                    AttackPlayer();
                    damageCheck = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            Console.Clear();
            Stage3();
        }
        public void Stage3()
        {
            beatenSheriff = false;
            player.searches = maxSearches;
            player.casts = player.maxcasts;
            stage = 3;
            bool damageCheck = false;
            Console.Title = "Hocus Pocus Cowboys: Fractalie";
            string command;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("You waltz into a town so confusing your head begins to swim... What do you do...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (beatenSheriff == false)
            {
                if (command == $"Cast {allSpells[0]}" && player.casts > 0 && player.Spells.Contains(allSpells[0]))
                    UseSpell(allSpells[0]);
                if (command == $"Cast {allSpells[1]}" && player.casts > 0 && player.Spells.Contains(allSpells[1]))
                    UseSpell(allSpells[1]);
                if (command == $"Cast {allSpells[2]}" && player.casts > 0 && player.Spells.Contains(allSpells[2]))
                    UseSpell(allSpells[2]);
                if (inCombat == false && command == "Buy 1" && shopSlot1 != null && player.cash >= shopCost)
                    BuyItem(1);
                if (inCombat == false && command == "Buy 2" && shopSlot2 != null && player.cash >= shopCost)
                    BuyItem(2);
                if (inCombat == false && command == "Buy 3" && shopSlot3 != null && player.cash >= shopCost)
                    BuyItem(3);
                if (inCombat == false && command == "Challenge Sheriff")
                    CreateEntity("Psychiff", 15, 400, 40, 15, true);
                if (inCombat == true && command == $"Attack")
                {
                    AttackEntity(currEnemy);
                    damageCheck = false;
                }
                if (command == "Check stats")
                    CheckStats();
                if (command == "Help")
                    TextHelp();
                if (command == "Search" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Search" && player.searches <= 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You cannot search anymore");
                }
                if (command == "Search" && player.searches > 0 && inCombat == false)
                    SearchGame();
                if (command == "Collect" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Collect" && nearbyItem == 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No items to collect");
                }
                if (command == "Collect" && nearbyItem != 0 && inCombat == false)
                    CollectItem();
                if (inCombat == true && damageCheck == false)
                {
                    AttackPlayer();
                    damageCheck = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            Console.Clear();
            Stage4();
        }
        public void Stage4()
        {
            beatenSheriff = false;
            player.searches = maxSearches;
            player.casts = player.maxcasts;
            stage = 3;
            bool damageCheck = false;
            Console.Title = "Hocus Pocus Cowboys: Scorched Villa";
            string command;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("You mozey on into what literally looks like a town in the middle of a forest fire... What do you do...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (beatenSheriff == false)
            {
                if (command == $"Cast {allSpells[0]}" && player.casts > 0 && player.Spells.Contains(allSpells[0]))
                    UseSpell(allSpells[0]);
                if (command == $"Cast {allSpells[1]}" && player.casts > 0 && player.Spells.Contains(allSpells[1]))
                    UseSpell(allSpells[1]);
                if (command == $"Cast {allSpells[2]}" && player.casts > 0 && player.Spells.Contains(allSpells[2]))
                    UseSpell(allSpells[2]);
                if (inCombat == false && command == "Buy 1" && shopSlot1 != null && player.cash >= shopCost)
                    BuyItem(1);
                if (inCombat == false && command == "Buy 2" && shopSlot2 != null && player.cash >= shopCost)
                    BuyItem(2);
                if (inCombat == false && command == "Buy 3" && shopSlot3 != null && player.cash >= shopCost)
                    BuyItem(3);
                if (inCombat == false && command == "Challenge Sheriff")
                    CreateEntity("Brand", 20, 400, 60, 25, true);
                if (inCombat == true && command == $"Attack")
                {
                    AttackEntity(currEnemy);
                    damageCheck = false;
                }
                if (command == "Check stats")
                    CheckStats();
                if (command == "Help")
                    TextHelp();
                if (command == "Search" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Search" && player.searches <= 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You cannot search anymore");
                }
                if (command == "Search" && player.searches > 0 && inCombat == false)
                    SearchGame();
                if (command == "Collect" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Collect" && nearbyItem == 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No items to collect");
                }
                if (command == "Collect" && nearbyItem != 0 && inCombat == false)
                    CollectItem();
                if (inCombat == true && damageCheck == false)
                {
                    AttackPlayer();
                    damageCheck = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            Console.Clear();
            Stage5();
        }
        public void Stage5()
        {
            beatenSheriff = false;
            player.searches = maxSearches;
            player.casts = player.maxcasts;
            stage = 3;
            bool damageCheck = false;
            Console.Title = "Hocus Pocus Cowboys: Bulwarks Islands";
            string command;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("You climb your way into the floating islands... What do you do...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (beatenSheriff == false)
            {
                if (command == $"Cast {allSpells[0]}" && player.casts > 0 && player.Spells.Contains(allSpells[0]))
                    UseSpell(allSpells[0]);
                if (command == $"Cast {allSpells[1]}" && player.casts > 0 && player.Spells.Contains(allSpells[1]))
                    UseSpell(allSpells[1]);
                if (command == $"Cast {allSpells[2]}" && player.casts > 0 && player.Spells.Contains(allSpells[2]))
                    UseSpell(allSpells[2]);
                if (inCombat == false && command == "Buy 1" && shopSlot1 != null && player.cash >= shopCost)
                    BuyItem(1);
                if (inCombat == false && command == "Buy 2" && shopSlot2 != null && player.cash >= shopCost)
                    BuyItem(2);
                if (inCombat == false && command == "Buy 3" && shopSlot3 != null && player.cash >= shopCost)
                    BuyItem(3);
                if (inCombat == false && command == "Challenge Sheriff")
                    CreateEntity("Solus", 35, 800, 30, 50, true);
                if (inCombat == true && command == $"Attack")
                {
                    AttackEntity(currEnemy);
                    damageCheck = false;
                }
                if (command == "Check stats")
                    CheckStats();
                if (command == "Help")
                    TextHelp();
                if (command == "Search" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Search" && player.searches <= 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You cannot search anymore");
                }
                if (command == "Search" && player.searches > 0 && inCombat == false)
                    SearchGame();
                if (command == "Collect" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Collect" && nearbyItem == 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No items to collect");
                }
                if (command == "Collect" && nearbyItem != 0 && inCombat == false)
                    CollectItem();
                if (inCombat == true && damageCheck == false)
                {
                    AttackPlayer();
                    damageCheck = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            Console.Clear();
            Stage6();
        }
        public void Stage6()
        {
            beatenSheriff = false;
            player.searches = maxSearches;
            player.casts = player.maxcasts;
            stage = 3;
            bool damageCheck = false;
            Console.Title = "Hocus Pocus Cowboys: Arcikhun";
            string command;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("You enter a town that looks like its still stuck in the ice age... What do you do...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (beatenSheriff == false)
            {
                if (command == $"Cast {allSpells[0]}" && player.casts > 0 && player.Spells.Contains(allSpells[0]))
                    UseSpell(allSpells[0]);
                if (command == $"Cast {allSpells[1]}" && player.casts > 0 && player.Spells.Contains(allSpells[1]))
                    UseSpell(allSpells[1]);
                if (command == $"Cast {allSpells[2]}" && player.casts > 0 && player.Spells.Contains(allSpells[2]))
                    UseSpell(allSpells[2]);
                if (inCombat == false && command == "Buy 1" && shopSlot1 != null && player.cash >= shopCost)
                    BuyItem(1);
                if (inCombat == false && command == "Buy 2" && shopSlot2 != null && player.cash >= shopCost)
                    BuyItem(2);
                if (inCombat == false && command == "Buy 3" && shopSlot3 != null && player.cash >= shopCost)
                    BuyItem(3);
                if (inCombat == false && command == "Challenge Sheriff")
                    CreateEntity("Frozary", 50, 300, 75, 0, true);
                if (inCombat == true && command == $"Attack")
                {
                    AttackEntity(currEnemy);
                    damageCheck = false;
                }
                if (command == "Check stats")
                    CheckStats();
                if (command == "Help")
                    TextHelp();
                if (command == "Search" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Search" && player.searches <= 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You cannot search anymore");
                }
                if (command == "Search" && player.searches > 0 && inCombat == false)
                    SearchGame();
                if (command == "Collect" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Collect" && nearbyItem == 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No items to collect");
                }
                if (command == "Collect" && nearbyItem != 0 && inCombat == false)
                    CollectItem();
                if (inCombat == true && damageCheck == false)
                {
                    AttackPlayer();
                    damageCheck = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            Console.Clear();
            Stage7();
        }
        public void Stage7()
        {
            beatenSheriff = false;
            player.searches = maxSearches;
            player.casts = player.maxcasts;
            stage = 3;
            bool damageCheck = false;
            Console.Title = "Hocus Pocus Cowboys: Galvanille";
            string command;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("This town looks like its from a different century almost steam punk like... What do you do...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (beatenSheriff == false)
            {
                if (command == $"Cast {allSpells[0]}" && player.casts > 0 && player.Spells.Contains(allSpells[0]))
                    UseSpell(allSpells[0]);
                if (command == $"Cast {allSpells[1]}" && player.casts > 0 && player.Spells.Contains(allSpells[1]))
                    UseSpell(allSpells[1]);
                if (command == $"Cast {allSpells[2]}" && player.casts > 0 && player.Spells.Contains(allSpells[2]))
                    UseSpell(allSpells[2]);
                if (inCombat == false && command == "Buy 1" && shopSlot1 != null && player.cash >= shopCost)
                    BuyItem(1);
                if (inCombat == false && command == "Buy 2" && shopSlot2 != null && player.cash >= shopCost)
                    BuyItem(2);
                if (inCombat == false && command == "Buy 3" && shopSlot3 != null && player.cash >= shopCost)
                    BuyItem(3);
                if (inCombat == false && command == "Challenge Sheriff")
                    CreateEntity("Transmutale", 60, 1000, 40, 10, true);
                if (inCombat == true && command == $"Attack")
                {
                    AttackEntity(currEnemy);
                    damageCheck = false;
                }
                if (command == "Check stats")
                    CheckStats();
                if (command == "Help")
                    TextHelp();
                if (command == "Search" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Search" && player.searches <= 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You cannot search anymore");
                }
                if (command == "Search" && player.searches > 0 && inCombat == false)
                    SearchGame();
                if (command == "Collect" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Collect" && nearbyItem == 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No items to collect");
                }
                if (command == "Collect" && nearbyItem != 0 && inCombat == false)
                    CollectItem();
                if (inCombat == true && damageCheck == false)
                {
                    AttackPlayer();
                    damageCheck = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            Console.Clear();
            Stage8();
        }
        public void Stage8()
        {
            beatenSheriff = false;
            player.searches = maxSearches;
            player.casts = player.maxcasts;
            stage = 3;
            bool damageCheck = false;
            Console.Title = "Hocus Pocus Cowboys: Duskelh";
            string command;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("You somehow find yourself in pitch blackness with not a light in sight... What do you do...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (beatenSheriff == false)
            {
                if (command == $"Cast {allSpells[0]}" && player.casts > 0 && player.Spells.Contains(allSpells[0]))
                    UseSpell(allSpells[0]);
                if (command == $"Cast {allSpells[1]}" && player.casts > 0 && player.Spells.Contains(allSpells[1]))
                    UseSpell(allSpells[1]);
                if (command == $"Cast {allSpells[2]}" && player.casts > 0 && player.Spells.Contains(allSpells[2]))
                    UseSpell(allSpells[2]);
                if (inCombat == false && command == "Buy 1" && shopSlot1 != null && player.cash >= shopCost)
                    BuyItem(1);
                if (inCombat == false && command == "Buy 2" && shopSlot2 != null && player.cash >= shopCost)
                    BuyItem(2);
                if (inCombat == false && command == "Buy 3" && shopSlot3 != null && player.cash >= shopCost)
                    BuyItem(3);
                if (inCombat == false && command == "Challenge Sheriff")
                    CreateEntity("Sunchen", 70, 800, 60, 50, true);
                if (inCombat == true && command == $"Attack")
                {
                    AttackEntity(currEnemy);
                    damageCheck = false;
                }
                if (command == "Check stats")
                    CheckStats();
                if (command == "Help")
                    TextHelp();
                if (command == "Search" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Search" && player.searches <= 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You cannot search anymore");
                }
                if (command == "Search" && player.searches > 0 && inCombat == false)
                    SearchGame();
                if (command == "Collect" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Collect" && nearbyItem == 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No items to collect");
                }
                if (command == "Collect" && nearbyItem != 0 && inCombat == false)
                    CollectItem();
                if (inCombat == true && damageCheck == false)
                {
                    AttackPlayer();
                    damageCheck = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            Console.Clear();
            Stage9();
        }
        public void Stage9()
        {
            beatenSheriff = false;
            player.searches = maxSearches;
            player.casts = player.maxcasts;
            stage = 3;
            bool damageCheck = false;
            Console.Title = "Hocus Pocus Cowboys: Wynslye";
            string command;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write("You enter the crimson town and worry for your own blood... What do you do...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (beatenSheriff == false)
            {
                if (command == $"Cast {allSpells[0]}" && player.casts > 0 && player.Spells.Contains(allSpells[0]))
                    UseSpell(allSpells[0]);
                if (command == $"Cast {allSpells[1]}" && player.casts > 0 && player.Spells.Contains(allSpells[1]))
                    UseSpell(allSpells[1]);
                if (command == $"Cast {allSpells[2]}" && player.casts > 0 && player.Spells.Contains(allSpells[2]))
                    UseSpell(allSpells[2]);
                if (inCombat == false && command == "Buy 1" && shopSlot1 != null && player.cash >= shopCost)
                    BuyItem(1);
                if (inCombat == false && command == "Buy 2" && shopSlot2 != null && player.cash >= shopCost)
                    BuyItem(2);
                if (inCombat == false && command == "Buy 3" && shopSlot3 != null && player.cash >= shopCost)
                    BuyItem(3);
                if (inCombat == false && command == "Challenge Sheriff")
                    CreateEntity("Von'N'Dhul", 85, 950, 70, 0, true);
                if (inCombat == true && command == $"Attack")
                {
                    AttackEntity(currEnemy);
                    damageCheck = false;
                }
                if (command == "Check stats")
                    CheckStats();
                if (command == "Help")
                    TextHelp();
                if (command == "Search" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Search" && player.searches <= 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You cannot search anymore");
                }
                if (command == "Search" && player.searches > 0 && inCombat == false)
                    SearchGame();
                if (command == "Collect" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Collect" && nearbyItem == 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No items to collect");
                }
                if (command == "Collect" && nearbyItem != 0 && inCombat == false)
                    CollectItem();
                if (inCombat == true && damageCheck == false)
                {
                    AttackPlayer();
                    damageCheck = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            Console.Clear();
            Stage10();
        }
        public void Stage10()
        {
            beatenSheriff = false;
            player.searches = maxSearches;
            player.casts = player.maxcasts;
            stage = 3;
            bool damageCheck = false;
            Console.Title = "Hocus Pocus Cowboys: End of the Line";
            string command;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("This is it... The final town... It looks a little like every place you've been... What do you do...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write(">", Console.ForegroundColor);
            command = Console.ReadLine();
            while (beatenSheriff == false)
            {
                if (command == $"Cast {allSpells[0]}" && player.casts > 0 && player.Spells.Contains(allSpells[0]))
                    UseSpell(allSpells[0]);
                if (command == $"Cast {allSpells[1]}" && player.casts > 0 && player.Spells.Contains(allSpells[1]))
                    UseSpell(allSpells[1]);
                if (command == $"Cast {allSpells[2]}" && player.casts > 0 && player.Spells.Contains(allSpells[2]))
                    UseSpell(allSpells[2]);
                if (inCombat == false && command == "Buy 1" && shopSlot1 != null && player.cash >= shopCost)
                    BuyItem(1);
                if (inCombat == false && command == "Buy 2" && shopSlot2 != null && player.cash >= shopCost)
                    BuyItem(2);
                if (inCombat == false && command == "Buy 3" && shopSlot3 != null && player.cash >= shopCost)
                    BuyItem(3);
                if (inCombat == false && command == "Challenge Sheriff")
                    CreateEntity("Hocus Pocus Himself", 100, 1000, 100, 100, true);
                if (inCombat == true && command == $"Attack")
                {
                    AttackEntity(currEnemy);
                    damageCheck = false;
                }
                if (command == "Check stats")
                    CheckStats();
                if (command == "Help")
                    TextHelp();
                if (command == "Search" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Search" && player.searches <= 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You cannot search anymore");
                }
                if (command == "Search" && player.searches > 0 && inCombat == false)
                    SearchGame();
                if (command == "Collect" && inCombat == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Cannot perform this action while in combat!");
                }
                if (command == "Collect" && nearbyItem == 0 && inCombat == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("No items to collect");
                }
                if (command == "Collect" && nearbyItem != 0 && inCombat == false)
                    CollectItem();
                if (inCombat == true && damageCheck == false)
                {
                    AttackPlayer();
                    damageCheck = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">", Console.ForegroundColor);
                command = Console.ReadLine();
            }
            Console.Clear();
        }
    }

}
