using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public static VfxManager instance = null;

    [SerializeField]  GameObject prefabParticlesExplosive;

    private void Awake()
    {
        Initialize();
    }

    public void SetParticlesDestroyShip(Vector3 _posShip)
    {
        GameObject explosivePArticles = Instantiate(prefabParticlesExplosive);
        explosivePArticles.transform.position = _posShip;
        Destroy(explosivePArticles, 1.0f);
    }

    private void Initialize()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
