using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private List<Image> buttons = new List<Image>();
    [SerializeField] private float delayBetweenButtons = 0.1f;

    [SerializeField] private Vector3 punchScale;

    [SerializeField] private Vector3 quitScale;

    [SerializeField] private GameObject particle;

    private Tween aboutTween;
    private Tween quitTween;

    private void Awake()
    {
        InitialAnimation();
    }

    private IEnumerator ShowButton()
    {
        foreach (var item in buttons)
        {
            item.transform.localScale = Vector3.zero;
            item.transform.DOScale(Vector3.one, 0.5f);
            yield return new WaitForSeconds(delayBetweenButtons);
        }
    }

    public void InitialAnimation()
    {
        foreach (var item in buttons)
        {
            item.transform.localScale = Vector3.zero;
        }

        StartCoroutine(ShowButton());
    }

    public void ClickPlay()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        var obj = Instantiate(particle, pos, Quaternion.identity);
        Destroy(obj, 3);
    }

    public void ClickAbout()
    {
        if (aboutTween != null) aboutTween.Kill();

        buttons[1].transform.localScale = Vector3.one;
        aboutTween = buttons[1].transform.DOPunchScale(punchScale, 0.3f);
    }

    public void ClickQuit()
    {
        buttons[2].transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.InBounce).OnComplete(()=>
        {
            buttons[2].transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        });
    }

    public void HoverPlay()
    {
        buttons[0].DOColor(Color.green, 0.2f).SetEase(Ease.OutBack);
    }

    public void ResetPlay()
    {
        buttons[0].DOColor(Color.white, 0.2f).SetEase(Ease.OutBack);
    }

    public void HoverAbout()
    {

    }

    public void HoverQuit()
    {
        if (quitTween != null) quitTween.Kill();

        quitTween = buttons[2].transform.DOScale(quitScale, 0.2f).SetEase(Ease.OutBack);
    }

    public void ResetQuit()
    {
        if (quitTween != null) quitTween.Kill();

        quitTween = buttons[2].transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
    }
}
