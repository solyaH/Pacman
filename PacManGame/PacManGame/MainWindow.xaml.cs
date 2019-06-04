using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PacManGame.GameVisualization;

namespace PacManGame
{
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
            int mazeSize = 10;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            GameInitialization game = new GameInitialization(mazeSize);
        }
    }
}
