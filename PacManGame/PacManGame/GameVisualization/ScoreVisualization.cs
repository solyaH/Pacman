using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PacManGame.Logic;
using PacManGame.Maze;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Data;

namespace PacManGame.GameVisualization
{
    public class ScoreVisualization
    {
        Label scoreLabel;

        public ScoreVisualization(GameScore gs)
        {
            scoreLabel = new Label();
            scoreLabel.FontFamily = new FontFamily("Algerian");
            scoreLabel.FontWeight = FontWeights.Bold;
            scoreLabel.FontStyle = FontStyles.Normal;
            scoreLabel.FontSize = 40;
            scoreLabel.Foreground = Brushes.Firebrick;
            scoreLabel.Margin = new Thickness(350,20,50,500);
            Binding myBinding = new Binding();
            myBinding.Mode = BindingMode.TwoWay;
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            myBinding.Source = gs;
            myBinding.Path = new PropertyPath("Score");
            scoreLabel.SetBinding(Label.ContentProperty, myBinding);

            Label label = new Label();
            label.Content = "Score: ";
            label.FontFamily = new FontFamily("Algerian");
            label.FontWeight = FontWeights.Bold;
            label.FontStyle = FontStyles.Normal;
            label.FontSize = 40;
            label.Foreground= Brushes.Firebrick;
            label.Height = 60;
            label.Height = 100;
            label.Margin = new Thickness(120, 20, 180, 500);
            ((MainWindow)System.Windows.Application.Current.MainWindow).grid.Children.Add(label);
            ((MainWindow)System.Windows.Application.Current.MainWindow).grid.Children.Add(scoreLabel);
        }
    }
}
