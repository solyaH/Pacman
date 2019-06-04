using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacManGame.Maze
{
    public class RecursiveMazeGenerator
    {
        private Random rand;

        private Cell[,] myMaze;
        private MyPoint start;
        private bool beginning;

        public MyPoint Start
        {
            get
            {
                return start;
            }
        }

        public Cell[,] MyMaze
        {
            get
            {
                return myMaze;
            }
        }

        private void CheckBorder(MyPoint currentPosition, ref Cell currentCell)
        {
            if (currentPosition.X == 0)
            {
                currentCell.leftDirChecked = true;
            }
            else if (currentPosition.X == myMaze.GetLength(0) - 1)
            {
                currentCell.rightDirChecked = true;
            }

            if (currentPosition.Y == 0)
            {
                currentCell.upperDirChecked = true;
            }
            else if (currentPosition.Y == myMaze.GetLength(1) - 1)
            {
                currentCell.lowerDirChecked = true;
            }
        }

        public RecursiveMazeGenerator(int width, int height)
        {
            rand = new Random();
            beginning = true;

            myMaze = new Cell[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    myMaze[i, j] = new Cell();
                }
            }

            SolveMaze(new MyPoint(rand.Next(width), rand.Next(height)), (Direction)rand.Next(4));
            AddPass();
        }


        private void SolveMaze(MyPoint currentPosition, Direction direction)
        {
            Cell currentCell = myMaze[currentPosition.X, currentPosition.Y]; //для оптимізації коду, будемо працювати з конкретною клітинкою, а не областю лабіринта

            if (beginning) // виконується лише при першому виклику метода 
            {
                start = currentPosition; // встановити початкову точку

                currentCell.start = true;
                currentCell.visited = true;

                myMaze[start.X, start.Y] = currentCell;
                beginning = false;
            }
            else
            {
                if (direction == Direction.left)
                {
                    currentCell.rightWall = false;
                    myMaze[currentPosition.X + 1, currentPosition.Y].leftWall = false;
                }
                else if (direction == Direction.right)
                {
                    currentCell.leftWall = false;
                    myMaze[currentPosition.X - 1, currentPosition.Y].rightWall = false;
                }
                else if (direction == Direction.up)
                {
                    currentCell.lowerWall = false;
                    myMaze[currentPosition.X, currentPosition.Y + 1].upperWall = false;
                }
                else if (direction == Direction.down)
                {
                    currentCell.upperWall = false;
                    myMaze[currentPosition.X, currentPosition.Y - 1].lowerWall = false;
                }

                myMaze[currentPosition.X, currentPosition.Y] = currentCell;
                currentCell.visited = true;
            }

            CheckBorder(currentPosition, ref currentCell);

            myMaze[currentPosition.X, currentPosition.Y] = currentCell;

            while (true) //цикл який буде відбуватися, поки ми не закінчимо формування лабіринта
            {
                if (currentPosition.X != 0) //перевірити, чи сусідні клітинки були відвідані
                {
                    if (myMaze[currentPosition.X - 1, currentPosition.Y].visited)
                    {
                        currentCell.leftDirChecked = true;
                        myMaze[currentPosition.X - 1, currentPosition.Y].rightDirChecked = true;
                    }
                }

                if (currentPosition.X != myMaze.GetLength(0) - 1)
                {
                    if (myMaze[currentPosition.X + 1, currentPosition.Y].visited)
                    {
                        currentCell.rightDirChecked = true;
                        myMaze[currentPosition.X + 1, currentPosition.Y].leftDirChecked = true;
                    }
                }

                if (currentPosition.Y != 0)
                {
                    if (myMaze[currentPosition.X, currentPosition.Y - 1].visited)
                    {
                        currentCell.upperDirChecked = true;
                        myMaze[currentPosition.X, currentPosition.Y - 1].lowerDirChecked = true;
                    }
                }

                if (currentPosition.Y != myMaze.GetLength(1) - 1)
                {
                    if (myMaze[currentPosition.X, currentPosition.Y + 1].visited)
                    {
                        currentCell.lowerDirChecked = true;
                        myMaze[currentPosition.X, currentPosition.Y + 1].upperDirChecked = true;
                    }
                }

                myMaze[currentPosition.X, currentPosition.Y] = currentCell;

                if (currentCell.leftDirChecked && currentCell.rightDirChecked && currentCell.upperDirChecked && currentCell.lowerDirChecked)
                {// Якщо всі комірки навколо були оброблені, то клітина, де ми зараз, є кінцевою
                    currentCell.end = true;
                    myMaze[currentPosition.X, currentPosition.Y] = currentCell;
                    return;
                }
                else // якщо ні, то згенерувати новий напрямок і перевірити, чи була клітинка відвідана чи ні
                {
                    Direction dir = (Direction)rand.Next(0, 4);

                    if (dir == Direction.left)
                    {
                        if (currentCell.leftDirChecked == false)
                        {
                            currentCell.leftDirChecked = true;
                            myMaze[currentPosition.X, currentPosition.Y] = currentCell;
                            SolveMaze(new MyPoint(currentPosition.X - 1, currentPosition.Y), dir);
                        }
                    }
                    else if (dir == Direction.right)
                    {
                        if (currentCell.rightDirChecked == false)
                        {
                            currentCell.rightDirChecked = true;
                            myMaze[currentPosition.X, currentPosition.Y] = currentCell;
                            SolveMaze(new MyPoint(currentPosition.X + 1, currentPosition.Y), dir);
                        }
                    }
                    else if (dir == Direction.up)
                    {
                        if (currentCell.upperDirChecked == false)
                        {
                            currentCell.upperDirChecked = true;
                            myMaze[currentPosition.X, currentPosition.Y] = currentCell;
                            SolveMaze(new MyPoint(currentPosition.X, currentPosition.Y - 1), dir);
                        }
                    }
                    else if (dir == Direction.down)
                    {
                        if (currentCell.lowerDirChecked == false)
                        {
                            currentCell.lowerDirChecked = true;
                            myMaze[currentPosition.X, currentPosition.Y] = currentCell;
                            SolveMaze(new MyPoint(currentPosition.X, currentPosition.Y + 1), dir);
                        }
                    }
                }
            }
        }

        private List<Direction> CellWalls(MyPoint position)
        {
            List<Direction> dirList = new List<Direction>();
            if (myMaze[position.X, position.Y].upperWall == true)
            {
                dirList.Add(Direction.up);
            }
            if (myMaze[position.X, position.Y].lowerWall == true)
            {
                dirList.Add(Direction.down);
            }
            if (myMaze[position.X, position.Y].leftWall == true)
            {
                dirList.Add(Direction.left);
            }
            if (myMaze[position.X, position.Y].rightWall == true)
            {
                dirList.Add(Direction.right);
            }
            return dirList;
        }

        private void RemoveWall(MyPoint position, Direction dir)
        {
            if (dir == Direction.up)
            {
                myMaze[position.X, position.Y].upperWall = false;
                myMaze[position.X, position.Y - 1].lowerWall = false;
            }
            else if (dir == Direction.down)
            {
                myMaze[position.X, position.Y].lowerWall = false;
                myMaze[position.X, position.Y + 1].upperWall = false;
            }
            else if (dir == Direction.left)
            {
                myMaze[position.X, position.Y].leftWall = false;
                myMaze[position.X - 1, position.Y].rightWall = false;
            }
            else if (dir == Direction.right)
            {
                myMaze[position.X, position.Y].rightWall = false;
                myMaze[position.X + 1, position.Y].leftWall = false;
            }
        }

        private void RemoveDeadEnd(MyPoint position, List<Direction> dirList)
        {
            if (position.X != 0 && position.X != myMaze.GetLength(0) - 1 && position.Y != 0 && position.Y != myMaze.GetLength(1) - 1)//видалити стіни всередині лабіринту
            {
                int randomWall = rand.Next(0, 3);
                RemoveWall(position, dirList[randomWall]);
            }
            else if ((position.X == 0 || position.X == myMaze.GetLength(0) - 1) && (position.Y != 0 && position.Y != myMaze.GetLength(1) - 1))//видалити нижню або верхню стіну (для лівого і правого кордонів)
            {
                Direction dirToRem = (position.X == 0) ? Direction.left : Direction.right;
                dirList.Remove(dirToRem);
                int randomWall = rand.Next(0, 2);
                RemoveWall(position, dirList[randomWall]);
            }
            else if ((position.Y == 0 || position.Y == myMaze.GetLength(1) - 1) && (position.X != 0 && position.X != myMaze.GetLength(0) - 1))//видалити праву або ліву стіну (для верхнього і нижнього кордонів)
            {
                Direction dirToRem = (position.Y == 0) ? Direction.up : Direction.down;
                dirList.Remove(dirToRem);
                int randomWall = rand.Next(0, 2);
                RemoveWall(position, dirList[randomWall]);
            }
            else if (position.X == 0 && position.Y == 0)//верхній лівий кут лабіринта
            {
                Direction dir;
                if (dirList.Contains(Direction.down))
                    dir = Direction.down;
                else
                    dir = Direction.right;

                RemoveWall(position, dir);
            }
            else if (position.X == myMaze.GetLength(0) - 1 && position.Y == 0)//верхній правий кут
            {
                Direction dir;
                if (dirList.Contains(Direction.down))
                    dir = Direction.down;
                else
                    dir = Direction.left;

                RemoveWall(position, dir);
            }
            else if (position.X == 0 && position.Y == myMaze.GetLength(1) - 1)//нижній лівий кут
            {
                Direction dir;
                if (dirList.Contains(Direction.up))
                    dir = Direction.up;
                else
                    dir = Direction.right;

                RemoveWall(position, dir);
            }
            else if (position.X == myMaze.GetLength(0) - 1 && position.Y == myMaze.GetLength(1) - 1)//нижній правий кут
            {
                Direction dir;
                if (dirList.Contains(Direction.up))
                    dir = Direction.up;
                else
                    dir = Direction.left;

                RemoveWall(position, dir);
            }
        }

        private void AddPass()//видалити одну з 3 стін для глухих кутів
        {
            for (int i = 0; i < myMaze.GetLength(0); i++)
            {
                for (int j = 0; j < myMaze.GetLength(1); j++)
                {
                    MyPoint position = new MyPoint(i, j);
                    List<Direction> dirList = CellWalls(position);
                    if (dirList.Count == 3)
                    {
                        RemoveDeadEnd(position, dirList);
                    }
                }
            }
        }
    }
}
