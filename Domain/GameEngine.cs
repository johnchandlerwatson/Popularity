using System.Collections.Generic;
using System.Linq;
using Popularity.Domain.Cards;
using Popularity.Models;

namespace Popularity.Domain
{
    public class GameEngine
    {
        public MoveModel ExecuteMove(User user1, User user2)
        {
            var allCards = user1.Played.Union(user2.Played).OrderByDescending(x => x.Speed).ToList();
            var actions = new List<CardAction>();
            foreach (var card in allCards)
            {
                var friendlyCards = allCards.Where(x => x.User == card.User).ToList();
                var enemyCards = allCards.Except(friendlyCards).ToList();
                card.ApplyMove(enemyCards, friendlyCards, actions);
            }
            return new MoveModel
            {
                User1 = user1,
                User2 = user2,
                Actions = actions
            };
        }

        private List<Card> PlayedCards(List<Card> allCards, User user)
        {
            return allCards.Where(x => x.User == user && !x.IsDead).ToList();
        }
    }
}