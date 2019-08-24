//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

namespace ML.NETML.ConsoleApp
{
    using DataModels;
    using Microsoft.ML;
    using Microsoft.ML.Trainers.FastTree;
    using System;
    using System.IO;

    public class Program
    {
        //Machine Learning model to load and use for predictions
        private const string MODEL_FILEPATH = @"MLModel.zip";

        //Dataset to use for predictions 
        private const string TRAIN_DATA_FILEPATH = @"C:\C#\C# Web\06. ML.NET\carsbg.csv";

        public static void Main(string[] args)
        {
            if (!File.Exists(MODEL_FILEPATH))
            {
                TrainModel();
            }

            var input = new ModelInput()
            {
                Make = "VW",
                Model = "Passat",
                CubicCapacity = 1900,
                FuelType = "Дизел",
                HorsePower = 116,
                Gear = "Ръчни",
                Range = 217000,
                Year = "01/08/1999"
            };

            TestModel(MODEL_FILEPATH, input);
        }

        private static void TestModel(string modelFilePath, ModelInput input)
        {
            MLContext mLContext = new MLContext();
            var model = mLContext.Model.Load(modelFilePath, out _);
            var predictionEngine = mLContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

            var predict = predictionEngine.Predict(input);
            Console.WriteLine($"Make: {input.Make}");
            Console.WriteLine($"Price: {predict.Score}");
        }

        private static void TrainModel()
        {
            MLContext mlContext = new MLContext();

            // Load Data
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: TRAIN_DATA_FILEPATH,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Build training pipeline
            IEstimator<ITransformer> trainingPipeline = BuildTrainingPipeline(mlContext);

            // Train Model
            ITransformer mlModel = TrainModel(mlContext, trainingDataView, trainingPipeline);

            // Save model
            SaveModel(mlContext, mlModel, MODEL_FILEPATH, trainingDataView.Schema);
        }

        private static IEstimator<ITransformer> BuildTrainingPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations 
            var dataProcessPipeline = mlContext.Transforms.Categorical
                .OneHotEncoding(new[]
                {
                    new InputOutputColumnPair("Make", "Make"),
                    new InputOutputColumnPair("FuelType", "FuelType"),
                    new InputOutputColumnPair("Year", "Year"),
                    new InputOutputColumnPair("Gear", "Gear")
                }).Append(mlContext.Transforms.Categorical.OneHotHashEncoding(new[]
                {
                    new InputOutputColumnPair("Model", "Model")
                })).Append(mlContext.Transforms.Concatenate("Features",
                    new[] { "Make", "FuelType", "Year", "Gear", "Model", "HorsePower", "Range", "CubicCapacity" }));

            // Set the training algorithm 
            var trainer = mlContext.Regression.Trainers.FastTreeTweedie(new FastTreeTweedieTrainer.Options()
            {
                NumberOfLeaves = 25,
                MinimumExampleCountPerLeaf = 1,
                NumberOfTrees = 500,
                LearningRate = 0.1082966f,
                Shrinkage = 2.957284f,
                LabelColumnName = "Price",
                FeatureColumnName = "Features"
            });
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return trainingPipeline;
        }

        private static ITransformer TrainModel(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            Console.WriteLine("=============== Training  model ===============");

            ITransformer model = trainingPipeline.Fit(trainingDataView);

            Console.WriteLine("=============== End of training process ===============");
            return model;
        }

        private static void SaveModel(MLContext mlContext, ITransformer mlModel, string modelRelativePath, DataViewSchema modelInputSchema)
        {
            // Save/persist the trained model to a .ZIP file
            Console.WriteLine($"=============== Saving the model  ===============");
            mlContext.Model.Save(mlModel, modelInputSchema, GetAbsolutePath(modelRelativePath));
            Console.WriteLine("The model is saved to {0}", GetAbsolutePath(modelRelativePath));
        }

        private static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
