using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Threading;

namespace PacManGame.Logic
{
    public class GameScore : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;//подія зміни рахунку
        public delegate void WinGameEventHandler(DispatcherTimer pacmanTimer, DispatcherTimer ghostTimer, string message);
        public event WinGameEventHandler WinGame;//подія виграшу гри
        private int score = 0;
        int mazeSize;
        DispatcherTimer ghostTimer;
        DispatcherTimer pacmanTimer;

        public GameScore(int mazeSize, DispatcherTimer ghostTimer ,DispatcherTimer pacmanTimer )
        {
            this.mazeSize = mazeSize;
            this.ghostTimer = ghostTimer;
            this.pacmanTimer = pacmanTimer;
        }

        public void AddScore()
        {
            Score += 100;
            if (score == (mazeSize * mazeSize)*100 -100)
            {
                WinGame(pacmanTimer, ghostTimer, "YOU WON!");//якщо зібрано всі монети-завершити гру
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                OnPropertyChanged("Score");//повідомити про зміни поля
            }
        }

        protected void OnPropertyChanged(string score)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(score));
            }
        }
    }
}
