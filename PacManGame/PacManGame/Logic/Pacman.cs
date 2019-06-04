using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PacManGame.Maze;
using PacManGame.GameVisualization;
using System.Windows.Input;
using System.Threading;

namespace PacManGame.Logic
{
    public class Pacman
    {
        public MyPoint currentPosition;//поточне розташування
        public bool cantPerformNextStep=false;//неможливість зробити наступний крок
        public Direction currentDirection;//поточний напрямок
        private Random rand;
        public Cell[,] myMaze;

        public Pacman(Cell[,] myMaze)
        {
            this.myMaze = myMaze;
            rand = new Random();
            currentPosition = new MyPoint(rand.Next(0, myMaze.GetLength(0)), rand.Next(0, myMaze.GetLength(1)));//випадковим чином обираємо початкову позицію
            for (int i = 0; i < myMaze.GetLength(0); i++)
            {
                for (int j = 0; j < myMaze.GetLength(1); j++)
                {
                    myMaze[i, j].visited = false;
                }
            }
            myMaze[currentPosition.X, currentPosition.Y].visited = true;
        }

        public bool CheckBorder()//перевірка на зіткнення із стіною
        {
            if (currentDirection == Direction.up && myMaze[currentPosition.X, currentPosition.Y].upperWall == true)
            {
                return false;
            }
            if (currentDirection == Direction.down && myMaze[currentPosition.X, currentPosition.Y].lowerWall == true)
            {
                return false;
            }
            if (currentDirection == Direction.left && myMaze[currentPosition.X, currentPosition.Y].leftWall == true)
            {
                return false;
            }
            if (currentDirection == Direction.right && myMaze[currentPosition.X, currentPosition.Y].rightWall == true)
            {
                return false;
            }
            return true;
        }

        public void NextStep()//наступний крок
        {
            cantPerformNextStep = false;
            if (!CheckBorder())
            {
                cantPerformNextStep = true;
                return;
            }

            if (currentDirection == Direction.up)
            {
                currentPosition.Y--;
            }
            else if (currentDirection == Direction.down)
            {
                currentPosition.Y++;
            }
            else if (currentDirection == Direction.left)
            {
                currentPosition.X--;
            }
            else if (currentDirection == Direction.right)
            {
                currentPosition.X++;
            }        
        }
    }
}
