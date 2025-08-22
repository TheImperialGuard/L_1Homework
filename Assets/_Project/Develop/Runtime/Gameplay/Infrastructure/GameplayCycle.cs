using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Gameplay.SequenceGenerator;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.RoundsScore;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
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
        private ICoroutinesPerformer _coroutinesPerformer;
        private ConfigsProviderService _configsProviderService;
        private SequenceGenerator<KeyCode> _sequenceGenerator;
        private WalletService _wallet;
        private RoundsScoreService _roundScore;

        private GameMode _gameMode;
        private SymbolSequenceGameConfig _gameConfig;

        private GameplayInputArgs _inputArgs;

        public GameplayCycle(DIContainer container, GameplayInputArgs inputArgs)
        {
            _container = container;
            _inputArgs = inputArgs;

            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            _sequenceGenerator = _container.Resolve<SequenceGenerator<KeyCode>>();
            _configsProviderService = _container.Resolve<ConfigsProviderService>();
            _wallet = _container.Resolve<WalletService>();
            _roundScore = _container.Resolve<RoundsScoreService>();
        }

        public IEnumerator Launch()
        {
            _gameConfig = _configsProviderService.GetConfig<SymbolSequenceGameConfig>();
            
            Debug.Log($"Для запуска игры нажмите {ConfirmKey}");

            yield return new WaitWhile(() => Input.GetKeyDown(ConfirmKey) == false);

            List<KeyCode> keys = GetSequenceFor(_inputArgs.GameMode);

            List<KeyCode> sequence = _sequenceGenerator.GenerateOf(keys, 3);

            _gameMode = new GameMode(sequence);

            _gameMode.Win += OnGameModeWin;
            _gameMode.Lose += OnGameModeLose;

            _gameMode.Start();
        }

        private List<KeyCode> GetSequenceFor(GameMods gameMod)
        {
            SymbolsSetConfig symbolSetConfig = _gameConfig.GetConfigBy(gameMod);

            List<KeyCode> keys = new List<KeyCode>(symbolSetConfig.Symbols);

            return keys;
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
            Debug.Log("Defeat!");

            GetLoseFine();
            _roundScore.IncreaseLosses();

            OnGameModeEnded();

            _container.Resolve<ICoroutinesPerformer>().StartPerform(Launch());
        }

        private void OnGameModeWin()
        {
            Debug.Log("Victory!");

            GetWinRewards();
            _roundScore.IncreaseWins();

            OnGameModeEnded();

            _container.Resolve<ICoroutinesPerformer>().StartPerform(Exit());
        }

        private void GetLoseFine()
        {
            foreach (CurrencyConfig currencyConfig in _gameConfig.LoseFineList)
            {
                if (_wallet.Enough(currencyConfig.Type, currencyConfig.Value))
                    _wallet.Spend(currencyConfig.Type, currencyConfig.Value);
                else
                    _wallet.SpendFully(currencyConfig.Type);
            }
        }

        private void GetWinRewards()
        {
            foreach (CurrencyConfig currencyConfig in _gameConfig.WinRewardsList)
                _wallet.Add(currencyConfig.Type, currencyConfig.Value);
        }

        private void SaveGameResult()
        {
            PlayerDataProvider dataProvider = _container.Resolve<PlayerDataProvider>();

            _coroutinesPerformer.StartPerform(dataProvider.Save());
        }

        private void OnGameModeEnded()
        {
            SaveGameResult();

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