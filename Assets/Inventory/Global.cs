using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {
    public static SlotManager slotManager;

    public static GameObject canvas;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
    }

    private void Start()
    {
        slotManager.CreatSlots();
    }
}
