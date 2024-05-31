using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeDirt : MonoBehaviour
{
    public CropPlant plant;
    public bool CanPlant()
    {
        if (plant == null)
            return true;
        else
            return false;
    }
}
