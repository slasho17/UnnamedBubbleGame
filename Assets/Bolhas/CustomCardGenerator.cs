using Bolhas.Enums;
using NueGames.NueDeck.Scripts.Data.Collection;
using NueGames.NueDeck.Scripts.Data.Settings;
using NueGames.NueDeck.Scripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bolhas
{
    public class CustomCardGenerator {

        private string CardSpritePath = Application.streamingAssetsPath + "\\Sprites\\Cards\\";
        private GameplayData gameplayData;
        private List<JsonCardDataItem> jsonCardsList;

        public CustomCardGenerator(GameplayData gameplayData) { 
            this.gameplayData = gameplayData;
            this.jsonCardsList = JsonHelper.LoadCardsJson();
        }
        private void SetAllCardsList()
        {
            gameplayData.AllCardsList.Clear();

            foreach (var card in jsonCardsList)
            {
                var newCardData = GenerateCard(card);

                gameplayData.AllCardsList.Add(newCardData);
            }
        }

        private void SetInitialDeck()
        {   
            
            gameplayData.InitalDeck.CardList.Clear();

            while(gameplayData.InitalDeck.CardList.Count < gameplayData.RandomCardCount)
            {
                int index = UnityEngine.Random.Range(0, gameplayData.AllCardsList.Count);
                gameplayData.InitalDeck.CardList.Add(gameplayData.AllCardsList[index]);
            }

        }

        private void SetCardAction(CardData cardData, JsonCardDataItem jsonCardDataItem)
        {
            if (cardData.cardActionDataList == null)
            {
                cardData.cardActionDataList = new List<CardActionData> { };
            } else
            {
                cardData.cardActionDataList.Clear();
            }

            if (jsonCardDataItem.type == ((int)CardType.Theme))
            {
                cardData.CardActionDataList.Add(new CardActionData
                {
                    cardActionType = CardActionType.Thematic,
                    actionTargetType = ActionTargetType.Ally,
                    curiosityChange = jsonCardDataItem.curiosity,
                    followersChange = jsonCardDataItem.followers,
                    moneyChange = jsonCardDataItem.money
                });
            }
        }

        private CardData GenerateCard(JsonCardDataItem jsonCardDataItem)
        {
            var card = jsonCardDataItem.ToCardData();
            card.specialKeywordsList = new List<SpecialKeywords>() { SpecialKeywords.Strength };
            card.cardSprite = SpriteHelper.LoadNewSprite(CardSpritePath + jsonCardDataItem.image);
            SetCardAction(card, jsonCardDataItem);

            return card;
        }

        public void GenerateInitialDeck()
        {
            SetAllCardsList();
            SetInitialDeck();
        }
    }
}