using System.Collections.Generic;
using UnityEngine;
using HoudiniEngineUnity;

public class ChunkLoader : MonoBehaviour
{
    public HEU_HoudiniAsset hdaPrefab;
    public int gridSize = 3;
    public float chunkSize = 200f;
    public float loadRadius = 300f;

    private Dictionary<Vector2Int, HEU_HoudiniAsset> loadedChunks = new Dictionary<Vector2Int, HEU_HoudiniAsset>();

    void Update()
    {
        Vector3 pos = transform.position;
        Vector2Int playerChunk = new Vector2Int(
            Mathf.FloorToInt(pos.x / chunkSize),
            Mathf.FloorToInt(pos.z / chunkSize)
        );

        // Load surrounding chunks
        for (int x = -gridSize; x <= gridSize; x++)
        {
            for (int y = -gridSize; y <= gridSize; y++)
            {
                Vector2Int chunkCoord = new Vector2Int(playerChunk.x + x, playerChunk.y + y);

                if (!loadedChunks.ContainsKey(chunkCoord))
                {
                    Vector3 chunkPos = new Vector3(chunkCoord.x * chunkSize, 0, chunkCoord.y * chunkSize);
                    HEU_HoudiniAsset newChunk = Instantiate(hdaPrefab, chunkPos, Quaternion.identity);

                    loadedChunks.Add(chunkCoord, newChunk);
                }
            }
        }

        // Unload far chunks
        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var kvp in loadedChunks)
        {
            float dist = Vector3.Distance(pos, new Vector3(kvp.Key.x * chunkSize, 0, kvp.Key.y * chunkSize));
            if (dist > loadRadius)
            {
                Destroy(kvp.Value.gameObject);
                toRemove.Add(kvp.Key);
            }
        }

        foreach (var rem in toRemove)
            loadedChunks.Remove(rem);
    }
}