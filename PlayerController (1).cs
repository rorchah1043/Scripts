using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Animator triggerAnimator;
    private AnimatorControllerParameter objectAnimatorTrigger;
    [SerializeField] private int triggerParameterIndex;
    [SerializeField] private string button;

    private void Start()
    {
        canvas.enabled = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerDirection = new Vector3(hor, 0, ver) * 2 * Time.deltaTime;
        transform.Translate(playerDirection, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InteractiveController>())
        {
            canvas.enabled = true;  
        }

        if (triggerAnimator == null)
        {
            triggerAnimator = other.GetComponent<Animator>();
            objectAnimatorTrigger = triggerAnimator.GetParameter(0);
        }
        DoTriggeredAnimation();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(button))
        {
            triggerAnimator.SetTrigger(objectAnimatorTrigger.name);
            Debug.Log("Анимация сработала");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InteractiveController>())
        {
            canvas.enabled = false;
        }
    }

    public void DoTriggeredAnimation()
    {
        Debug.Log("Объект-триггер что-то делает");
    }
}
