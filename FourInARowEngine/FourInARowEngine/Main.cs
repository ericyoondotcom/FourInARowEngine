// MADE BY ERIC YOON (http://yooniverse.games).
using System;

class FourInARowEngine
{

    static states[,] board = new states[7, 6];
    public static void Main()
    {
        Console.Write("Initializing board...");
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                board[x, y] = states.empty;
            }
        }
        Console.WriteLine(" DONE!");
        Console.WriteLine("=================");
        while (true)
        {
            Console.WriteLine("Defender's turn!");
            int d = Programs.DefendingProgram(board);
            if (!MoveLegalCheck(d))
            {
                Console.WriteLine("Defender makes illegal move! Attacker wins.");
                return;
            }
            bool foundD = false;
            for (int y = board.GetLength(1) - 1; y >= 0; y--)
            {
                if (board[d, y] != states.empty)
                {
                    board[d, y + 1] = states.defender;
                    foundD = true;
                    break;
                }
            }
            if (!foundD)
            {
                board[d, 0] = states.defender;
            }
            ShowBoard();
            if(WinCheck())
            {
                return;
            }

            Console.WriteLine("=================");
            Console.WriteLine("Attacker's turn!");
            int a = Programs.AttackingProgram(board);
            if (!MoveLegalCheck(a))
            {
                Console.WriteLine("Attacker makes illegal move! Defender wins.");
                return;
            }
            bool foundA = false;
            for (int y = board.GetLength(1) - 1; y >= 0; y--)
            {
                if (board[a, y] != states.empty)
                {
                    board[a, y + 1] = states.attacker;
                    foundA = true;
                    break;
                }
            }
            if (!foundA)
            {
                board[a, 0] = states.attacker;
            }
            ShowBoard();
			if (WinCheck())
			{
				return;
			}

            Console.WriteLine("=================");
        }
    }

    //totally seperate from CheckWin(). CheckWin() is the logic while this is just to save me from writing duplicate code
    static bool WinCheck(){
        switch(CheckWin()){
            case winStates.attacker:
                Console.WriteLine("Attacker wins by four-in-a-row! Good Game!");
                return true;
            case winStates.defender:
                Console.WriteLine("Defender wins by four-in-a-row! Good Game!");
                return true;
            case winStates.draw:
                Console.WriteLine("Draw!");
                return true;
            case winStates.none:
                return false;
            default:
                return false;
        }
    }

    static winStates CheckWin(){
        //HORIZONTAL WIN CHECK
        for (int team = 0; team < 2; team++)
        {
            states teamState = (team == 0 ? states.defender : states.attacker);
            for (int y = 0; y < board.GetLength(1); y++)
            {
                for (int x = 0; x < board.GetLength(0) - 3; x++)
                {
                    bool successful = true;
                    for (int pos = 0; pos < 4; pos++){
                        if (board[x + pos, y] != teamState)
                        {
                            successful = false;
                        }

                    }
                    if (successful)
                    {
                        return (team == 0 ? winStates.defender : winStates.attacker);
                    }
                }
            }






        for (int t = 0; t < 2; t++)
        {
            states tState = (team == 0 ? states.defender : states.attacker);
            for (int x = 0; x < board.GetLength(0); x++){

                    for (int y = 0; y < board.GetLength(1) - 3; y++)
                    {
                        bool success = true;
                        for (int pos = 0; pos < 4; pos++)
                        {
                            if (board[x, y + pos] != tState)
                            {
                                success = false;
                            }
                        }
                        if (success)
                        {
                            return (team == 0 ? winStates.defender : winStates.attacker);
                        }
                    }
                }
        }
    }





        return winStates.none;
	}

    static bool MoveLegalCheck(int move)
    {
        if (move < 0 || move >= 7)
        {
            return false;
        }
        if (board[move, board.GetLength(1) - 1] != states.empty)
        {
            return false;
        }
        return true;
    }

    static void ShowBoard()
    {
        Console.Write("Board ASCII Representation");
        for (int y = board.GetLength(1) - 1; y >= 0; y--)
        {
            Console.WriteLine("\n---------------");
            for (int x = 0; x < board.GetLength(0); x++)
            {
                Console.Write("|");
                switch (board[x, y])
                {
                    case states.defender:
                        Console.Write("O");
                        break;
                    case states.attacker:
                        Console.Write("X");
                        break;
                    case states.empty:
                        Console.Write(" ");
                        break;
                }

            }
            Console.Write("|");
        }
        Console.WriteLine("\n---------------\n");
    }

}
