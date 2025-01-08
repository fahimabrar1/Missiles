using UnityEngine;

namespace clouds
{
    public class CloudGenerator : MonoBehaviour
    {
        public Transform player; // The player or camera transform to track movement
        public GameObject[] cloudChunks; // Array of cloud chunk GameObjects
        public float chunkSize = 50f; // Size of each chunk (width and height)
        public int gridWidth = 3; // Number of chunks horizontally
        public int gridHeight = 3; // Number of chunks vertically

        private Vector2Int _currentGridCenter;

        private void Start()
        {
            // Initialize the grid center based on the starting position
            _currentGridCenter = new Vector2Int(gridWidth / 2, gridHeight / 2);
        }

        private void Update()
        {
            var newGridCenter = GetGridCenterFromPlayer();

            if (newGridCenter == _currentGridCenter) return;
            ShiftChunks(_currentGridCenter, newGridCenter);
            _currentGridCenter = newGridCenter;
        }

        private Vector2Int GetGridCenterFromPlayer()
        {
            // Calculate the current grid center based on the player's position
            var centerX = Mathf.FloorToInt(player.position.x / chunkSize) + gridWidth / 2;
            var centerY = Mathf.FloorToInt(player.position.y / chunkSize) + gridHeight / 2;
            return new Vector2Int(centerX, centerY);
        }

        private void ShiftChunks(Vector2Int oldCenter, Vector2Int newCenter)
        {
            var deltaX = newCenter.x - oldCenter.x;
            var deltaY = newCenter.y - oldCenter.y;

            foreach (var chunk in cloudChunks)
            {
                var chunkPos = chunk.transform.position;

                // Shift horizontally
                if (deltaX != 0)
                    switch (deltaX)
                    {
                        case > 0 when chunkPos.x < newCenter.x * chunkSize - gridWidth * chunkSize / 2:
                            chunk.transform.position += Vector3.right * (gridWidth * chunkSize);
                            break;
                        case < 0 when chunkPos.x > newCenter.x * chunkSize + gridWidth * chunkSize / 2:
                            chunk.transform.position -= Vector3.right * (gridWidth * chunkSize);
                            break;
                    }

                // Shift vertically
                if (deltaY != 0)
                    switch (deltaY)
                    {
                        case > 0 when chunkPos.y < newCenter.y * chunkSize - gridHeight * chunkSize / 2:
                            chunk.transform.position += Vector3.up * (gridHeight * chunkSize);
                            break;
                        case < 0 when chunkPos.y > newCenter.y * chunkSize + gridHeight * chunkSize / 2:
                            chunk.transform.position -= Vector3.up * (gridHeight * chunkSize);
                            break;
                    }
            }
        }
    }
}