using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Follower
{
    public Vector2 Direction;
    public Vector2 input;
    public Grid grid;
    public Vector2 gridPosition;
    public bool Destroyed;
    public float speed;

    public List<Cargo> cargoes;

    private void Awake()
    {
        cargoes = new List<Cargo>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetPosition(transform.position, transform.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Destroyed) return;
        Debug.Log(grid.walls.Count);
        if (Vector2.Distance(transform.position, targetPosition) <= 0)
        {
            UpdateDirection(input);
            UpdateCargoes();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    public void SetPosition(Vector3 position, float direction)
    {
        transform.position = position;
        gridPosition = grid.GetGridPosition(position);
        targetPosition = transform.position;
        lastPosition = targetPosition;
        transform.localEulerAngles = new Vector3(0, 0, direction);
        Direction = GetDirection();
        input = new Vector2(Direction.x, Direction.y);
    }

    public void UpdatePlayer(Vector2 input)
    {
        if (input != Vector2.zero && this.input != -new Vector2(-input.y, input.x) && GetDirection() != -new Vector2(-input.y, input.x)) this.input = new Vector2(-input.y, input.x);
    }

    public Vector2 UpdateDirection(Vector2 input)
    {
        Vector2 direction = new Vector3(input.y, -input.x);
        Vector2 nextPosition = grid.GetGridPosition(transform.position + new Vector3(input.y, -input.x) * grid.cellSize);
        if (nextPosition.x >= 0 && nextPosition.y >= 0 && !grid.walls.ContainsKey(nextPosition))
        {

            lastPosition = targetPosition;
            targetPosition = grid.GetWorldPosition(nextPosition);
        }
        else // Find alternative route
        {
            gridPosition = grid.GetGridPosition(transform.position);
            //Debug.Log("Alt:" + gridPosition);
            Vector2 up = new Vector2(-1, 0);
            Vector2 right = new Vector2(0, 1);
            Vector2 down = new Vector2(1, 0);
            Vector2 left = new Vector2(0, -1);

            Direction = GetDirection();
            if (IsValidPosition(Direction))
            {
                Debug.Log("True" + gridPosition + Direction);
                direction = UpdateTargetPostion(Direction);
            }
            else if (direction.x != 0) // Moving left or right
            {
                if (direction.x > 0 && IsValidPosition(up))
                {
                    direction = UpdateTargetPostion(up);
                }
                else if (direction.x < 0 && IsValidPosition(down))
                {
                    direction = UpdateTargetPostion(down);
                }
                else if (IsValidPosition(up)) // up
                {
                    direction = UpdateTargetPostion(up);
                }
                else if (IsValidPosition(down)) // down
                {
                    direction = UpdateTargetPostion(down);
                }
                else
                {
                    Debug.Log("Trapped LR!");
                    GameManager.instance.HandleDestroyPlayer(this);
                }
            }
            else // Moving up or down
            {
                if (direction.y < 0 && IsValidPosition(right))
                {
                    direction = UpdateTargetPostion(right);
                }
                else if (direction.y > 0 && IsValidPosition(left))
                {
                    direction = UpdateTargetPostion(left);
                }
                else if (IsValidPosition(right)) // right
                {
                    direction = UpdateTargetPostion(right);
                }
                else if (IsValidPosition(left)) // left
                {
                    direction = UpdateTargetPostion(left);
                }
                else
                {
                    Debug.Log("Trapped UD!");
                    GameManager.instance.HandleDestroyPlayer(this);
                }
            }
        }
        float rot_z = Mathf.Atan2(((Vector3)targetPosition - transform.position).y, ((Vector3)targetPosition - transform.position).x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        return direction;
    }

    public bool IsValidPosition(Vector2 input)
    {
        if (this.input == -input) return false;
        Vector2 newPos = gridPosition + input;
        return newPos.x >= 0 && newPos.y >= 0 && newPos.y < grid.columns && newPos.x < grid.rows && !grid.walls.ContainsKey(newPos);
    }

    public Vector2 UpdateTargetPostion(Vector2 input)
    {
        this.input = input;
        lastPosition = targetPosition;
        targetPosition = grid.GetWorldPosition(gridPosition + input);
        return this.input;
    }

    public Vector2 GetDirection()
    {
        int angle = Mathf.RoundToInt(transform.localEulerAngles.z);
        switch (angle)
        {
            case 0:
                return new Vector2(-1, 0); // up
            case 270:
                return new Vector2(0, 1); // right
            case 180:
                return new Vector2(1, 0); // down
            case 90:
                return new Vector2(0, -1); // left

        }
        return new Vector2(-1, 0);
    }

    public void UpdateCargoes()
    {
        foreach (Cargo c in cargoes)
        {
            c.lastPosition = c.targetPosition;
            c.targetPosition = c.target.lastPosition;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            GameManager.instance.HandleDestroyPlayer(other.GetComponent<Player>());
        }
    }
}
