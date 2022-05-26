using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController1;
    [SerializeField] private PlayerController _playerController2;
    [SerializeField] private float _turnTimeInSeconds = 5f;
    [SerializeField] private UIInfoPanel _uiInfoPanel;
    [SerializeField] private UIStartPanel _uiStartPanel;
    [SerializeField] private UIFinishPanel _uiFinishPanel;

    private float _currentTimer;
    private int _activePlayerNumber = 0;

    private void Start()
    {
        _playerController1.Initialize();
        _playerController2.Initialize();
        _uiStartPanel.Initialize();
        _uiFinishPanel.Initialize();

        _playerController1.OnLose += OnLose1;
        _playerController2.OnLose += OnLose2;
        _uiStartPanel.OnStartButtonClicked += StartGame;
        _uiFinishPanel.OnRestartButtonClicked += RestartGame;
    }

    private void OnLose1()
    {
        _uiFinishPanel.Show();
    }

    private void OnLose2()
    {
        _uiFinishPanel.Show();
    }

    private void Update()
    {
        _currentTimer -= Time.deltaTime;

        if (_currentTimer <= 0)
            ResetTimer();
        
        _uiInfoPanel.UpdateFiller(_currentTimer/_turnTimeInSeconds);
    }
    
    private void StartGame()
    {
        ActivatePlayer(GetRandomPlayerIndex());
        ResetTimer();
    }

    private void RestartGame() => 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void ResetTimer()
    {
        _currentTimer = _turnTimeInSeconds;
        SwitchPlayer(_activePlayerNumber);
    }

    private void ActivatePlayer(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                _activePlayerNumber = 1;
                _playerController2.Deactivate();
                _playerController1.Activate();
                _uiInfoPanel.SetInfoColor(_playerController1.PlayerData.Material.color);
                _uiInfoPanel.TurnInfoPanelTo(1);
                break;
            case 2:
                _activePlayerNumber = 2;
                _playerController1.Deactivate();
                _playerController2.Activate();
                _uiInfoPanel.SetInfoColor(_playerController2.PlayerData.Material.color);
                _uiInfoPanel.TurnInfoPanelTo(2);
                break;
        }
    }

    private void SwitchPlayer(int fromPlayer)
    {
        if (fromPlayer == 1)
            ActivatePlayer(2);

        if (fromPlayer == 2)
            ActivatePlayer(1);
        
        _playerController1.HideAllPointers();
        _playerController2.HideAllPointers();
    }

    private int GetRandomPlayerIndex()
    {
        var randomIndex = Random.Range(1, 2);

        return randomIndex switch
        {
            1 => 1,
            2 => 2,
            _ => 0
        };
    }
}
