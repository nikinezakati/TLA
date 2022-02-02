using System;
using System.Collections.Generic;
using System.Linq;

namespace Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(new string[] { "00" },
                StringSplitOptions.None);
            List<Trans> trans = new List<Trans>();
            foreach (var inp in input)
                trans.Add(MakeTrans(inp.Split('0')));

            TMachine tm = new TMachine(trans, FinalState(trans));
            int n = int.Parse(Console.ReadLine());
            List<string> newInputs = new List<string>();
            for (int i = 0; i < n; i++)
            {
                string[] curr = Console.ReadLine().Split('0');
                List<char> a = new List<char>();
                foreach (var c in curr)
                    a.Add(MakeAlphabet(c));
    
                string b = "";
                foreach (var t in a)
                {
                    b += t;
                }
                newInputs.Add(b);
            }
            foreach (var ni in newInputs)
            {
                if (tm.isStringAccepted(ni))
                    Console.WriteLine("Accepted");
                else
                    Console.WriteLine("Rejected");
            }
        }
        public static string MakeHead(string input)
        {
            string d = "";
            if (input == "1")
                d = "L";

            else if (input == "11")

                d = "R";

            return d;
        }
        public static char MakeAlphabet(string input)
        {
            char d;
            if (input == "1" || input == "")
                d = '#';
            else
                d = (char)(97 + input.Length - 2);
            return d;
        }
        public static string MakeState(string input)
        {
            string d = "q" + input.Length;
            return d;
        }
        public static Trans MakeTrans(string[] input)
        {
            Trans d = new Trans(MakeState(input[0]), MakeAlphabet(input[1]),
                MakeState(input[2]), MakeAlphabet(input[3]), MakeHead(input[4]));
            return d;
        }
        public static string FinalState(List<Trans> trans)
        {
            List<string> temp = new List<string>();
            foreach (var t in trans)
            {
                temp.Add(t.start);
                temp.Add(t.last);
            }
            temp = temp.Distinct().ToList();
            return temp.Max();
        }
    }
    public class Trans
    {
        public string start, last, head;
        public char inputTape, outputTape;
        public Trans(string s, char it, string l,
            char op, string h)
        {
            this.start = s;
            this.inputTape = it;
            this.last = l;
            this.outputTape = op;
            this.head = h;
        }
    }
    class TMachine
    {
        public string FinalState;
        public List<Trans> Trans;
        public TMachine(List<Trans> trans, string finalState)
        {
            this.Trans = trans;
            this.FinalState = finalState;
        }
        public List<char> FirstNull(List<char> tape)
        {
            List<char> temp = new List<char>();
            temp.Add('#');
            for (int i = 0; i < tape.Count; i++)
                temp.Add(tape[i]);

            return temp;
        }
        public bool isStringAccepted(string str)
        {
            int currIndx;
            string currState;
            List<char> tape = new List<char>();
            for (int i = 0; i < tape.Count; i++)
                tape[i] = str[i];

            tape = FirstNull(tape);
            tape = LastNull(tape);
            currIndx = 1;
            currState = "q1";
            bool halts = false;
            while (!halts)
            {
                bool transFound = false;
                foreach (var t in Trans)
                {
                    if (t.start == currState && t.inputTape == tape[currIndx])
                    {
                        transFound = true;
                        currState = t.last;
                        tape[currIndx] = t.outputTape;
                        if (t.head == "L")
                        {
                            currIndx -= 1;
                            if (t.inputTape == '#')
                            {
                                tape = FirstNull(tape);
                                currIndx += 1;
                            }
                        }
                        else if (t.head == "R")
                        {
                            currIndx += 1;
                            if (t.inputTape == '#')
                                tape = LastNull(tape);
                        }
                        break;
                    }
                }
                if (currState == FinalState)
                    return true;
                
                else if (!transFound)
                    return false;
                
            }
            return false;
        }
        public List<char> LastNull(List<char> tape)
        {
            tape.Add('#');
            return tape;
        }
    }
}
