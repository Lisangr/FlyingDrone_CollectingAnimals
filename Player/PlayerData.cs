using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerData : MonoBehaviour
{
    private const string CurrentExperienceKey = "CurrentPlayerExperience";
    private const string CurrentGoldKey = "CurrentPlayerGold";
    private const string AllExperienceKey = "PlayerExperience";
    private const string AllGoldKey = "PlayerGold";
    private int AdID = 2; //����� �������
    public delegate void OnRewarded();
    public static event OnRewarded OnRewardedDone;
    private void OnEnable()
    {
        CollectableItems.OnCollectedGold += CollectableItems_OnCollectedGold;
        CollectableItems.OnCollectedExp += CollectableItems_OnCollectedExp;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void CollectableItems_OnCollectedExp(int t)
    {
        int exp = PlayerPrefs.GetInt(CurrentExperienceKey, 0);
        exp += t;
        PlayerPrefs.SetInt(CurrentExperienceKey, exp);
        PlayerPrefs.Save(); // ��������� ���������

        Debug.Log("�������� ����� � ��� " +  exp + " ��������� " + t);
    }

    private void CollectableItems_OnCollectedGold(int t)
    {
        int gold = PlayerPrefs.GetInt(CurrentGoldKey, 0);
        gold += t;
        PlayerPrefs.SetInt(CurrentGoldKey, gold);
        PlayerPrefs.Save(); // ��������� ���������

        Debug.Log("�������� ������ � ��� " + gold + " ��������� " + t);
    }

    private void OnDisable()
    {
        CollectableItems.OnCollectedGold -= CollectableItems_OnCollectedGold;
        CollectableItems.OnCollectedExp -= CollectableItems_OnCollectedExp;
    }
    public void Rewarded(int id)
    {
        if (id == AdID)
            DoubleExpAndGold();

        int currentGold = PlayerPrefs.GetInt(CurrentGoldKey, 0);
        int currentExp = PlayerPrefs.GetInt(CurrentExperienceKey, 0);
        int allGold = PlayerPrefs.GetInt(AllGoldKey, 0);
        int allExp= PlayerPrefs.GetInt(AllExperienceKey, 0);

        int newGold = currentGold + allGold;
        int newExp = currentExp + allExp;
        Debug.Log("������� ���������� ����� " + newExp + " ������� ���������� ������ " + newGold);

        PlayerPrefs.SetInt(AllGoldKey, newGold);
        PlayerPrefs.SetInt(AllExperienceKey, newExp);
        OnRewardedDone?.Invoke();
        PlayerPrefs.DeleteKey(CurrentGoldKey);
        PlayerPrefs.DeleteKey(CurrentExperienceKey);
        PlayerPrefs.Save(); // ��������� ���������
        Debug.Log("� �������� �������� ������ " + PlayerPrefs.GetInt(AllGoldKey, 0) + " ����� " + PlayerPrefs.GetInt(AllExperienceKey, 0));

        //gameObject.SetActive(false);
        YandexGame.RewardVideoEvent -= Rewarded;      
    }
    
    private void DoubleExpAndGold()
    {
        int gold = PlayerPrefs.GetInt(CurrentGoldKey, 0);
        int exp = PlayerPrefs.GetInt(CurrentExperienceKey, 0);

        gold *= 2;
        exp *= 2;

        PlayerPrefs.SetInt(CurrentGoldKey, gold);
        PlayerPrefs.SetInt(CurrentExperienceKey, exp);
        PlayerPrefs.Save(); // ��������� ���������

        Debug.Log("� �������� ���������� ������ � ����");
    }
}
