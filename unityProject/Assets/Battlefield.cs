using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour {

    public int width = 8;
    public int depth = 8;

    public Tile tilePrefab;
    private Vector3 tileSize;

    private void Start ()
    {
        Regenerate();
	}


    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            Regenerate();
        }
    }

    private void Regenerate()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        float realWidth = width * tilePrefab.size.x;
        float realDepth = depth * tilePrefab.size.z;

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < depth; ++j)
            {
                var tile = Instantiate(tilePrefab, transform);
                tile.transform.localPosition = new Vector3(
                    -realWidth / 2 + (i + 0.5f) * tilePrefab.size.x,
                    0,
                    -realDepth / 2 + (j + 0.5f) * tilePrefab.size.z
                );
                tile.transform.rotation = Quaternion.identity;
                tile.x = i;
                tile.y = j;
            }
        }
    }

    public void Hit(Tile tile)
    {
        Debug.LogFormat("Tile ({0},{1}) was hit", tile.x, tile.y);
    }
}
