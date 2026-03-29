using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;

    private PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.hasStunPowerUp == true)
        {
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize();
            rb.AddForce(dir * speed);
        }
    }
}