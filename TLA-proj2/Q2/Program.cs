using System;
using System.Collections.Generic;

namespace Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Calculator cal = new Calculator(); 
            }
            catch
            {
              Console.WriteLine("INVALID");
            }
        }
    }
    class Calculator
    {
        public String Input;
        public List<string> Expression;
        public List<string> PostFix;
        public Stack<string> pdaStack;
        public Stack<double> calculateStack;
        Dictionary<string, int> Operations;
        public Calculator()
        {
            this.Input = Console.ReadLine();
            Expression = ConvertInputToList(this.Input);
            PostFix = MakeEquation();
            string resultwer=CalculatorPDA().ToString();
            string result1 = resultwer.Split('.')[0];
            string result2;
            if (resultwer.Split('.').Length == 1)
            {
                
                result2 = "00";
            }
            else
            {
                result2 = resultwer.Split('.')[1][0].ToString();
                result2+= resultwer.Split('.')[1][1].ToString();
            }
            Console.WriteLine(result1+"."+result2);
            
        }
        public List<string> ConvertInputToList(string input)
        {
            List<string> result=new List<string>();
            string[] checkarr = input.Split(' ');
            for(int i = 0; i < checkarr.Length-1; i++)
            {
                if (Char.IsDigit(checkarr[i][checkarr[i].Length-1]) && 
                    Char.IsDigit(checkarr[i+1][0]))
                {
                    
                    throw new Exception();
                }
            }
            for (int i = 0; i < input.Length;)
            {
                
                if (Char.IsDigit(input[i]))
                {
                    string newEx = "";
                    while (i<input.Length && (Char.IsDigit(input[i]) || input[i]=='.'))
                    {
                        newEx += input[i].ToString();
                        i++;
                    }
                    result.Add(newEx);
                }else if (input[i] == 's' && input[i + 1] == 'q' && 
                    input[i + 2] == 'r' && input[i + 3] == 't' && input[i + 4] == '(')
                {
                    result.Add("sqrt");
                    i += 4;
                }
                else if (input[i] == '-' && input[i + 1] == 's' && 
                    input[i + 2] == 'q' && input[i + 3] == 'r' && 
                    input[i + 4] == 't' && input[i + 5] == '(')
                {
                    result.Add("-sqrt");
                    i += 5;
                }
                else if (input[i] == 'a' && input[i + 1] == 's' && 
                    input[i + 2] == 'i' && input[i + 3] == 'n' && input[i + 4] == '(')
                {
                    result.Add("asin");
                    i += 4;
                }
                else if (input[i] == '-' && input[i + 1] == 'a' && 
                    input[i + 2] == 's' && input[i + 3] == 'i' && 
                    input[i + 4] == 'n' && input[i + 5] == '(')
                {
                    result.Add("-asin");
                    i += 5;
                }
                else if (input[i] == 'a' && input[i + 1] == 'c' && 
                    input[i + 2] == 'o' && input[i + 3] == 's' && input[i + 4] == '(')
                {
                    result.Add("acos");
                    i += 4;
                }
                else if (input[i] == '-' && input[i + 1] == 'a' && 
                    input[i + 2] == 'c' && input[i + 3] == 'o' && 
                    input[i + 4] == 's' && input[i + 5] == '(')
                {
                    result.Add("-acos");
                    i += 5;
                }
                else if (input[i] == 'a' && input[i + 1] == 't' && input[i + 2] == 'a' && 
                    input[i + 3] == 'n' && input[i + 4] == '(')
                {
                    result.Add("atan");
                    i += 4;
                }
                else if (input[i] == '-' && input[i + 1] == 'a' && 
                    input[i + 2] == 't' && input[i +3] == 'a' && 
                    input[i + 4] == 'n' && input[i + 5] == '(')
                {
                    result.Add("-atan");
                    i += 5;
                }
                else if (input[i] == 's' && input[i + 1] == 'i' && 
                    input[i + 2] == 'n' && input[i + 3] == 'h' && input[i + 4] == '(')
                {
                    result.Add("sinh");
                    i += 4;
                }
                else if (input[i] == '-' && input[i + 1] == 's' && 
                    input[i + 2] == 'i' && input[i + 3] == 'n' && 
                    input[i + 4] == 'h' && input[i + 5] == '(')
                {
                    result.Add("-sinh");
                    i += 5;
                }
                else if (input[i] == 'c' && input[i + 1] == 'o' && 
                    input[i + 2] == 's' && input[i + 3] == 'h' && input[i + 4] == '(')
                {
                    result.Add("cosh");
                    i += 4;
                }
                else if (input[i] == '-' && input[i + 1] == 'c' && 
                    input[i + 2] == 'o' && input[i + 3] == 's' && 
                    input[i + 4] == 'h' && input[i + 5] == '(')
                {
                    result.Add("-cosh");
                    i += 5;
                }
                else if (input[i] == 't' && input[i + 1] == 'a' && 
                    input[i + 2] == 'n' && input[i + 3] == 'h' && input[i + 4] == '(')
                {
                    result.Add("tanh");
                    i += 4;
                }
                else if (input[i] == '-' && input[i + 1] == 't' && 
                    input[i + 2] == 'a' && input[i + 3] == 'n' && 
                    input[i + 4] == 'h' && input[i + 5] == '(')
                {
                    result.Add("-tanh");
                    i += 5;
                }
                else if (input[i] == 's' && input[i + 1] == 'i' && 
                    input[i + 2] == 'n' && input[i + 3] == '(')
                {
                    result.Add("sin");
                    i += 3;
                }
                else if (input[i] == '-' && input[i + 1] == 's' && 
                    input[i + 2] == 'i' && input[i + 3] == 'n' && 
                    input[i + 4] == '(')
                {
                    result.Add("-sin");
                    i += 4;
                }
                else if (input[i] == 'c' && input[i + 1] == 'o' && 
                input[i + 2] == 's' && input[i + 3] == '(')
                {
                    result.Add("cos");
                    i += 3;
                }
                else if (input[i] == '-' && input[i + 1] == 'c' &&
                input[i + 2] == 'o' && input[i + 3] == 's' && 
                input[i + 4] == '(')
                {
                    result.Add("-cos");
                    i += 4;
                }
                else if (input[i] == 't' && input[i + 1] == 'a' && 
                input[i + 2] == 'n' && input[i + 3] == '(')
                {
                    result.Add("tan");
                    i += 3;
                }
                else if (input[i] == '-' && input[i + 1] == 't' && 
                input[i + 2] == 'a' && input[i + 3] == 'n' && 
                input[i + 4] == '(')
                {
                    result.Add("-tan");
                    i += 4;
                }
                else if (input[i] == 's' && input[i + 1] == 'g' && 
                input[i + 2] == 'n' && input[i + 3] == '(')
                {
                    result.Add("sgn");
                    i += 3;

                }
                else if (input[i] == '-' && input[i + 1] == 's' && 
                input[i + 2] == 'g' && input[i + 3] == 'n' && 
                input[i + 4] == '(')
                {
                    result.Add("-sgn");
                    i += 4;

                }
                else if (input[i] == 'a' && input[i + 1] == 'b' && 
                input[i + 2] == 's' && input[i + 3] == '(')
                {
                    result.Add("abs");
                    i += 3;
                }
                else if (input[i] == '-' && input[i + 1] == 'a' && 
                input[i + 2] == 'b' && input[i + 3] == 's' && input[i + 4] == '(')
                {
                    result.Add("-abs");
                    i += 4;
                }
                else if (input[i] == 'e' && input[i + 1] == 'x' && 
                input[i + 2] == 'p' && input[i + 3] == '(')
                {
                    result.Add("exp");
                    i += 3;
                }
                else if (input[i] == '-' && input[i + 1] == 'e' && 
                input[i + 2] == 'x' && input[i + 3] == 'p' && 
                input[i +4] == '(')
                {
                    result.Add("-exp");
                    i += 4;
                }
                else if (input[i] == 'l' && input[i + 1] == 'n' && 
                input[i + 2] == '(')
                {
                    result.Add("ln");
                    i += 2;
                }
                else if (input[i] == '-' && input[i + 1] == 'l' && 
                input[i + 2] == 'n' && input[i + 3] == '(')
                {
                    result.Add("-ln");
                    i += 3;
                }
                else if (input[i] == '-')
                {

                    if (Char.IsDigit(input[i + 1]))
                    {
                        if (Char.IsDigit(result[result.Count - 1][result[result.Count - 1].Length - 1]) || result[result.Count - 1][result[result.Count - 1].Length - 1]==')')
                        {
                            result.Add("-");
                            i++;
                        }
                        else
                        {
                            string str = "-";
                            i++;
                            while (i < input.Length && (Char.IsDigit(input[i]) || input[i] == '.'))
                            {
                                str += input[i].ToString();
                                i++;
                            }
                            result.Add(str);
                        }
                    }
                    else
                    {
                        result.Add("-");
                        i++;
                    }
                }
                else if (input[i]=='+' || input[i] == '/' || input[i] == '*' || 
                input[i] == '(' || input[i] == ')' || input[i] == '^')
                {
                    result.Add(input[i].ToString());
                    i++;
                    continue;
                }
                else if(input[i]==' ')
                    i++;
                else
                {
                    result = new List<string>();
                    result.Add("error");
                    throw new Exception();
                }
            }
            return result;
        }
        public List<string> MakeEquation()
        {
            pdaStack = new Stack<string>();
            Operations = new Dictionary<string, int>();
            Operations.Add("+", 1);
            Operations.Add("-", 1);
            Operations.Add("*", 2);
            Operations.Add("/", 2);
            Operations.Add("^", 3);
            Operations.Add("sqrt", 4);
            Operations.Add("sin", 4);
            Operations.Add("cos", 4);
            Operations.Add("tan", 4);
            Operations.Add("asin", 4);
            Operations.Add("acos", 4);
            Operations.Add("atan", 4);
            Operations.Add("sinh", 4);
            Operations.Add("cosh", 4);
            Operations.Add("tanh", 4);
            Operations.Add("exp", 4);
            Operations.Add("ln", 4);
            Operations.Add("sgn", 4);
            Operations.Add("abs", 4);
            Operations.Add("-sqrt", 4);
            Operations.Add("-sin", 4);
            Operations.Add("-cos", 4);
            Operations.Add("-tan", 4);
            Operations.Add("-asin", 4);
            Operations.Add("-acos", 4);
            Operations.Add("-atan", 4);
            Operations.Add("-sinh", 4);
            Operations.Add("-cosh", 4);
            Operations.Add("-tanh", 4);
            Operations.Add("-exp", 4);
            Operations.Add("-ln", 4);
            Operations.Add("-sgn", 4);
            Operations.Add("-abs", 4);
            List<string> result = new List<string>();
            for(int i = 0; i < Expression.Count; i++)
            {
                if (Expression[i] == "(")
                    pdaStack.Push("(");
                
                else if(Expression[i] == ")")
                {
                    while (pdaStack.Count!=0 && pdaStack.Peek() != "(")
                        result.Add(pdaStack.Pop());
                    
                    if (pdaStack.Count == 0)     
                        throw new Exception();

                    pdaStack.Pop();
                }
                else if(Expression[i]=="+" || Expression[i]=="-" || Expression[i] == "*" || 
                    Expression[i] == "/" || Expression[i] == "^" || Expression[i] == "sqrt" || 
                    Expression[i] == "sin" || Expression[i] == "cos" || Expression[i] == "tan" || 
                    Expression[i] == "sinh" || Expression[i] == "cosh" || Expression[i] == "tanh" || 
                    Expression[i] == "asin" || Expression[i] == "acos" || Expression[i] == "atan" || 
                    Expression[i] == "abs" || Expression[i] == "sgn" || Expression[i] == "exp" || 
                    Expression[i] == "ln" || Expression[i] == "-sqrt" || Expression[i] == "-sin" ||
                    Expression[i] == "-cos" || Expression[i] == "-tan" || Expression[i] == "-sinh" || 
                    Expression[i] == "-cosh" || Expression[i] == "-tanh" || Expression[i] == "-asin" || 
                    Expression[i] == "-acos" || Expression[i] == "-atan" || Expression[i] == "-abs" || 
                    Expression[i] == "-sgn" || Expression[i] == "-exp" || Expression[i] == "-ln")
                {
                    while (pdaStack.Count>0 && 
                        pdaStack.Peek()!="(" && Operations[pdaStack.Peek()] >= Operations[Expression[i]])
                    {
                        result.Add(pdaStack.Pop());
                    }
                    pdaStack.Push(Expression[i]);
                }
                else
                {
                    result.Add(Expression[i]);
                }
            }
            while (pdaStack.Count > 0)
            {
                if (pdaStack.Peek() == "(")
                {
                    throw new Exception();
                }
                result.Add(pdaStack.Pop());
            }
            return result;
        }

        public double CalculatorPDA()
        {
            calculateStack = new Stack<double>();
            double result=0;
            for(int i = 0; i < PostFix.Count; i++)
            {
                if (PostFix[i] == "+")
                {
                    double x = calculateStack.Pop();
                    double y = calculateStack.Pop();
                    calculateStack.Push(x + y);
                }
                else if(PostFix[i] == "-")
                {
                    double x = calculateStack.Pop();
                    double y = calculateStack.Pop();
                    calculateStack.Push(y - x);
                }
                else if (PostFix[i] == "*")
                {
                    double x = calculateStack.Pop();
                    double y = calculateStack.Pop();
                    calculateStack.Push(x * y);
                }
                else if (PostFix[i] == "/")
                {
                    double x = calculateStack.Pop();
                    double y = calculateStack.Pop();
                    if (x == 0)
                    {
                        throw new Exception();
                    }
                    calculateStack.Push(y / x);
                }
                else if (PostFix[i] == "^")
                {
                    double x = calculateStack.Pop();
                    double y = calculateStack.Pop();
                    calculateStack.Push(Math.Pow(y,x));
                }
                else if (PostFix[i] == "sqrt")
                {
                    double x = calculateStack.Pop();
                    if (x < 0)
                    {
                        throw new Exception();
                    }
                    calculateStack.Push(Math.Sqrt(x));
                }
                else if (PostFix[i] == "-sqrt")
                {
                    
                    double x = calculateStack.Pop();
                    if (x < 0)
                    {
                        throw new Exception();
                    }
                    calculateStack.Push((-1)*Math.Sqrt(x));
                }
                else if (PostFix[i] == "sin")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push(Math.Sin(x));
                }
                else if (PostFix[i] == "-sin")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push((-1)*Math.Sin(x));
                }
                else if (PostFix[i] == "cos")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push(Math.Cos(x));
                }
                else if (PostFix[i] == "-cos")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push((-1)*Math.Cos(x));
                }
                else if (PostFix[i] == "tan")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push(Math.Tan(x));
                }
                else if (PostFix[i] == "-tan")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push((-1)*Math.Tan(x));
                }
                else if (PostFix[i] == "asin")
                {
                    
                    double x = calculateStack.Pop();
                    if (x < -1 || x > 1)
                    {
                        throw new Exception();
                    }
                    calculateStack.Push(Math.Asin(x));
                }
                else if (PostFix[i] == "-asin")
                {
                    
                    double x = calculateStack.Pop();
                    if (x < -1 || x > 1)
                    {
                        throw new Exception();
                    }
                    calculateStack.Push((-1)*Math.Asin(x));
                }
                else if (PostFix[i] == "acos")
                {
                    
                    double x = calculateStack.Pop();
                    if (x < -1 || x > 1)
                    {
                        throw new Exception();
                    }
                    calculateStack.Push(Math.Acos(x));
                }
                else if (PostFix[i] == "-acos")
                {
                    double x = calculateStack.Pop();
                    if (x < -1 || x>1)
                    {
                        throw new Exception();
                    }
                    calculateStack.Push((-1)*Math.Acos(x));
                }
                else if (PostFix[i] == "atan")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push(Math.Atan(x));
                }
                else if (PostFix[i] == "-atan")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push((-1)*Math.Atan(x));
                }
                else if (PostFix[i] == "sgn")
                {
                    double x = calculateStack.Pop();
                    if (x > 0)
                    {
                        calculateStack.Push(+1);
                    }
                    else if (x == 0)
                    {
                        calculateStack.Push(0);
                    }
                    else
                    {
                        calculateStack.Push(-1);
                    }
                }
                else if (PostFix[i] == "-sgn")
                {
                    double x = calculateStack.Pop();
                    if (x > 0)
                    {
                        calculateStack.Push(-1);
                    }
                    else if (x == 0)
                    {
                        calculateStack.Push(0);
                    }
                    else
                    {
                        calculateStack.Push(1);
                    }
                }
                else if (PostFix[i] == "abs")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push(Math.Abs(x));
                }
                else if (PostFix[i] == "-abs")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push((-1)*Math.Abs(x));
                }
                else if (PostFix[i] == "exp")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push(Math.Exp(x));
                }
                else if (PostFix[i] == "-exp")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push((-1)*Math.Exp(x));
                }
                else if (PostFix[i] == "ln")
                {
                    double x = calculateStack.Pop();
                    if (x <= 0)
                    {
                        throw new Exception();
                    }
                    calculateStack.Push(Math.Log(x,Math.E));
                }
                else if (PostFix[i] == "-ln")
                {
                    double x = calculateStack.Pop();
                    if (x <= 0)
                    {
                        throw new Exception();
                    }
                    calculateStack.Push((-1)*Math.Log(x, Math.E));
                }
                else if (PostFix[i] == "sinh")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push(Math.Sinh(x));
                }
                else if (PostFix[i] == "-sinh")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push((-1)*Math.Sinh(x));
                }
                else if (PostFix[i] == "cosh")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push(Math.Cosh(x));
                }
                else if (PostFix[i] == "-cosh")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push((-1)*Math.Cosh(x));
                }
                else if (PostFix[i] == "tanh")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push(Math.Tanh(x));
                }
                else if (PostFix[i] == "-tanh")
                {
                    double x = calculateStack.Pop();
                    calculateStack.Push((-1)*Math.Tanh(x));
                }
                else
                {
                    calculateStack.Push(double.Parse(PostFix[i]));
                }
            }
            if (calculateStack.Count == 1)
            {
                result = calculateStack.Pop();
            }
            else
                throw new Exception();

            return result;
        }
    }
}
