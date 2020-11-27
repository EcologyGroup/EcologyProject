using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//[RequireComponent(typeof(Image))]
public class UIspriteAnimation : MonoBehaviour
{
    public float duration;
    private bool start=false;
    [SerializeField] private Sprite[] sprites;

    private Image image;
    private int index = 0;
    private float timer = 0;

    void Start()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        if (start)
        {
            if ((timer += Time.fixedDeltaTime) >= (duration / sprites.Length))
            {
                timer = 0;
                image.sprite = sprites[index];
                index = (index + 1) % sprites.Length;
            }
           
        }
    }
   
    void OnEnable()
    {
        start = true;
    }
}
