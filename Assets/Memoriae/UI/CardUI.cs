using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Memoriae
{
    public class CardUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _originalPos;
        private Quaternion _originalRotation;
        private CanvasGroup _canvasGroup;
        private Transform _originalParent;

        private void Awake()
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        #region IDragHandler Implementation
        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPos = transform.position;
            _originalRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(0, 0, 0); // 拖動時重置旋轉

            _originalParent = transform.parent;
            _canvasGroup.blocksRaycasts = false; // 讓射線穿透卡片以偵測下方的槽
            transform.SetAsLastSibling(); // 顯示在最前層
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;

            // 偵測滑鼠下方是否有 CommandBlock
            GameObject hovered = eventData.pointerCurrentRaycast.gameObject;
            if (hovered != null && hovered.TryGetComponent<CommandBlock>(out var block))
            {
                block.SetCard(this);
            }
            else
            {
                transform.position = _originalPos; // 回到原位
                transform.rotation = _originalRotation;
            }
        }
        #endregion

        public void ReturnToHand()
        {
            transform.SetParent(_originalParent);
            transform.position = _originalPos;
            // 確保回到手牌層級後，重新計算扇形排列（可視需求呼叫 HandUI）
        }
    }
}