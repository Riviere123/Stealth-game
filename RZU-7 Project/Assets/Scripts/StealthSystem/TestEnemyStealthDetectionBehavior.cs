using UnityEngine;


public class TestEnemyStealthDetectionBehavior : MonoBehaviour
{
    public bool spotted;
    public float rotationSpeed;
    public GameObject target;
    EnemyVisualCone evc;

    bool turning;

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
        if (!spotted)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
        }
        if (spotted)
        {
            if (target)
            {
                
                


                var dir = target.transform.position - transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                /*                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * (rotationSpeed/2));
            }           
        }
    }


}
