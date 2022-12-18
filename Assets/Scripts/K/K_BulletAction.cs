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
        //��������
        if(collisions>maxCollisions) Explode();

        //�ð��� ����
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
        //���ο� ��Ḧ �����.
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //��Ḧ �ݶ��̴��� �ο��Ѵ�
        GetComponent<SphereCollider>().material = physics_mat;

        //�߷� ����
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
