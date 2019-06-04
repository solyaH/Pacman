using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using PacManGame.Maze;
using System.Windows;

namespace PacManGame.GameVisualization
{
    public class DrawMaze
    {
        int thickness;//товщина стінок
        int imageSize;//розмір картинок, які відповідатимуть за героїв
        int mazeSize;//розмір лабіринту
        Maze.RecursiveMazeGenerator m;

        public DrawMaze(Maze.RecursiveMazeGenerator m,int thickness, int imageSize, int mazeSize)
        {
            this.m = m;
            this.thickness = thickness;
            this.imageSize = imageSize;
            this.mazeSize = mazeSize;

            ((MainWindow)System.Windows.Application.Current.MainWindow).Background = System.Windows.Media.Brushes.Black;
            double mazeCelllSize = (imageSize + 2 * thickness) * mazeSize;
            double marg = (((MainWindow)System.Windows.Application.Current.MainWindow).Width - (mazeCelllSize)) / 2;
            ((MainWindow)System.Windows.Application.Current.MainWindow).canvas.Margin = new Thickness(marg, 2 * marg, marg, marg);// відступ ігрового поля
            Create(((MainWindow)System.Windows.Application.Current.MainWindow).canvas, m);
        }

        public void Create(Canvas canvas, Maze.RecursiveMazeGenerator m)//візуалізувати лабіринт
        {
            int size = imageSize + 2 * thickness;//розмір клітинки
            Line wall;

            for (int x = 0; x < m.MyMaze.GetLength(0); x++)
            {
                for (int y = 0; y < m.MyMaze.GetLength(1); y++)
                {
                    if (m.MyMaze[x, y].upperWall)//верхня стіна
                    {
                        wall = new Line();
                        wall.Stroke = System.Windows.Media.Brushes.Blue;
                        wall.StrokeThickness = thickness;

                        wall.X1 = x * size;
                        wall.Y1 = y * size;

                        wall.X2 = x * size + size;
                        wall.Y2 = y * size;
                        canvas.Children.Add(wall);
                    }

                    if (m.MyMaze[x, y].lowerWall)//нижня стіна
                    {
                        wall = new Line();
                        wall.Stroke = System.Windows.Media.Brushes.Blue;
                        wall.StrokeThickness = thickness;

                        wall.X1 = x * size;
                        wall.Y1 = y * size + size;

                        wall.X2 = x * size + size;
                        wall.Y2 = y * size + size;
                        canvas.Children.Add(wall);
                    }

                    if (m.MyMaze[x, y].leftWall)//ліва стіна
                    {
                        wall = new Line();
                        wall.Stroke = System.Windows.Media.Brushes.Blue;
                        wall.StrokeThickness = thickness;

                        wall.X1 = x * size;
                        wall.Y1 = y * size;

                        wall.X2 = x * size;
                        wall.Y2 = y * size + size;
                        canvas.Children.Add(wall);
                    }

                    if (m.MyMaze[x, y].rightWall)//права стіна
                    {
                        wall = new Line();
                        wall.Stroke = System.Windows.Media.Brushes.Blue;
                        wall.StrokeThickness = thickness;

                        wall.X1 = x * size + size;
                        wall.Y1 = y * size;

                        wall.X2 = x * size + size;
                        wall.Y2 = y * size + size;
                        canvas.Children.Add(wall);
                    }
                }
            }
        }
    }
}
