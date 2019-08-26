using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWave : MonoBehaviour
{
    public GameObject BlastWaveObject;
    public GameObject BlastWaveAnim;

    private Vector2 m_blastPos;
    private Quaternion m_blastRot;

    [SerializeField]
    private float m_blastSpeed;
    
    private GameObject targetPortal;
    [SerializeField]
    private GameObject currBlastWave;
    private bool moveBalstWave;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Blast();
        }

        if (moveBalstWave)
        {
            currBlastWave.transform.position = Vector3.MoveTowards(currBlastWave.transform.position, targetPortal.transform.position, m_blastSpeed * Time.deltaTime);
            if(Vector2.Distance(currBlastWave.transform.position, targetPortal.transform.position) < 0.2f)
            {
                Destroy(currBlastWave);
                moveBalstWave = false;
            }
        }
    }

    void Blast()
    {
        currBlastWave = Instantiate(BlastWaveObject, transform.position, transform.rotation);
        m_blastPos = transform.position;
        m_blastRot = transform.rotation;
        InvokeRepeating("BlastEffect", 0f, 0.2f);
        Invoke("StopInvokes", 0.6f);
        moveBalstWave = true;

        Debug.DrawRay(transform.position + transform.up, transform.up, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.up, Mathf.Infinity);

        if (hit.collider != null && hit.collider.gameObject.tag == "Portal")
        {
            Debug.Log(hit.collider.gameObject.name);
            targetPortal = hit.collider.gameObject;
        }
    }

    void BlastEffect()
    {
        Instantiate(BlastWaveAnim, m_blastPos, m_blastRot);
    }

    void StopInvokes()
    {
        CancelInvoke();
    }
}
