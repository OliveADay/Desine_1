using UnityEngine;
using UnityEditor;

public class Enemy : Entity
{
    public Patrol enemyMovement;
    float startspeed;
    public float dazedSpeed;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        startspeed = enemyMovement.speed;
    }
    // Update is called once per frame
    private void Update()
    {
        Dazed();
    }
    public override void Dazed()
    {
        base.Dazed();
        if (isDead == false)
        {
            if (dazedTime <= 0)
            {
                if (enemyMovement.isFlipped)
                {
                    enemyMovement.speed = -startspeed;
                }
                else
                {
                    enemyMovement.speed = startspeed;
                }
            }
            else
            {
                if (hitter.transform.position.x - transform.position.x >= 0)
                {
                    enemyMovement.speed = dazedSpeed;
                }
                else
                {
                    enemyMovement.speed = -dazedSpeed;
                }
                dazedTime -= Time.deltaTime;
            }
        }
    }
}
