using UnityEngine;
using UnityEngine.EventSystems;

namespace Memoriae
{
    public class CardUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        private Vector3 _originalPos;
        private Quaternion _originalRotation;

        private Vector3 _originalHandPos;
        private Quaternion _originalHandRotation;

        private CanvasGroup _canvasGroup;
        private Transform _originalParent;

        private GameObject hoveredCommandBlock;

        private bool isFirstDrag_Flag = true;

        private void Awake()
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        #region IDragHandler Implementation
        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPos = transform.position;
            _originalRotation = transform.rotation;

            if (isFirstDrag_Flag)
            {
                _originalHandPos = transform.position;
                _originalHandRotation = transform.rotation; 
                isFirstDrag_Flag = false;
            }

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
            hoveredCommandBlock = eventData.pointerCurrentRaycast.gameObject;
            if (hoveredCommandBlock != null && hoveredCommandBlock.TryGetComponent<CommandBlock>(out var block))
            {
                block.SetCard(this);
            }
            else
            {
                transform.SetPositionAndRotation(_originalPos, _originalRotation);
            }
        }
        #endregion

        #region IPointerClickHandler Implementation

        public void OnPointerClick(PointerEventData eventData)
        {
            if (hoveredCommandBlock != null && eventData.button == PointerEventData.InputButton.Left)
            {
                hoveredCommandBlock.GetComponent<CommandBlock>().Clear();
                ReturnToHand();
            }
        }

        public void ReturnToHand()
        {
            transform.SetPositionAndRotation(_originalHandPos, _originalHandRotation);
            transform.SetParent(_originalParent);
        }

        #endregion
    }
}