using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringUpKeyboard : MonoBehaviour
{
    Vector2 _screenPosition;
    private Vector3 _worldPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("LLegue aca. Touch count:"+Input.touchCount);
            _screenPosition = Input.GetTouch(0).position;
            Debug.Log("Touch placement" + _screenPosition);
        }
        else { return; }
        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log("LLegue aca");
            if (hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("Y yo aca");
                TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            }
        }

    }


    private void hideInputField()
    {
        
    }
}
