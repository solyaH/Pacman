using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacManGame.Logic;
using PacManGame.Maze;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace PacManGame.GameVisualization
{
    public class Coins
    {
        public Image[,] coinsMap;
        Pacman pacman;
        public Coins(Cell[,] myMaze, Pacman pacman, int thickness, int imageSize)
        {
            this.pacman = pacman;
            CoordinatesConverter conver = new CoordinatesConverter(thickness, imageSize);
            coinsMap = new Image[myMaze.GetLength(0), myMaze.GetLength(1)];
            BitmapImage coinBitmap = new BitmapImage(new Uri(@".\Images\coin.png", UriKind.Relative));

            for (int i = 0; i < myMaze.GetLength(0); i++)//розмістити в кожній клітинці монетку
            {
                for (int j = 0; j < myMaze.GetLength(1); j++)
                {
                    Image coinImage = new Image();
                    coinImage.Source = coinBitmap;
                    coinsMap[i, j] = coinImage;
                    MyPoint coinPosition = conver.ToCanvasCoordinates(new MyPoint(i, j));
                    Canvas.SetLeft(coinImage, coinPosition.X);
                    Canvas.SetTop(coinImage, coinPosition.Y);
                    ((MainWindow)System.Windows.Application.Current.MainWindow).canvas.Children.Add(coinImage);
                }
            }
        }

        public void SetCellVisited(MyPoint position)//приховати 'з'їджену' монету та позначити клітинку, як відвідана.
        {
            coinsMap[position.X, position.Y].Visibility = System.Windows.Visibility.Hidden;
            pacman.myMaze[position.X, position.Y].visited = true;
        }
    }
}
