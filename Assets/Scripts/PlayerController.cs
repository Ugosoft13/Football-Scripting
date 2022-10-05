using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    private Rigidbody rbPlayer;
    private GameObject focalPoint;
    public GameObject powerupindicator;
    public bool hasPowerup = false;
    private float powerupStrength = 10;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        rbPlayer.AddForce(focalPoint.transform.forward * forwardInput * playerSpeed);

        powerupindicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupindicator.SetActive(true);

            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }

    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupindicator.SetActive(false);
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength,  ForceMode.Impulse);

            Debug.Log("Yes");
        }


    }
}
