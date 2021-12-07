/*
By: Romon Wafa
*/ 
using System;
using System.Collections.Generic;
using stacks;
//using DoublyLinkedListTab;

class Browser
{
    //Declaring Variables as well as two lists.
    //One list is for the Tab numbers and the other will contain the tabs
    private static int currentTab, count;
    private static List<Tab> T = new List<Tab>();
    private static List<int> TabNumbers = new List<int>();
    public static void Main()
    {
        //A switch in a do while loop to enter and parse user input.
        //also contains a try/catch to give an error in case invalid input is entered.
        char selection;
        Console.WriteLine("Welcome to Browser Startpage");
        Console.WriteLine("Press S to start a new tab, M to move to an existing tab, C to close a tab, V to visit a site, \nand B and F to move backwards and forwards through sites in a tab. Finally press Q to quit.");
        Start();
        do {
            try {
                Console.WriteLine("\nWhat would you like to do?");
                selection = Convert.ToChar(Console.ReadLine());
                

                switch (Char.ToUpper(selection))
                {
                    case 'S':
                        Start();
                        break;
                    case 'M':
                        Console.WriteLine("What tab will you move to?");
                        Move(Convert.ToInt16(Console.ReadLine()));
                        break;
                    case 'C':
                        Console.WriteLine("What tab will you close?");
                        Close(Convert.ToInt16(Console.ReadLine()));
                        break;
                    case 'V':
                        Console.WriteLine("What site will you visit?");
                        Visit(Console.ReadLine());
                        break;
                    case 'B':
                        Back();
                        break;
                    case 'F':
                        Forward();
                        break;
                    case 'P':
                        Print();
                        break;
                    case 'Q':
                        Console.WriteLine("Good Bye");
                        break;
                    default:
                        Console.WriteLine("Invalid entry detected. Try again.");
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid entry detected. Try again.");
                //arbitrary selection to trigger the default case
                selection = 'n';
            }
        } while (char.ToUpper(selection) != 'Q');
        Console.ReadLine();
    }
    //BrowserMethod is the method that creates a new Tab in a node of list T
    //It also contains a second list that looks at what tab numbers exist and create the first availible one
    //The bool flag ensures only one new TabNumbers is created
    //The list is then sorted to keep orgainization.
    public static void BrowserMethod()
    {
        T.Add(new Tab());       
        bool flag = false;
        for (int i=0; i <= count; i++)
        {
            if (!TabNumbers.Contains(i) && flag == false)
            {          
                TabNumbers.Add(i);
                flag = true;
                currentTab = TabNumbers[i];                
            }
        }        
        TabNumbers.Sort();
        count++;
    }
    //Creates a new tab and sets the webpage to "Homepage" by using the Visit() method with 0 constructors 
    public static void Start()
    {
        BrowserMethod();
        T[currentTab].Visit();
        Print();
    }
    //Checks to see if a tab exists in TabNumber and moves to it if it does. Gives error if it does not exist.
    public static void Move(int tab)
    {
        if (TabNumbers.Contains(tab))
        {
            currentTab = tab;
        }
        else
        {
            Console.WriteLine("Tab not found");
        }
        Print();
    }
    //First checks to see if the count is not 1 and if the user selection is contained with in the list. If not the user selection is invalid or there is only one item in the list and we can't close it.
    //Then the tab is removed and count reduced by 1.
    //finally the currentTab is set to 0 as per deliverables.
    public static void Close (int tab)
    {
        if (count != 1 && TabNumbers.Contains(tab))
        {
            TabNumbers.Remove(tab);
            count--;            
            currentTab = TabNumbers[0];
        }
        else
        {
            Console.WriteLine("cannot close last tab, or tab that does not exist");
        }
        Print();
    }   
    //mirrored in tab class
    //calls the Visit() method with the users input to the Tab class
    public static void Visit (string s)
    {
        T[currentTab].Visit(s);
        Print();
    }
    //calls the Back() method to the Tab class
    public static void Back()
    {
        T[currentTab].Back();
        Print();
    }
    //calls the Forward() method to the Tab class
    public static void Forward()
    {
        T[currentTab].Forward();
        Print();
    }
    //goes through the TabNumbers list printing out each instance
    //if the currentTab is printed the colour will be changed to red for ease of viewing
    //The method then calls the T[currentTab].Print() method in the Tab class to print the website being viewed
    public static void Print()
    {
        foreach (int i in TabNumbers)
        {
            
            if (i == currentTab)
            {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.Write(i + "      ");
               Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write(i + "      ");
            }
            
        }
        Console.WriteLine("\nYou are on tab: " + currentTab);
        T[currentTab].Print();
    }
}
//Namespace for the two stacks approach
namespace stacks
{
    class Tab
    {
        //Created two new stacks to contain a string for the websites
        private Stack<string> S1 = new Stack<string>();
        private Stack<string> S2 = new Stack<string>();
        
