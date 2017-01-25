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

namespace KTL_game.Pages
{
    /// <summary>
    /// Interaction logic for SetupGame.xaml
    /// </summary>
    public partial class SetupGame : Page
    {
        private MainWindow Window { get; set; }
        private StartPage StartPage { get; set; }
        private int GameLength { get; set; }
        private int SeriesLength { get; set; }
        private int ColorsCount { get; set; }
        private int ColorListCount { get; set; }
        private int Level { get; set; }
        public SetupGame(MainWindow window, StartPage startPage)
        {
            this.Window = window;
            this.StartPage = startPage;
            InitializeComponent();

            levelsCbox.SelectedIndex = 0;
        }

        private void PrevPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Window.Content = StartPage;
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateAndAssingInputValues())
            {
                GameplayPage gameplayPage = new GameplayPage(Window, StartPage, GameLength, SeriesLength, ColorsCount, ColorListCount, Level);
                Window.Content = gameplayPage;
            }
        }

        //TODO: Proper validation
        private bool ValidateAndAssingInputValues()
        {
            bool isCorrect = true;
            int checkedValue = 0;
            if (int.TryParse(GameLengthTextBox.Text, out checkedValue) == false )
            {
                MessageBox.Show("Error ! Game Lenght must be a number.");
                return false;
            }
            else if (checkedValue > 15 || checkedValue < 3)
            {
                MessageBox.Show("Error ! Game Lebgth must take values from 3 to 15.");
                return false;
            }
            else
                GameLength = checkedValue;

            if (int.TryParse(SeriesLengthTextBox.Text, out checkedValue) == false)
            {
                MessageBox.Show("Error ! Series Lenght must be a number.");
                return false;
            }
            else if (checkedValue > 5 || checkedValue < 1)
            {
                MessageBox.Show("Error ! Series Length must take values from 1 to 5.");
                return false;
            }
            else
                SeriesLength = checkedValue;

            if (int.TryParse(ColorCountTextBox.Text, out checkedValue) == false)
            {
                MessageBox.Show("Error ! Colors number must be a number.");
                return false;
            }
            else if (checkedValue > 10 || checkedValue < 2)
            {
                MessageBox.Show("Error ! Color number must take values from 2 to 10.");
                return false;
            }
            else
                ColorsCount = checkedValue;

            if (int.TryParse(ListColorCountTextBox.Text, out checkedValue) == false)
            {
                MessageBox.Show("Error ! Random colors number must be a number.");
                return false;
            }
            else if (checkedValue > ColorsCount || checkedValue < 1)
            {
                MessageBox.Show("Error ! Random colors number must take values from 2 to " + ColorsCount+ ".");
                return false;
            }
            else
                ColorListCount = checkedValue;

            Level = levelsCbox.SelectedIndex;

            return isCorrect;
            // GameLength = int.Parse(GameLengthTextBox.Text);
            // SeriesLength = int.Parse(SeriesLengthTextBox.Text);
            // ColorsCount = int.Parse(ColorCountTextBox.Text);
            // ColorListCount = int.Parse(ListColorCountTextBox.Text);
            // Level = levelsCbox.SelectedIndex;
        }

    }
}
