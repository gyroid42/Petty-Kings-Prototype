using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIconScript : MonoBehaviour {

    public float UIScaleModifier;
    public float BaseScale;

    // If mouse is over UI icon, enlarge
    public void Enlarge()
    {
        transform.localScale = new Vector3(BaseScale*UIScaleModifier, BaseScale*UIScaleModifier, 1.0f);
    }
    
    // Reduce size when cursor leaves
    public void Reduce()
    {
        transform.localScale = new Vector3(BaseScale, BaseScale, 1.0f);
    }
}
