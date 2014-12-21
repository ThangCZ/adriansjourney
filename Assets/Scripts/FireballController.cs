using UnityEngine;
using System.Collections;

public class FireballController : MonoBehaviour {

    public int damage;
    public int speed;

    private Vector3 _direction;
    public Vector3 Direction {
        set {
            _direction = value.normalized;
        }
    }

    void Update() {
        transform.Translate(_direction.x * speed * Time.deltaTime,
            _direction.y * speed * Time.deltaTime,
            _direction.z * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        //avoid any interaction with fireball and Adrian
        if (other.gameObject.tag != "Player")
        {
            //mark pillars as red and subsequently remove them
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("enemy collision");
                EnemyStatsHolder enemyStatsHolder = other.gameObject.GetComponent<EnemyStatsHolder>();
                enemyStatsHolder.modifyHealth(-damage);
                Destroy(gameObject);
            }
        }
    }
}
