using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace bookreview
{//bookreview creates new .txt files that act as book reviews (or possibly any media you've witnessed), and in the filename you include the name author/creator format and a unique tag. You can also search through old files.
    class Program
    {
        static void Main()
        {
            string no = "";
            bool cont = true;
            while (cont == true)
            {
                Console.WriteLine("Welcome! n for New, o for Old"); //submits n or an o
                no = Console.ReadLine();
                if (no == "n" || no == "o")//so long as proper input is given Filer() can be ran. Improper input just returns you to the Console Write Line above
                {
                    Filer(no);
                }
                no = "";
            }
        }
        public static void Filer(string newold) //Filer either creates a new filename based on submitted info, verifies info is correct, then calls NewFileEntry OR asks for ID/lists all current filenames in bookreviews folder
        {
            while (newold == "n")//so this is a new .txt file in the bookreviews folder
            {
                string filePath;
                string id;
                string whole;
              
                Console.WriteLine("FORMAT: [title] [last name/author] [format of media]");
                Console.WriteLine("DO NOT USE NUMBERS FOR ANYTHING BUT THE ID WHICH WILL BE PROMPTED BELOW!!");
                Console.WriteLine("EXAMPLE: CRYING OF LOT 49 SHOULD BE CRYING OF LOT FORTY NINE");
                Console.WriteLine("To return to the new old menu type n");
                string taf = Console.ReadLine();
                if (taf != "n")
                {
                    Console.WriteLine("Enter ID here: use this format [#][AA][5 digit number]");//using this format so that it is easy to search while newold = o
                    id = Console.ReadLine();
                    whole = id + " " + taf;//so whole would be #AA00000 The Stranger Camus Book
                    Console.WriteLine("Is this correct? " + whole + " y/n");
                    string yn = Console.ReadLine();
                    if (yn == "y")//so cw is printed out above, confirmation to double check formatting/name is correct, lets go ahead and make a new file with this ID and taf
                    {
                        filePath = @"!!!YOUR BOOKREVIEW FOLDER GOES HERE!!!" + whole + ".txt";//BE SURE TO CREATE YOUR OWN FOLDER, THEN PUT IT IN BETWEEN THE QUOTES ABOVE
                        using (StreamWriter sw = File.CreateText(filePath))//create new txt file
                        {
                            Console.WriteLine(filePath + " was successfully created!");
                        }
                        PostEntry(filePath, true);//first place postentry can get called
                    }
                }
                taf = "";
                id = "";
                whole = "";
                filePath = "";
                newold = "";
            }
            while (newold == "o")
            {
                Console.WriteLine("Enter ID, or type list for a list of all ID's. type no to go back to new/old.");
                string r = Console.ReadLine();

                if (r == "list")//so print out all the current contents of my bookreview folder that end with .txt
                {
                    DirectoryInfo a = new DirectoryInfo(@"!!!YOUR BOOKREVIEW FOLDER GOES HERE!!!");//so setting a to our bookreviews folder on hd. MUST BE FILLED IN WITH YOUR FOLDER THAT IS IN 
                    FileInfo[] Files = a.GetFiles("*.txt"); //Getting Text files from your Bookreview folder
                    string str = ""; //empty string
                    foreach (FileInfo file in Files)//foreach .txt file in book reviews
                    {
                        str = file.Name;
                        Console.WriteLine(str);
                        str = "";
                        //so this foreach loop goes through each .txt file and prints out the filename line by line
                    }
                }
                if (r != "list" && r != "no")//so even if the format is improper it wont matter, we still try to search for the file; as long as r is not "list" or "no" try to search for the file.
                {
                    string fullName;
                    DirectoryInfo b = new DirectoryInfo(@"!!!YOUR BOOKREVIEW FOLDER GOES HERE");
                    FileInfo[] filesInDir = b.GetFiles("*" + r + "*.*");
                    foreach (FileInfo foundFile in filesInDir)//if you do not give it exact ID it will get stalled here at post entry and not let you pass until it has foreach'd each ID that matches your incorrect entry
                    {
                        fullName = foundFile.FullName;
                        Console.WriteLine("This is the file I found: " + fullName);
                        PostEntry(fullName, true); //second place postentry can get called
                        fullName = " ";
                    }
                }
                if (r == "no")
                {
                    newold = "";
                }
                
            }
        }
        public static void PostEntry(string fp, bool entry1) //collects entry, posts entry to new or old file
        {
            string entry = "";
            if (entry1 == true)
            {
                DateTime localDate = DateTime.Now;
                string current = localDate.ToString();//adds timestamp if this is the first time we are posting to this file while this .exe is open
                entry += current + " ";
                entry1 = false;
            }
            while (entry != "m")
            {
                Console.WriteLine("type m to go back to new/old, or add thoughts to this file.");
                entry = Console.ReadLine();

                if (entry != "m") { 

                using (StreamWriter writer = new StreamWriter(fp, true))
                {
                    writer.WriteLine(entry);
                }
                                  }
               
            }
        }
    }
}
