using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalker : MonoBehaviour
{
    public int iterations;
    public GameObject tilePrefab;
    public GameObject dungeonParent; // Parent GameObject für den Dungeon
    public float checkRadius = 0.25f;
    private LayerMask layermask;
    private Vector2 currentDirection;

    void Start()
    {
        CreateDungeon(); // Erstellen Sie einen Dungeon beim Start als Beispiel
    }

    public void CreateDungeon()
    {
        ClearDungeon(); // Löschen des alten Dungeons

        Vector3 startPosition = transform.position;

        for (int i = 0; i < iterations; i++)
        {
            if (!CheckForTile(startPosition))
            {
                GameObject tile = Instantiate(tilePrefab, startPosition, Quaternion.identity);
                tile.transform.SetParent(dungeonParent.transform); // Setze das Parent-GameObject
            }
            Vector2 direction = new Vector2();

            if (i > iterations / 2 && i < (iterations /2) + 50)
            {
                direction = ChooseCorridorDirection();
                currentDirection = direction;
            }
            else
            {
                direction = ChooseRandomDirection();
                currentDirection = direction;
            }

            startPosition += new Vector3(direction.x, direction.y, 0);
        }
    }

    private bool CheckForTile(Vector3 position)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(position, checkRadius, layermask);

        return hitCollider != null;
    }

    private Vector2 ChooseRandomDirection()
    {
        switch (Random.Range(0, 4))
        {
            case 0: return Vector2.up;
            case 1: return Vector2.down;
            case 2: return Vector2.left;
            case 3: return Vector2.right;
            default: return Vector2.zero;
        }
    }

    private Vector2 ChooseCorridorDirection()
    {
        switch (Random.Range(0, 2))
        {
            case 0: return  currentDirection;
            case 1: return ChooseRandomDirection();
            default: return Vector2.zero;
        }
    }

    private void ClearDungeon()
    {
        // Löschen aller Child-Objekte des Dungeon-Parent
        foreach (Transform child in dungeonParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

