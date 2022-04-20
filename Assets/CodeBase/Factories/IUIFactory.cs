using CodeBase.Game;
using CodeBase.Game.Controllers;
using CodeBase.Game.Counters;
using CodeBase.SaveLoadSystem;
using UnityEngine;

namespace CodeBase.Factories
{
    public interface IUIFactory
    {
        void Initialize(AppleCounter appleCounter, KnivesCounter knivesCounter, StagesCounter stagesCounter, IGameFactory gameFactory, 
            ScoreCounter scoreCounter, ISaveLoadSystem saveLoadSystem, Skins skins, Camera cameraPrefab, VictoryController victoryController);
        void CreateCamera();
        void CreateCanvas();
        void CreateHUD();
        void CreateLoseScreen();
        void CreateStartScreen();
        void CreateSkinsScreen();
        void CreateMaxStageScreen();
        GameObject CreateKnife();
        RectTransform CreatNewSkinWindow(Sprite sprite);
        void DestroyUI(UIType type);
        void HideStage();
        void HideScore();
        void ShowStage();
        void ShowScore();
    }
}