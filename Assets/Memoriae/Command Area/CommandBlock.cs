using UnityEngine;
using UnityEngine.EventSystems;

namespace Memoriae
{
    public class CommandBlock : MonoBehaviour
    {
        public CardUI OccupyingCard { get; private set; }

        public void SetCard(CardUI card)
        {
            // 若原本已有卡片，先將其退回
            if (OccupyingCard != null) Clear();

            OccupyingCard = card;
            card.transform.position = transform.position;
        }

        public void Clear()
        {
            OccupyingCard = null;
        }
    }
}