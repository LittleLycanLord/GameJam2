using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityStandardAssets.Effects;

public class GridInputManager : MonoBehaviour
{
    //REFERENCE USED: https://www.youtube.com/watch?v=l0emsAHIBjU&list=PLcRSafycjWFepsLiAHxxi8D_5GGvu6arf
    //https://www.youtube.com/watch?v=nKF4Pwb-6Ow&list=PLcRSafycjWFepsLiAHxxi8D_5GGvu6arf&index=2
    
   [SerializeField] private Camera sceneCamera;
   private Vector3 lastPosition;
   [SerializeField] private LayerMask placementLayerMask;

   public List<Vector2Int> gridList;

   public Material gridCoords;

   private void Start()
   {
        //labeling each grid
        for (int i = 1; i <= 10; i++)
        {
            Vector2Int grid = new Vector2Int(i,i);
            gridList.Add(grid);
        }

        //gridCoords.setFloat("DefaultScale",5);
        gridCoords = G;
   }

   public Vector3 GetSelectedMapPosition()
   {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastPosition = hit.point;
            Debug.Log(lastPosition);
        }
        return lastPosition;
   }
}
