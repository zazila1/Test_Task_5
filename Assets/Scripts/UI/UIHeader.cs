using TMPro;
using UnityEngine;

public class UIHeader : MonoBehaviour
{
    [SerializeField] private PlayerController _Player;
    [SerializeField] private TMP_Text _Score;

    private void Awake()
    {
        _Player._OnScoreChanged += (score) =>
        {
            _Score.text = $"Очки: {score.ToString()}";
        };
    }
}
