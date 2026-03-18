using UnityEngine;

namespace Memoriae
{
    [RequireComponent(typeof(MapDisplay))]
    public class MapManager : MonoBehaviour
    {
        public int width = 7;
        public int height = 7;

        [SerializeField] private MapDisplay display;

        private void Start()
        {
            // 如果沒在 Inspector 指定，就嘗試在同一物件上找
            if (display == null) display = GetComponent<MapDisplay>();

            if (display != null)
            {
                display.InitializeMap(width, height);

                // 在中心點放置 Piece
                Vector2Int center = new(width / 2, height / 2);
                display.SpawnPieceAt(new("Friend"), new(1, 3));
                display.SpawnPieceAt(new("Enemy"), new(9, 3));
            }

            if (Camera.main.TryGetComponent<CameraController>(out var camCtrl))
            {
                camCtrl.Setup(width, height);
            }
        }
    }
}