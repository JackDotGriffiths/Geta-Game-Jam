using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    [SerializeField]
    private float m_destroyTimer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", m_destroyTimer);
    }
    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
