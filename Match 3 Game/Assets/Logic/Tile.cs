using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int value = -1;
    public List<Sprite> sprites;

    private UnityEngine.UI.Image image;

    private Main main;

    private void Start()
    {
        value = Random.Range(0, 4);

        image = GetComponent<UnityEngine.UI.Image>();
        image.sprite = sprites[value];

        main = GameObject.Find("MainContainer").GetComponent<Main>();
    }

    void Update()
    {
        //Move to parent Location
        if ((transform.position - transform.parent.position).magnitude > 0.1)
        {
            transform.position = Vector3.Lerp(transform.position, transform.parent.position, 0.01f);
        }
    }

    public void ButtonInteraction()
    {
        main.TileToSwap(transform);
    }
}
