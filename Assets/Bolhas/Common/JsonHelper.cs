using NueGames.NueDeck.Scripts.Data.Collection;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class JsonCardList
{
    public List<JsonCardDataItem> data;
}

[Serializable]
public class JsonCardDataItem 
{
    public int type;
    public int theme;
    public string title;
    public string description;
    public string image;
    public int curiosity;
    public int money;
    public int followers;
}

public static class JsonHelper
{
    public static List<JsonCardDataItem> LoadCardsJson()
    {
        string jsonFilePath = Application.streamingAssetsPath + "\\Data\\initialDeck.json";
        List<JsonCardDataItem> theList = null;

        using (StreamReader r = new StreamReader(jsonFilePath))
        {
            string jsonString = r.ReadToEnd();
            var jsonCardListObj = JsonUtility.FromJson<JsonCardList>(jsonString);
            theList = jsonCardListObj.data;
        }

        return theList;
    }
    public static CardData ToCardData(this JsonCardDataItem jsonCardDataItem)
    {
        // Cria a instância de CardData usando ScriptableObject
        var cardData = ScriptableObject.CreateInstance<CardData>();
        
        cardData.cardName = jsonCardDataItem.title;
        cardData.cardDescriptionDataList = new List<CardDescriptionData>
        {
            new CardDescriptionData { descriptionText = jsonCardDataItem.description }
        };
        cardData.curiosityChange = jsonCardDataItem.curiosity;
        cardData.moneyChange = jsonCardDataItem.money;
        cardData.followersChange = jsonCardDataItem.followers;
        cardData.card_type = jsonCardDataItem.type;
        cardData.card_theme = jsonCardDataItem.theme;

        return cardData;
    }
} 

