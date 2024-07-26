using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementSystem : MonoBehaviour
{
   [SerializeField] private GameObject mouseIndicator, cellIndicator;
   [SerializeField] private GridInputManager gridInputManager;
   [SerializeField] private Grid grid;

   private void Update()
   {
        Vector3 mousePosition = gridInputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
   }
}
