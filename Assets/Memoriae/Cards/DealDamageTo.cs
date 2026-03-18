using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using UnityEngine;

namespace Memoriae
{
    public class DealDamageTo : MonoBehaviour
    {
        public MapManager mapManager;

        public void Execute()
        {
            Piece sourceObj = mapManager.gameMap.GetPieceAt(new(1, 3));
            Piece targetObj = mapManager.gameMap.GetPieceAt(new(9, 3));

            if (sourceObj == null || targetObj == null)
            {
                Debug.LogError("Fontem vel metam non invenire potuit.");
                return;
            }

            bool isSpiritual = true;

            Dictionary<string, int> sourceVariables = sourceObj.GetDamageCalculationVariables();
            Dictionary<string, int> targetVariables = targetObj.GetDamageCalculationVariables();

            int finalDamage = DamageCalculator.CalculateFromVariables(
                sourceVariables: sourceVariables,
                targetVariables: targetVariables,
                efficiency: 1.0f,
                isSpiritual: isSpiritual
            );

            targetObj.ModifyHP(-finalDamage);
            Debug.Log($"Latro {sourceObj.Name} in {targetObj.Name} damnum {finalDamage} inflixit.");
        }
    }
}