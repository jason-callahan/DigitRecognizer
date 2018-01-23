﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitRecognizer.Reader;
using DigitRecognizer.Distance;

namespace DigitRecognizer.Classifier
{
    public class BasicClassifier : IClassifier
    {
        private IEnumerable<Observation> data;
        private readonly IDistance distance;

        public BasicClassifier(IDistance distance)
        {
            this.distance = distance;
        }

        public string Predict(int[] pixels)
        {
            Observation currentBest = null;
            var shortest = Double.MaxValue;

            foreach(Observation obs in data)
            {
                var dist = this.distance.Between(obs.Pixels, pixels);
                if(dist < shortest)
                {
                    shortest = dist;
                    currentBest = obs;
                }
            }

            return currentBest.Label;
        }

        public void Train(IEnumerable<Observation> trainingSet)
        {
            this.data = trainingSet;
        }
    }
}
