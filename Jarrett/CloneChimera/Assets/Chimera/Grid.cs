using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour
{
    public int rows;
    public int columns;
    public float cellSize;
    public Vector2 origin;
    public GameObject PickupPrefab;
    public List<Pickup> startingPickups;
    private List<Vector2> pickupLocations;

    public GameObject wallPrefab;
    public Transform wallsContainer;
    public Dictionary<Vector2, Wall> walls;
    [HideInInspector] private List<Vector2> wallLocations;

    private void Awake()
    {
        walls = new Dictionary<Vector2, Wall>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pickupLocations = new List<Vector2>();
        foreach (Pickup p in startingPickups)
        {
            p.OnPickedUp += SpawnPickup;
            pickupLocations.Add(p.transform.position);
        }

        Invoke("ReadWalls", Time.deltaTime);
    }

    public void ReadWalls()
    {
        wallLocations = new List<Vector2>();
        foreach (Wall w in FindObjectsOfType<Wall>())
        {
            walls.Add(GetGridPosition(w.transform.position), w);
            wallLocations.Add(w.transform.position);
        }

    }

    public void RestartMap()
    {

        foreach (Pickup p in FindObjectsOfType<Pickup>())
        {
            Destroy(p.gameObject);
        }
        foreach (Vector2 pos in pickupLocations)
        {
            GameObject g = Instantiate(PickupPrefab, pos, Quaternion.identity);
            g.GetComponent<Pickup>().OnPickedUp += SpawnPickup;
        }

        foreach (Wall w in FindObjectsOfType<Wall>())
        {
            DestroyImmediate(w.gameObject);
        }
        walls.Clear();
        walls = new Dictionary<Vector2, Wall>();

        foreach (Vector2 pos in wallLocations)
        {
            GameObject g = Instantiate(wallPrefab, pos, Quaternion.identity);
            walls.Add(GetGridPosition(pos), g.GetComponent<Wall>());
            g.GetComponent<Wall>().enabled = true;
            g.transform.parent = wallsContainer;
        }
    }

    /// <summary>
    /// Returns the row, column position of a object within the grid. Expected to be in the grid when calling
    /// </summary>
    public Vector2 GetGridPosition(Vector2 worldPosition)
    {
        Vector2 diff = worldPosition - origin;
        diff /= cellSize;
        if (diff.x < 0 || diff.x >= (columns) * cellSize) return new Vector2(-1, -1);
        if (-diff.y < 0 || -diff.y >= (rows) * cellSize) return new Vector2(-1, -1);
        return new Vector2(Mathf.Floor(-diff.y), Mathf.Floor(diff.x));
    }

    /// <summary>
    /// Returns the world position of a object within the grid
    /// </summary>
    public Vector2 GetWorldPosition(Vector2 gridPosition)
    {
        return new Vector2(gridPosition.y, -gridPosition.x) + (new Vector2(origin.x + cellSize / 2, origin.y - cellSize / 2));
    }

    public void SpawnPickup()
    {
        GameObject g = Instantiate(PickupPrefab, GetRandomPosition(), Quaternion.identity);
        g.GetComponent<Pickup>().OnPickedUp += SpawnPickup;
    }

    private Vector2 GetRandomPosition()
    {
        HashSet<Vector2> picklist = new HashSet<Vector2>();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                picklist.Add(new Vector2(i, j));
            }
        }
        foreach (Player p in GameManager.instance.players)
        {
            picklist.Remove(p.gridPosition);
            foreach (Cargo c in p.cargoes)
            {
                picklist.Remove(GetGridPosition(c.transform.position));
            }
        }

        foreach (Wall w in walls.Values)
        {
            picklist.Remove(w.gridPosition);
        }
        Vector2 randomPosition = GetWorldPosition(picklist.ElementAt(Random.Range(0, picklist.Count)));
        return randomPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 0, .1f);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Gizmos.DrawWireCube(origin + new Vector2(cellSize * j, -cellSize * i) + new Vector2(cellSize, -cellSize) / 2, new Vector3(cellSize, cellSize, 0));
            }
        }
    }
}
