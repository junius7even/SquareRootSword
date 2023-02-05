using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 startPosition;
    public int cardValue;
    public CandiScript candiZone;


    // // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.position = GetMouseTransformPosition() + offset;
        }
    }

    // try to use the card with an overlapping hero (return false if unable to use)
    private bool TryEnterUpgradeZone()
    {
        if (candiZone != null && candiZone.isOccupiedBy == this) {
            transform.position = candiZone.transform.position;
            return true;
        }
        return false;
    }

    private void OnMouseDown()
    {
        offset = transform.position - GetMouseTransformPosition();
        dragging = true;
    }
    private void OnMouseUp()
    {
        dragging = false;
        if (TryEnterUpgradeZone()) {
            Debug.Log("SNAP TO GRID");
        } else {
            transform.position = startPosition; // if fail to use card, move it back to start position
        }
        
    }

    private Vector3 GetMouseTransformPosition()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }

    public void EnterCandidateNotify(GameObject colliderObject)
    {
        CandiScript candidate = colliderObject.GetComponent<CandiScript>();
        if (candidate == null) return;

        if (candidate.isOccupiedBy == null) {
            candidate.isOccupiedBy = this; 
        }

        Debug.Log("isoccupied." + candidate.isOccupiedBy);
    }

    public void ExitCandidateNotify(GameObject colliderObject)
    {
        CandiScript candidate = colliderObject.GetComponent<CandiScript>();
        if (candidate == null) return;

        if (candidate.isOccupiedBy == this) {
            candidate.isOccupiedBy = null;
        }

        Debug.Log("isoccupied." + candidate.isOccupiedBy);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
    //     if (collider.is(Candidate)) 
    //     Candidate hero = colliderObject.GetComponent<Candidate>();
    // //     if (hero == null) return;
        Debug.Log("Card selected successful." + collider.gameObject.name);
        EnterCandidateNotify(collider.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Card discard TriggerExit2D: " + collider.gameObject.name);
        ExitCandidateNotify(collider.gameObject);
    }
}