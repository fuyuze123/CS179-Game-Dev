using UnityEngine;
using UnityEngine.EventSystems;
public class TowerClickDetector : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask towerLayer;
    [SerializeField] private Color highlightColor = Color.yellow;

    private Color ogColor;
    private SpriteRenderer sr;



    private void Awake()
    {

        if(mainCamera == null){mainCamera = Camera.main; }
    }

    private void Highlight()
    {
        Debug.Log("Highlighting tower!");
        sr.color = highlightColor; 
    }
    

    private void cancelHighlight()
    {
        sr.color = ogColor;
    }


   private void Start()
    {
        Debug.Log("TowerClickingScriptActivated");
        sr = GetComponentInChildren<SpriteRenderer>();
        ogColor = sr.color;
    }

   
   //We use the if statement to interact with UI panel and world objects(towers in this case) at the same time
   void Update()
   {
    if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, towerLayer);
            if (hit.collider != null)
            {
              
                if (hit.collider.gameObject == gameObject)
                {
                    Highlight(); 
                }
            }
        }

   }
}
