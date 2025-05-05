using UnityEngine;

public class TowerClickDetector : MonoBehaviour
{
   private void Start()
    {
        Debug.Log("TowerClickingScriptActivated");
    }

   private void OnMouseDown()
    {
        Debug.Log("TowerClickingDetected");
    }
}
