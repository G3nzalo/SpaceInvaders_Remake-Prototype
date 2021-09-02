using UnityEngine;

[CreateAssetMenu(fileName = "Player Controls" , menuName = "Player Controls", order =0)]

public class PlayerControls : ScriptableObject
{
    [Header("Controles")]
    public string movHorizontal;
    public string fire;
}
