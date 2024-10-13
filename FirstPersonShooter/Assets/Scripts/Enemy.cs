using UnityEngine;
using UnityEngine.AI;

public class Enemy : DamagableObject
{
    public NavMeshAgent Agent { get; set; }


    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
    }

    public override void OnObjectHit(HitData hitData)
    {
        base.OnObjectHit(hitData);


        CurrentHealth -= hitData.damage;
        Debug.Log(CurrentHealth);
        if (CurrentHealth <= 0 && IsDestroyed == false)
        {
            OnObjectDestroy();
            IsDestroyed = true;
            gameObject.SetActive(false);
        }
    }
}
