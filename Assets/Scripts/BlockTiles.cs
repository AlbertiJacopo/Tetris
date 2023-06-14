using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTiles : MonoBehaviour, IBlockInterface
{
    public Block Block;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        IBlockInterface testInterface = collision.gameObject.GetComponent<IBlockInterface>();
        if (testInterface != null) { Block.CanFall = false; }
    }

    private void OnCollisionExit(Collision collision)
    {
        IBlockInterface testInterface = collision.gameObject.GetComponent<IBlockInterface>();
        if (testInterface != null) { Block.CanFall = true; }
    }
}
