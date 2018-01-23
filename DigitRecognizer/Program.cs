using DigitRecognizer.Classifier;
using DigitRecognizer.Distance;
using DigitRecognizer.Elvaluate;
using DigitRecognizer.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitRecognizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var distance = new ManhattanDistance();
            var classifier = new BasicClassifier(distance);

            var trainingPath = @"Data/trainingsample.csv";
            var training = DataReader.ReadObservations(trainingPath);

            classifier.Train(training);

            var validationPath = @"Data/validationsample.csv";
            var validation = DataReader.ReadObservations(validationPath);

            var wrongPredictions = new Dictionary<string,int>();
            var numWrong = 0;
            for(var i = 0; i < validation.Length; i++)
            {
                var prediction = classifier.Predict(validation[i].Pixels);

                if (prediction != validation[i].Label)
                {
                    numWrong++;
                    Console.WriteLine("{0} != {1}", prediction, validation[i].Label);
                    if (wrongPredictions.ContainsKey(validation[i].Label))
                        wrongPredictions[validation[i].Label]++;
                    else
                        wrongPredictions.Add(validation[i].Label, 1);

                }
            }
            wrongPredictions = 
                (from entry in wrongPredictions orderby entry.Value descending select entry)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            Console.WriteLine("#Wrong: {0}/{1}, {2:P2}", numWrong, validation.Length, (1.0-((float)numWrong/validation.Length)) );
            Console.WriteLine("Wrong predictions were:");
            foreach (var prediction in wrongPredictions)
                Console.WriteLine("Missed \"{0}\" {1} times", prediction.Key, prediction.Value);

            //Console.WriteLine(classifier.Predict(validation[4].Pixels));

            //var correct = Evaluator.Correct(validation, classifier);
            //Console.WriteLine("Correctly classified: {0:P2}", correct);

            Console.ReadLine();
        }
    }
}
