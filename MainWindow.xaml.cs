using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

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
        Piece _aiPeice = Piece.Naught;
        Random _rnd;
        Engine _engine;
        DispatcherTimer _timer;
        int _trainingGames = 0;
        Thread _thread;
        bool _learning;
        public MainWindow()
        {
            InitializeComponent();
            _rnd = new Random();
            _engine = new Engine(_rnd);
            _timer = new DispatcherTimer(DispatcherPriority.Normal);
            _learning = true;
            _timer.Tick += _timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            _timer.Start();
            _thread = new Thread(Learn);
            NewGame();
            Draw();
            _thread.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            Draw();
            if (!_learning)
            {
                _timer.Stop();
                NewGame();
                AiMove();
                CheckGame();
            }
        }
        private void Learn()
        {
            _learning = true;

            int total = (int)Math.Pow(3, 9);

            for (int i = 0; i < total; i++) {
                Piece winner;
                do
                {
                    LearnLoop();
                    winner = _currentState.HasWinner();
                } while (winner == Piece.Empty);

                if (winner == Piece.Draw)
                {
                    _engine.RewardDecisions(0);
                }
                if (winner == _aiPeice)
                {
                    _engine.RewardDecisions(1);
                }
                if (winner == _playerPeice)
                {
                    _engine.RewardDecisions(-1);
                }

                _trainingGames++;
                NewGame(Util.GenerateState(i));                
            }
            _learning = false;
        }
        private void LearnLoop()
        {
            int aiDecision;
            if (!_playerTurn)
            {
                aiDecision = _engine.GetDecision(_currentState);
                if (aiDecision != -1)
                {
                    _currentState.Place(aiDecision, _aiPeice);
                }
            }
            else
            {
                aiDecision = _engine.GetDecision(_currentState.Invert(), false);
                if (aiDecision != -1)
                {
                    _currentState.Place(aiDecision, _playerPeice);
                }
            }

            if (!_currentState.Valid())
            {
                throw new Exception("Invalid Game State");
            }
            _playerTurn = !_playerTurn;
        }

        private void NewGame()
        {
            _currentState = new State();
            _playerTurn = _rnd.Next(0, 2) == 0;
        }

        private void NewGame(State state)
        {
            _currentState = state;
            _playerTurn = _rnd.Next(0, 2) == 0;
        }

        private void BoardClicked(int position)
        {
            if (_playerTurn && _currentState.CanGo(position))
            {
                _currentState.Place(position, _playerPeice);
                _playerTurn = false;

            }
            CheckGame();
            AiMove();
            CheckGame();
        }

        private void AiMove()
        {
            if (!_playerTurn)
            {
                int aiDecision = _engine.GetDecision(_currentState);
                if (aiDecision != -1)
                {
                    _currentState.Place(aiDecision, _aiPeice);
                    _playerTurn = true;
                }
                else
                {
                    MessageBox.Show("Some error occurred");
                    NewGame();
                }
            }
        }
        private void CheckGame() { 

            Draw();
            var winner = _currentState.HasWinner();
            if (winner != Piece.Empty && MessageBox.Show("Winner: " + winner + "s", "Game Over", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                NewGame();
                AiMove();
                Draw();                
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
