using DigitRecognizer.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitRecognizer.Classifier
{
    public interface IClassifier
    {
        void Train(IEnumerable<Observation> trainingSet);
        string Predict(int[] pixels);
    }
}
