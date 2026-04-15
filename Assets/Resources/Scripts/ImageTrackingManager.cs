using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTrackingManager : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject boardPrefab;
    public GameObject chompPrefab;

    private GameObject spawnedBoard;
    private bool boardSpawned = false;
    private bool chompSpawned = false;

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            HandleMarker(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            HandleMarker(trackedImage);
        }
    }

    void HandleMarker(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Debug.Log("Detected: " + name);

        if (name == "marker")
        {
            Debug.Log("marker seen | boardSpawned=" + boardSpawned);
        }

        if (name == "marker2")
        {
            Debug.Log("marker2 seen | boardSpawned=" + boardSpawned + " | chompSpawned=" + chompSpawned);
        }

        if (name == "marker" && !boardSpawned)
        {
            Debug.Log("Spawning board");

            spawnedBoard = Instantiate(
                boardPrefab,
                trackedImage.transform.position,
                trackedImage.transform.rotation
            );

            spawnedBoard.transform.SetParent(trackedImage.transform);
            spawnedBoard.transform.localPosition = Vector3.zero;
            spawnedBoard.transform.localRotation = Quaternion.identity;

            boardSpawned = true;
            Debug.Log("Board spawned");
        }

        if (name == "marker2" && boardSpawned && !chompSpawned)
        {
            Debug.Log("Trying to spawn Chomp");

            Transform spawnPoint = spawnedBoard.transform.Find("PlayerSpawnPoint");

            if (spawnPoint == null)
            {
                Debug.LogError("Spawn point not found!");
                return;
            }

            Instantiate(
                chompPrefab,
                spawnPoint.position + Vector3.up * 0.1f,
                Quaternion.identity
            );

            chompSpawned = true;
            Debug.Log("Chomp spawned");
        }
    }
}