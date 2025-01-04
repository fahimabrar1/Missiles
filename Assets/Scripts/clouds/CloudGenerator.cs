using System.Collections.Generic;
using UnityEngine;

namespace clouds
{
    public class CloudGenerator : MonoBehaviour
    {
        public List<GameObject> cloudChunkPrefabs; // List of cloud chunk prefabs
        public int gridSize = 3; // Grid size (e.g., 3x3)
        private readonly Vector2 chunkSize = new(50, 50); // Fixed chunk size
        private Vector2Int centralChunkIndex; // The index of the central chunk
        private GameObject[,] chunkGrid; // Stores chunk instances
        private Vector2Int planeChunk;
        private Transform planeTransform; // Plane's transform

        private void Start()
        {
            planeTransform = FindObjectOfType<Plane>().transform; // Replace with your plane's identifier
            chunkGrid = new GameObject[gridSize, gridSize];

            InitializeChunks();
            centralChunkIndex = new Vector2Int(gridSize / 2, gridSize / 2); // Start in the middle chunk
        }

        private void Update()
        {
            var planePosition = planeTransform.position;
            planeChunk = new Vector2Int(Mathf.RoundToInt(planePosition.x / chunkSize.x),
                Mathf.RoundToInt(planePosition.y / chunkSize.y));
            // Determine the chunk index the plane is in
        }

        private void InitializeChunks()
        {
            // Initialize grid
            for (var x = 0; x < gridSize; x++)
            for (var y = 0; y < gridSize; y++)
            {
                var position = new Vector3(
                    (x - 1) * chunkSize.x,
                    (y - 1) * chunkSize.y,
                    0f
                );

                chunkGrid[x, y] = Instantiate(GetRandomChunkPrefab(), position, Quaternion.identity);
            }
        }


        private GameObject GetRandomChunkPrefab()
        {
            return cloudChunkPrefabs[Random.Range(0, cloudChunkPrefabs.Count)];
        }
    }
}