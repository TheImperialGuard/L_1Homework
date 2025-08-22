using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.RoundsScore;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameMode
    {
        public event Action Win;
        public event Action Lose;

        private Queue<KeyCode> _sequence;

        private bool _isRunning;

        public GameMode(List<KeyCode> sequence)
        {
            _sequence = new Queue<KeyCode>(sequence);
        }

        public void Start()
        {
            Debug.Log("Sequence:\n" + string.Join(" ", _sequence));

            _isRunning = true;
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;

            if(Input.GetKeyDown(_sequence.Peek()))
                _sequence.Dequeue();
            else if (Input.anyKeyDown)
                ProcessLose();

            if (IsSequenceCompleted())
                ProcessWin();
        }

        private void ProcessWin()
        {
            ProcessEndGame();

            Win?.Invoke();
        }

        private void ProcessLose()
        {
            ProcessEndGame();

            Lose?.Invoke();
        }

        private void ProcessEndGame() => _isRunning = false;

        private bool IsSequenceCompleted() => _sequence.Count < 1; 
    }
}
