using System;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private UICoordinator uiCoordinator;

    private ProductRepository repository;
    private RecommendationFacade recommendationFacade;

    private void Start()
    {
        repository = new ProductRepository();
        recommendationFacade = CreateRecommendationFacade();

        uiCoordinator.Init(recommendationFacade);
        uiCoordinator.OnCloseApplicationEvent += CloseApplication;
    }

    private void OnDestroy()
    {
        uiCoordinator.OnCloseApplicationEvent -= CloseApplication;
        uiCoordinator.Dispose();
    }

    private RecommendationFacade CreateRecommendationFacade()
    {
        PopularityEngine popularityEngine = new PopularityEngine(repository);
        ContentBasedEngine contentBasedEngine = new ContentBasedEngine(repository);
        HybridEngine hybridEngine = new HybridEngine(popularityEngine, contentBasedEngine);

        return new RecommendationFacade(popularityEngine, contentBasedEngine, hybridEngine);
    }

    private void CloseApplication()
    {
        Debug.Log("Выход из приложения");

        Application.Quit();
    }
}