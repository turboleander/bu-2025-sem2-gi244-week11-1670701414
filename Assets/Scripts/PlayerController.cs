using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform focalPoint;
    public GameObject powerUpIndicator;

    public bool hasPowerUp;
    public bool hasStunPowerUp;

    private Rigidbody rb;

    private InputAction moveAction;
    private InputAction smashAction;
    private InputAction breakAction;

    private Coroutine powerUpRoutine;
    private Coroutine stunRoutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        smashAction = InputSystem.actions.FindAction("Smash");
        breakAction = InputSystem.actions.FindAction("Break");
    }

    // Update is called once per frame
    void Update()
    {
        var move = moveAction.ReadValue<Vector2>();
        rb.AddForce(move.y * speed * focalPoint.forward);
        if (breakAction.IsPressed())
        {
            rb.linearVelocity = Vector3.zero;
        }
        //set powerUpIndicator
        if (hasPowerUp)
        {
            powerUpIndicator.SetActive(true);
        }
        else
        {
            powerUpIndicator.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerUp)
            {
                var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                //var v = enemyRb.linearVelocity;
                //v.Normalize();
                var dir = enemyRb.transform.position - transform.position;
                dir.Normalize();
                enemyRb.AddForce(dir * 10, ForceMode.Impulse);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            if (powerUpRoutine != null)
            {
                StopCoroutine(powerUpRoutine);
            }
            powerUpRoutine = StartCoroutine(PowerUpCooldown());
        }
        if (other.CompareTag("StunPowerUp"))
        {
            hasStunPowerUp = true;
            Destroy(other.gameObject);
            if (stunRoutine != null)
            {
                StopCoroutine(stunRoutine);
            }
            stunRoutine = StartCoroutine(StunPowerUpCoolDown());
        }
    }

    IEnumerator PowerUpCooldown()
    {
        yield return new WaitForSeconds(10f);
        hasPowerUp = false;
    }

    IEnumerator StunPowerUpCoolDown()
    {
        yield return new WaitForSeconds(5f);
        hasStunPowerUp = false;
    }
}
