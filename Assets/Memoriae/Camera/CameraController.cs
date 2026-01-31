using UnityEngine;

namespace Memoriae
{
    public class CameraController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float zoomSpeed = 2f;
        public float minSize = 2f;
        public float maxSize = 10f;

        private Camera _cam;
        private Vector2 _mapBounds;
        private Vector3 _initialCenter;

        public void Setup(int width, int height)
        {
            _cam = GetComponent<Camera>();
            _mapBounds = new Vector2(width - 1, height - 1);
            _initialCenter = new Vector3(_mapBounds.x / 2f, _mapBounds.y / 2f, -10f);

            // 初始置中
            transform.position = _initialCenter;
        }

        private void Update()
        {
            HandleMovement();
            HandleZoom();
            HandleReset();
        }

        private void HandleMovement()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 move = new Vector3(h, v, 0) * moveSpeed * Time.deltaTime;
            Vector3 targetPos = transform.position + move;

            // 限制移動範圍：中心點不得超出地圖邊界 (0,0) 到 (Width-1, Height-1)
            targetPos.x = Mathf.Clamp(targetPos.x, 0, _mapBounds.x);
            targetPos.y = Mathf.Clamp(targetPos.y, 0, _mapBounds.y);

            transform.position = targetPos;
        }

        private void HandleZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize - scroll * zoomSpeed, minSize, maxSize);
            }
        }

        private void HandleReset()
        {
            if (Input.GetMouseButtonDown(2)) // 中鍵
            {
                transform.position = _initialCenter;
            }
        }
    }
}