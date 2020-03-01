using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTEST : MonoBehaviour
{
    HRCollector dummy;
    public GameObject enemy;
    ViridaxGameStudios.AI.BasicAIController enemy_control;
    public int lowBPM = 65;
    public int highBPM = 120;
    private float detectRad;
    public float radIncrease = 15f;
    public int speedIncrease = 2;
    private float startSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        dummy = new HRCollector();
        dummy.Connect();
        dummy.StartCollecting();
        enemy_control = enemy.GetComponent<ViridaxGameStudios.AI.BasicAIController>();
        detectRad = enemy_control.m_DetectionRadius;
        //startSpeed = enemy_control.MovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(dummy.CurrentBPM());
        float scaled = ScaleBPM();
        Debug.Log("scaled BPM: " + scaled);
        Debug.Log("Current move speed: " + enemy_control.MovementSpeed);
        enemy_control.m_DetectionRadius = detectRad + (radIncrease * scaled);
        enemy_control.MovementSpeed = startSpeed + (speedIncrease * scaled);
        Debug.Log(startSpeed);
        Debug.Log("Detection radius" + enemy_control.m_DetectionRadius);
    }

    private float ScaleBPM()
    {
        float result = 0f;
        float currBPM = dummy.CurrentBPM();
        if(currBPM < lowBPM)
        {
            currBPM = lowBPM;
        }
        else if(currBPM > highBPM)
        {
            currBPM = highBPM;
        }
        result = (currBPM - lowBPM) / (highBPM - lowBPM);
        return result;
    }
}
