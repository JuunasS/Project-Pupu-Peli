using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class speechBubble : MonoBehaviour
{
    SpriteRenderer sr;
    TextMeshPro text;

    public string str;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();

        //Setup("Hello world!!!!!!");
    }

    private void Update()
    {
        Setup(str);
    }

    void Setup(string _text)
    {
        text.SetText( _text );
        text.ForceMeshUpdate();
        Vector2 textSize = text.GetRenderedValues(false);

        Vector2 padding = new Vector2(2f, 2f);
        sr.size = textSize + padding;
    }
}
