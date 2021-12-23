using System;

namespace RecursionKnight
{
    class Program
    {
        static int BoardSize = 8;
        static int attemptedMoves = 0;
        //all the possible combinations for the knight to move
        static int[] xMove = { 2, 1,-1,-2,-2,-1,1,2 };
        static int[] yMove = {1, 2, 2, 1,-1,-2,-2,-1};

        static int[,] boardGrid = new int[BoardSize, BoardSize]; 
        //each place on the board will have a default of -1 (no move has been made on it) or the attempted move number
        static void Main(string[] args)
        {
            SolveKt();
            Console.ReadLine();
        }
        //method contains all the logic for my program
         static void SolveKt()
        {
            //initalize all squares to not visited
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    //set initial value to -1, not visited status
                    boardGrid[i, j] = -1; 
                }
            }

            //initalize starting point, number of moves
            var startX = 0;
            var startY = 0; //starting point can be wherever we want
            boardGrid[startX, startY] = 0; //first move is here (move number 0) at starting point
            attemptedMoves = 0;

            //recursively try all possible legal moves. Backtrack on dead end solutions
            //this function will tell us if our starting point is valid
            if (!SolveKtUtility(startX,startY,1)) //takes starting point and move number
            {
                Console.WriteLine($"No solution found for {startX} {startY}");
            }
            else //there is a solution
            {
                printBoard(boardGrid); //prints board with move places
                Console.WriteLine($"Total attempted moves: {attemptedMoves}");
            }

            bool SolveKtUtility(int x, int y, int moveCount) //recursive method
            {
                attemptedMoves++;
                if (attemptedMoves % 1000000 == 0) //print message only if method was tried 1,000,000 times, method has been called a million times
                {
                    Console.WriteLine($"Attempted moves: {attemptedMoves}");
                }
                int k; //counter for moving through the nextX and nextY  arrays  (move possibilities), the knights possibel move
                int next_x, next_y; //location for next move in recursion

                //check to see if we have solved the game
                if (moveCount == BoardSize * BoardSize) //check if we have moved 64 times and our method has worked
                {
                    return true;
                }
                // cycle through all of the possible next moves for the knight
                for (k = 0; k < 8; k++)
                {
                    next_x = x + xMove[k];
                    next_y = y + yMove[k];
                    //before moving to the next place in our options to move we have to check that that place is empty and on the board
                    if (safeSquare(next_x,next_y)) //the square can be moved to
                    {
                        boardGrid[next_x, next_y] = moveCount; //set value of that square to be move count
                        if (SolveKtUtility(next_x, next_y, moveCount + 1)) //check if byb moving to this spot we can also move to a next spot
                            return true;
                        else //there is a dead end.
                            boardGrid[next_x, next_y] = -1; //we will not visit that square and keep it as unvisited. safesquare will check to make sure it wasn't visited before we change and look for a dead end
                    }
                  
                }
                return false;
            }
            //checks if the next move is still on the board
            bool safeSquare(int x, int y)
            {
                //check to see if x, y is on the board and not an out of bounds index
                //also check to see if the square is visited or not
                //is x or y between 0 and 8, on the board and that the square is -1.
                return (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize && boardGrid[x, y] == -1); //if any of these conditions fail the square is not safe to move to 
            }
            void printBoard(int[,] boardToPrint)
            {
                for (int a = 0; a < BoardSize; a++)
                {
                    for (int b = 0; b < BoardSize; b++)
                    {
                        if (boardGrid[a,b] < 10) //if number isn't 2 digits we add an extra space
                        {
                            Console.Write(" ");
                        }
                        Console.Write(boardGrid[a, b] + " ");
                    }
                    Console.WriteLine(); //creates a new line after each row
                }
            }
        }
    }
}
