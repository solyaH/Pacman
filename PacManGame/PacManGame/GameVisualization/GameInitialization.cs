using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacManGame.Maze;
using System.Windows.Threading;
using PacManGame.Logic;
using System.Windows;

namespace PacManGame.GameVisualization
{
    public class GameInitialization
    {
        int thickness=7;
        int imageSize=30;
        int mazeSize;
        Random rand = new Random();
        DispatcherTimer ghostTimer = new DispatcherTimer();//таймер руху привидів
        DispatcherTimer pacmanTimer = new DispatcherTimer();//таймер руху пакмена

        void SizeAccuracy()
        {
            if (mazeSize <5)
            {
                throw new ArgumentException();
            }
        }

        public GameInitialization(int mazeSize)
        {
            try
            {
                this.mazeSize = mazeSize;
                SizeAccuracy();
                Maze.RecursiveMazeGenerator m = new Maze.RecursiveMazeGenerator(mazeSize, mazeSize);
                DrawMaze dm = new DrawMaze(m, thickness, imageSize, mazeSize);
                GameScore gs = new GameScore(mazeSize, ghostTimer, pacmanTimer);
                WinOrLose wol = new WinOrLose();
                gs.WinGame += wol.Message;
                PacmanMovement pm = new PacmanMovement(m.MyMaze, thickness, imageSize, gs, pacmanTimer);
                ScoreVisualization sv = new ScoreVisualization(pm.gs);
                if (mazeSize >= 5)
                {
                    GhostMovement yellow = new GhostMovement(m.MyMaze, pm.pacman, thickness, imageSize, @".\Images\yellow.png", rand, ghostTimer, pm.moveTimer);
                    yellow.LoseGame += wol.Message;
                }
                if (mazeSize >= 6)
                {
                    GhostMovement red = new GhostMovement(m.MyMaze, pm.pacman, thickness, imageSize, @".\Images\red.png", rand, ghostTimer, pm.moveTimer);
                    red.LoseGame += wol.Message;
                }
                if (mazeSize >= 8)
                {
                    GhostMovement pink = new GhostMovement(m.MyMaze, pm.pacman, thickness, imageSize, @".\Images\pink.png", rand, ghostTimer, pm.moveTimer);
                    pink.LoseGame += wol.Message;
                }
                if (mazeSize == 10)
                {
                    GhostMovement blue = new GhostMovement(m.MyMaze, pm.pacman, thickness, imageSize, @".\Images\blue.png", rand, ghostTimer, pm.moveTimer);
                    blue.LoseGame += wol.Message;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("{0} Exception caught.", e.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                ((MainWindow)System.Windows.Application.Current.MainWindow).Close();
            }           
        }
    }
}
