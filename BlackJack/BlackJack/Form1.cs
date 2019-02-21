using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace BlackJack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Не готово: анимации, рестарт игры

        public int countP = 0; public int countD = 0;
        public int bet = 0; public int balance = Convert.ToInt32(File.ReadAllText("save.txt"));
        public int WorL; //0-draw 1-win 2-lose
        Deck line = new Deck();
        Random rand = new Random();
        List<int> playingDeck = new List<int>();
        Card cardDealt;
        int countCards = 0; int botCount = 0;
        public int countGames = 0; public int countWins = 0;

        public int countPHiLo = 0; public int countDHiLo = 0;
        Deck lineHiLo = new Deck();
        List<int> playingDeckHiLo = new List<int>();
        Card cardDealtHiLo;
        int betHiLo = 0; bool youWin = false; int perCent = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            Table tb = new Table();

            label6.Text = balance.ToString();
            label12.Text = balance.ToString();
        } // Загрузка формы

        public void StartGame()
        {
            pictureBox11.Enabled = true;

            bet = Convert.ToInt32(textBox3.Text);

            if (bet == 0)
            {
                MessageBox.Show("Сделайте ставку !");
            }
            else
            {
                cardDealt = line.Deal();
                countCards = 0;
                line.Shuffle();
                pictureBox11.Enabled = true;
                button4.Enabled = true;
            }
        } // Начало игры. Тасовка колоды

        public void ReStart()
        {
            Bet();
            line.Delete();
            line.DeckCreate();
            StartGame();
            ClearCard();
        } // Рестарт

        public int CardForPlayer()
        {
            switch (cardDealt.Face)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    countP = cardDealt.Face; break;
                case 11:
                case 12:
                case 13:
                    countP = 10; break;
                case 14:
                    if (countP < 11)
                    {
                        countP = 11;
                    }
                    else
                    if (countP >= 11)
                    {
                        countP = 1;
                    }
                    break;
            }
            return countP;
        } // Карта для игрока

        public int CardForDealer()
        {
            switch (cardDealt.Face)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    countD = cardDealt.Face; break;
                case 11:
                case 12:
                case 13:
                    countD = 10; break;
                case 14:
                    if (countD < 11)
                    {
                        countD = 11;
                    }
                    else
                    if (countD >= 11)
                    {
                        countD = 1;
                    }
                    break;
            }
            return countD;
        } // Карта для диллера

        public void WhoWin()
        {
            if (countD == countP && countD <= 21 && countP <= 21)
            {
                WorL = 0;
                MessageBox.Show("Ничья !");
            }
            else if (countD > countP && countD <= 21 && countP <= 21)
            {
                WorL = 2;
                MessageBox.Show("Вы проиграли !");
            }
            else if (countD < countP && countD <= 21 && countP <= 21)
            {
                WorL = 1;
                MessageBox.Show("Вы победили !");
                countWins++;
            }
            else if (countP > 21)
            {
                WorL = 2;
                MessageBox.Show("Вы проиграли !");
            }
            else if (countD > 21 && countP < 22)
            {
                WorL = 1;
                MessageBox.Show("Вы победили !");
                countWins++;
            }
            else if (countD > 21 && countP > 21)
            {
                WorL = 2;
                MessageBox.Show("Вы проиграли !");
            }
        } // Определение победителя

        public void BalanceChange()
        {
            switch (WorL)
            {
                case 0:
                    balance += bet; break;
                case 1:
                    balance += 2 * bet; break;
                case 2:
                    balance = balance; break;
            }
        } // Изменения баланса при ставках

        public void MoveCards()
        {
            if (countCards == 1)
            {
                pictureBox1.Left = 662;
                while (pictureBox1.Left != 532)
                {
                    pictureBox1.Left -= 1;
                    Thread.Sleep(100);
                }
            }
        } // Анимация раздачи карт(не работает)

        public void ShowPlayerCards()
        {
            {
                if (cardDealt.Face == 2 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_2.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_2.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_2.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_2.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_2.png");
                }
                if (cardDealt.Face == 3 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_3.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_3.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_3.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_3.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_3.png");
                }
                if (cardDealt.Face == 4 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_4.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_4.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_4.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_4.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_4.png");
                }
                if (cardDealt.Face == 5 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_5.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_5.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_5.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_5.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_5.png");
                }
                if (cardDealt.Face == 6 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_6.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_6.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_6.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_6.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_6.png");
                }
                if (cardDealt.Face == 7 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_7.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_7.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_7.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_7.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_7.png");
                }
                if (cardDealt.Face == 8 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_8.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_8.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_8.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_8.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_8.png");
                }
                if (cardDealt.Face == 9 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_9.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_9.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_9.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_9.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_9.png");
                }
                if (cardDealt.Face == 10 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_10.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_10.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_10.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_10.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_10.png");
                }
                if (cardDealt.Face == 11 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_jack.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_jack.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_jack.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_jack.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_jack.png");
                }
                if (cardDealt.Face == 12 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_queen.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_queen.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_queen.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_queen.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_queen.png");
                }
                if (cardDealt.Face == 13 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_king.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_king.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_king.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_king.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_king.png");
                }
                if (cardDealt.Face == 14 && cardDealt.Suit == "hearts")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/heart_ace.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/heart_ace.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/heart_ace.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/heart_ace.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/heart_ace.png");
                }
            } //hearts
            {
                if (cardDealt.Face == 2 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_2.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_2.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_2.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_2.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_2.png");
                }
                if (cardDealt.Face == 3 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_3.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_3.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_3.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_3.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_3.png");
                }
                if (cardDealt.Face == 4 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_4.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_4.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_4.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_4.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_4.png");
                }
                if (cardDealt.Face == 5 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_5.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_5.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_5.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_5.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_5.png");
                }
                if (cardDealt.Face == 6 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_6.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_6.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_6.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_6.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_6.png");
                }
                if (cardDealt.Face == 7 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_7.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_7.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_7.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_7.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_7.png");
                }
                if (cardDealt.Face == 8 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_8.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_8.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_8.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_8.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_8.png");
                }
                if (cardDealt.Face == 9 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_9.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_9.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_9.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_9.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_9.png");
                }
                if (cardDealt.Face == 10 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_10.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_10.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_10.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_10.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_10.png");
                }
                if (cardDealt.Face == 11 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_jack.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_jack.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_jack.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_jack.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_jack.png");
                }
                if (cardDealt.Face == 12 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_queen.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_queen.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_queen.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_queen.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_queen.png");
                }
                if (cardDealt.Face == 13 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_king.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_king.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_king.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_king.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_king.png");
                }
                if (cardDealt.Face == 14 && cardDealt.Suit == "clubs")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/clubs_ace.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/clubs_ace.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/clubs_ace.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/clubs_ace.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/clubs_ace.png");
                }
            } //clubs
            {
                if (cardDealt.Face == 2 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_2.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_2.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_2.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_2.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_2.png");
                }
                if (cardDealt.Face == 3 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_3.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_3.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_3.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_3.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_3.png");
                }
                if (cardDealt.Face == 4 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_4.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_4.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_4.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_4.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_4.png");
                }
                if (cardDealt.Face == 5 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_5.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_5.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_5.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_5.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_5.png");
                }
                if (cardDealt.Face == 6 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_6.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_6.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_6.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_6.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_6.png");
                }
                if (cardDealt.Face == 7 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_7.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_7.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_7.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_7.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_7.png");
                }
                if (cardDealt.Face == 8 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_8.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_8.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_8.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_8.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_8.png");
                }
                if (cardDealt.Face == 9 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_9.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_9.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_9.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_9.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_9.png");
                }
                if (cardDealt.Face == 10 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_10.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_10.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_10.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_10.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_10.png");
                }
                if (cardDealt.Face == 11 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_jack.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_jack.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_jack.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_jack.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_jack.png");
                }
                if (cardDealt.Face == 12 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_queen.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_queen.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_queen.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_queen.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_queen.png");
                }
                if (cardDealt.Face == 13 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_king.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_king.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_king.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_king.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_king.png");
                }
                if (cardDealt.Face == 14 && cardDealt.Suit == "diamonds")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/diamonds_ace.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/diamonds_ace.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/diamonds_ace.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/diamonds_ace.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/diamonds_ace.png");
                }
            } //diamonds
            {
                if (cardDealt.Face == 2 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_2.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_2.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_2.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_2.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_2.png");
                }
                if (cardDealt.Face == 3 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_3.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_3.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_3.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_3.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_3.png");
                }
                if (cardDealt.Face == 4 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_4.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_4.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_4.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_4.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_4.png");
                }
                if (cardDealt.Face == 5 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_5.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_5.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_5.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_5.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_5.png");
                }
                if (cardDealt.Face == 6 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_6.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_6.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_6.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_6.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_6.png");
                }
                if (cardDealt.Face == 7 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_7.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_7.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_7.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_7.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_7.png");
                }
                if (cardDealt.Face == 8 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_8.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_8.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_8.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_8.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_8.png");
                }
                if (cardDealt.Face == 9 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_9.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_9.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_9.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_9.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_9.png");
                }
                if (cardDealt.Face == 10 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_10.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_10.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_10.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_10.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_10.png");
                }
                if (cardDealt.Face == 11 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_jack.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_jack.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_jack.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_jack.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_jack.png");
                }
                if (cardDealt.Face == 12 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_queen.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_queen.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_queen.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_queen.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_queen.png");
                }
                if (cardDealt.Face == 13 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_king.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_king.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_king.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_king.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_king.png");
                }
                if (cardDealt.Face == 14 && cardDealt.Suit == "spades")
                {
                    if (countCards == 1)
                        pictureBox1.Image = Image.FromFile("Cards/spades_ace.png");
                    if (countCards == 2)
                        pictureBox2.Image = Image.FromFile("Cards/spades_ace.png");
                    if (countCards == 3)
                        pictureBox3.Image = Image.FromFile("Cards/spades_ace.png");
                    if (countCards == 4)
                        pictureBox4.Image = Image.FromFile("Cards/spades_ace.png");
                    if (countCards == 5)
                        pictureBox5.Image = Image.FromFile("Cards/spades_ace.png");
                }
            } //spades
        } // Вывод карты в picturebox

        public void ShowBotCards()
        {
            {
                if (cardDealt.Face == 2 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_2.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_2.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_2.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_2.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_2.png");
                }
                if (cardDealt.Face == 3 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_3.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_3.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_3.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_3.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_3.png");
                }
                if (cardDealt.Face == 4 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_4.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_4.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_4.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_4.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_4.png");
                }
                if (cardDealt.Face == 5 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_5.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_5.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_5.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_5.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_5.png");
                }
                if (cardDealt.Face == 6 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_6.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_6.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_6.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_6.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_6.png");
                }
                if (cardDealt.Face == 7 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_7.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_7.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_7.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_7.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_7.png");
                }
                if (cardDealt.Face == 8 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_8.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_8.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_8.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_8.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_8.png");
                }
                if (cardDealt.Face == 9 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_9.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_9.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_9.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_9.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_9.png");
                }
                if (cardDealt.Face == 10 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_10.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_10.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_10.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_10.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_10.png");
                }
                if (cardDealt.Face == 11 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_jack.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_jack.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_jack.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_jack.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_jack.png");
                }
                if (cardDealt.Face == 12 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_queen.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_queen.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_queen.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_queen.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_queen.png");
                }
                if (cardDealt.Face == 13 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_king.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_king.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_king.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_king.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_king.png");
                }
                if (cardDealt.Face == 14 && cardDealt.Suit == "clubs")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/clubs_ace.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/clubs_ace.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/clubs_ace.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/clubs_ace.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/clubs_ace.png");
                }
            } //clubs
            {
                if (cardDealt.Face == 2 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_2.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_2.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_2.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_2.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_2.png");
                }
                if (cardDealt.Face == 3 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_3.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_3.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_3.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_3.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_3.png");
                }
                if (cardDealt.Face == 4 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_4.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_4.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_4.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_4.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_4.png");
                }
                if (cardDealt.Face == 5 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_5.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_5.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_5.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_5.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_5.png");
                }
                if (cardDealt.Face == 6 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_6.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_6.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_6.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_6.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_6.png");
                }
                if (cardDealt.Face == 7 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_7.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_7.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_7.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_7.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_7.png");
                }
                if (cardDealt.Face == 8 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_8.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_8.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_8.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_8.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_8.png");
                }
                if (cardDealt.Face == 9 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_9.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_9.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_9.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_9.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_9.png");
                }
                if (cardDealt.Face == 10 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_10.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_10.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_10.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_10.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_10.png");
                }
                if (cardDealt.Face == 11 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_jack.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_jack.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_jack.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_jack.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_jack.png");
                }
                if (cardDealt.Face == 12 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_queen.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_queen.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_queen.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_queen.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_queen.png");
                }
                if (cardDealt.Face == 13 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_king.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_king.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_king.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_king.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_king.png");
                }
                if (cardDealt.Face == 14 && cardDealt.Suit == "hearts")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/heart_ace.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/heart_ace.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/heart_ace.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/heart_ace.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/heart_ace.png");
                }
            } //hearts
            {
                if (cardDealt.Face == 2 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_2.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_2.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_2.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_2.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_2.png");
                }
                if (cardDealt.Face == 3 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_3.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_3.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_3.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_3.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_3.png");
                }
                if (cardDealt.Face == 4 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_4.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_4.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_4.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_4.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_4.png");
                }
                if (cardDealt.Face == 5 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_5.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_5.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_5.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_5.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_5.png");
                }
                if (cardDealt.Face == 6 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_6.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_6.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_6.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_6.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_6.png");
                }
                if (cardDealt.Face == 7 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_7.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_7.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_7.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_7.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_7.png");
                }
                if (cardDealt.Face == 8 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_8.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_8.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_8.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_8.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_8.png");
                }
                if (cardDealt.Face == 9 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_9.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_9.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_9.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_9.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_9.png");
                }
                if (cardDealt.Face == 10 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_10.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_10.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_10.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_10.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_10.png");
                }
                if (cardDealt.Face == 11 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_jack.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_jack.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_jack.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_jack.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_jack.png");
                }
                if (cardDealt.Face == 12 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_queen.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_queen.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_queen.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_queen.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_queen.png");
                }
                if (cardDealt.Face == 13 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_king.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_king.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_king.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_king.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_king.png");
                }
                if (cardDealt.Face == 14 && cardDealt.Suit == "diamonds")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/diamonds_ace.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/diamonds_ace.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/diamonds_ace.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/diamonds_ace.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/diamonds_ace.png");
                }
            } //diamonds
            {
                if (cardDealt.Face == 2 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_2.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_2.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_2.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_2.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_2.png");
                }
                if (cardDealt.Face == 3 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_3.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_3.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_3.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_3.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_3.png");
                }
                if (cardDealt.Face == 4 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_4.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_4.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_4.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_4.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_4.png");
                }
                if (cardDealt.Face == 5 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_5.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_5.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_5.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_5.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_5.png");
                }
                if (cardDealt.Face == 6 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_6.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_6.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_6.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_6.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_6.png");
                }
                if (cardDealt.Face == 7 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_7.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_7.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_7.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_7.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_7.png");
                }
                if (cardDealt.Face == 8 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_8.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_8.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_8.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_8.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_8.png");
                }
                if (cardDealt.Face == 9 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_9.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_9.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_9.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_9.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_9.png");
                }
                if (cardDealt.Face == 10 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_10.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_10.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_10.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_10.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_10.png");
                }
                if (cardDealt.Face == 11 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_jack.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_jack.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_jack.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_jack.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_jack.png");
                }
                if (cardDealt.Face == 12 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_queen.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_queen.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_queen.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_queen.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_queen.png");
                }
                if (cardDealt.Face == 13 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_king.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_king.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_king.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_king.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_king.png");
                }
                if (cardDealt.Face == 14 && cardDealt.Suit == "spades")
                {
                    if (botCount == 1)
                        pictureBox6.Image = Image.FromFile("Cards/spades_ace.png");
                    if (botCount == 2)
                        pictureBox7.Image = Image.FromFile("Cards/spades_ace.png");
                    if (botCount == 3)
                        pictureBox8.Image = Image.FromFile("Cards/spades_ace.png");
                    if (botCount == 4)
                        pictureBox9.Image = Image.FromFile("Cards/spades_ace.png");
                    if (botCount == 5)
                        pictureBox10.Image = Image.FromFile("Cards/spades_ace.png");
                }
            } //spades
        } // Вывод карт бота в picturebox

        public void PictureForBot()
        {
            if (botCount == 1)
                pictureBox6.Visible = true;
            if (botCount == 2)
            {
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
            }
            if (botCount == 3)
            {
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
            }
            if (botCount == 4)
            {
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
            }
            if (botCount == 5)
            {
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
            }
        } // Заполнение карт бота

        public void LogicForDealer()
        {
            botCount = 0;
            while (countD <= 17)
            {
                cardDealt = line.Deal();
                countD += CardForDealer();
                label4.Text = countD.ToString();
                botCount++;
                PictureForBot();
                ShowBotCards();
            }
            WhoWin(); BalanceChange();
            label6.Text = balance.ToString();
            File.WriteAllText("save.txt", balance.ToString());
        }// Псевдологика для диллера

        public void Bet()
        {
            bet = Convert.ToInt32(textBox3.Text);
            if (bet > balance || balance < 0)
            {
                MessageBox.Show("Недостаточно средств !");
            }
            else if (bet <= balance)
            {
                balance -= bet;
                label6.Text = balance.ToString();
                label12.Text = balance.ToString();
                File.WriteAllText("save.txt", balance.ToString());
            }
        } //Ставка

        public void VisibleStart()
        {
            if (textBox3.Text == "0")
            {
                button1.Enabled = false;
            }
            if (textBox3.Text != "0")
            {
                button1.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox11.Enabled = false;
            button4.Enabled = false;
            LogicForDealer();
            countGames++;
        } // Псевдологика для диллера

        private void button6_Click(object sender, EventArgs e)
        {
            textBox3.Text = (Convert.ToInt32(textBox3.Text) + 10).ToString();
        } // +10 к сумме ставки

        private void button7_Click(object sender, EventArgs e)
        {
            textBox3.Text = (Convert.ToInt32(textBox3.Text) + 50).ToString();
        } // +50 к сумме ставки

        private void button8_Click(object sender, EventArgs e)
        {
            textBox3.Text = (Convert.ToInt32(textBox3.Text) + 100).ToString();
        } // +100 к сумме ставки

        private void button9_Click(object sender, EventArgs e)
        {
            Bet();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "0";
            }
            VisibleStart();
        } // Поле для изменения ставки

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            try
            {
                countCards++;
                switch (countCards)
                {
                    case 1:
                        pictureBox1.Visible = true; break;
                    case 2:
                        pictureBox2.Visible = true; break;
                    case 3:
                        pictureBox3.Visible = true; break;
                    case 4:
                        pictureBox4.Visible = true; break;
                    case 5:
                        pictureBox5.Visible = true; break;
                }
                if (countCards <= 5)
                {
                    cardDealt = line.Deal();
                    countP += CardForPlayer();
                    label3.Text = countP.ToString();
                    ShowPlayerCards();
                }
                if (countP > 21)
                {
                    pictureBox11.Enabled = false;
                    button4.Enabled = false;
                    LogicForDealer();
                }
                else
                {
                    //LogicForDealer();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Каким-то образом закончились карты (");
            }
        } // Выводим карту визуально

        private void ClearCard()
        {
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
            }
            countP = 0; countD = 0;
            label3.Text = countP.ToString(); label4.Text = countD.ToString();
        } // Конец игры(очистка полей для карт)

        public void button3_Click(object sender, EventArgs e)
        {
            Table tb = new Table();
            tb.Show();
        } // Переход на форму с таблицей

        private void button1_Click(object sender, EventArgs e)
        {
            StartGame();
            ReStart();
            button1.Enabled = false;
            pictureBox11.Enabled = true;
        } // Кнопка старта


        /* ------------------------------ Hi&Lo ------------------------------ */

        public void VisibleStartHiLo()
        {
            if (textBox1.Text == "0")
            {
                button2.Enabled = false;
            }
            if (textBox1.Text != "0")
            {
                button2.Enabled = true;
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            try
            {
                BetHiLo();
                StartHiLoGame();

                cardDealtHiLo = line.Deal();

                CardForDealerHiLo();
                ShowDillerCardHiLo();

                label8.Text = countDHiLo.ToString();

                WhoWinHiLo();

                if (youWin == true)
                    MessageBox.Show("Вы победили !");
                if (youWin == false)
                    MessageBox.Show("Вы проиграли !");
            }
            catch (FormatException)
            {
                MessageBox.Show("Миша, вырубай, что-то пошло не так !!!");
            }
        } // Кнопка старта игры

        public void ShowPlayerCardHiLo()
        {
            {
                if (cardDealtHiLo.Face == 2 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_2.png");
                if (cardDealtHiLo.Face == 3 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_3.png");
                if (cardDealtHiLo.Face == 4 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_4.png");
                if (cardDealtHiLo.Face == 5 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_5.png");
                if (cardDealtHiLo.Face == 6 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_6.png");
                if (cardDealtHiLo.Face == 7 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_7.png");
                if (cardDealtHiLo.Face == 8 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_8.png");
                if (cardDealtHiLo.Face == 9 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_9.png");
                if (cardDealtHiLo.Face == 10 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_10.png");
                if (cardDealtHiLo.Face == 11 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_jack.png");
                if (cardDealtHiLo.Face == 12 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_queen.png");
                if (cardDealtHiLo.Face == 13 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_king.png");
                if (cardDealtHiLo.Face == 14 && cardDealtHiLo.Suit == "hearts")
                    pictureBox13.Image = Image.FromFile("Cards/heart_ace.png");
            } //heart
            {
                if (cardDealtHiLo.Face == 2 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_2.png");
                if (cardDealtHiLo.Face == 3 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_3.png");
                if (cardDealtHiLo.Face == 4 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_4.png");
                if (cardDealtHiLo.Face == 5 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_5.png");
                if (cardDealtHiLo.Face == 6 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_6.png");
                if (cardDealtHiLo.Face == 7 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_7.png");
                if (cardDealtHiLo.Face == 8 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_8.png");
                if (cardDealtHiLo.Face == 9 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_9.png");
                if (cardDealtHiLo.Face == 10 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_10.png");
                if (cardDealtHiLo.Face == 11 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_jack.png");
                if (cardDealtHiLo.Face == 12 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_queen.png");
                if (cardDealtHiLo.Face == 13 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_king.png");
                if (cardDealtHiLo.Face == 14 && cardDealtHiLo.Suit == "clubs")
                    pictureBox13.Image = Image.FromFile("Cards/clubs_ace.png");
            } //clubs
            {
                if (cardDealtHiLo.Face == 2 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_2.png");
                if (cardDealtHiLo.Face == 3 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_3.png");
                if (cardDealtHiLo.Face == 4 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_4.png");
                if (cardDealtHiLo.Face == 5 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_5.png");
                if (cardDealtHiLo.Face == 6 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_6.png");
                if (cardDealtHiLo.Face == 7 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_7.png");
                if (cardDealtHiLo.Face == 8 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_8.png");
                if (cardDealtHiLo.Face == 9 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_9.png");
                if (cardDealtHiLo.Face == 10 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_10.png");
                if (cardDealtHiLo.Face == 11 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_jack.png");
                if (cardDealtHiLo.Face == 12 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_queen.png");
                if (cardDealtHiLo.Face == 13 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_king.png");
                if (cardDealtHiLo.Face == 14 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox13.Image = Image.FromFile("Cards/diamonds_ace.png");
            } //diamonds
            {
                if (cardDealtHiLo.Face == 2 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_2.png");
                if (cardDealtHiLo.Face == 3 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_3.png");
                if (cardDealtHiLo.Face == 4 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_4.png");
                if (cardDealtHiLo.Face == 5 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_5.png");
                if (cardDealtHiLo.Face == 6 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_6.png");
                if (cardDealtHiLo.Face == 7 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_7.png");
                if (cardDealtHiLo.Face == 8 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_8.png");
                if (cardDealtHiLo.Face == 9 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_9.png");
                if (cardDealtHiLo.Face == 10 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_10.png");
                if (cardDealtHiLo.Face == 11 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_jack.png");
                if (cardDealtHiLo.Face == 12 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_queen.png");
                if (cardDealtHiLo.Face == 13 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_king.png");
                if (cardDealtHiLo.Face == 14 && cardDealtHiLo.Suit == "spades")
                    pictureBox13.Image = Image.FromFile("Cards/spades_ace.png");
            } //spades
        }// *HiLo* Визуальный вывод карт игрока

        public void ShowDillerCardHiLo()
        {
            {
                if (cardDealtHiLo.Face == 2 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_2.png");
                if (cardDealtHiLo.Face == 3 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_3.png");
                if (cardDealtHiLo.Face == 4 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_4.png");
                if (cardDealtHiLo.Face == 5 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_5.png");
                if (cardDealtHiLo.Face == 6 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_6.png");
                if (cardDealtHiLo.Face == 7 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_7.png");
                if (cardDealtHiLo.Face == 8 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_8.png");
                if (cardDealtHiLo.Face == 9 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_9.png");
                if (cardDealtHiLo.Face == 10 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_10.png");
                if (cardDealtHiLo.Face == 11 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_jack.png");
                if (cardDealtHiLo.Face == 12 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_queen.png");
                if (cardDealtHiLo.Face == 13 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_king.png");
                if (cardDealtHiLo.Face == 14 && cardDealtHiLo.Suit == "hearts")
                    pictureBox12.Image = Image.FromFile("Cards/heart_ace.png");
            } //heart
            {
                if (cardDealtHiLo.Face == 2 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_2.png");
                if (cardDealtHiLo.Face == 3 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_3.png");
                if (cardDealtHiLo.Face == 4 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_4.png");
                if (cardDealtHiLo.Face == 5 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_5.png");
                if (cardDealtHiLo.Face == 6 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_6.png");
                if (cardDealtHiLo.Face == 7 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_7.png");
                if (cardDealtHiLo.Face == 8 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_8.png");
                if (cardDealtHiLo.Face == 9 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_9.png");
                if (cardDealtHiLo.Face == 10 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_10.png");
                if (cardDealtHiLo.Face == 11 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_jack.png");
                if (cardDealtHiLo.Face == 12 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_queen.png");
                if (cardDealtHiLo.Face == 13 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_king.png");
                if (cardDealtHiLo.Face == 14 && cardDealtHiLo.Suit == "clubs")
                    pictureBox12.Image = Image.FromFile("Cards/clubs_ace.png");
            } //clubs
            {
                if (cardDealtHiLo.Face == 2 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_2.png");
                if (cardDealtHiLo.Face == 3 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_3.png");
                if (cardDealtHiLo.Face == 4 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_4.png");
                if (cardDealtHiLo.Face == 5 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_5.png");
                if (cardDealtHiLo.Face == 6 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_6.png");
                if (cardDealtHiLo.Face == 7 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_7.png");
                if (cardDealtHiLo.Face == 8 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_8.png");
                if (cardDealtHiLo.Face == 9 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_9.png");
                if (cardDealtHiLo.Face == 10 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_10.png");
                if (cardDealtHiLo.Face == 11 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_jack.png");
                if (cardDealtHiLo.Face == 12 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_queen.png");
                if (cardDealtHiLo.Face == 13 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_king.png");
                if (cardDealtHiLo.Face == 14 && cardDealtHiLo.Suit == "diamonds")
                    pictureBox12.Image = Image.FromFile("Cards/diamonds_ace.png");
            } //diamonds
            {
                if (cardDealtHiLo.Face == 2 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_2.png");
                if (cardDealtHiLo.Face == 3 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_3.png");
                if (cardDealtHiLo.Face == 4 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_4.png");
                if (cardDealtHiLo.Face == 5 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_5.png");
                if (cardDealtHiLo.Face == 6 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_6.png");
                if (cardDealtHiLo.Face == 7 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_7.png");
                if (cardDealtHiLo.Face == 8 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_8.png");
                if (cardDealtHiLo.Face == 9 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_9.png");
                if (cardDealtHiLo.Face == 10 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_10.png");
                if (cardDealtHiLo.Face == 11 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_jack.png");
                if (cardDealtHiLo.Face == 12 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_queen.png");
                if (cardDealtHiLo.Face == 13 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_king.png");
                if (cardDealtHiLo.Face == 14 && cardDealtHiLo.Suit == "spades")
                    pictureBox12.Image = Image.FromFile("Cards/spades_ace.png");
            } //spades
        }// *HiLo* Визуальный вывод карт диллера

        public void BetHiLo()
        {
            betHiLo = Convert.ToInt32(textBox1.Text);
            if (betHiLo > balance || betHiLo < 0)
            {
                MessageBox.Show("Недостаточно средств !");
            }
            else if (betHiLo <= balance)
            {
                balance -= betHiLo;
                label12.Text = balance.ToString();
                label6.Text = balance.ToString();
                File.WriteAllText("save.txt", balance.ToString());
            }
        } //*HiLo* Ставка

        public void StartHiLoGame()
        {
            betHiLo = Convert.ToInt32(textBox1.Text);

            if (betHiLo == 0)
            {
                MessageBox.Show("Сделайте ставку !");
            }
            else
            {
                line.Delete();
                line.DeckCreate();
                line.Shuffle();
                cardDealtHiLo = line.Deal();
                CardForPlayerHiLo();
                ShowPlayerCardHiLo();
                label7.Text = countPHiLo.ToString();
            }
        } // *HiLo* Начало игры. Тасовка колоды

        public void WhoWinHiLo()
        {
            if (radioButton1.Checked == true && countDHiLo > countPHiLo)
            {
                youWin = true;
                balance += 2 * betHiLo;
                label12.Text = balance.ToString();
            } // DealerWin
            if (radioButton1.Checked == true && countDHiLo <= countPHiLo)
            {
                youWin = false;
            } // DealerLoose
            if (radioButton2.Checked == true && countDHiLo < countPHiLo)
            {
                youWin = true;
                balance += 2 * betHiLo;
                label12.Text = balance.ToString();
            } // PlayerWin
            if (radioButton2.Checked == true && countPHiLo <= countDHiLo)
            {
                youWin = false;
            } // PlayerLoose
            if (radioButton3.Checked == true && countDHiLo == countPHiLo)
            {
                youWin = true;
                balance += 12 * betHiLo;
                label12.Text = balance.ToString();
            } // Draw
            if (radioButton3.Checked == true && countPHiLo != countDHiLo)
            {
                youWin = false;
            } // PlayerLoose

            //else
            //{
            //    youWin = false;
            //} // Loose
        } // *HiLo* Определение победителя

        public int CardForPlayerHiLo()
        {
            switch (cardDealtHiLo.Face)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    countPHiLo = cardDealtHiLo.Face; break;
            }
            return countPHiLo;
        } // *HiLo* Карта для игрока

        public int CardForDealerHiLo()
        {
            switch (cardDealtHiLo.Face)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    countDHiLo = cardDealtHiLo.Face; break;
            }
            return countDHiLo;
        } // *HiLo* Карта для диллера

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToInt32(textBox1.Text) + 10).ToString();
        } // +10 к сумме ставки

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToInt32(textBox1.Text) + 50).ToString();
        } // +50 к сумме ставки

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToInt32(textBox1.Text) + 100).ToString();
        } // +100 к сумме ставки

        private void button9_Click_1(object sender, EventArgs e)
        {
            textBox3.Text = "0";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            {
                textBox1.Text = "0";
            }
            VisibleStartHiLo();
        }
    }
}