        //Visit() has a 0 argument and 1 argument constructor
        //if called with 0 arguments then s will default to "Homepage"
        //otherwise it will take whatever string the user passed using the Visit() method in the Browser class
        public void Visit(string s = "Homepage")
        {
            if (s != "Homepage")
            {
                //method using the visit command
                //adds user input to the top of the page and clears anything in S2 as per deliverables
                S1.Push(s);                
                S2.Clear();              
            }
            else
            {
                //method using the start command
                //adds Homepage to S1
                S1.Push(s);               
            }
        }
        //Checks if S1.Count != 1 to ensure it doesn't go back into a void area 
        //Copies S1 into a variable and pushes the variable into S2 and pops S1 removing it from the stack 
        public void Back()
        {
            if (S1.Count !=1)
            {
                string page;
                page = S1.Peek();
                S2.Push(page);
                S1.Pop();
                Console.WriteLine("You have moved back to: " + S1.Peek());
            }
        }
        //Checks if S2.Count != 0 to ensure it doesn't go forward into a void area 
        //Copies S2 into a variable and pushes the variable into S1 and pops S2 removing it from the stack
        public void Forward()
        {
            string page;
            if (S2.Count != 0)
            {
                page = S2.Peek();
                S1.Push(page);
                S2.Pop();
                Console.WriteLine("You have moved back to: " + S1.Peek()+ "\n" );
            }           
        }
        //Prints out the current homepage
        public void Print()
        {
            Console.WriteLine("You are on page " + S1.Peek() + "\n" );
        }
    }
}

namespace DoublyLinkedListTab
{
    public class Node<T>
    {
        public string WebPage { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }

        public Node(string webPage, Node<T> prev, Node<T> next = null)
        {
            WebPage = webPage;
            Next = next;
            Prev = prev;
        }
    }

    class Tab
    {
        private Node<string> head;
        private Node<string> curr;
        private int count;

        public void Visit(string s = "Homepage")
        {
            if (s == "Homepage")
            {
                head = new Node<string>("Homepage", null, null);
                curr = head;
                //Console.WriteLine("You are on page " + head.WebPage);
                count = 1;
            }

            else
            {
                while (curr.Next != null)
                {
                    curr = curr.Next;
                    count++;
                }
                curr.Next = new Node<string>(s, curr.Prev, curr.Next);
                curr = curr.Next;
                //Console.WriteLine("You are on page " + s);
                count++;
            }
        }

        public void Back()
        {
            if (curr != head)
            {


                curr = head;
                for (int i = 1; i < count - 1; i++)
                {
                    curr = curr.Next;
                }
                Console.WriteLine("You moved back to " + curr.WebPage);
                count--;
            }

            else
            {
                Console.WriteLine("'{0}' is the first web page. There are no more websites to move backwards to.", curr.WebPage);
            }
        }

        public void Forward()
        {
            if (curr.Next != null)
            {
                curr = head;
                for (int i = 1; i < count + 1; i++)
                {
                    curr = curr.Next;
                }
                Console.WriteLine("You moved forwards to " + curr.WebPage);
                count++;
            }

            else
            {
                Console.WriteLine("'{0}' is the most recent web page. There are no more websites to move forward to.", curr.WebPage);
            }
        }
        public void Print()
        {
            Console.WriteLine("You are on page: " + curr.WebPage);
        }
    }
}