using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    internal class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private MainMenuCycle _cycle;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs inputArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены главного меню");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены главного меню");
            _cycle = new MainMenuCycle(_container);
        }

        private void Update() => _cycle?.Update(Time.deltaTime);
    }
}
