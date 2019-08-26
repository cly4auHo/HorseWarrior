using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text balanceText;
    [SerializeField] private Text counter;
    [SerializeField] private float currentModifier = 10;
    [SerializeField] private Horse horse;
    [SerializeField] private Vector3 horseLablePosition; //calclate based on horsee posiiton?
    [SerializeField] private int modifierUpdatePrice = 100;
    [SerializeField] private int minionBananasAmount = 10;

    [SerializeField] private Minion minionPrefab;
    [SerializeField] private List<RectTransform> minionsSpawnPoints;
    [SerializeField] private float mioinSpawnSize = 150;
    [SerializeField] private Text incomeLabel;
    [SerializeField] private RectTransform labelParent;

    private float currentBalance = 0;
    private float lastCheckTime = 0;
    private float lastCheckBalance = 0;

    private const float ModifierMultyplayer = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        horse.OnClick += OnClick;
        horseLablePosition = horse.GetComponent<RectTransform>().position;
    }

    private void OnClick()
    {
        currentBalance += currentModifier;
        OnBalanceChanged(currentModifier, horseLablePosition);
    }

    public void OnModifierUpdateClick()
    {
        if (currentBalance <= modifierUpdatePrice)
            return;

        currentModifier *= ModifierMultyplayer;
        currentBalance -= modifierUpdatePrice;
        balanceText.text = Mathf.RoundToInt(currentBalance).ToString();
    }

    public void OnMinionBuyClick()
    {
        int index = Random.Range(0, minionsSpawnPoints.Count);
        var minion = GameObject.Instantiate(minionPrefab, minionsSpawnPoints[index], false);
        minion.GetComponent<RectTransform>().localPosition = GetminionPosition();

        minion.OnMinion += OnMinion;
        balanceText.text = Mathf.RoundToInt(currentBalance).ToString();
    }

    private void OnMinion(Vector3 labelPosition)
    {
        currentBalance += minionBananasAmount;
        OnBalanceChanged(minionBananasAmount, labelPosition);
    }

    void OnBalanceChanged(float amount, Vector3 labelPosition)
    {

        var text = GameObject.Instantiate(incomeLabel, labelParent, false);
        text.rectTransform.position = labelPosition;
        text.text = "+" + amount + "!";

        balanceText.text = Mathf.RoundToInt(currentBalance).ToString();
    }

    void UpdateBananasPerSecond()
    {
        counter.text = Mathf.Clamp(currentBalance - lastCheckBalance, 0, float.MaxValue).ToString();
        lastCheckBalance = currentBalance;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad - lastCheckTime > 1)
        {
            UpdateBananasPerSecond();
            lastCheckTime = Time.timeSinceLevelLoad;
        }
    }

    Vector2 GetminionPosition()
    {
        float xPos = Random.Range(-mioinSpawnSize, mioinSpawnSize);
        float yPos = Random.Range(-mioinSpawnSize, mioinSpawnSize);

        return new Vector2(xPos, yPos);
    }
}
