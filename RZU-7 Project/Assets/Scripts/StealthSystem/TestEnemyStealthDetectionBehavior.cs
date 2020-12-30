using UnityEngine;

/// <summary>
/// This is a reference script for enemy functionality.
/// </summary>
public class TestEnemyStealthDetectionBehavior : MonoBehaviour
{ 
    bool spotted;
    [SerializeField]
    float rotationSpeed = 15;
    public GameObject target;
    EnemyVisualCone evc;

    private void Start()
    {
        evc = GetComponent<EnemyVisualCone>();
    }
    private void FixedUpdate()
    {
        if(evc.target != null)
        {
            target = evc.target;
            spotted = true;
        }
        else
        {
            target = null;
            spotted = false;
        }
        if (spotted)
        {
            if (target)
            {
                var dir = target.transform.position - transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * (rotationSpeed/2));
            }           
        }
        else
        {
             transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
        }
    }
}
