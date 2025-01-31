using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Card.CardActions
{
    public class ThematicAction: CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Thematic;
        public override void DoAction(CardActionParameters actionParameters)
        {
            Debug.Log("Jogou essa carta: " + actionParameters.CardData.CardName);

            var selfCharacter = actionParameters.SelfCharacter;
            selfCharacter.curiosity += actionParameters.CardData.curiosityChange;
            selfCharacter.money += actionParameters.CardData.moneyChange;
            selfCharacter.followers += actionParameters.CardData.followersChange;

            selfCharacter.CharacterStats.Damage(0);

            if (FxManager != null)
            {
                FxManager.PlayFx(actionParameters.TargetCharacter.transform,FxType.Attack);
            }
           
            if (AudioManager != null) 
                AudioManager.PlayOneShot(actionParameters.CardData.AudioType);
        }
    }
}