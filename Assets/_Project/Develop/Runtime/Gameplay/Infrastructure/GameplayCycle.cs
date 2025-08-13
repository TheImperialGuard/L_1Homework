using Assets._Project.Develop.Runtime.Gameplay.SequenceGenerator;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayCycle : IDisposable
    {
        private const KeyCode ConfirmKey = KeyCode.Space;

        private DIContainer _container;
        private SceneSwitcherService _sceneSwitcherService;
        private ICoroutinesPerformer _coroutinesPerformer;
        private SequenceGenerator<KeyCode> _sequenceGenerator;
        private GameMode _gameMode;

        private GameplayInputArgs _inputArgs;

        public GameplayCycle(DIContainer container, GameplayInputArgs inputArgs)
        {
            _container = container;
            _inputArgs = inputArgs;

            _sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            _sequenceGenerator = _container.Resolve<SequenceGenerator<KeyCode>>();
        }

        public IEnumerator Launch()
        {
            Debug.Log($"Для запуска игры нажмите {ConfirmKey}");

            yield return new WaitWhile(() => Input.GetKeyDown(ConfirmKey) == false);

            List<KeyCode> keys = new List<KeyCode>(_inputArgs.SymbolsSetConfig.Symbols);

            List<KeyCode> sequence = _sequenceGenerator.GenerateOf(keys, 3);

            _gameMode = new GameMode(sequence);

            _gameMode.Win += OnGameModeWin;
            _gameMode.Lose += OnGameModeLose;

            _gameMode.Start();
        }

        private IEnumerator Exit()
        {
            Debug.Log($"Для выхода в меню нажмите {ConfirmKey}");

            yield return new WaitWhile(() => Input.GetKeyDown(ConfirmKey) == false);

            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
        }

        public void Update(float deltaTime)
        {
            _gameMode?.Update(deltaTime);
        }

        private void OnGameModeLose()
        {
            OnGameModeEnded();
            Debug.Log("Defeat!");
            _container.Resolve<ICoroutinesPerformer>().StartPerform(Launch());
        }

        private void OnGameModeWin()
        {
            OnGameModeEnded();
            Debug.Log("Victory!");
            _container.Resolve<ICoroutinesPerformer>().StartPerform(Exit());
        }

        private void OnGameModeEnded()
        {
            if (_gameMode != null)
            {
                _gameMode.Win -= OnGameModeWin;
                _gameMode.Lose -= OnGameModeLose;
            }
        }

        public void Dispose()
        {
            OnGameModeEnded();
        }
    }
}