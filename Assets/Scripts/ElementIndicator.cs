using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementIndicator : MonoBehaviour
{
    public enum Element
    {
        Water,
        Earth,
        Fire,
        Air
    }
    public Element currentElement;
    public Image fireImage;
    public Image waterImage;
    public Image airImage;
    public Image earthImage;

    Image previousImage;
    Image highlightedImage;
    Color pastColor;
    Color currColor;

    // Start is called before the first frame update
    void Start()
    {
        //currentElement = ThrowProjectile.currentElement;
        previousImage = airImage;
        SetElementTransparency(fireImage);
        SetElementTransparency(waterImage);
        SetElementTransparency(earthImage);
        SetElementTransparency(airImage);
    }

    // Update is called once per frame
    void Update()
    {
        currentElement = (Element)ThrowProjectile.currentElement;
        switch (currentElement)
        {
            case Element.Water:
                highlightedImage = waterImage;
                break;
            case Element.Air:
                highlightedImage = airImage;
                break;
            case Element.Fire:
                highlightedImage = fireImage;
                break;
            case Element.Earth:
                highlightedImage = earthImage;
                break;
        }
        pastColor = previousImage.color;
        pastColor.a = .2f;
        previousImage.color = pastColor;

        currColor = highlightedImage.color;
        currColor.a = 1f;
        highlightedImage.color = currColor;

        previousImage = highlightedImage;

    }
    private void SetElementTransparency(Image image)
    {
        Color tempColor = image.color;
        tempColor.a = .2f;
        image.color = tempColor;
    }
}
