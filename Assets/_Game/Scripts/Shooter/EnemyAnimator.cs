using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        if(agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("Ruun", true);
        }
        else
        {
            anim.SetBool("Ruun", false);
        }
    }

    public void SetShoot(bool shoot)
    {
        anim.SetBool("Shooting", shoot);
    }
}
