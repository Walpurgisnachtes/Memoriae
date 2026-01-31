using UnityEngine;
using System.Collections.Generic;

namespace Memoriae
{
    public class HandUI : MonoBehaviour
    {
        [Header("Layout Settings")]
        public GameObject cardPrefab; 
        public float radius = 1200f;      // 增加半徑讓弧度更寬、更平
        public float angleStep = 15f;     // 增加角度讓卡片間距拉開
        public float yOffset = -1100f;    // 配合大半徑，將圓心進一步下移

        public float cardSize = 3.0f;    // 卡片大小比例

        private List<GameObject> _cards = new List<GameObject>();

        public void AddCard()
        {
            GameObject card = Instantiate(cardPrefab, transform);
            card.GetComponent<RectTransform>().localScale = new(cardSize, cardSize, cardSize);
            _cards.Add(card);
            UpdateHandLayout();
        }

        private void UpdateHandLayout()
        {
            int count = _cards.Count;
            for (int i = 0; i < count; i++)
            {
                // 計算相對角度：以中間卡片為 0 度
                float currentAngle = (i - (count - 1) / 2f) * angleStep;

                // 角度轉弧度 (注意：Unity 座標系中 0 度在上方)
                float radian = (90f - currentAngle) * Mathf.Deg2Rad;

                // 計算座標
                float x = Mathf.Cos(radian) * radius;
                float y = Mathf.Sin(radian) * radius + yOffset;

                RectTransform rect = _cards[i].GetComponent<RectTransform>();
                rect.localPosition = new Vector3(x, y, 0);
                rect.localRotation = Quaternion.Euler(0, 0, -currentAngle);
            }
        }

        private void Start()
        {
            // 測試：初始添加幾張卡片
            for (int i = 0; i < 5; i++)
            {
                AddCard();
            }

        }
    }
}