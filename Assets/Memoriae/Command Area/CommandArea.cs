using UnityEngine;
using UnityEngine.UI;

namespace Memoriae
{
    [RequireComponent(typeof(RectTransform))]
    public class CommandArea : MonoBehaviour
    {
        public GameObject blockPrefab;
        public int blockCount = 7;
        public float spacing = 10f;
        public float cardSize = 3.0f;    // 卡片大小比例

        private void Start()
        {
            ConfigureLayout();
            SpawnBlocks();
        }

        private void ConfigureLayout()
        {
            RectTransform rect = GetComponent<RectTransform>();

            // 設定錨點為水平拉伸，對齊螢幕寬度
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(0.5f, 0.5f);

            // 寬度 offset 設為 0 (全寬)，高度可根據需求調整
            rect.offsetMin = new Vector2(0, rect.offsetMin.y);
            rect.offsetMax = new Vector2(0, rect.offsetMax.y);

            // 自動管理子物件排列
            HorizontalLayoutGroup layout = gameObject.GetComponent<HorizontalLayoutGroup>();
            if (layout == null) layout = gameObject.AddComponent<HorizontalLayoutGroup>();

            layout.spacing = spacing;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false; // 由 Prefab 或自身控制寬度
            layout.childControlHeight = false; 
            layout.childForceExpandWidth = false;
        }

        private void SpawnBlocks()
        {
            for (int i = 0; i < blockCount; i++)
            {
                GameObject commandBlock = Instantiate(blockPrefab, transform);
                // 根據 Kit 的需求設定縮放
                commandBlock.GetComponent<RectTransform>().localScale = new Vector3(cardSize, cardSize, cardSize);
            }
        }
    }
}