using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedKill : MonoBehaviour
{
    public bool neddToKill = false;
    public bool playerInside = false;
    public float timeBeforeStart = 0;
    public float phase1ContinueFor = 1;
    public float phase2ContinueFor = 1.5f;
    public float phase3ContinueFor = 2;

    IEnumerator Start()
    {
        if (transform.GetComponent<TrafficLight>() == null)
        {
            transform.gameObject.AddComponent<TrafficLight>();
        }
        if (timeBeforeStart != 0)
        {
            yield return new WaitForSeconds(timeBeforeStart);
        }
        if (
            phase1ContinueFor != 0 &&
            phase2ContinueFor != 0 &&
            phase3ContinueFor != 0
        )
        {
            while (true)
            {
                Phase1();
                yield return new WaitForSeconds(phase2ContinueFor);
                Phase2();
                yield return new WaitForSeconds(phase3ContinueFor);
                Phase3();
                yield return new WaitForSeconds(phase1ContinueFor);
            }
        }

        if (
            phase1ContinueFor != 0 &&
            phase2ContinueFor != 0
        )
        {
            while (true)
            {
                Phase1();
                yield return new WaitForSeconds(phase2ContinueFor);
                Phase2();
                yield return new WaitForSeconds(phase1ContinueFor);
            }
        }

        if (
            phase1ContinueFor != 0
        )
        {
            while (true)
            {
                Phase1();
                yield return new WaitForSeconds(phase1ContinueFor);
            }
        }
        if (
            phase2ContinueFor != 0
        )
        {
            while (true)
            {
                Phase2();
                yield return new WaitForSeconds(phase2ContinueFor);
            }
        }
        if (
            phase3ContinueFor != 0
        )
        {
            while (true)
            {
                Phase3();
                yield return new WaitForSeconds(phase3ContinueFor);
            }
        }
    }

    private void Phase1()
    {
        transform.GetComponent<TrafficLight>().SetColor("g");
        neddToKill = false;
    }

    private void Phase2()
    {
        transform.GetComponent<TrafficLight>().SetColor("y");
        neddToKill = false;
    }

    private void Phase3()
    {
        transform.GetComponent<TrafficLight>().SetColor("r");
        neddToKill = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        playerInside = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        playerInside = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        playerInside = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        playerInside = true;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        playerInside = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        playerInside = false;
    }

    private void Update()
    {
        if (playerInside && neddToKill)
        {
            DamageController.GetDamage(999);
        }
    }
}
