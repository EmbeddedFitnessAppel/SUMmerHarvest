using System;
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

        #region ScoreBoards
        [SerializeField]
        private Transform scoreBoardRed;
        [SerializeField]
        private Transform scoreBoardBlu;
        private Team red;
        private Team blu;
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

        public void SetTeams(Team red, Team blu) {
            this.red = red;
            this.scoreBoardRed.FindChild("Team").GetComponent<Text>().text = red.Name;
            Image redScorePanel = this.scoreBoardRed.GetComponent<Image>();
            redScorePanel.color = new Color(red.Color.r, red.Color.g, red.Color.b, redScorePanel.color.a);

            this.blu = blu;
            this.scoreBoardBlu.FindChild("Team").GetComponent<Text>().text = blu.Name;
            Image bluScorePanel = this.scoreBoardBlu.GetComponent<Image>();
            bluScorePanel.color = new Color(blu.Color.r, blu.Color.g, blu.Color.b, bluScorePanel.color.a);
        }
        public void UpdateScoreBoards() {
            this.scoreBoardRed.FindChild("Score").GetComponent<Text>().text = string.Format("{0} mandjes", this.red.Score);
            this.scoreBoardBlu.FindChild("Score").GetComponent<Text>().text = string.Format("{0} mandjes", this.blu.Score);
        }


        public void ShowEndgamePanel(List<Team> teams)
        {
            teams.Sort(new FuncComparer<Team>((t1, t2) => t2.Score.CompareTo(t1.Score)));

            if (teams.Count < 2)
            {
                throw new InvalidItemCountException("Not enough teams! At least 2 are required.");
            }

            var winningTeamsCount = 0;
            for (var i = 1; i < teams.Count; i++)
            {
                if (teams[i].Score < teams[0].Score)
                {
                    winningTeamsCount = i;
                    break;
                }
            }

            if (winningTeamsCount == 1)
            {
                whichTeamWins.text = string.Format("Team '{0}' wint!", teams[0].Name);
            }
            else
            {
                whichTeamWins.text = "Gelijkspel!";
            }


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

        public void PutInWorldCanvas(GameObject toPut)
        {
            toPut.transform.SetParent(InWorldCanvas.transform);
        }

        private class InvalidItemCountException : Exception
        {
            public InvalidItemCountException()
            {
            }

            public InvalidItemCountException(string message)
                : base(message)
            {
            }

            public InvalidItemCountException(string message, Exception inner)
                : base(message, inner)
            {
            }
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