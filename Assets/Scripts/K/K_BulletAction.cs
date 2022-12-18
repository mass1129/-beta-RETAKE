using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_BulletAction : MonoBehaviour
{
    // Start is called before the first frame update

    //Assignable
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemy;

    //Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;

    //LifeTime
    public int maxCollisions;
    public float maxLifeTime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;


    
    
    
    void Start()
    {
        //Setup();
    }

    // Update is called once per frame
    void Update()
    {
        //터졌을때
        if(collisions>maxCollisions) Explode();

        //시간차 터짐
        maxLifeTime -= Time.deltaTime;
        if(maxLifeTime <= 0) Explode();
    }

    
    void Explode()
    {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        //Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemy);

        //for(int i = 0; i < enemies.Length; i++)
        //{
        //    enemies[i].GetComponent<ShootingAi>().ta
        //}
        Invoke("Delay", 0.05f);
    }

    void Delay()
    {
        Destroy(gameObject);
    }
    private void Setup()
    {   
        //새로운 재료를 만든다.
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //재료를 콜라이더에 부여한다
        GetComponent<SphereCollider>().material = physics_mat;

        //중력 설정
        rb.useGravity = useGravity; 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet")) return;

        collisions++;

        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode();
    }

   
}
