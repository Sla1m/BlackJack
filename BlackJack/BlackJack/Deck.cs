using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Deck : List<Card>
    {
        public int cardCount;

        public Deck()
        {
            this.DeckCreate();
        } // Конструктор

        public void DeckCreate()
        {
            string suit = "";
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        suit = "hearts";
                        break;
                    case 1:
                        suit = "clubs";
                        break;
                    case 2:
                        suit = "diamonds";
                        break;
                    case 3:
                        suit = "spades";
                        break;
                }
                for (int face = 2; face < 15; face++)
                {
                    this.Add(new Card(face, suit));
                }
            }
        } // Создание колоды

        public void Delete()
        {
            while (this.Count > 0)
            {
                Card removedCard = this[0];
                this.RemoveAt(0);
            }
        } // Удаление карт из колоды

        public string Display()
        {
            string str = "";
            string href = "";

            foreach (Card card in this)
            {
                str += card.Face + " of " + card.Suit;
                href = "D:/C#/OneMoreBlackJack/Cards/" + card;
            }
            return str;
        } // Вывод информации о колоде

        public void Shuffle()
        {
            Random random = new Random();

            cardCount = this.Count;

            for (int currentCard = 0; currentCard < cardCount; currentCard++)
            {
                Card tempCard = this[currentCard];
                int randomCard = random.Next(0, cardCount);
                this[currentCard] = this[randomCard];
                this[randomCard] = tempCard;
            }
        } // Перетасовка колоды

        public Card Deal()
        {
            Card returnCard = this[0];

            this.RemoveAt(0);

            return returnCard;
        } // Выдача карты
    }
}
