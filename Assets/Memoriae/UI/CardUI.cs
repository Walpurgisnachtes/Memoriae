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

        private GameObject settedCommandBlock;

        private bool isFirstDrag_Flag = true;

        private void Awake()
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        #region IDragHandler Implementation
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (settedCommandBlock == null)
            {
                settedCommandBlock = eventData.pointerCurrentRaycast.gameObject;
            }
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
            GameObject hoveredCommandBlock = eventData.pointerCurrentRaycast.gameObject;
            if (hoveredCommandBlock != null)
            {
                if (hoveredCommandBlock.TryGetComponent<CommandBlock>(out var block))
                {
                    settedCommandBlock = hoveredCommandBlock;
                    block.SetCard(this);

                }
                else if (hoveredCommandBlock.TryGetComponent<CardUI>(out var card))
                {
                    SwapCards(card);
                }
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
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                settedCommandBlock.GetComponent<CommandBlock>().Clear();
                ReturnToHand();
            }
        }

        public void ReturnToHand()
        {
            transform.SetPositionAndRotation(_originalHandPos, _originalHandRotation);
            transform.SetParent(_originalParent);
        }

        #endregion

        #region Card Swap

        /// <summary>
        /// 交換卡片位置：將目前卡片移到目標 CommandBlock，原本的卡片移回此卡片的原始 CommandBlock
        /// </summary>
        private void SwapCards(CardUI targetCard)
        {
            targetCard.settedCommandBlock.TryGetComponent(out CommandBlock occupiedCommandBlock);
            
            // 將佔據目標區塊的卡片移到原始區塊
            if (settedCommandBlock.TryGetComponent(out CommandBlock originalCommandBlock))
            {
                originalCommandBlock.SetCard(targetCard);
                targetCard.settedCommandBlock = settedCommandBlock;
            }
            else
            {
                // 若原始位置不是 CommandBlock，將卡片退回
                targetCard.ReturnToHand();
            }

            // 將拖動的卡片移到目標區塊
            settedCommandBlock = occupiedCommandBlock.gameObject;
            occupiedCommandBlock.SetCard(this);
        }

        #endregion
    }
}