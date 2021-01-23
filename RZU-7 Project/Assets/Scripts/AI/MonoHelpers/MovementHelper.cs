using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class MovementHelper : StateMonoHelper
{
    [HideInInspector]
    public Grid grid;

    [HideInInspector]
    public PathFinding pathFinding;
    [HideInInspector]
    public bool patrolLoop;
    public Vector2[] patrolPoints;

    [HideInInspector]
    public Vector2 currentPatrolPoint;
    [HideInInspector]
    public AIConstants.PatrolCycleDirection patrolDirection;
    [HideInInspector]
    public List<Node> path;

    GameObject player;
    SpriteRenderer sr;

    [SerializeField]
    Color wayPointGizmoColor;
    [SerializeField]
    Color pathNodeColor;

    EnemyVisualCone vision;
    bool attacking;

    public bool GetPatrolLoop()
    {
        return patrolLoop;
    }

    public void GetPatrolLoop(bool state)
    {
        patrolLoop = state;
    }

    private void Start()
    {
        if (patrolPoints.Length > 0)
        {
            currentPatrolPoint = patrolPoints[0];
        }
        vision = controller.references.Get<EnemyVisualCone>(EnemyReferencesConstants.visualCone);

        grid = GameObject.FindGameObjectWithTag(Constants.gameMaster).GetComponent<Grid>();
        pathFinding = grid.pathFinding;
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        SnapConeDirection();

        if (CheckVision())
        {
            sr.color = new Color(1, 1, 1, 0);
        }
        else
        {
            sr.color = new Color(1, 1, 1, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Gizmos.color = wayPointGizmoColor;
            Gizmos.DrawWireSphere(patrolPoints[i], .25f);
        }
        for (int i = 0; i < path.Count; i++)
        {
            Gizmos.color = pathNodeColor;
            Gizmos.DrawWireSphere(path[i].position, .33f);
        }
    }

    void SnapConeDirection()
    {
        Animator animator = controller.references.Get<Animator>(EnemyReferencesConstants.animator);
        GameObject visualCone = controller.references.Get<GameObject>(EnemyReferencesConstants.visualConeGameObject);

        if (!vision.target)
        {
            float x = animator.GetFloat(AnimationConstants.lastX);
            float y = animator.GetFloat(AnimationConstants.lastY);
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            visualCone.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Vector3 dir = vision.target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            visualCone.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    public void Attack(EnemyAnimations animations)
    { 
        if(!attacking)
        {
            StartCoroutine(EndAttack(animations));
        }
    }
    IEnumerator EndAttack(EnemyAnimations animations)
    {
        animations.SetImobileBools(AnimationConstants.ImobileStates.ATTACK);
        attacking = true;
        yield return new WaitForSeconds(.1f);
        GetComponentInChildren<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(.25f);
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        attacking = false;
    }

    bool CheckVision()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position,Vector2.Distance(player.transform.position,transform.position));
        
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Wall")
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                return true;
            }
        }
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
        return false;
    }
}
