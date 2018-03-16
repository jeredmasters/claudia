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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        State _currentState;
        bool _playerTurn;
        Piece _playerPeice = Piece.Cross;
        Random _rnd;
        Engine _engine;
        public MainWindow()
        {
            InitializeComponent();
            _rnd = new Random();
            _engine = new Engine();

            NewGame();
        }

        private void Learn()
        {
            Engine oposition = new Engine();
            bool primaryTurn = _rnd.Next(0, 2) == 0;
            for (int i = 0; i < 1000; i++)
            {
                while (_currentState.HasWinner() == Piece.Empty) {
                    if (primaryTurn)
                    {
                        int aiDecision = _engine.GetDecision(_currentState, _rnd.Next(0, 10));
                        if (aiDecision != -1)
                        {
                            _currentState.Place(aiDecision, Piece.Naught);
                        }
                    }
                    else
                    {
                        int aiDecision = oposition.GetDecision(_currentState, _rnd.Next(0, 10));
                        if (aiDecision != -1)
                        {
                            _currentState.Place(aiDecision, Piece.Cross);
                        }
                    }
                }

                primaryTurn = !primaryTurn;

                NewGame();
            }
        }

        private void NewGame()
        {
            _currentState = new State();
            _playerTurn = _rnd.Next(0, 2) == 0;

            Internal();
        }

        private void BoardClicked(int position)
        {
            if (_playerTurn && _currentState.CanGo(position))
            {
                _currentState.Place(position, _playerPeice);
                _playerTurn = false;

            }

            Internal();
        }

        private void Internal()
        {
            if (!_playerTurn)
            {
                int aiDecision = _engine.GetDecision(_currentState, _rnd.Next(0, 10));
                if (aiDecision != -1)
                {
                    _currentState.Place(aiDecision, Piece.Naught);
                    _playerTurn = true;
                }
                else
                {
                    MessageBox.Show("Some error occurred");
                    NewGame();
                }
            }

            Draw();
            var winner = _currentState.HasWinner();
            if (winner != Piece.Empty && MessageBox.Show("Winner: " + winner + "s", "Game Over", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                NewGame();
            }
        }

        private void Draw()
        {
            b0.Content = Symbol(0);
            b1.Content = Symbol(1);
            b2.Content = Symbol(2);
            b3.Content = Symbol(3);
            b4.Content = Symbol(4);
            b5.Content = Symbol(5);
            b6.Content = Symbol(6);
            b7.Content = Symbol(7);
            b8.Content = Symbol(8);
        }

        private char Symbol(int position)
        {
            switch (_currentState.Get(position))
            {
                case Piece.Empty:
                    return ' ';
                case Piece.Naught:
                    return 'O';
                case Piece.Cross:
                    return 'X';
            }
            return '?';
        }

        private void b0_Click(object sender, RoutedEventArgs e)
        {
            BoardClicked(0);
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            BoardClicked(1);
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            BoardClicked(2);
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            BoardClicked(3);
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            BoardClicked(4);
        }

        private void b5_Click(object sender, RoutedEventArgs e)
        {
            BoardClicked(5);
        }

        private void b6_Click(object sender, RoutedEventArgs e)
        {
            BoardClicked(6);
        }

        private void b7_Click(object sender, RoutedEventArgs e)
        {
            BoardClicked(7);
        }

        private void b8_Click(object sender, RoutedEventArgs e)
        {
            BoardClicked(8);
        }
    }
}
