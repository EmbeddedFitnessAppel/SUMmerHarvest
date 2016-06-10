using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
    #region EndGameOverlay
    [SerializeField]
    private GameObject endGameOverlay;

    [SerializeField]
    private Text whichTeamWins;
    [SerializeField]
    private GameObject scoreList;
    [SerializeField]
    private GameObject scoreListItemPrefab;
    #endregion

    #region CountdownTimer
    [SerializeField]
    private Text timerText;
    #endregion

    public void SetCountdownText(float seconds) {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        this.timerText.text = string.Format("{0}:{1:00}", minutes, Mathf.FloorToInt(seconds - minutes * 60));
    }


    public void ShowEndgamePanel(List<Team> teams) {
        teams.Sort(new FuncComparer<Team>((t1, t2) => t1.Score.CompareTo(t2.Score)));

        this.whichTeamWins.text = string.Format("Team '{0}' wint!", teams[0].Name);

        foreach (Team team in teams) {
            GameObject scoreListItem = Instantiate(scoreListItemPrefab);
            scoreListItem.transform.SetParent(scoreList.transform);

            Text teamName = scoreListItem.transform.Find("TeamName").GetComponent<Text>();
            Text teamScore = scoreListItem.transform.Find("TeamScore").GetComponent<Text>();
            teamName.text = team.Name;
            teamScore.text = string.Format("{0} Mandjes", team.Score);
        }

        this.endGameOverlay.SetActive(true);
    }
}
