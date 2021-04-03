using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopController : MonoBehaviour
{
    // Start is called before the first frame update

    public float spin;
    public float homingForce;
    
    private float angularDrag = 0.1f;
    private float lowSpinCap = 1f;
    private Vector3 targetPoint = new Vector3(0, 0, 0);
    private Rigidbody2D rb = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = Mathf.Sign(Random.Range(-1f, 1f)) * spin + Random.Range(-100f, 100f);
        rb.angularDrag = angularDrag;
    }

    private void FixedUpdate()
    {
        var scaler = Mathf.Clamp(Mathf.Abs(rb.angularVelocity), 0.01f, lowSpinCap) / lowSpinCap;
        if (scaler >= 1.0f)
        {
            var homingDir = (targetPoint - transform.position) * homingForce;
            rb.AddForce(new Vector2(homingDir.x, homingDir.y) * scaler);
        }
        else
        {
            rb.angularDrag = angularDrag * (1 + (50f * (1-scaler)));
            rb.drag = 10f;
        }
    }
}
