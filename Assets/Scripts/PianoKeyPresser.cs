using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKeyPresser : MonoBehaviour
{
    private PianoKey previousKey;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            PianoKey key = hit.collider.GetComponent<PianoKey>();
            if (key != null)
            {
                if (previousKey != key)
                {
                    previousKey?.UnHover();
                    key.Hover();
                }
                previousKey = key;
            }
        }
        else if (previousKey != null)
        {
            previousKey.UnHover();
            previousKey = null;
        }

        if (Input.GetMouseButton(0) && previousKey != null)
        {
            previousKey.Press();
        }
    }
}
