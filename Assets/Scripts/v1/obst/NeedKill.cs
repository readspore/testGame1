using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedKill : MonoBehaviour
{
    public bool neddToKill = false;
    public bool playerInside = false;
    public float timeBeforeStart = 0;
    public float timeTo1Phase = 1;
    public float timeTo2Phase = 1.5f;
    public float timeTo3Phase = 2;
    //Collision collisionPlayer = null;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (transform.GetComponent<TrafficLight>() == null)
        {
            transform.gameObject.AddComponent<TrafficLight>();
        }
        while (true)
        {
            Phase1();
            yield return new WaitForSeconds(timeTo2Phase);
            Phase2();
            yield return new WaitForSeconds(timeTo3Phase);
            Phase3();
            yield return new WaitForSeconds(timeTo1Phase);
        }
        //yield return new WaitForSeconds(timeBeforeStart);  
        //if (
        //    timeTo1Phase != 0 &&
        //    timeTo2Phase != 0 &&
        //    timeTo3Phase != 0
        //)
        //{
        //    while (true)
        //    {
        //        Phase1();
        //        yield return new WaitForSeconds(timeTo2Phase);
        //        Phase2();
        //        yield return new WaitForSeconds(timeTo3Phase);
        //        Phase3();
        //        yield return new WaitForSeconds(timeTo1Phase);
        //    }
        //}

        //if (
        //    timeTo1Phase != 0 &&
        //    timeTo2Phase != 0
        //)
        //{
        //    while (true)
        //    {
        //        Phase1();
        //        yield return new WaitForSeconds(timeTo2Phase);
        //        Phase2();
        //        yield return new WaitForSeconds(timeTo1Phase);
        //    }
        //}

        //if (
        //    timeTo1Phase != 0
        //)
        //{
        //    while (true)
        //    {
        //        Phase1();
        //        yield return new WaitForSeconds(timeTo1Phase);
        //    }
        //}
        //if (
        //    timeTo2Phase != 0
        //)
        //{
        //    while (true)
        //    {
        //        Phase2();
        //        yield return new WaitForSeconds(timeTo2Phase);
        //    }
        //}
        //if (
        //    timeTo3Phase != 0
        //)
        //{
        //    while (true)
        //    {
        //        Phase3();
        //        yield return new WaitForSeconds(timeTo3Phase);
        //    }
        //}
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
        //collisionPlayer = collision;
        //if (neddToKill && playerInside)
        //{
            //collision.gameObject.GetComponent<Player>().PlayerIsDead();
        //}
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        playerInside = true;
        //collisionPlayer = collision;
        //if (neddToKill && playerInside)
        //{
            //collision.gameObject.GetComponent<Player>().PlayerIsDead();
        //}
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
        //collisionPlayer = collision;
        //if (neddToKill && playerInside)
        //{
            //collision.gameObject.GetComponent<Player>().PlayerIsDead();
        //}
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        playerInside = true;
        //collisionPlayer = collision;
        //if (neddToKill && playerInside)
        //{
            //collision.gameObject.GetComponent<Player>().PlayerIsDead();
        //}
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
           //   GameObject.Find("Player").gameObject.GetComponent<Player>().PlayerIsDead();
        }
    }
}
