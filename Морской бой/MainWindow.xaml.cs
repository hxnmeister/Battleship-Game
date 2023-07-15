using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Морской_бой
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Battleship battleshipGame;

        public MainWindow()
        {
            InitializeComponent();

            battleshipGame = new Battleship();

            FirstPlayerDisabler.Visibility = Visibility.Visible;
            SecondPlayerDisabler.Visibility = Visibility.Visible;
        }

        private void CoordinatesTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CoordinatesTextBox.Text = string.Empty;
        }
        private void ConfirmCoordinatesButton_Click(object sender, RoutedEventArgs e)
        {
            //Первичная проверка координат
            if (!string.IsNullOrEmpty(CoordinatesTextBox.Text) && (CoordinatesTextBox.Text.Length > 1 && CoordinatesTextBox.Text.Length < 15))
            {
                CoordinatesTextBox.Text = CoordinatesTextBox.Text.ToUpper();

                foreach (var temp in CoordinatesTextBox.Text.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!(temp[0] > 64 && temp[0] < 91))
                    {
                        MessageBox.Show(String.WrongCoordinatesMessage);
                        return;
                    }
                }

                switch (ChooseShipTypeComboBox.SelectedIndex)
                {
                    case 1:
                        battleshipGame.ShipPlacement(ChooseShipTypeComboBox.SelectedIndex, CoordinatesTextBox.Text, FirstPlayerFieldGrid, SecondPlayerFieldGrid);
                        break;
                    case 2:
                        battleshipGame.ShipPlacement(ChooseShipTypeComboBox.SelectedIndex, CoordinatesTextBox.Text, FirstPlayerFieldGrid, SecondPlayerFieldGrid);
                        break;
                    case 3:
                        battleshipGame.ShipPlacement(ChooseShipTypeComboBox.SelectedIndex, CoordinatesTextBox.Text, FirstPlayerFieldGrid, SecondPlayerFieldGrid);
                        break;
                    case 4:
                        battleshipGame.ShipPlacement(ChooseShipTypeComboBox.SelectedIndex, CoordinatesTextBox.Text, FirstPlayerFieldGrid, SecondPlayerFieldGrid);
                        break;
                }
            }
            else MessageBox.Show(String.WrongCoordinatesMessage);

            //Передача хода при расстановке
            if (battleshipGame.FirstPlayerTurn)
            {
                if (battleshipGame.CheckShipsAvaible())
                {
                    Player1ReadyButton.IsEnabled = true;
                    CoordinatesTextBox.IsEnabled = false;
                    ChooseShipTypeComboBox.IsEnabled = false;
                    ConfirmCoordinatesButton.IsEnabled = false;

                    MessageBox.Show(String.TurnEndedMessage);
                }
            }
            else
            {
                if (battleshipGame.CheckShipsAvaible())
                {
                    Player2ReadyButton.IsEnabled = true;
                    CoordinatesTextBox.Visibility = Visibility.Collapsed;
                    ChooseShipTypeComboBox.Visibility = Visibility.Collapsed;
                    ConfirmCoordinatesButton.Visibility = Visibility.Collapsed;

                    MessageBox.Show(String.TurnEndedMessage);
                }
            }
            //---------------------------------
        }
        private void ChooseShipTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ChooseShipTypeComboBox.SelectedIndex)
            {
                case 1:
                    CoordinatesTextBox.Text = String.InfoForCoordinatesTextBox_1;
                    break;
                case 2:
                    CoordinatesTextBox.Text = String.InfoForCoordinatesTextBox_2;
                    break;
                case 3:
                    CoordinatesTextBox.Text = String.InfoForCoordinatesTextBox_3;
                    break;
                case 4:
                    CoordinatesTextBox.Text = String.InfoForCoordinatesTextBox_4;
                    break;
            }

            if (CoordinatesTextBox != null) CoordinatesTextBox.IsEnabled = true;
            if (ConfirmCoordinatesButton != null) ConfirmCoordinatesButton.IsEnabled = true;
        }
        private void SelectLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SelectLanguageComboBox.SelectedIndex)
            {
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
                    break;
                case 2:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    break;
                case 3:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
                    break;
                case 4:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");
                    break;
            }

            this.Title = String.WindowTitle;
            Player1ReadyButton.Content = String.ReadyButton;
            Player2ReadyButton.Content = String.ReadyButton;
            ConfirmCoordinatesButton.Content = String.SubmitCoordinatesButton;

            ChooseShipTypeComboBox.Items.Clear();
            ChooseShipTypeComboBox.Items.Add(String.HeaderForShiptypeComboBox);
            ChooseShipTypeComboBox.Items.Add(ChooseShipTypeComboBox.Items.Count + String.ShipTypeComboBox);
            ChooseShipTypeComboBox.Items.Add(ChooseShipTypeComboBox.Items.Count + String.ShipTypeComboBox);
            ChooseShipTypeComboBox.Items.Add(ChooseShipTypeComboBox.Items.Count + String.ShipTypeComboBox);
            ChooseShipTypeComboBox.Items.Add(ChooseShipTypeComboBox.Items.Count + String.ShipTypeComboBox);
            ChooseShipTypeComboBox.SelectedIndex = 0;

            CoordinatesTextBox.Text = string.Empty;
        }


        private void Player1ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            battleshipGame.FirstPlayerTurn = false;
            CoordinatesTextBox.IsEnabled = true;
            CoordinatesTextBox.Text = string.Empty;
            ChooseShipTypeComboBox.IsEnabled = true;
            ConfirmCoordinatesButton.IsEnabled = true;
            FirstPlayerDisabler.Visibility = Visibility.Hidden;
            Player1ReadyButton.Visibility = Visibility.Collapsed;
            FirstPlayerFieldCoverRectangle.Visibility = Visibility.Visible;
            SecondPlayerFieldCoverRectangle.Visibility = Visibility.Hidden;
        }
        private void Player2ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            battleshipGame.FirstPlayerTurn = true;
            CoordinatesTextBox.Text = string.Empty;
            Player2ReadyButton.Visibility = Visibility.Collapsed;
            SecondPlayerFieldCoverRectangle.Visibility = Visibility.Visible;
            Thread.Sleep(3000);
            FirstPlayerFieldCoverRectangle.Visibility = Visibility.Hidden;
            SecondPlayerDisabler.Visibility = Visibility.Hidden;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    UIElement element = FirstPlayerFieldGrid.FindName($"{(char)('A' + i)}{(char)('0' + j)}_1") as UIElement;
                    if (element.Visibility == Visibility.Hidden) element.Visibility = Visibility.Visible;

                    element.IsEnabled = true;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    UIElement element = FirstPlayerFieldGrid.FindName($"{(char)('A' + i)}{(char)('0' + j)}_2") as UIElement;
                    if (element.Visibility == Visibility.Hidden) element.Visibility = Visibility.Visible;

                    element.IsEnabled = true;
                }
            }
        }


        //Ниже обработчики CheckBox, они служат полем для того чтобы можно было "стрелять" по кораблям
        private void A0_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A0",FirstPlayerFieldGrid,SecondPlayerFieldGrid,FirstPlayerFieldCoverRectangle,SecondPlayerFieldCoverRectangle);
        }
        private void A1_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A2_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A3_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A4_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A5_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A6_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A7_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A8_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A9_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void B0_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B1_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B2_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B3_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B4_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B5_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B6_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B7_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B8_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B9_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void C0_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C1_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C2_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C3_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C4_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C5_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C6_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C7_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C8_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C9_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void D0_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D1_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D2_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D3_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D4_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D5_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D6_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D7_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D8_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D9_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void E0_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E1_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E2_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E3_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E4_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E5_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E6_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E7_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E8_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E9_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void F0_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F1_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F2_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F3_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F4_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F5_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F6_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F7_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F8_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F9_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void G0_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G1_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G2_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G3_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G4_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G5_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G6_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G7_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G8_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G9_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void H0_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H1_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H2_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H3_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H4_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H5_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H6_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H7_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H8_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H9_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void I0_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I1_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I2_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I3_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I4_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I5_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I6_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I7_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I8_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I9_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void J0_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J1_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J2_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J3_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J4_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J5_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J6_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J7_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J8_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J9_1_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }



        private void A0_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A1_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A2_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A3_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A4_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A5_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A6_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A7_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A8_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void A9_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("A9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void B0_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B1_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B2_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B3_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B4_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B5_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B6_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B7_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B8_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void B9_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("B9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void C0_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C1_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C2_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C3_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C4_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C5_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C6_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C7_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C8_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void C9_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("C9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void D0_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D1_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D2_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D3_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D4_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D5_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D6_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D7_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D8_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void D9_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("D9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void E0_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E1_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E2_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E3_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E4_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E5_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E6_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E7_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E8_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void E9_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("E9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void F0_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F1_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F2_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F3_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F4_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F5_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F6_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F7_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F8_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void F9_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("F9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void G0_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G1_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G2_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G3_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G4_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G5_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G6_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G7_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G8_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void G9_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("G9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void H0_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H1_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H2_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H3_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H4_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H5_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H6_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H7_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H8_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void H9_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("H9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void I0_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I1_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I2_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I3_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I4_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I5_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I6_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I7_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I8_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void I9_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("I9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }

        private void J0_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J0", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J1_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J1", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J2_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J2", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J3_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J3", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J4_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J4", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J5_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J5", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J6_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J6", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J7_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J7", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J8_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J8", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
        private void J9_2_Checked(object sender, RoutedEventArgs e)
        {
            battleshipGame.CheckWinner("J9", FirstPlayerFieldGrid, SecondPlayerFieldGrid, FirstPlayerFieldCoverRectangle, SecondPlayerFieldCoverRectangle);
        }
    }
}
