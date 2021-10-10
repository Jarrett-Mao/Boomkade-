using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Grid grid;
    public Vector2 gridPosition;
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        gridPosition = grid.GetGridPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        grid.walls.Remove(gridPosition);
    }
}
