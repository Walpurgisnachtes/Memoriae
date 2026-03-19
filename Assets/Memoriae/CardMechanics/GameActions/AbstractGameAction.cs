using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public enum AtomicGameActionType
    {
        None = 0,
        // 改變牌的位置（例如從Deck移動到Hand，或從Hand移動到Graveyard）
        CardPositionModificationAction = 1,
        // 改變牌的效果（例如改變、增加或無效化一張牌的效果）
        CardEffectModificationAction = 2,
        CharacterHPModificationAction = 3,
        CharacterCoordinatesModificationAction = 4,
        CharacterStatusEffectModificationAction = 5,
        TileStatusEffectModificationAction = 6,
        PlayerStatusEffectModificationAction = 7,
        RoundStatusModificationAction = 8,
    }

    /// <summary>
    /// 遊戲動作工廠 - 根據動作類型創建對應的遊戲動作實例
    /// </summary>
    public static class GameActionFactory
    {
        public static AbstractGameAction CreateGameActionFromType(AtomicGameActionType actionType, Dictionary<string, object> parameters = null)
        {
            AbstractGameAction gameAction = null;

            try
            {
                // Actionem ludicam ex typo creare conabimur
                gameAction = actionType switch
                {
                    AtomicGameActionType.CardPositionModificationAction => new CardPositionModificationAction(parameters),
                    AtomicGameActionType.CardEffectModificationAction => new CardEffectModificationAction(parameters),
                    AtomicGameActionType.CharacterHPModificationAction => new CharacterHPModificationAction(parameters),
                    AtomicGameActionType.CharacterCoordinatesModificationAction => new CharacterCoordinatesModificationAction(parameters),
                    AtomicGameActionType.CharacterStatusEffectModificationAction => new CharacterStatusEffectModificationAction(parameters),
                    AtomicGameActionType.TileStatusEffectModificationAction => new TileStatusEffectModificationAction(parameters),
                    AtomicGameActionType.PlayerStatusEffectModificationAction => new PlayerStatusEffectModificationAction(parameters),
                    AtomicGameActionType.RoundStatusModificationAction => new RoundStatusModificationAction(parameters),
                    _ => throw new ArgumentException($"Actionem ludicam ignotam accepi: {actionType}", nameof(actionType))
                };
            }
            catch (ArgumentException argEx)
            {
                Debug.LogError($"Argumentum invalidum in CreateGameActionFromType: {argEx.Message}");
                throw;
            }
            catch (NullReferenceException nullEx)
            {
                Debug.LogError($"Referentia nulla detecta in CreateGameActionFromType: {nullEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error inopinatus in CreateGameActionFromType: {ex.GetType().Name} - {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
            finally
            {
                if (gameAction == null)
                {
                    Debug.LogWarning($"Actio ludica pro typo {actionType} non creata est.");
                }
            }

            return gameAction;
        }
    }

    public abstract class AbstractGameAction
    {
        public AtomicGameActionType GameActionType { get; protected set; }
        // 所有GameAction都必須有一張AbstractCard類型作為Source，表示這個GameAction是由哪張牌觸發的
        public AbstractCard SourceCard { get; protected set; }
        public int Amount { get; protected set; }
        public Guid Guid { get; private set; } = Guid.NewGuid();
        public abstract Dictionary<string, object> GetParameters();
    }
}