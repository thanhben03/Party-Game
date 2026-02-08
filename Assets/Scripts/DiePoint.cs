using UnityEngine;

public class DiePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit DiePoint, respawning...");
            SpawnPlayer.Instance.Spawn(other.GetComponent<PlayerController>());
        }
    }
}
