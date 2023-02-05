using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandiScript : MonoBehaviour
{
    public CardScript isOccupiedBy;

    // Start is called before the first frame update
    void Start()
    {
        isOccupiedBy = null;
    }
}
