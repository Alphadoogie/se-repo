using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace tamagotchi
{
    class Program
    {
        static void Main(string[] args)
        {
            string you = "";
            string name = "";

            while (you == "")
            {
                Write("*TYPE YOUR NAME*");
                Console.WriteLine();
                you = Console.ReadLine();
                Console.WriteLine();
            }

            while (name == "")
            {
                Write("Name your own Tamagottchiii!");
                YouTalk(you);
                name = Console.ReadLine();
            }


            // Яйцо
            var tamagotchi = new Tama(name);
            tamagotchi.Hatching();
            tamagotchi.ChangeStage("baby");


            
            tamagotchi.WriteTama();
            tamagotchi.TamaTalks("Hi " + you + "! *rumbling*");
            Write(tamagotchi.Name + " is very hungry, you have to feed it!");
            Feed(tamagotchi, you);

            tamagotchi.WriteTama();
            tamagotchi.TamaTalks(tamagotchi.food != "nothing" ? "Yum yum yum, " + tamagotchi.food + "!" : "*rumble rumble*");
            tamagotchi.TamaTalks("Play with me!");
            bool play = YesNo(tamagotchi, you);

            tamagotchi.WriteTama();
            tamagotchi.TamaTalks(play ? "YAY!" : "Boooo!");
            tamagotchi.TamaTalks("I'm still hungry! Feed me!");
            Feed(tamagotchi, you);

            tamagotchi.WriteTama();
            tamagotchi.TamaTalks(tamagotchi.food != "nothing" ? "Nam nam nam, delicious " + tamagotchi.food + "!" : "AAYYH I'm just a baby, I need food!");
            tamagotchi.TamaTalks("It's getting late and I'm really sleepy. \r\nWill you turn off this lights just for me?");
            bool lights = YesNo(tamagotchi, you);
            if (lights)
            {
                tamagotchi.Happy += 1;
                tamagotchi.Dicipline += 1;
                Night();
            }
            else
            {
                tamagotchi.WriteTama();
                tamagotchi.TamaTalks("Please, I'm really sleepy! *AWWWWWWW*");
                lights = YesNo(tamagotchi, you);

                if (lights)
                {
                    tamagotchi.Happy += 1; //После сна растет показатель Happiness
                    Night();
                }
                else
                {
                    tamagotchi.Dicipline = (tamagotchi.Dicipline != 0 ? tamagotchi.Dicipline -= 1 : 0);
                    tamagotchi.WriteTama();
                    tamagotchi.TamaTalks("*Angry*");
                    Write("Sorry, but now both you and " + tamagotchi.Name + " will be upp all night...");
                    System.Threading.Thread.Sleep(2000);
                    Night();
                }
            }

            tamagotchi.WriteTama();
            tamagotchi.TamaTalks(tamagotchi.Happy > 1 ? "Good morning " + you + "!" : "I don't like you " + you + " very much *sob sob*");
            Write("Time for breakfast!");
            Feed(tamagotchi, you);

            tamagotchi.WriteTama();

            if (tamagotchi.Poop == 0)
            {
                tamagotchi.ChangeStage("dead");
                tamagotchi.WriteTama();
                Write("Poor " + tamagotchi.Name + " starved to death!");
                Write("You shouldn't have pets, " + you + "...");
                Console.WriteLine();
                Write("Hit ENTER to shut down.");
                Console.ReadLine();
                return;
                //Смерть
            }

            Write("Looks like " + tamagotchi.Name + " made a doo-doo, will you clean it?");
            bool poop = YesNo(tamagotchi, you);
            if (poop) tamagotchi.Poop = 0;

            if (tamagotchi.Dicipline > 2) tamagotchi.ChangeStage("goodTeen"); //Тут уже идет выбор между поведением подростка, bad or good, в зависимости от этих показателей, тамагоч не будет выполнять действие с какого-то раза
            else tamagotchi.ChangeStage("badTeen");


            //В возрасте подростка(Teen)
            tamagotchi.WriteTama();
            if (tamagotchi.Good)
            {
                tamagotchi.TamaTalks(poop ? "Thank you " + you + "!" : "I guess it can be taken care of later...");
                Write("Oh! " + tamagotchi.Name + " just grew!");
                Write("And it looks like it's healthy and well disciplined.\r\nKeep raising it this way!");
            }
            else
            {
                tamagotchi.TamaTalks("What ever...");
                Write("Uh oh! Looks like " + tamagotchi.Name + " just grew into a spoiled teen!");
                Write("You should really step up your parenting game...");
            }

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine();
            tamagotchi.TamaTalks(tamagotchi.Good ? "Want to play a game!?" : "Entertain me!");
            play = YesNo(tamagotchi, you);

            tamagotchi.WriteTama();
            if (tamagotchi.Good) tamagotchi.TamaTalks(play ? "You're so much fun! All that playing made me hungry!" : "Ok, next time... May I have something to eat?");
            else tamagotchi.TamaTalks(play ? "You call that fun? Now - FEED ME!" : "*Humpf* FEED ME!");
            Feed(tamagotchi, you);

            tamagotchi.WriteTama();
            Write("It's getting late, you should put " + tamagotchi.Name + " to bed and turn off the lights!");
            lights = YesNo(tamagotchi, you);
            bool stayUp;

            if (tamagotchi.Good)
            {
                if (lights)
                {
                    tamagotchi.Dicipline += 1;
                    tamagotchi.TamaTalks("Good night " + you + "!");
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    tamagotchi.Dicipline = (tamagotchi.Dicipline != 0 ? tamagotchi.Dicipline -= 1 : 0);
                    tamagotchi.TamaTalks("So I can stay up all night!?");
                    stayUp = YesNo(tamagotchi, you);

                    if (stayUp)
                    {
                        tamagotchi.Dicipline = (tamagotchi.Dicipline != 0 ? tamagotchi.Dicipline -= 3 : 0);
                        tamagotchi.WriteTama();
                        Write("Not a wise decision...");
                        lights = false;
                        System.Threading.Thread.Sleep(2000);
                    }
                    else
                    {
                        tamagotchi.Happy += 1;
                    }
                }
            }
            else
            {
                var complaint = new List<string>();
                complaint.AddRange(new String[] {
                        "I'm not going to bed!",
                        "You can't make me!",
                        "Look at me - NOT SLEEPING! You're not the boss of me!",
                        });

                for (int i = 0; i < 3; i++)
                {
                    if (lights) tamagotchi.Dicipline += 1;
                    else tamagotchi.Dicipline = (tamagotchi.Dicipline != 0 ? tamagotchi.Dicipline -= 1 : 0);

                    tamagotchi.TamaTalks(complaint[i]);
                    Write("Send " + tamagotchi.Name + " to bed?");
                    lights = YesNo(tamagotchi, you);
                }

                if (!lights)
                {
                    tamagotchi.TamaTalks("Good, I'm never going to sleep.");
                    Write("You have to put " + name + " to bed!");
                    lights = YesNo(tamagotchi, you);
                    if (!lights)
                    {
                        tamagotchi.Dicipline = (tamagotchi.Dicipline != 0 ? tamagotchi.Dicipline -= 2 : 0);
                        tamagotchi.Happy = (tamagotchi.Happy != 0 ? tamagotchi.Happy -= 2 : 0);
                        tamagotchi.WriteTama();
                        tamagotchi.TamaTalks("*going berserk*");
                        Write("Suit yourself...");
                        System.Threading.Thread.Sleep(2000);
                    }
                    else
                    {
                        tamagotchi.Happy = (tamagotchi.Happy != 0 ? tamagotchi.Happy -= 1 : 0);
                        tamagotchi.Dicipline += 1;
                    }
                }
            }
            Night();

            if (!lights)
            {
                Console.Clear();
                Write("Since you let " + tamagotchi.Name + " stay up all night it's not waking up.");
                Write("If you don't want to end up with a bad pet \r\nyou need to let it know who's the boss!");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine();
                Write("Wake " + tamagotchi.Name + " up!");
                bool wake = YesNo(tamagotchi, you);

                while (!wake)
                {
                    var wakeIt = new List<string>();
                    wakeIt.AddRange(new String[] {
                        "You should really wake "+tamagotchi.Name+" up...",
                        "Wake it!",
                        "Wake "+tamagotchi.Name+" or it will become lazy!",
                        "Really, you should take some responsibility for your pet!",
                        you+", wake it!!",
                        "Come on, wake "+tamagotchi.Name+" up!",
                        "Wake it up now!"
                        });
                    Random r = new Random();
                    int index = r.Next(wakeIt.Count);

                    tamagotchi.TamaTalks("Zzzzzzz...");
                    Write(wakeIt[index]);
                    wake = YesNo(tamagotchi, you);
                }
                if (wake) tamagotchi.Dicipline += 1;
            }

            tamagotchi.WriteTama();
            tamagotchi.TamaTalks(tamagotchi.Good ? "Good morning " + you + "! \r\nCan I have some breakfast, please?" : "Why did you wake me up!? \r\nYou better give me something tasty for breakfast... \r\n I only want candy!");
            Feed(tamagotchi, you);

            if (!tamagotchi.Good)
            {
                int i = 0;

                while (tamagotchi.food == "bread")
                {
                    var complaint = new List<string>();
                    complaint.AddRange(new String[] {
                        "Bread isn't tasty... I want candy!",
                        "I told you! I don't want bread, I want candy!!",
                        "NOOOO BREEEAAAD!!!"
                        });

                    tamagotchi.Dicipline += 1;
                    tamagotchi.WriteTama();
                    tamagotchi.TamaTalks(complaint[i]);
                    i += 1;
                    if (i == 3) break;
                    Feed(tamagotchi, you);
                }

                Write(tamagotchi.food == "candy" ? "You shouldn't reward such bad behavior with candy..." : "Good, you're starting to make some progress."); //Баловство конфетой(системное предупреждение)
                System.Threading.Thread.Sleep(2000);
            }

            tamagotchi.WriteTama();
            if (tamagotchi.food == "bread" && !tamagotchi.Good) tamagotchi.TamaTalks(tamagotchi.food + "Bread... *pout*");
            if (tamagotchi.food != "nothing" && tamagotchi.Good) tamagotchi.TamaTalks("YUMMM, " + tamagotchi.food + "!");
            if (tamagotchi.food == "nothing") tamagotchi.TamaTalks(tamagotchi.Good ? "Ok, but I'm really hungry..." : "Nothing, what! You're not feeding me...?");

            if (tamagotchi.Poop > 0)
            {
                Write("Looks like you need to clean up after " + tamagotchi.Name + ". \r\nWill you do it");
                poop = YesNo(tamagotchi, you);
                if (poop) tamagotchi.Poop = 0;
            }

            if (tamagotchi.Poop > 0 || tamagotchi.Happy < 3) //Не дает поднятся уровню Happiness больше 3 при грязном состоянии
            {
                Write("Oh no, " + tamagotchi.Name + " isn't doing so well... \r\nYou have to give it some medicine!");
                bool meds = YesNo(tamagotchi, you);
                if (!meds)
                {
                    tamagotchi.ChangeStage("dead");
                    tamagotchi.WriteTama();
                    Write("Why, " + you + "!? \r\nNow " + tamagotchi.Name + " is dead...");
                    Write("You really shouldn't have pets, " + you + "...");
                    Console.WriteLine();
                    Write("Hit ENTER to shut down.");
                    Console.ReadLine();
                    return;
                    //Плохая концовка
                }
                else
                {
                    tamagotchi.Happy += 2;
                }
            }
            else System.Threading.Thread.Sleep(2000);

            tamagotchi.ChangeStage(tamagotchi.Dicipline > 3 ? "goodAdult" : "badAdult");
            tamagotchi.WriteTama();
            Write(tamagotchi.Good ? "Good job " + you + ", \r\nyou've raised your " + tamagotchi.Name + " to become good and well behaved pet!" : "Sorry, " + you + ". You haven't done such a good job in raising " + tamagotchi.Name + "...");
            if (tamagotchi.Good)
            {
                tamagotchi.TamaTalks("Would you like to play with me?");
                play = YesNo(tamagotchi, you);

                int i = 0;
                while (!play)
                {
                    var wannaPlay = new List<string>();
                    wannaPlay.AddRange(new String[] {
                        "But I thought we had fun together,\r\nwon't you play with me?",
                        "You don't like me anymore? I want to play with you!",
                        "Now I'm very sad... Please play with me?",
                        "*cries*"
                        });

                    tamagotchi.Happy = (tamagotchi.Happy != 0 ? tamagotchi.Happy -= 1 : 0);
                    tamagotchi.TamaTalks(wannaPlay[i]);
                    i += 1;
                    if (i == 4) break;
                    YesNo(tamagotchi, you);
                }
            }
            else
            {
                Write("You should play some with " + tamagotchi.Name + ".");
                play = YesNo(tamagotchi, you);

                int i = 0;
                while (play)
                {
                    var wannaPlay = new List<string>();
                    wannaPlay.AddRange(new String[] {
                        "I don't wanna play with you...",
                        "Didn't you hear me? I don't want to play!",
                        "NOOOOO!"
                        });

                    tamagotchi.Happy -= 1;
                    tamagotchi.Dicipline += 1;
                    tamagotchi.TamaTalks(wannaPlay[i]);
                    Write("Play with " + tamagotchi.Name);
                    i += 1;
                    if (i == 3) break;
                    YesNo(tamagotchi, you);
                }
            }

            tamagotchi.WriteTama();
            if (tamagotchi.Good) tamagotchi.TamaTalks(play ? "That was so much fun " + you + " !" : "I'm sad now...");
            else tamagotchi.TamaTalks(play ? "That was so much fun... NOT!" : "What ever...");

            Console.WriteLine();
            tamagotchi.TamaTalks("I'm not feeling very well...");
            tamagotchi.TamaTalks("I'm so cold, will you comfort me?");
            var comfort = YesNo(tamagotchi, you);

            if (comfort)
            {
                tamagotchi.Happy += 2;
                tamagotchi.TamaTalks("It feels like I'm slipping away... \r\n");
                tamagotchi.TamaTalks(tamagotchi.Good ? "You've been so good to me..." : "Thank you for comforting me, even though I acted bad...");
            }
            else
                tamagotchi.Happy = 3;

            System.Threading.Thread.Sleep(2000);

            tamagotchi.ChangeStage(tamagotchi.Happy > 4 ? "angel" : "dead");
            tamagotchi.WriteTama();
            if (tamagotchi.Good)
            {
                Write(tamagotchi.Name + " has passed... You took good care of it. It had a happy life!");
                Write("Hit ENTER to exit the game.");
                Console.ReadLine();
                return;
                //END
            }
            else
            {
                Write(tamagotchi.Name + " has passed... \r\nSorry, but you're a terrible pet owner " + you);
                Write("Hit ENTER to exit the game.");
                Console.ReadLine();
                return;
                //END
            }


        }




        static void Write(string String)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine(String);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void YouTalk(string you)
        {
            Console.WriteLine();
            Console.Write(you + "> ");
        }

        static bool YesNo(Tama tama, string you)
        {
            bool answer = false;
            bool final = false;

            Write("[YES]   [NO]");
            YouTalk(you);

            while (answer == false)
            {
                string readLine = Console.ReadLine().ToLower();

                if (readLine == "yes" || readLine == "y")
                {
                    tama.Happy += 1;
                    answer = true;
                    final = true;
                }
                else if (readLine == "no" || readLine == "n")
                {
                    tama.Happy = (tama.Happy != 0 ? tama.Happy -= 1 : 0);
                    answer = true;
                    final = false;
                }
                else
                {
                    Write("[ YES ]   [ NO ]");
                    YouTalk(you);
                    answer = false;
                }

            }
            return final;
        }

        static void Feed(Tama tamagotchi, string you)
        {
            bool fed = false; //Сытость

            Write("Choose from [BREAD] [CANDY] [NOTHING]");
            YouTalk(you);

            while (fed == false)
            {
                tamagotchi.food = Console.ReadLine().ToLower();

                if (tamagotchi.food == "bread")
                {
                    tamagotchi.Dicipline += 1;
                    tamagotchi.Hungry += 3;
                    fed = true;
                }
                else if (tamagotchi.food == "candy")
                {
                    tamagotchi.Dicipline = (tamagotchi.Dicipline != 0 ? tamagotchi.Dicipline -= 1 : 0);
                    tamagotchi.Hungry += 2;
                    fed = true;
                }
                else if (tamagotchi.food == "nothing")
                {
                    tamagotchi.Hungry = (tamagotchi.Hungry != 0 ? tamagotchi.Hungry -= 1 : 0);
                    fed = true;
                }
                else
                {
                    Write("Choose from [BREAD] [CANDY] [NOTHING]");
                    YouTalk(you);
                }

            }

        }


        static void Night() //Анимка ночи
        {
            Console.Clear();
            var stars = new List<string>();
            stars.AddRange(new String[] {
                        "        *       ",
                        "              *       ",
                        "    *            ",
                        "          *          ",
                        "  *        ",
                        "           *    ",
                        "        *      "
                        });


            for (int i = 0; i < 200; i++)
            {

                Random s = new Random();
                int index = s.Next(stars.Count);

                if (i % 2 == 0)
                    Console.ForegroundColor = ConsoleColor.Gray;
                else if (i % 3 == 0)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                else
                    Console.ForegroundColor = ConsoleColor.DarkCyan;

                Console.Write(stars[index]);
                System.Threading.Thread.Sleep(2);

            }

            System.Threading.Thread.Sleep(700);
            Console.ForegroundColor = ConsoleColor.White;

        }


    }


    class Tama
    {
        public string Name { get; set; }
        public int Dicipline { get; set; }
        public int Hungry { get; set; }
        public int Happy { get; set; }
        public int Poop { get; set; }
        public bool Good { get; set; }

        public string food;

        public string Stage { get; set; }
        ConsoleColor Color { get; set; }

        public Tama(string name)
        {
            Name = name;
            Stage = "egg";
            Color = ConsoleColor.DarkMagenta;
        }

        public void WriteTama()
        {
            if (Hungry > 5)
            {
                Poop += 1;
                Hungry = 2;
            }

            Console.Clear();
            WriteName();
            DrawTama();
            DrawChart();

            if (Poop > 0) DrawPoop();
        }

        public void ChangeStage(string stage)
        {
            Stage = stage;

            if (Stage == "goodTeen" || Stage == "goodAdult" || Stage == "angel")
                Good = true;
            else
                Good = false;
        }

        public void WriteName()
        {
            Console.SetCursorPosition(7, 1);
            Console.WriteLine("  ■  ■  ■  {0}  ■  ■  ■", Name);
            Console.WriteLine();
        }

        public void DrawChart()
        {
            Console.SetCursorPosition(5, 17);

            Console.Write("Dicipline: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (var i = 0; i < Dicipline; i++)
            {
                Console.Write("■");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("  Hungry: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (var i = 0; i < Hungry; i++)
            {
                Console.Write("■");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("  Happy: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (var i = 0; i < Happy; i++)
            {
                Console.Write("■");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        public void DrawPoop()
        {
            Console.SetCursorPosition(5, 19);
            Console.Write("Poop: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (var i = 0; i < Poop; i++)
            {
                Console.Write("■");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        public void DrawTama()
        {
            switch (Stage)
            {
                case "baby":
                    Color = ConsoleColor.Magenta;
                    Baby();
                    break;
                case "goodTeen":
                    Color = ConsoleColor.Cyan;
                    GoodTeen();
                    break;
                case "badTeen":
                    Color = ConsoleColor.Green;
                    BadTeen();
                    break;
                case "goodAdult":
                    Color = ConsoleColor.Blue;
                    GoodAdult();
                    break;
                case "badAdult":
                    Color = ConsoleColor.DarkGreen;
                    BadAdult();
                    break;
                case "angel":
                    Color = ConsoleColor.DarkCyan;
                    Angel();
                    break;
                case "dead":
                    Color = ConsoleColor.DarkGray;
                    Dead();
                    break;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public void TamaTalks(string String)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine();
            Console.Write("{0}> ", Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(String);
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
        }

        public void Hatching()
        {
            for (int i = 0; i < 15; i++)
            {
                Console.Clear();

                WriteName();

                if (i % 2 == 0)
                    Egg();
                else if (i % 3 == 0)
                    Egg2();
                else
                    Egg3();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(Name + " is hatching!!");
                System.Threading.Thread.Sleep(200);
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Clear();
            WriteName();
            Egg();
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(500);

            Console.Clear();
            WriteName();
            Egg4();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            TamaTalks("MOMMY!");
            System.Threading.Thread.Sleep(400);
            TamaTalks("*squeeek*");
            System.Threading.Thread.Sleep(800);
            Console.ForegroundColor = ConsoleColor.White;
        }

        void Egg()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("                    ■■       ");
            Console.WriteLine("                  ■    ■     ");
            Console.WriteLine("                ■        ■   ");
            Console.WriteLine("               ■          ■  ");
            Console.WriteLine("              ■            ■ ");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("               ■          ■  ");
            Console.WriteLine("                 ■ ■■■■ ■    ");
        }

        void Egg2()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("                   ■■        ");
            Console.WriteLine("                 ■    ■      ");
            Console.WriteLine("               ■        ■    ");
            Console.WriteLine("              ■          ■   ");
            Console.WriteLine("             ■             ■  ");
            Console.WriteLine("            ■  Пожилое яйцо ■ ");
            Console.WriteLine("            ■               ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("               ■          ■  ");
            Console.WriteLine("                 ■ ■■■■ ■    ");
        }

        void Egg3()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("                     ■■      ");
            Console.WriteLine("                   ■    ■     ");
            Console.WriteLine("                 ■        ■   ");
            Console.WriteLine("               ■           ■  ");
            Console.WriteLine("             ■  Пожилое яйцо ■ ");
            Console.WriteLine("             ■               ■");
            Console.WriteLine("             ■               ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("               ■          ■  ");
            Console.WriteLine("                 ■ ■■■■ ■    ");
        }

        void Egg4()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("                    ■■       ");
            Console.WriteLine("                  ■    ■     ");
            Console.WriteLine("                ■        ■   ");
            Console.WriteLine("              ■            ■ ");
            Console.WriteLine("             ■  ■ ■ ■■ ■ ■  ■");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("            ▄███▀ *  ▌ * ▐▀███▄ ");
            Console.WriteLine("              ■    ■■■■    ■ ");
            Console.ForegroundColor = Color;
            Console.WriteLine("             ■  ■ ■ ■■ ■ ■  ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("               ■          ■  ");
            Console.WriteLine("                 ■ ■■■■ ■    ");
        }


        void Baby()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                  ■ ■■■■ ■   ");
            Console.WriteLine("                ■          ■ ");
            Console.WriteLine("          ■  ▄███▀ *  ▌ * ▐▀███▄  ■");
            Console.WriteLine("               ■    ■■■■    ■");
            Console.WriteLine("               ■            ■");
            Console.WriteLine("                ■          ■ ");
            Console.WriteLine("                  ■ ■■■■ ■   ");
        }

        void GoodTeen()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine();
            Console.WriteLine("                  ■ ■■■■ ■■   ");
            Console.WriteLine("                ■           ■  ");
            Console.WriteLine("            ■▄███▀ *  ▌ * ▐▀███▄■ ");
            Console.WriteLine("               ■    ■■■■■    ■ ");
            Console.WriteLine("              ■■             ■■");
            Console.WriteLine("              ■■  ■           ■■  ");
            Console.WriteLine("                 ■    ■    ■   ");
            Console.WriteLine("                      ■   ");
            Console.WriteLine("                   ■     ■     ");
        }

        void BadTeen()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine();
            Console.WriteLine("                     ■ ■■■ ■      ");
            Console.WriteLine("                   ■         ■    ");
            Console.WriteLine("                  ■   *   *    ■  ");
            Console.WriteLine("                  ■           ");
            Console.WriteLine("                  ■    :---:   ■  ");
            Console.WriteLine("                  ■            ■  ");
            Console.WriteLine("                  ■             ■");
            Console.WriteLine("                  ■      ■       ■");
            Console.WriteLine("                    ■ ■ ■  ■ ■ ■  ");
        }

        void GoodAdult()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("               _________________    ");
            Console.WriteLine("                     ^    ^     ");
            Console.WriteLine("                        3    ");
            Console.WriteLine("                  ■■■■■■■■■■■    ");
            Console.WriteLine("                 ■■■■■■■■■■■■■   ");
            Console.WriteLine("                   ■■■■■■■■■■     ");
            Console.WriteLine("                     ■■■■■■      ");
            Console.WriteLine("                ■      ■■     ■  ");
          
        }

        void BadAdult()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine();
            Console.WriteLine("                     ■ ■■■ ■      ");
            Console.WriteLine("                   ■         ■    ");
            Console.WriteLine("                  ■   III  III ■  ");
            Console.WriteLine("                  ■           ");
            Console.WriteLine("                  ■    ,:---:,   ■  ");
            Console.WriteLine("                  ■            ■  ");
            Console.WriteLine("                  ■             ■");
            Console.WriteLine("                  ■      ■       ■");
            Console.WriteLine("                    ■ ■ ■  ■ ■ ■  ");
        }

        void Angel()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("                     ■■■■■       ");
            Console.WriteLine("                                 ");
            Console.WriteLine("                  ■■ ■ ■ ■ ■■    ");
            Console.WriteLine("                 ■           ■   ");
            Console.WriteLine("      ■         ■  &      &  ■  ");
            Console.WriteLine("    ■ ■ ■       ■      3     ■  ");
            Console.WriteLine("      ■         ■            ■  ");
            Console.WriteLine("      ■          ■  B. Y. E.■   ");
            Console.WriteLine("                   ■       ■     ");
            Console.WriteLine("                      ■ ■        ");
            Console.WriteLine("                       ■         ");
        }

        void Dead()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("                   ■ ■ ■ ■      ");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("             ■ ■ ■ ■     ■ ■ ■ ■");
            Console.WriteLine("             ■      R.I.P.     ■");
            Console.WriteLine("             ■ ■ ■ ■     ■ ■ ■ ■");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("                   ■ ■ ■ ■      ");
        }
    }
}


