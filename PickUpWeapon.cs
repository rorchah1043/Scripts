using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpWeapon : MonoBehaviour
{
    private Transform weaponPosition;
    [SerializeField] private Canvas canvas;
    // Start is called before the first frame update
    private void Start()
    {        
        weaponPosition = GetComponent<Transform>();
        if(canvas == null)
        {
            Debug.Log("Назначьте CANVAS!!!");
        }
        canvas.enabled = !canvas.enabled;
    }

    private void OnTriggerStay(Collider other)
    {
        canvas.enabled = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.CompareTag("Weapon"))
            {
                other.enabled = false;
                other.transform.SetParent(weaponPosition);
                other.transform.position =  weaponPosition.position;
                if (other.transform.rotation.eulerAngles.y != 0 || weaponPosition.GetComponentInParent<Transform>().rotation.eulerAngles.y != 0)
                {
                    weaponPosition.transform.Rotate(0, 180, 0);
                }
            }
            canvas.enabled = false;
        }
    }
}
