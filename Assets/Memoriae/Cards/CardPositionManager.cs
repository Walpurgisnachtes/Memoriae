using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public enum CardPositionType
    {
        Hand,
        Deck,
        CommandArea,
        Graveyard,
        Exile,
        Battlefield
    }

    public class CardPositionManager
    {
        public List<AbstractCard> HandCards { get; private set; } = new();
        public List<AbstractCard> DeckCards { get; private set; } = new();
        public List<AbstractCard> CommandAreaCards { get; private set; } = new();
        public List<AbstractCard> GraveyardCards { get; private set; } = new();
        public List<AbstractCard> ExileCards { get; private set; } = new();
        public Dictionary<Vector2, AbstractCard> BattlefieldCards { get; private set; } = new();

        public List<AbstractCard> GetCardsInPosition(CardPositionType position)
        {
            if (position == CardPositionType.Battlefield)
            {
                return new List<AbstractCard>(BattlefieldCards.Values);
            }
            return position switch
            {
                CardPositionType.Hand => HandCards,
                CardPositionType.Deck => DeckCards,
                CardPositionType.CommandArea => CommandAreaCards,
                CardPositionType.Graveyard => GraveyardCards,
                CardPositionType.Exile => ExileCards,
                _ => null
            };
        }
    }
}