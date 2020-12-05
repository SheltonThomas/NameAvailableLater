using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{

    public Animator animator;

    Vector2 playerToMouseDirection; //direction from the player to the mouse
    Vector3 attackDirection;
    public int attackDamage = 20;
    public float attackRange = 0.5f;
    private float timeSinceLastAttack;
    public float attackDelta = 0.6f;
    public float actionDelta = 0.4f;
    Shooting shooting;
    PlayerMovement playerMovement;
    LayerMask enemyLayers;

    float rotation;
    private void Start()
    {
        enemyLayers = LayerMask.GetMask("Enemy");
        shooting = gameObject.GetComponent<Shooting>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && shooting.GetTimeSinceLastShot() > actionDelta && timeSinceLastAttack > attackDelta)
        {
            Attack();
            timeSinceLastAttack = 0.0f;
        }
    }

    void Attack()
    {
        //play sound

        //get direction of the attack
        playerToMouseDirection = playerMovement.GetPlayerToMouseDirection();
        playerToMouseDirection.Normalize();
        rotation = Mathf.Atan2(playerToMouseDirection.y, playerToMouseDirection.x) * Mathf.Rad2Deg;
        float angle = rotation + 180f;
        playerMovement.playerAngle = angle;
        float angleMappedToMatchSineFunction = Mathf.Round(angle / 45) * 0.79f;
        attackDirection.x = Mathf.Round(Mathf.Sin(angleMappedToMatchSineFunction - 1.57f));//Since the dash only works in 8 directions angles 22.5-67.5 need to all give the same output of (-1,-1) as well and similarly for the other angles
        attackDirection.y = Mathf.Round(Mathf.Sin(angleMappedToMatchSineFunction + 3.14f));//Dividing by 45 and rounding makes it so that each set of angles is now a digit any angle 22.5-67.5 would become 1 this groups the angles so that they can easily be used as input points on a sine fucntion to get the necessary outputs (the multipication and addition are there to match the sine function to the points needed) the rounding at the end is so that the output will be either 1 or 0
        attackDirection.Normalize();
        attackDirection *= 0.5f;
        //Play attack animation
        animator.SetTrigger("Attack");


        //Detect enemies in range
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(gameObject.transform.position + attackDirection, attackRange, enemyLayers);

        //Damage enemies
        foreach(Collider2D enemies in enemiesHit)
        {
            //Debug.Log(enemies.name + " hit");
            enemies.gameObject.GetComponent<NonPlayerCharacter>().TakeDamage(attackDamage); //multiply attack damage by any modifiers and pass in other effects to be aplied
        }

    }
    
    void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(gameObject.transform.position + attackDirection, attackRange);
    }

    public float GetTimeSinceLastAttack()
    {
        return timeSinceLastAttack;
    }
    
}
