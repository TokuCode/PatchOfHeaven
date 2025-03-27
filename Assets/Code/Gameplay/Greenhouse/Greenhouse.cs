using System;
using UnityEngine;

public class Greenhouse : MonoBehaviour
{
    [SerializeField] private GameObject[] greenhouse;
    private int index;
    
    [Header("Input")]
    [SerializeField] private float scrollCooldown;
    private float timer;
    
    private void Start()
    {
        index = greenhouse.Length - 1;
    }

    private void Update()
    {
        HandleKeyAxis();
        if(timer > 0) timer -= Time.deltaTime;
        else HandleScroll();
        
        SetGreenhouse();
    }

    private void HandleScroll()
    {
        int dir = (int)Mathf.Sign(Input.mouseScrollDelta.y);
        if (dir == 0) return;

        if (index >= greenhouse.Length - 1 && dir > 0) index = 0;
        else if(index <= 0 && dir < 0) index = greenhouse.Length - 1;
        else index += dir;
        
        timer = scrollCooldown;
    }

    private void HandleKeyAxis()
    {
        int arrowUp = Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0;
        int arrowDown = Input.GetKeyDown(KeyCode.DownArrow) ? -1 : 0;
        int dir = arrowUp + arrowDown; 
        if (dir == 0) return;
        
        if (index >= greenhouse.Length - 1 && dir > 0) index = 0;
        else if(index <= 0 && dir < 0) index = greenhouse.Length - 1;
        else index += dir;
        
        timer = scrollCooldown;
    }

    private void SetGreenhouse()
    {
        for(int i = 0; i < greenhouse.Length; i++)
            greenhouse[i].SetActive(i <= index);
    }
}
