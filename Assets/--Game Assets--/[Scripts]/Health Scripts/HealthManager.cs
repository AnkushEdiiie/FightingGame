using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public HealthBar leftHealthBar;
    public HealthBar rightHealthBar;
    public TextMeshProUGUI timerText;

    public int maxHealth = 100;
    private int player1Health;
    private int player2Health;
    private float timerAmount;
    private float timerSpeed;


    [Header("GameOver")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text playerInfo;
    [SerializeField] private Button MainMenuBtn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        leftHealthBar = GameObject.FindGameObjectWithTag("LeftHealthBar").GetComponent<HealthBar>();
        rightHealthBar = GameObject.FindGameObjectWithTag("RightHealthBar").GetComponent<HealthBar>();
        MainMenuBtn.onClick.AddListener(() => SceneManager.LoadScene(0));
    }
    private void Start()
    {
        player1Health = maxHealth;
        player2Health = maxHealth;

        timerAmount = 60f;
        timerSpeed = 0.7f;

        leftHealthBar.health = player1Health;
        rightHealthBar.health = player2Health;

        UpdateUI();
    }
    private void Update()
    {
        if (timerAmount > 0)
        {
            timerAmount -= Time.deltaTime * timerSpeed;
            int secondsInt = Mathf.FloorToInt(timerAmount);
            timerText.text = Mathf.Max(secondsInt, 0).ToString();
        }

        leftHealthBar.UpdateSlider();
        rightHealthBar.UpdateSlider();
    }
    public void TakeDamage(int playerNumber, int damageAmount)
    {
        if (playerNumber == 1)
        {
            player1Health -= damageAmount;
            player1Health = Mathf.Clamp(player1Health, 0, maxHealth);
        }
        else if (playerNumber == 2)
        {
            player2Health -= damageAmount;
            player2Health = Mathf.Clamp(player2Health, 0, maxHealth);
        }

        UpdateUI();

        CheckForPlayerDeath(playerNumber);
    }

    private void UpdateUI()
    {
        leftHealthBar.health = player1Health;
        rightHealthBar.health = player2Health;
    }

    private void CheckForPlayerDeath(int playerNumber)
    {
        if ((playerNumber == 1 && player1Health <= 0) || (playerNumber == 2 && player2Health <= 0))
        {
            HandlePlayerDeath(playerNumber);
        }
    }

    private void HandlePlayerDeath(int playerNumber)
    {
        Debug.Log("Player " + playerNumber + " has died!");
        gameOverPanel.SetActive(true);
        if (playerNumber == 1)
        {
            playerInfo.text = "Player" + 2 + " won the match";
        }
        else
        {
            playerInfo.text = "Player" + 1 + " won the match";
        }
    }
}
