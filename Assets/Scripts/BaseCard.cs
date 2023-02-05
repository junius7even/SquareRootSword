using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 startPosition;
    Dictionary<BaseHero,int> overlappingHeros = new Dictionary<BaseHero, int>();
    private BaseHero closestHero;
    int cardValue;

    // Start is called before the first frame update
    void Start()
    {
        cardValue = Random.Range(1, 10); // currently initialize with random int in [1,9]
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
    private bool TryUseCard()
    {
        if (overlappingHeros.Count == 0) return false; // no hero to use with

        // find closest hero
        float smallest_dist_so_far = float.PositiveInfinity;

        foreach (KeyValuePair<BaseHero, int> kvp in overlappingHeros)
        {
            float cur_dist = Vector3.Distance(kvp.Key.transform.position, this.transform.position);
            if (cur_dist < smallest_dist_so_far)
            {
                closestHero = kvp.Key;
                smallest_dist_so_far = cur_dist;
            }
        }

        // TO DO: use cardValue
        Operator heroType = closestHero.currentOperator;
        if (heroType == Operator.Multiplication)
        {
            Debug.Log("Multiplication " + cardValue);
        }
        else if (heroType == Operator.Division)
        {
            Debug.Log("Division " + cardValue);
        }
        else if (heroType == Operator.Plus)
        {
            Debug.Log("Plus " + cardValue);
        }
        else if (heroType == Operator.Minus)
        {
            Debug.Log("Minus " + cardValue);
        }

        Destroy(gameObject); // destroy used card
        return true;
    }

    private void OnMouseDown()
    {
        offset = transform.position - GetMouseTransformPosition();
        startPosition = transform.position;
        dragging = true;
    }
    private void OnMouseUp()
    {
        dragging = false;
        if (!TryUseCard()) transform.position = startPosition; // if fail to use card, move it back to start position
    }

    private Vector3 GetMouseTransformPosition()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }

    public void EnterHeroNotify(GameObject colliderObject)
    {
        BaseHero hero = colliderObject.GetComponent<BaseHero>();
        if (hero == null) return;
        overlappingHeros.Add(hero, 0);
    }

    public void ExitHeroNotify(GameObject colliderObject)
    {
        BaseHero hero = colliderObject.GetComponent<BaseHero>();
        if (hero == null) return;
        overlappingHeros.Remove(hero);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        EnterHeroNotify(collider.gameObject);

        //Debug.Log("Hero TriggerEnter2D: " + collider.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        ExitHeroNotify(collider.gameObject);

        //Debug.Log("Hero TriggerExit2D: " + collider.gameObject.name);
    }
}
