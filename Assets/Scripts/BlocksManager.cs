using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    #region Variables
    public GameObject Spawner;
    public GridManager GridManager;
    public List<GameObject> SpawnableBlocks = new List<GameObject>();
    public bool CanSpawn;
    private Block m_block;
    private GameObject[] BlockDestroyers;
    #endregion

    void Start()
    {
        BlockDestroyers = new GameObject[GridManager.maxRow];
        InstanciateSpawners();
        InstanciateDestroyers();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InstanciateBlock();
        }
        DestroyBlocks();
    }

    #region Spawner&Destroyers
    private void InstanciateSpawners()
    {
        float j = GridManager.maxColumn * (GridManager.gridData.cellSize.x + GridManager.gridData.cellGap.x) / 2;
        for (int i = 0; i < GridManager.maxColumn; i++)
        {
            Instantiate(Spawner, transform.position + new Vector3(j, 1f, GridManager.maxRow * (GridManager.gridData.cellSize.z + GridManager.gridData.cellGap.z) / 2), Quaternion.identity);
            j -= GridManager.gridData.cellSize.x - GridManager.gridData.cellGap.x;
        }
    }
    private void InstanciateDestroyers()
    {
        float j = GridManager.maxRow * (GridManager.gridData.cellSize.z + GridManager.gridData.cellGap.z) / 2;
        for (int i = 0; i < GridManager.maxRow; i++)
        {
            BlockDestroyers[i] = Instantiate(Spawner, transform.position + new Vector3(GridManager.maxColumn * (GridManager.gridData.cellSize.x + GridManager.gridData.cellGap.x) / 2, 1f, j), Quaternion.identity);
            j -= GridManager.gridData.cellSize.z - GridManager.gridData.cellGap.z;
        }
    }
    #endregion

    private void InstanciateBlock()
    {
        int i = Random.Range(0, SpawnableBlocks.Count);

        if (CanSpawn)
        {
            GameObject newBlock = Instantiate(SpawnableBlocks[i]);
            CanSpawn = false;
            m_block = newBlock.GetComponent<Block>();
            m_block.XPosition = i;
        }
    }

    private void DestroyBlocks()
    {

        foreach (GameObject destroyer in BlockDestroyers)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(destroyer.transform.position, Vector3.right, GridManager.maxColumn * (GridManager.gridData.cellSize.x + GridManager.gridData.cellGap.x));

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.GetComponent<IBlockInterface>() != null)
                {
                    Destroy(hitObject);
                }
            }
        }
    }
}
