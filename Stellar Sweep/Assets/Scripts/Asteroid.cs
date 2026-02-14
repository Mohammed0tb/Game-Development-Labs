using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float speed;
    private Vector3 moveDirection;
    private float arenaBound = 28f; 
    public void Initialize(Vector3 direction, float asteroidSpeed)
    {
        moveDirection = direction.normalized;
        speed = asteroidSpeed;
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        if (Mathf.Abs(transform.position.x) > arenaBound ||
            Mathf.Abs(transform.position.z) > arenaBound)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("GAME OVER! An asteroid hit the player!");

            Destroy(other.gameObject);

            SpawnManager spawner = FindObjectOfType<SpawnManager>();
            if (spawner != null)
            {
                spawner.GameOver();
            }

            Destroy(gameObject);
        }
    }
}