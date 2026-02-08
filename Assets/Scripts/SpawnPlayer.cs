using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public static SpawnPlayer Instance { get; private set; }
    [SerializeField] private Transform[] spawnPoints;


    private void Awake()
    {
        Instance = this;
    }

    public void Spawn(PlayerController player)
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("SpawnPlayer: not assign spawnPoints!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

        if (cc != null) cc.enabled = true;

        Debug.Log($"SpawnPlayer: Spawned player at {spawnPoint.position}");
    }

}
