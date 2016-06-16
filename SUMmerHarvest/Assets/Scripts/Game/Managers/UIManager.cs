using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        public Canvas InWorldCanvas;

        #region CountdownTimer

        [SerializeField]
        private Text timerText;

        #endregion

        public override void Awake()
        {
            base.Awake();
        }

        public void SetCountdownText(float seconds)
        {
            var minutes = Mathf.FloorToInt(seconds / 60f);
            timerText.text = string.Format("{0}:{1:00}", minutes, Mathf.FloorToInt(seconds - minutes * 60));
        }


        public void ShowEndgamePanel(List<Team> teams)
        {
            teams.Sort(new FuncComparer<Team>((t1, t2) => t2.Score.CompareTo(t1.Score)));

            whichTeamWins.text = string.Format("Team '{0}' wint!", teams[0].Name);

            foreach (var team in teams)
            {
                var scoreListItem = Instantiate(scoreListItemPrefab);
                scoreListItem.transform.SetParent(scoreList.transform);

                var teamName = scoreListItem.transform.Find("TeamName").GetComponent<Text>();
                var teamScore = scoreListItem.transform.Find("TeamScore").GetComponent<Text>();
                teamName.text = team.Name;
                teamScore.text = string.Format("{0} Mandjes", team.Score);
            }

            endGameOverlay.SetActive(true);
        }

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
    }
}