using UnityEngine;

public class SpawnAlignToGrid : MonoBehaviour
{
    [SerializeField] private bool IsSpawnAlignedToGrid = true;
    void Start()
    {
        if (IsSpawnAlignedToGrid)
        {
            this.transform.position = new Vector3(Mathf.Floor(this.transform.position.x) + 0.5f, Mathf.Floor(this.transform.position.y) + 0.5f, this.transform.position.z);
        }
    }
}
