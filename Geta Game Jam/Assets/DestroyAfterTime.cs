using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    [SerializeField]
    private float m_destroyTimer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", 1f);
    }
    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
