using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownUI : MonoBehaviour
{
  [SerializeField]
  private TMP_Text _countdownText;

  public void StartCoundown(int seconds) => 
    StartCoroutine(CountDownStartCounter(seconds));

  private IEnumerator CountDownStartCounter(int timeToCount)
  {
    _countdownText.gameObject.SetActive(true);
    for (int i = timeToCount; i > 0; i--)
    {
      _countdownText.text = i.ToString();
      yield return new WaitForSecondsRealtime(1f);      
    }
    _countdownText.gameObject.SetActive(false);
  }
}
