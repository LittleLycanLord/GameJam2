using System.Collections;
using System.Collections.Generic;
using LilLycanLord_Official;
using Unity.VisualScripting;
using UnityEngine;

public class MotionSensorController : MonoBehaviour
{
    [SerializeField]
    private float offThreshold = 3.0f;

    [SerializeField]
    private float offTicks;

    [SerializeField]
    private float onThreshold = 1.5f;

    [SerializeField]
    private float onTicks;

    [SerializeField]
    private bool lightOff;

    [SerializeField]
    private float mercyThreshold = 2.75f;

    [SerializeField]
    private float mercyTicks;

    private bool foundPlayer;
    private bool caughtPlayer;

    public Color detected;
    public Color danger;

    [SerializeField]
    private Light motionSensorLight;

    private void Awake()
    {
        offThreshold = +Random.Range(1.1f, 8.0f);
        onThreshold = +Random.Range(1.1f, 8.0f);
        this.offTicks = 0.0f;
        this.onTicks = 0.0f;

        this.mercyTicks = 0.0f;
        this.foundPlayer = false;
        this.caughtPlayer = false;

        this.lightOff = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player") && !this.lightOff)
        {
            SimpleAudioManager.Instance.Play("Motion Sensor - Warning");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collider detected: " + other.gameObject.name);
        if (other.gameObject.name.Contains("Player") && !this.lightOff)
        {
            //Debug.Log("entedanger motion sensor trigger IF CONDITION!");

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
                //Debug.Log("entedanger updatelight collor detected if!");
                this.motionSensorLight.color = detected;
            }
            else
            {
                this.motionSensorLight.color = danger;
                SimpleAudioManager.Instance.Play("Motion Sensor - Detected");
                SceneTransitionManager.Instance.targetSceneName = "Bad Ending";
                SceneTransitionManager.Instance.selectedTransitionName = "Crossfade";
                SceneTransitionManager.Instance.LoadSceneWithTransition();
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
                this.motionSensorLight.intensity = 2.0f;
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
