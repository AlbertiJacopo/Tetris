using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBlockInterface
{
    #region Variables
    public GridManager GridManager;
    public BlocksManager BlockManager;
    public int XPosition;
    public bool CanFall;
    private bool m_CanMove;
    #endregion


    // Update is called once per frame
    void Update()
    {
        CheckBlocksUnder();
        OrizontalMovement();
        FallingMovement();
    }

    #region Movement
    public void FallingMovement()
    {
        for (int i = 0; i < GridManager.maxRow && CanFall; i++)
        {
            if (CanFall)
            {
                continue;
            }
            transform.position = Vector3.back * (GridManager.gridData.cellSize.z + GridManager.gridData.cellGap.z);
            new WaitForSeconds(2f);
        }
        BlockManager.CanSpawn = true;
    }

    public void OrizontalMovement()
    {
        if (m_CanMove)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && (XPosition < GridManager.maxColumn - 1))
            {
                transform.position = Vector3.right * (GridManager.gridData.cellSize.x + GridManager.gridData.cellGap.x);
                XPosition += 1;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && XPosition >= 1)
            {
                transform.position = Vector3.left * (GridManager.gridData.cellSize.x + GridManager.gridData.cellGap.x);
                XPosition -= 1;
            }
        }
    }
    #endregion

    #region Collisions

    private void CheckBlocksUnder()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, GridManager.gridData.cellSize.z + GridManager.gridData.cellGap.z))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.GetComponent<IBlockInterface>() != null)
            {
                CanFall = false;
                m_CanMove = false;
            }
            if (hitObject.GetComponent<IBlockInterface>() == null)
            {
                CanFall = true;
            }
        }
    }
    #endregion
}
