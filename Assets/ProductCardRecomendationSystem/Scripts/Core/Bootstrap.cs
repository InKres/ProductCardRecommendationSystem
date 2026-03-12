using UnityEngine;
using RecomendationSystem.Data;
using RecomendationSystem.Encoding;
using RecomendationSystem.Recommendation;
using System.Collections.Generic;

public class Bootstrap : MonoBehaviour
{
    [Header("Databases")]
    [SerializeField]
    private ProductDatabaseSO productDatabase;

    [Header("UI")]
    [SerializeField]
    private UICoordinator uiCoordinator;

    private IRecommendationFacade recommendationFacade;

    private IProductRepository repository;
    private IVectorCache vectorCache;
    private FeatureEncoder encoder;

    private void Awake()
    {
        InitRecommendationSystem();
        InitUI();
    }

    private void OnDestroy()
    {
        DisposeUI();
        DisposeRecommendationSystem();
    }

    private void InitRecommendationSystem()
    {
        repository = new ProductRepository(productDatabase);
        repository.Init();

        IReadOnlyList<IProductData> products = repository.GetAllProducts();

        encoder = CreateAndFitEncoder(products);

        vectorCache = CreateVectorCache(encoder, products);

        SimilarItemsEngine similarItemsEngine = CreateSimilarItemsEngine(vectorCache);

        RankingEngine rankingEngine = new RankingEngine();

        recommendationFacade = new RecommendationFacade(
            repository,
            similarItemsEngine,
            rankingEngine
        );
    }

    private void DisposeRecommendationSystem()
    {
        recommendationFacade = null;
        vectorCache = null;
        encoder = null;
        repository = null;
    }

    private void InitUI()
    {
        uiCoordinator.InjectFacade(recommendationFacade);
        uiCoordinator.Init();
    }

    private void DisposeUI()
    {
        if (uiCoordinator != null)
        {
            uiCoordinator.Dispose();
        }
    }

    private FeatureEncoder CreateAndFitEncoder(IReadOnlyList<IProductData> products)
    {
        FeatureEncoder encoder = new FeatureEncoder();
        encoder.Fit(products);

        return encoder;
    }

    private IVectorCache CreateVectorCache(
        FeatureEncoder encoder,
        IReadOnlyList<IProductData> products)
    {
        IVectorCache cache = new VectorCache(encoder);
        cache.Build(products);

        return cache;
    }

    private SimilarItemsEngine CreateSimilarItemsEngine(IVectorCache cache)
    {
        SimilarityCalculator similarityCalculator = new SimilarityCalculator();
        return new SimilarItemsEngine(similarityCalculator, cache);
    }
}