using UnityEngine;
using UnityEngine.EventSystems;

namespace Memoriae
{
    public class CommandBlock : MonoBehaviour, IPointerClickHandler
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
            if (OccupyingCard != null)
            {
                OccupyingCard.ReturnToHand();
                OccupyingCard = null;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // 檢查是否為左鍵點擊且目前有放置卡片
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Clear();
            }
        }
    }
}