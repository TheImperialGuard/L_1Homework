using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Gameplay.SequenceGenerator;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(CreateSequenceGenerator);
        }

        private static SequenceGenerator<KeyCode> CreateSequenceGenerator(DIContainer c)
            => new SequenceGenerator<KeyCode>();
    }
}
