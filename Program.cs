using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Security.Permissions;
using System.Reflection;
using System.Timers;
using System.Runtime.CompilerServices;
using System.IO;
using System.Reflection.Emit;
using System.Configuration;

namespace Ceasar
{
    internal class Program
    {
        public static void Archive(string encryptedMessage, string level)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\nick_\source\repos\Syne23\Ceasar_Extended\Archive.txt", true);
            sw.WriteLine(encryptedMessage);
            sw.Close();

        } /*This method stores the messages and otpionally the changing value*/
        private static void Login(string username, string password)
        {
            bool found = false;
            string info = username + ":" + password;
            string[] lines = File.ReadAllLines(@"C:\Users\nick_\source\repos\Syne23\Ceasar_Extended\Inlogin.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == info)
                {
                    found = true;
                    break;
                }
            }
            if (found == true)
            {
                Console.WriteLine("\n\tLoging succeed");
                Thread.Sleep(1500);
                MainMenu();
            }
            else
            {

                Console.WriteLine("\n\tInvalid username or password");
                Console.ReadLine();
            }
        } /*This method checks if the users input is the same as one of the information in the storage*/
        private static void NewAccount()
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\nick_\source\repos\Syne23\Ceasar_Extended\Inlogin.txt", true);
            Logo();
            Console.Write("\tCreat a username: "); 
            string username = Console.ReadLine().ToLower();
            Console.Write("\tCreat a password (xxxx): ");
            string password = Console.ReadLine();
            sw.WriteLine(username + ":" + password);
            sw.Close();
            Main(null);
        } /*This method creats and stores new username and password information*/
        private static void MainMenu()
        {
            int level = 0;
            bool run = true;
            while (run)
            {
                Logo();
                Console.Write("\tSelect operation\n" +
                    "\t1. Encryption\n" +
                    "\t2. Decryption\n" +
                    "\t3. Log out\n" +
                    "\t> ");
                bool correct = Int32.TryParse(Console.ReadLine(), out int choice);
                if (correct)
                { 
                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                Logo();
                                Console.Write("\nMessage to be encrypted\n" +
                                    "> ");
                                char[] message = Console.ReadLine().ToCharArray();
                                Console.Write("\nSelect encryption level(1-10)\n" +
                                    "> ");
                                level = Int32.Parse(Console.ReadLine());
                                Console.WriteLine($"\nEncrypted message: {Encryption(message, level)}");
                                Console.ReadLine();
                            }
                            catch
                            {
                                Console.WriteLine("\t\nWrong input");
                                Console.ReadLine();
                                goto case 1;
                            }
                            break;
                        case 2:
                            DecryptionMenu(level);
                            break;
                        case 3:
                            Console.Clear();
                            Logo();
                            Console.WriteLine("\t--- Signing out ---");
                            Thread.Sleep(2000);
                            run = false;
                            break;
                        default:
                            Console.WriteLine("\tVälj mellan 1 och 3");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\t\nWrong input");
                    Console.ReadLine();
                }
            }
        } /*In the MainMenu its getting initated the secondary methods according to the users input*/
        static void Logo()
        {
            Console.Clear();
            Console.WriteLine("************************************\n" +
                              " ********\x1b[93m Roman Securities\x1b[39m ********\n" +
                              "  **********\u001b[93m Code-Ceasar\u001b[39m *********\n" +
                              "   ******************************\n");
        }/*A method that prints out the Logo when its called*/
        static void ProcessVisuals()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Logo();
                Console.WriteLine("\tEncryption/Decryption in progress...");
                Thread.Sleep(1000);
                Console.Clear();
                Logo();
                Console.WriteLine("\tPlease hold...");
                Thread.Sleep(1000);
                Console.Clear();
                Logo();
            }
        } /*In this method is printed two deiferent messages with a delay funktion(Thread.Sleep())*/
        static void DecryptionMenu(int level)
        {
            try
            {
                bool innerRun = true;
                while (innerRun)
                {
                    Logo();
                    Console.Write("\tSelect operation\n" +
                                   "\t1. Message to be decrypted\n" +
                                   "\t2. Clear archive\n" +
                                   "\t3. Back\n" +
                                   "\t> ");
                    int alt = Int32.Parse(Console.ReadLine());
                    switch (alt)
                    {
                        case 1:
                            Console.Clear();
                            Logo();
                            string[] lines = File.ReadAllLines(@"C:\Users\nick_\source\repos\Syne23\Ceasar_Extended\Archive.txt");
                            if (lines != null)
                            {
                                for (int i = 0; i < lines.Length; i++)
                                {
                                    Console.WriteLine($"\t{(i + 1)} - {lines[i]} \n");
                                }

                                Console.Write("\nMessage to be decrypted\n" +
                                    "> ");
                                int temp = Int32.Parse(Console.ReadLine());

                                char[] encryptedMessage = lines[temp - 1].ToCharArray();
                                Console.Write("\nSelect decryption level(1-10)\n" +
                                    "> ");
                                level = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("\nMessage decrypted: " + Decryption(encryptedMessage, level));
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("\tThere is nothing in the archive");
                                Console.ReadLine();
                            }
                            break;
                        case 2:
                            Logo();
                            Console.Write("\tAre you sure you want to\n" +
                                          "\t------- \x1b[91mDELETE\x1b[39m -------\n" +
                                          "\tall the sensitive info?\n");
                            Console.Write("\t> ");
                            string answer = Console.ReadLine().ToLower();
                            if (answer == "y")
                            {
                                File.Delete(@"C:\Users\nick_\source\repos\Syne23\Ceasar_Extended\Archive.txt");
                                Console.WriteLine("\n\tEverything is removed");
                                Console.ReadLine();
                            }
                            else
                            {
                                innerRun = false;
                            }
                            break;
                        case 3:
                            innerRun = false;
                            break;

                    }
                }
            }
            catch
            {
                Console.WriteLine("\tWrong input");
                Console.ReadLine();
            }
        } /*This method prints out a menu for decryption and initiates the Decryption method*/
        static string Decryption(char[] message, int level)
        {
            //return Encryption(message, 26 - level);
            StringBuilder decryptedMessage = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                if (Char.IsLetter(message[i]) == true)
                {
                    int correspondingCharNum = (int)message[i];
                    if (Char.IsLower(message[i]))
                    {
                        if (correspondingCharNum - level < 97)
                        {
                            decryptedMessage.Append((char)(correspondingCharNum + (26 - level)));
                        }
                        else
                        {
                            decryptedMessage.Append((char)(correspondingCharNum - level));
                        }
                    }
                    else if (Char.IsUpper(message[i]))
                    {
                        if (correspondingCharNum - level < 65)
                        {
                            decryptedMessage.Append((char)(correspondingCharNum + (26 - level)));
                        }
                        else
                        {
                            decryptedMessage.Append((char)(correspondingCharNum - level));
                        }
                    }
                }
                else if (Char.IsLetter(message[i]) == false)
                {
                    decryptedMessage.Append(message[i]);
                }
            }
            ProcessVisuals();
            return decryptedMessage.ToString();
        } /*This method decrypts a message chosen from the storage and prints it out*/
        static string Encryption(char[] message, int level)
        {
            StringBuilder encryptedMessage = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                if (Char.IsLetter(message[i]))
                {
                    int correspondingCharNum = (int)message[i];
                    if (Char.IsLower(message[i]))
                    {
                        if (correspondingCharNum + level > 122)
                        {
                            encryptedMessage.Append((char)(correspondingCharNum - (26 - level)));
                        }
                        else
                        {
                            encryptedMessage.Append((char)(correspondingCharNum + level));
                        }
                    }
                    else if (Char.IsUpper(message[i]))
                    {
                        if (correspondingCharNum + level > 90)
                        {
                            encryptedMessage.Append((char)(correspondingCharNum - (26 - level)));
                        }
                        else
                        {
                            encryptedMessage.Append((char)(correspondingCharNum + level));
                        }
                    }
                }
                else if (!Char.IsLetter(message[i]))
                {
                    encryptedMessage.Append(message[i]);
                }
            }
            ProcessVisuals();
            Archive(encryptedMessage.ToString(), level.ToString());
            return encryptedMessage.ToString();
        } /*This method encrypts the message according to the changing value, stores it and prints it out*/
        static void Main(string[] args)
        {
            bool start = true;
            while (start)
            {
                Logo();
                Console.Write("\tSelect operation\n" +
                    "\t1. Log in\n" +
                    "\t2. Register\n" +
                    "\t3. Exit\n" +
                    "\t> ");
                bool correct = Int32.TryParse(Console.ReadLine(), out int startMenu);
                if (correct)
                {

                    switch (startMenu)
                    {
                        case 1:
                            Logo();
                            Console.Write("\tEnter your username: ");
                            string username = Console.ReadLine().ToLower();
                            Console.Write("\tEnter your password: ");
                            string password = Console.ReadLine().ToLower();
                            Login(username, password);
                            break;
                        case 2:
                            Logo();
                            NewAccount();
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("\n\tVälj mellan 1 och 3");
                            Thread.Sleep(1500);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\n\tWrong input");
                    Console.ReadLine();
                }
            }
        } /*In the Main starts the program and initiates the primary methods according to the users input*/
    }
}
