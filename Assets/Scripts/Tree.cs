using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 _screenPosition;
    private Vector3 _worldPosition;

    [SerializeField] AudioSource treeSFX;

    [SerializeField] GameObject fruit;
    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            _screenPosition = Input.GetTouch(0).position;
        }
        else { return; }
        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
        if (hit.collider !=null) { 
        if (hit.collider.gameObject == this.gameObject)
        {
            Debug.Log("Veo Veo. Pino");
            treeSFX.Play();
            fruit.gameObject.SetActive(true);
        }
        }

    }
}
