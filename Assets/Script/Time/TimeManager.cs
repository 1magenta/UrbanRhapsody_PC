using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("InGame Clock")]
    [SerializeField]
    GameTimeStamp timeStamp;

    [Header("Day and Night Circle")]
    public float timeScale = 1.0f;

    //to simulate the sun 
    public Transform sunTransform;


    //Observer pattern, TimeManager is the subject for the obersvers/listeners
    // list of objects to inform change to the time
    List<ITimeTracker> listeners = new List<ITimeTracker>();


    private void Awake()
    {
        //if there is more than one instance, destory the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // set the static instance to this instance
            Instance = this;
        }
    }

    void Start()
    {
        //initialize time stamp
        timeStamp = new GameTimeStamp(0, GameTimeStamp.Season.Spring, 1, 6, 0);

        //set up the time update machanism
        StartCoroutine(TimeUpdate());
    }

    // In this Game, time scale: 1 sec = 1 min
    
    IEnumerator TimeUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / timeScale);
            Tick();
        }
    }

    /// <summary>
    /// A tick of InGame time
    /// </summary>
    public void Tick()
    {
        timeStamp.UpdataClock();

        //Inform the listeners of the new time state
        foreach(ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timeStamp);
        }

        UpdateSunMovement();

    }

    //------------------------Day and Night cycle------------------------------------

    /// <summary>
    /// Update sun position to simulate the day and night cycle
    /// </summary>
    void UpdateSunMovement()
    {
        //convert curr time to minutes
        int timeInMinutes = GameTimeStamp.HoursToMinutes(timeStamp.hour) + timeStamp.minute;

        //simulate the sun's movement during the day
        //sun moves 15 degrees in a hour
        //0.25 in a min
        // mid night, sun angle == -90
        float sunAngle = 0.25f * timeInMinutes - 90;

        //rotate dir light to simulate the sun
        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }


    //----------------------------handle listensers----------------------------
    //Add objects to the list of listeners
    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }
    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }

    //get timeStamp
    public GameTimeStamp GetGameTimeStamp()
    {
        //return cloned instance
        return new GameTimeStamp(timeStamp);
    }
}
