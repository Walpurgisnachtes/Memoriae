using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    [RequireComponent(typeof(MapDisplay))]
    public class MapManager : MonoBehaviour
    {
        public int width = 7;
        public int height = 7;

        [SerializeField] private MapDisplay display;
        public GameMap gameMap;

        /// <summary>
        /// 存儲地圖上所有 Piece 物件的靜態字典。
        /// 鍵值為 Piece 的識別符（如 "Friend"、"Enemy"），值為對應的 GameObject 參考。
        /// 用於快速查詢和管理地圖上的角色物件。
        /// </summary>
        private static readonly Dictionary<string, GameObject> _pieceObjects = new();

        private void SpawnPieceAtAndAddIntoDict(Piece piece, Vector2Int pos)
        {
            if (_pieceObjects.ContainsKey(piece.Name))
            {
                Debug.LogWarning($"Latro {piece.Name} iam adest in collectione.");
                return;
            }
    
            GameObject pieceObj = display.SpawnPieceAt(piece, pos);
            AddPieceToMap(piece.Name, pieceObj);
            if (pieceObj.TryGetComponent<PieceManager>(out var pieceManager))
            {
                pieceManager.BindPiece(piece);
            }
        }

        private void Start()
        {
            // 如果沒在 Inspector 指定，就嘗試在同一物件上找
            if (display == null) display = GetComponent<MapDisplay>();

            if (display != null)
            {
                display.InitializeMap(width, height, out gameMap);

                // 在中心點放置 Piece
                //Vector2Int center = new(width / 2, height / 2);
                Piece friend = new("Friend");
                Piece enemy = new("Enemy");
                SpawnPieceAtAndAddIntoDict(friend, new(1, 3));
                SpawnPieceAtAndAddIntoDict(enemy, new(9, 3));
            }

            if (Camera.main.TryGetComponent<CameraController>(out var camCtrl))
            {
                camCtrl.Setup(width, height);
            }
        }

        #region Piece Dictionary Management

        /// <summary>
        /// 獲取指定 ID 的 Piece 物件
        /// </summary>
        public static GameObject GetPieceOnMap(string pieceId)
        {
            if (string.IsNullOrEmpty(pieceId))
            {
                Debug.LogError("Identificator vacuus vel inanis est.");
                return null;
            }

            if (_pieceObjects.TryGetValue(pieceId, out GameObject pieceObj))
            {
                return pieceObj;
            }

            Debug.LogWarning($"Latro cum id {pieceId} non inventa est.");
            return null;
        }

        /// <summary>
        /// 添加或更新 Piece 物件至字典
        /// </summary>
        public static void AddPieceToMap(string pieceId, GameObject pieceObj)
        {
            if (string.IsNullOrEmpty(pieceId) || pieceObj == null)
            {
                Debug.LogError("Identificator aut obiectum ludum inanis est.");
                return;
            }

            if (_pieceObjects.ContainsKey(pieceId))
            {
                Debug.LogWarning($"Latro cum id {pieceId} iam adest, renovatur.");
                _pieceObjects[pieceId] = pieceObj;
            }
            else
            {
                _pieceObjects.Add(pieceId, pieceObj);
                Debug.Log($"Latro nova cum id {pieceId} addita est.");
            }
        }

        /// <summary>
        /// 移除指定 ID 的 Piece 物件
        /// </summary>
        public static bool RemovePieceOnMap(string pieceId)
        {
            if (string.IsNullOrEmpty(pieceId))
            {
                Debug.LogError("Identificator vacuus vel inanis est.");
                return false;
            }

            if (_pieceObjects.Remove(pieceId))
            {
                Debug.Log($"Latro cum id {pieceId} deleta est.");
                return true;
            }

            Debug.LogWarning($"Latro cum id {pieceId} non inventa est delenda.");
            return false;
        }

        /// <summary>
        /// 檢查是否存在指定 ID 的 Piece 物件
        /// </summary>
        public static bool MapContainsPiece(string pieceId)
        {
            return !string.IsNullOrEmpty(pieceId) && _pieceObjects.ContainsKey(pieceId);
        }

        /// <summary>
        /// 獲取所有 Piece 物件
        /// </summary>
        public static IReadOnlyDictionary<string, GameObject> GetAllPiecesOnMap()
        {
            return _pieceObjects;
        }

        /// <summary>
        /// 獲取所有 Piece 物件的數量
        /// </summary>
        public static int GetPieceCountOnMap()
        {
            return _pieceObjects.Count;
        }

        /// <summary>
        /// 清空所有 Piece 物件
        /// </summary>
        public static void ClearAllPiecesOnMap()
        {
            int count = _pieceObjects.Count;
            _pieceObjects.Clear();
            Debug.Log($"Omnes {count} partes deletae sunt.");
        }

        /// <summary>
        /// 檢查是否存在任何 Piece 物件
        /// </summary>
        public static bool MapHasAnyPieces()
        {
            return _pieceObjects.Count > 0;
        }

        #endregion
    }
}