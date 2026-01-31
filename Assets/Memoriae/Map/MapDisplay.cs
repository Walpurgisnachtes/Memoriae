using UnityEngine;

namespace Memoriae
{
    public class MapDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject piecePrefab;

        private GameMap _map;

        public void InitializeMap(int width, int height)
        {
            _map = new GameMap(width, height);

            // 生成 Tile 視覺效果
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    CreateVisualElement(tilePrefab, new Vector2Int(x, y), "Tile");
                }
            }
        }

        public GameObject SpawnPieceAt(Piece piece, Vector2Int position)
        {
            if (_map.TryPlacePiece(piece, position))
            {
                return CreateVisualElement(piecePrefab, position, $"Piece_{piece.Name}");
            }
            return null;
        }

        private GameObject CreateVisualElement(GameObject prefab, Vector2Int pos, string name)
        {
            // 若 prefab 為空，則建立預設原始幾何體 (MVP 測試用)
            GameObject obj = prefab ? Instantiate(prefab) : (name.Contains("Piece") ?
                GameObject.CreatePrimitive(PrimitiveType.Sphere) :
                GameObject.CreatePrimitive(PrimitiveType.Quad));

            obj.name = $"{name}_{pos.x}_{pos.y}";
            obj.transform.SetParent(transform);
            obj.transform.position = new Vector3(pos.x, pos.y, 0);
            return obj;
        }
    }
}