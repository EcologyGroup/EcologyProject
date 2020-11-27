using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    [SerializeField] private Sprite[] interactiveIcons;
    [SerializeField] private float visibleTime = 40f;
    [SerializeField] AudioSource btnclick;
    private Setup setup;
    private int index;
    private IEnumerator currentCoroutine;
    private IEnumerator masterCoroutine;

    void Start()
    {
        //btnclick = GetComponent<AudioSource>();
        setup = FindObjectOfType<Setup>();
        masterCoroutine = initiate();
        StartCoroutine(masterCoroutine);
    }
    private IEnumerator fade(char c, float fadeTime)
    {
        SpriteRenderer icon = gameObject.GetComponent<SpriteRenderer>();
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, (c == '+') ? 0 : 1);
        while (icon.color.a <= 1 && icon.color.a >= 0) 
        {
            if (c == '+') 
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, icon.color.a + Time.deltaTime / fadeTime);
            else
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, icon.color.a - Time.deltaTime / fadeTime);
            yield return null;
        }
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, (c == '+') ? 1 : 0);
    }
    private IEnumerator toggleIcon()
    {
        index = Random.Range(0, setup.Buildings.Length);
        gameObject.transform.position = new Vector3(setup.Buildings[index].position.x, setup.Buildings[index].position.y, -4f);
        gameObject.GetComponent<SpriteRenderer>().sprite = interactiveIcons[Random.Range(0, interactiveIcons.Length)];
        yield return StartCoroutine(fade('+', 1f));
        yield return new WaitForSeconds(visibleTime);
        yield return StartCoroutine(fade('-', 1f));
    }
    private IEnumerator initiate()
    {
        while (true)
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            currentCoroutine = toggleIcon();
            yield return StartCoroutine(currentCoroutine);
        }
    }
    public IEnumerator OnClick()
    {
        if (masterCoroutine != null)
            StopCoroutine(masterCoroutine);
        Upgrade.score += 50;
        btnclick.Play();
        yield return StartCoroutine(fade('-', 1f));
        masterCoroutine = initiate();
        StartCoroutine(masterCoroutine);
    }
}
