using UnityEngine;

[CreateAssetMenu(fileName = "Ship Template", menuName = "Ship Template", order = 0)]

public class ShipTemplate : ScriptableObject
{
    [Header("Values")]
    public byte life;
}
