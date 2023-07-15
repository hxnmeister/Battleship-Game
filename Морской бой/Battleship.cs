using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Морской_бой
{
    internal class Battleship
    {
        bool firstPlayerTurn; //Определяет чей ход
        //----------------------------------------
        int firstPlayerHits; //Поле для отслеживания попаданий по полю противника, для победы необходимо 20
        int secondPlayerHits;
        //-------------------------------------------------------------------------------------------------
        //Поля для игры, 0 - Пустота, 1 - Корабль, 2 - Занятое пространство вокруг корабля
        int[,] firstPlayerField =
        {
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 }
        };
        int[,] secondPlayerField =
        {
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 }
        };
        //--------------------------------------------------------------------------------
        int[] firstPlayerShipsAvaible = { 0, 4, 3, 2, 1 }; //Доступные корабли, пропущена первая ячейка для удобства обращения по типу корабля
        int[] secondPlayerShipsAvaible = { 0, 4, 3, 2, 1 };
        //------------------------------------------------------------------------------------------------------------------------------------

        public Battleship()
        {
            firstPlayerTurn = true;
            firstPlayerHits = 0;
            secondPlayerHits = 0;
        }

        public bool FirstPlayerTurn
        {
            get { return firstPlayerTurn; }
            set { firstPlayerTurn = value; }
        }
        public int FirstPlayerHits
        {
            get { return firstPlayerHits; }
            set { firstPlayerHits = value; }
        }
        public int SecondPlayerHits
        {
            get { return secondPlayerHits; }
            set { secondPlayerHits = value; }
        }

        //Метод для проверки правильности написания координат
        bool CheckCoordinates(int shipType, string CoordinatesTextBoxInfo)
        {
            //Проверка длинны координат, относительно того какой тип корабля выбран
            switch (shipType)
            {
                case 2:
                    if (CoordinatesTextBoxInfo.Length != 5 && CoordinatesTextBoxInfo.Length != 6) return false;
                    break;
                case 3:
                    if (!(CoordinatesTextBoxInfo.Length > 7 && CoordinatesTextBoxInfo.Length < 11)) return false;
                    break;
                case 4:
                    if (!(CoordinatesTextBoxInfo.Length > 10 && CoordinatesTextBoxInfo.Length < 15)) return false;
                    break;
            }
            //---------------------------------------------------------------------


            //Поля ниже нужны для удобства использования конвертированных координат
            int firstDigit;
            int secondDigit;
            int firstLetter;
            int secondLetter;
            //--------------------------------------------------------------------------------

            //Поля ниже нужны для хранения и проверки координат на корректность их ввода
            string[] currentCoordinates = CoordinatesTextBoxInfo.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] temp = currentCoordinates;
            //--------------------------------------------------------------------------------------------------------------------------

            for (int i = 0; i < currentCoordinates.Length; i++)
            {
                //Проверка первой координаты относительно второй во избежания дублирования координат
                for (int j = i + 1; j < currentCoordinates.Length; j++)
                {
                    if (temp[i] == currentCoordinates[j]) return false;
                }
                //-----------------------------------------------------------------------------------

                //Проверка каждой координаты чтобы не было равно 2-м
                if (currentCoordinates[i].Length != 2) return false;
                //--------------------------------------------------

                //Проверка каждого символа относительно второго, для того чтобы нельзя было перескочить через один, например: I1,I2,I4
                if (i + 1 == currentCoordinates.Length)
                {
                    if (currentCoordinates[i][0] != currentCoordinates[0][0] && currentCoordinates[i][1] != currentCoordinates[0][1])
                    {
                        return false;
                    }
                }
                else
                {
                    firstLetter = (int)currentCoordinates[i][0];
                    secondLetter = (int)currentCoordinates[i + 1][0];
                    firstDigit = (int)currentCoordinates[i][1];
                    secondDigit = (int)currentCoordinates[i + 1][1];

                    if (currentCoordinates[i][0] != currentCoordinates[i + 1][0] && currentCoordinates[i][1] != currentCoordinates[i + 1][1])
                    {
                        return false;
                    }
                    else if (!((firstLetter - secondLetter == 1 || firstLetter - secondLetter == -1) || (firstDigit - secondDigit == 1 || firstDigit - secondDigit == -1)))
                    {
                        return false;
                    }
                    //--------------------------------------------------------------------------------------------------------------------
                }

                //Проверка координат на поле, есть ли по этой координате корабль
                firstDigit = (int)currentCoordinates[i][1] - '0';
                firstLetter = (int)currentCoordinates[i][0] - 'A';

                if (i + 1 == currentCoordinates.Length)
                {
                    if ((firstPlayerTurn ? firstPlayerField : secondPlayerField)[firstDigit, firstLetter] != 0) return false;

                    break;
                }

                secondDigit = (int)currentCoordinates[i + 1][1] - '0';
                secondLetter = (int)currentCoordinates[i + 1][0] - 'A';

                if ((firstPlayerTurn ? firstPlayerField : secondPlayerField)[firstDigit, firstLetter] != 0 || (firstPlayerTurn ? firstPlayerField : secondPlayerField)[secondDigit, secondLetter] != 0)
                {
                    return false;
                }
                //--------------------------------------------------------------
            }

            return true;
        }

        //Метод проходится по полям ...PlayerHits, и отслеживает ходы игроков
        public void CheckWinner(string coordinates, Grid FirstPlayerField, Grid SecondPlayerField, Rectangle FirstPlayerFieldCover, Rectangle SecondPlayerFieldCover)
        {
            int letter = coordinates[0] - 'A';
            int digit = coordinates[1] - '0';

            //Выделяется память на текущий CheckBox по координатам
            UIElement el = (firstPlayerTurn ? FirstPlayerField : SecondPlayerField).FindName($"{coordinates}_{(firstPlayerTurn ? 1 : 2)}") as UIElement;
            //------------------------------------------------------------------------------------------------------------------------------------------

            if ((firstPlayerTurn ? secondPlayerField : firstPlayerField)[digit, letter] == 1)
            {
                if (firstPlayerTurn) ++firstPlayerHits;
                else ++secondPlayerHits;

                el.IsEnabled = false;

                //Ниже проверки перекрывают поля по соседству после попадания по кораблю
                if (digit != 9 && letter != 9)
                {
                    if ((firstPlayerTurn ? secondPlayerField : firstPlayerField)[digit + 1, letter + 1] == 2)
                    {
                        ((firstPlayerTurn ? SecondPlayerField : FirstPlayerField).FindName($"{(char)(coordinates[0] + 1)}{(char)(coordinates[1] + 1)}_{(firstPlayerTurn ? 1 : 2)}") as UIElement).Visibility = Visibility.Collapsed;
                    }
                }
                if (digit != 0 && letter != 0)
                {
                    if ((firstPlayerTurn ? secondPlayerField : firstPlayerField)[digit - 1, letter - 1] == 2)
                    {
                        ((firstPlayerTurn ? SecondPlayerField : FirstPlayerField).FindName($"{(char)(coordinates[0] - 1)}{(char)(coordinates[1] - 1)}_{(firstPlayerTurn ? 1 : 2)}") as UIElement).Visibility = Visibility.Collapsed;
                    }
                }
                if (digit != 0 && letter != 9)
                {
                    if ((firstPlayerTurn ? secondPlayerField : firstPlayerField)[digit - 1, letter + 1] == 2)
                    {
                        ((firstPlayerTurn ? SecondPlayerField : FirstPlayerField).FindName($"{(char)(coordinates[0] + 1)}{(char)(coordinates[1] - 1)}_{(firstPlayerTurn ? 1 : 2)}") as UIElement).Visibility = Visibility.Collapsed;
                    }
                }
                if (digit != 9 && letter != 0)
                {
                    if ((firstPlayerTurn ? secondPlayerField : firstPlayerField)[digit + 1, letter - 1] == 2)
                    {
                        ((firstPlayerTurn ? SecondPlayerField : FirstPlayerField).FindName($"{(char)(coordinates[0] - 1)}{(char)(coordinates[1] + 1)}_{(firstPlayerTurn ? 1 : 2)}") as UIElement).Visibility = Visibility.Collapsed;
                    }
                }

                if (digit != 9)
                {
                    if ((firstPlayerTurn ? secondPlayerField : firstPlayerField)[digit + 1, letter] == 2)
                    {
                        ((firstPlayerTurn ? SecondPlayerField : FirstPlayerField).FindName($"{coordinates[0]}{(char)(coordinates[1] + 1)}_{(firstPlayerTurn ? 1 : 2)}") as UIElement).Visibility = Visibility.Collapsed;
                    }
                }
                if (digit != 0)
                {
                    if ((firstPlayerTurn ? secondPlayerField : firstPlayerField)[digit - 1, letter] == 2)
                    {
                        ((firstPlayerTurn ? SecondPlayerField : FirstPlayerField).FindName($"{coordinates[0]}{(char)(coordinates[1] - 1)}_{(firstPlayerTurn ? 1 : 2)}") as UIElement).Visibility = Visibility.Collapsed;
                    }
                }
                if (letter != 0)
                {
                    if ((firstPlayerTurn ? secondPlayerField : firstPlayerField)[digit, letter - 1] == 2)
                    {
                        ((firstPlayerTurn ? SecondPlayerField : FirstPlayerField).FindName($"{(char)(coordinates[0] - 1)}{coordinates[1]}_{(firstPlayerTurn ? 1 : 2)}") as UIElement).Visibility = Visibility.Collapsed;
                    }
                }
                if (letter != 9)
                {
                    if ((firstPlayerTurn ? secondPlayerField : firstPlayerField)[digit, letter + 1] == 2)
                    {
                        ((firstPlayerTurn ? SecondPlayerField : FirstPlayerField).FindName($"{(char)(coordinates[0] + 1)}{coordinates[1]}_{(firstPlayerTurn ? 1 : 2)}") as UIElement).Visibility = Visibility.Collapsed;
                    }
                }
                //----------------------------------------------------------------------

                MessageBox.Show(String.HitMessage);
            }
            else
            {
                if (el.Visibility == Visibility.Visible)
                {
                    el.Visibility = Visibility.Collapsed;

                    //Передача хода с задержкой в 2 с.
                    (firstPlayerTurn ? FirstPlayerFieldCover : SecondPlayerFieldCover).Visibility = Visibility.Visible;
                    Thread.Sleep(2000);
                    (firstPlayerTurn ? SecondPlayerFieldCover : FirstPlayerFieldCover).Visibility = Visibility.Hidden;
                    //-------------------------------------------------------------------------------------------------

                    //Передача хода
                    if (firstPlayerTurn) firstPlayerTurn = false;
                    else firstPlayerTurn = true;
                    //-------------------------------------------

                    MessageBox.Show(String.MissMessage);

                    return;
                }
            }

            //Проверка на победу
            if ((firstPlayerTurn ? FirstPlayerHits : SecondPlayerHits) == 20)
            {
                MessageBox.Show((firstPlayerTurn ? $"{String.WinMessage} 1!" : $"{String.WinMessage} 2!"));
                FirstPlayerFieldCover.Visibility = Visibility.Collapsed;
                SecondPlayerFieldCover.Visibility = Visibility.Collapsed;

                System.Windows.Application.Current.Shutdown();
            }
            //---------------------------------------------------------------
        }
        //Метод для расстановки кораблей по полям
        public void ShipPlacement(int shipType, string CoordinatesTextBoxInfo, Grid FirstPlayerField, Grid SecondPlayerField)
        {
            if (CheckCoordinates(shipType, CoordinatesTextBoxInfo) && (firstPlayerTurn ? firstPlayerShipsAvaible : secondPlayerShipsAvaible)[shipType] != 0)
            {
                int firstDigit;
                int firstLetter;
                int secondDigit;
                int secondLetter;
                string[] currentCoordinates = CoordinatesTextBoxInfo.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                //Вложенный метод для проверки места где будет располагатся корабль, и чтобы клетки рядом зарезервировать
                void spaceAroundCheck(ref int leterForCheck, ref int digitForCheck)
                {
                    if (digitForCheck != 9 && leterForCheck != 9) (firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck + 1, leterForCheck + 1] = 2;
                    if (digitForCheck != 0 && leterForCheck != 0) (firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck - 1, leterForCheck - 1] = 2;
                    if (digitForCheck != 0 && leterForCheck != 9) (firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck - 1, leterForCheck + 1] = 2;
                    if (digitForCheck != 9 && leterForCheck != 0) (firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck + 1, leterForCheck - 1] = 2;

                    if (digitForCheck != 9) if ((firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck + 1, leterForCheck] != 1) (firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck + 1, leterForCheck] = 2;
                    if (digitForCheck != 0) if ((firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck - 1, leterForCheck] != 1) (firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck - 1, leterForCheck] = 2;
                    if (leterForCheck != 0) if ((firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck, leterForCheck - 1] != 1) (firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck, leterForCheck - 1] = 2;
                    if (leterForCheck != 9) if ((firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck, leterForCheck + 1] != 1) (firstPlayerTurn ? firstPlayerField : secondPlayerField)[digitForCheck, leterForCheck + 1] = 2;
                }
                //-------------------------------------------------------------------------------------------------------

                //Та самая расстановка кораблей на полях
                for (int i = 0; i < currentCoordinates.Length; i++)
                {
                    firstDigit = (int)currentCoordinates[i][1] - '0';
                    firstLetter = (int)currentCoordinates[i][0] - 'A';

                    if (i + 1 == currentCoordinates.Length)
                    {
                        (firstPlayerTurn ? firstPlayerField : secondPlayerField)[firstDigit, firstLetter] = 1;

                        spaceAroundCheck(ref firstLetter, ref firstDigit);
                    }
                    else
                    {
                        secondDigit = (int)currentCoordinates[i + 1][1] - '0';
                        secondLetter = (int)currentCoordinates[i + 1][0] - 'A';

                        (firstPlayerTurn ? firstPlayerField : secondPlayerField)[firstDigit, firstLetter] = 1;
                        (firstPlayerTurn ? firstPlayerField : secondPlayerField)[secondDigit, secondLetter] = 1;

                        spaceAroundCheck(ref firstLetter, ref firstDigit);
                        spaceAroundCheck(ref secondLetter, ref secondDigit);
                    }

                    ((firstPlayerTurn ? FirstPlayerField : SecondPlayerField).FindName($"{currentCoordinates[i]}_{(firstPlayerTurn ? 1 : 2)}") as UIElement).Visibility = Visibility.Hidden;
                }
                //-------------------------------------------------

                /*for (int i = 0; i < currentCoordinates.Length; i++)
                {
                    firstDigit = (int)currentCoordinates[i][1] - '0';
                    firstLetter = (int)currentCoordinates[i][0] - 'A';

                    if (i + 1 == currentCoordinates.Length)
                    {


                        break;
                    }

                    secondDigit = (int)currentCoordinates[i + 1][1] - '0';
                    secondLetter = (int)currentCoordinates[i + 1][0] - 'A';


                }*/

                --(firstPlayerTurn ? firstPlayerShipsAvaible : secondPlayerShipsAvaible)[shipType];

                MessageBox.Show(String.ShipSuccessfullyPlacedMessage + " " + CoordinatesTextBoxInfo);
            }
            else if ((firstPlayerTurn ? firstPlayerShipsAvaible : secondPlayerShipsAvaible)[shipType] == 0)
            {
                MessageBox.Show(shipType + String.ShipOutOfStock);
            }
            else
            {
                MessageBox.Show(String.WrongCoordinatesMessage);
            }
        }
        //Проверка наличия кораблей
        public bool CheckShipsAvaible()
        {
            for (int i = 1; i < 5; i++)
            {
                if ((firstPlayerTurn ? firstPlayerShipsAvaible : secondPlayerShipsAvaible)[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
