using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        CardColor FakeColorToChange = CardColor.Blue;

        if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        {
            Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            if (clickedButton.name == "RED")
                FakeColorToChange = CardColor.Red;
            else if (clickedButton.name == "GREEN")
                FakeColorToChange = CardColor.Green;
            else if (clickedButton.name == "BLUE")
                FakeColorToChange = CardColor.Blue;
            else if(clickedButton.name == "YELLOW")
                FakeColorToChange = CardColor.Yellow;

            print(FakeColorToChange.ToString());
        }
    }

    public void Init()
    {
        StartCoroutine(ColorChangeCortine());
    }

    IEnumerator ColorChangeCortine()
    {
        yield return new WaitForEndOfFrame();
    }
}