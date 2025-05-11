using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Perk")] //This is a scriptable object.

//CreatAssetmenu allows us to create the object with right click.


public class TowerPerk : ScriptableObject
{
    public string perkName;
    public string description;
    public Sprite icon; 

    public float damageModifier = 1f;
    public float fireRateModifier = 1f;
    public float rangeModifier = 1f;

    public TowerPerk nextPerk; 
}
