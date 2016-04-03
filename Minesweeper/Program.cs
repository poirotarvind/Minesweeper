using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{

    public class Player
    {
        private string validInputs = null;
        Stack<string> moveStack = null;
        int i_ptY, i_ptX;
        String validCommand;

        MineField newMine = null;

        public Player( MineField newMine)
        {
            this.newMine = newMine;
            validInputs = "of";
            moveStack = new Stack<string>();
        }

        public String GameMove(string move)
        {
            String currentToken;
            Boolean b;
            int status = 0;

            String[] token = move.Split(' ');
            for (int i = 0; i < token.Length; i++)
            {
                currentToken = token[i];
                b = IsInteger(currentToken);

                if (b)
                {
                    moveStack.Push(currentToken);

                    if (moveStack.Count == 2)
                    {
                        i_ptY = Convert.ToInt32(moveStack.Pop());
                        i_ptX = Convert.ToInt32(moveStack.Pop());
                    }
                }

                else if (validInputs.IndexOf(currentToken) > -1)
                {
                    validCommand = currentToken;
                    status= doCommand(currentToken, i_ptX, i_ptY);
                }
            }

            return publish_values(status);
        }

        public int doCommand(String currentToken, int x, int y)
        {
            int status = 0;
            if (newMine != null)
            {
                if (currentToken == "o")
                    status = newMine.OpenBox(x, y);
                else
                    status = newMine.FlagBox(x, y);
            }
            return status;

        }

        public bool IsInteger(string theValue)
        {
            try
            {
                Convert.ToInt32(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public String publish_values( int status)
        {
            String s = null;
            if (status == 1)
            {
                s = validCommand + " " + i_ptX + " " + i_ptY + " ";
                Console.WriteLine(s);
                
            }

            else
            {
                Console.WriteLine("Game over!!!");
            }
            return s;
        }

    }


    public class MineField
    {
        private int x_input, y_input;
        private int[,] box;

        public MineField(int gridCount)
        {
            int mine = 0;
            int safeSpot = 1;
            this.box = new int[gridCount, gridCount];
            for (int i = 0; i < gridCount; i++)
            {
                for (int j = 0; j < gridCount; j++)
                {
                    box[i, j] = safeSpot;
                }
            }

            int numOfMine = gridCount - 1;
            for (int i = 0; i < numOfMine; i++)
            {
                for (int j = 0; j < numOfMine; j++)
                {
                    box[i, j] = mine;
                }
            }

        }
        public int OpenBox(int x_input, int y_input)
        {
            this.x_input = x_input;
            this.y_input = y_input;

            if (box[x_input, y_input] == 1)
                return 1;
            else
                return 0;
        }

        public int FlagBox(int x_input, int y_input)
        {
            this.x_input = x_input;
            this.y_input = y_input;

            if (box[x_input, y_input] == 0)
                return 1;
            else
                return 0;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            int gridCount;
            string exit = null;
            String moves = String.Empty;
            Console.WriteLine("Please Enter the GridCount: - (interger value)");
            gridCount = Console.Read();
            MineField newMine = new MineField(gridCount);

            Console.WriteLine("Game Start Now: -\n");
            Console.ReadLine();

            Player newPlayer = new Player( newMine);
            //do
            //{
                Console.WriteLine("Please Enter the Operation and Coordinates: - (eg: -  1 2 o)");
                moves = Console.ReadLine();
                newPlayer.GameMove(moves);
            //} while (exit.Equals("1"));

            //Console.WriteLine("Please Enter 1 to continue and X to exit");
            //exit = Console.ReadLine();

        }

    }
}

