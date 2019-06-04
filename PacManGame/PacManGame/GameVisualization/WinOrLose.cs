using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace PacManGame.GameVisualization
{
    class WinOrLose
    {
        public void Message(DispatcherTimer pacmanTimer, DispatcherTimer ghostTimer,string message)
        {
            pacmanTimer.Stop();
            ghostTimer.Stop();
            (System.Windows.Application.Current.MainWindow).KeyDown += this.DisableKeyDown;//заблокувати введення з клавіатури
            MessageBox.Show(message);
            (System.Windows.Application.Current.MainWindow).Close();//закрити вікно
        }

        private void DisableKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }
    }
}
