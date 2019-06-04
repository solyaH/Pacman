using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PacManGame.Maze;

namespace PacManGame.Logic
{
    public class Ghost
    {
        public MyPoint currentPosition;//поточне розташування
        Random rand ;
        private Cell[,] myMaze;
        public Direction currentDirection;// поточний напрям руху

        public Ghost(Cell[,] myMaze, MyPoint currentPacmanPosition, Random rand)
        {
            this.myMaze = myMaze;
            this.rand = rand;
            currentDirection = (Maze.Direction)rand.Next(0, 4);
            currentPosition = new MyPoint(rand.Next(0, myMaze.GetLength(0)), rand.Next(0, myMaze.GetLength(1)));
            while (currentPosition.X == currentPacmanPosition.X && currentPosition.Y == currentPacmanPosition.Y)
            {//у випадку якщо поточне розташування привида співпадає з позицією пакмена
                currentPosition = new MyPoint(rand.Next(0, myMaze.GetLength(0)), rand.Next(0, myMaze.GetLength(1)));
            }
        }

        public void MoveGhost()//здійснити крок привида
        {           
            if (currentDirection == Direction.up)
            {
                if ((myMaze[currentPosition.X, currentPosition.Y]).upperWall == false)
                {
                        currentPosition.Y--;
                }
                else
                {
                    currentDirection = (Maze.Direction)rand.Next(0, 4);//якщо зіткнулися із стіною-змінити напрям руху
                }
            }
            if (currentDirection == Direction.down)
            {
                if ((myMaze[currentPosition.X, currentPosition.Y]).lowerWall == false)
                {
                    currentPosition.Y++;
                }
                else
                {
                    currentDirection = (Maze.Direction)rand.Next(0, 4);
                }
            }
            if (currentDirection == Direction.left)
            {
                if ((myMaze[currentPosition.X, currentPosition.Y]).leftWall == false)
                {
                    currentPosition.X--;
                }
                else
                {
                    currentDirection = (Maze.Direction)rand.Next(0, 4);
                }
            }
            if (currentDirection == Direction.right)
            {
                if ((myMaze[currentPosition.X, currentPosition.Y]).rightWall == false)
                {
                    currentPosition.X++;
                }
                else
                {
                    currentDirection = (Maze.Direction)rand.Next(0, 4);
                }
            }
        }
    }
}
