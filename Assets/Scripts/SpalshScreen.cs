
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class SpalshScreen : MonoBehaviour
{
    [SerializeField] Image logoImage;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] AudioSource keyPress;
    void Start()
    {
       _ = ShowText("Team Yudiz");
        logoImage.DOColor(Color.white, 1);
    }


    async Task ShowText(string showString)
    {
        await Task.Delay(1500);
        for (int i=1; i<=showString.Length; i++)
        {
            text.SetText(showString.Substring(0, i));
            keyPress.Play();
            await Task.Delay(150);
        }
        await Task.Delay(500);
        logoImage.DOColor(Color.black, 1);
        text.DOColor(Color.black, 1);
        await Task.Delay(1000);
        gameObject.SetActive(false);
    }
}
