using System;
using CodeBase.Configs;
using CodeBase.Game;
using CodeBase.Game.Controllers;
using CodeBase.Game.Counters;
using CodeBase.Game.Hit;
using CodeBase.ObjectType;
using UnityEngine;

namespace CodeBase.Factories
{
    public interface IGameFactory
    {
        StageConfig[] StageConfig { get; }
        GameObject Enemy { get; }
        GameObject Apple { get; }
        GameObject PlayerKnife { get; }
        
        event Action EnemyCreated;
        event Action<Knife> KnifeCreated;
        event Action AttachedKnivesCreated;
        
        void Initialize(LoseController loseController, StagesCounter stagesCounter, AppleHit appleHit, EnemyHit enemyHit, Skins skins);
        void CreateContainer();
        void CreateEnemy();
        void CreateApple();
        void TryCreateAttachedKnives();
        void CreatePlayerKnife();
        void DestroyContainer();
        void DestroyEnemy();
        void DestroyApple(float destructionTime);
        void DestroyKnife(Knife knife, float destructionTime);
        void CreateParticlesEnemyExplosion();
        void CreateParticlesOnImpact(Vector3 position);
        void CreateAppleParticles(Vector3 position);
        void CreateKnivesParticles(Sprite sprite);
        void AddAttach(GameObject entity, float attachmentDepth);
    }
}