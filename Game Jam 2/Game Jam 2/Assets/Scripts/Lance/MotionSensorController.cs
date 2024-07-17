using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MotionSensorController : MonoBehaviour
{
    [SerializeField] private float offThreshold = 3.0f;
    [SerializeField] private float offTicks;

    [SerializeField] private float onThreshold = 1.5f;
    [SerializeField] private float onTicks;

    [SerializeField] private bool lightOff;
    
    [SerializeField] private float mercyThreshold = 2.75f;
    [SerializeField] private float mercyTicks;

    private bool foundPlayer;
    private bool caughtPlayer;

    private Color yellow;
    private Color red;

    [SerializeField] private Light motionSensorLight;

    private void Awake()
    {
        this.offTicks = 0.0f;
        this.onTicks = 0.0f;

        this.mercyTicks = 0.0f;
        this.foundPlayer = false;
        this.caughtPlayer = false; 
   
        this.lightOff = true;

        yellow = new Color(255, 255, 0);
        red = new Color(255, 0, 0);
    }

    public void ResetMotionSensor()
    {
        this.offTicks = 0.0f;
        this.onTicks = 0.0f;

        this.mercyTicks = 0.0f;
        this.foundPlayer = false;
        this.caughtPlayer = false;

        this.lightOff = true;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collider detected: " + other.gameObject.name);
        if (other.gameObject.name.Contains("Player") && !this.lightOff) 
        {
            //Debug.Log("entered motion sensor trigger IF CONDITION!");
            this.foundPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name + " has exited the collider!");
        if (other.gameObject.name.Contains("Player"))
        {
            this.foundPlayer = false;
            this.caughtPlayer = false;
            this.mercyTicks = 0;
            this.motionSensorLight.color = Color.white;
        }
    }

    private void updateLightColor()
    {
        if (!foundPlayer)
        {
            this.motionSensorLight.color = Color.white;
        }
        else
        {
            if (!caughtPlayer)
            {
                //Debug.Log("entered updatelight collor yellow if!");
                this.motionSensorLight.color = yellow;
            }
            else
            {
                this.motionSensorLight.color = red;
                Debug.Log("player caught!");
            }
        }
    }

    private void offTicksCountDown()
    {
        if (!this.foundPlayer && this.lightOff)
        {
            this.offTicks += Time.deltaTime;
            if (this.offTicks >= this.offThreshold)
            {
                this.motionSensorLight.intensity = 20.0f;
                this.offTicks = 0.0f;
                this.lightOff = false;
            }

        }
    }

    private void onTicksCountDown()
    {
        if (!this.foundPlayer && !this.lightOff)
        {
            this.onTicks += Time.deltaTime;
            if (this.onTicks >= this.onThreshold)
            {
                this.motionSensorLight.intensity = 0.0f;
                this.onTicks = 0.0f;
                this.lightOff = true;
               
            }

        }
    }

    private void mercyTickDown()
    {
        if (this.foundPlayer && !this.caughtPlayer)
        {
            this.mercyTicks += Time.deltaTime;
            if (mercyTicks >= this.mercyThreshold)
            {
                this.caughtPlayer = true;
            }
        }
    }

    private void Update()
    {
        this.updateLightColor();
        this.offTicksCountDown();
        this.onTicksCountDown();
        this.mercyTickDown();
    }
}
