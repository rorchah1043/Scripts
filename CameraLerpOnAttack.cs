using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpOnAttack : MonoBehaviour
{
    private float elapsed = 0;
    private int newValue = 0;
    private int oldValue = 0;
    [SerializeField] private float time = 2;
    private bool IsReady = true;
    [SerializeField] bool RandomValuesLerp;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)&&IsReady)
        {
            Lerp();
        }
        else if(Input.GetKeyDown(KeyCode.K)&&!IsReady)
        {
            Debug.Log("WAIT FOR COOLDOWN");
        }
    }

    public void Lerp()
    {
        if (RandomValuesLerp)
        {
            StartCoroutine(RandomlyLerpCamera());
        }
        else
        {
            StartCoroutine(ConsistentlyLerpCamera());
        }
    }

    private IEnumerator RandomlyLerpCamera()
    {
        Quaternion originalPosition = transform.rotation;
        Quaternion rollToTheLeft = Quaternion.Euler(-5, 0, 0);
        Quaternion rollToTheRight = Quaternion.Euler(5, 0, 0);
        Quaternion pitchForward = Quaternion.Euler(0, 0, -5);

        List<Quaternion> quaternions = new List<Quaternion> {rollToTheLeft, rollToTheRight, pitchForward};
        if( newValue == oldValue)
        {
            while(newValue == oldValue)
            {
               newValue= Random.Range(0,3);
            }
        }
        elapsed = 0;
        IsReady = false;
            while (elapsed < time)
            {
                transform.rotation = Quaternion.Lerp(originalPosition, quaternions[newValue], elapsed / time);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.rotation = quaternions[newValue];
        IsReady = true;
        oldValue = newValue;
    }

    private IEnumerator ConsistentlyLerpCamera()
    {
        Quaternion originalPosition = transform.rotation;
        Quaternion rollToTheLeft = Quaternion.Euler(-5, 0, 0);
        Quaternion rollToTheRight = Quaternion.Euler(5, 0, 0);
        Quaternion pitchForward = Quaternion.Euler(0, 0, -5);

        List<Quaternion> quaternions = new List<Quaternion> { rollToTheLeft, rollToTheRight, pitchForward };
       
        elapsed = 0;
        IsReady = false;
        while (elapsed < time)
        {
            transform.rotation = Quaternion.Lerp(originalPosition, quaternions[newValue], elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = quaternions[newValue];
        IsReady = true;
        newValue++;
        if (newValue == 3)
        {
            newValue = 0;
        }
    }

}
