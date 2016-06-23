using System;
using System.Collections.Generic;
using Assets.Scripts.Game.GameObjects;
using Assets.Scripts.Game.UI.InWorld;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Randomm = System.Random;

namespace Assets.Scripts.Utility.Random
{
    public class ProportionValue<T>
    {
        public double Proportion { get; set; }
        public T Value { get; set; }
    }

    public static class ProportionValue
    {
        public static ProportionValue<T> Create<T>(double proportion, T value)
        {
            return new ProportionValue<T> { Proportion = proportion, Value = value };
        }
        
        static Randomm random = new Randomm();
        public static T ChooseByRandom<T>(
            this IEnumerable<ProportionValue<T>> collection)
        {
            var rnd = random.NextDouble();
            foreach (var item in collection)
            {
                if (rnd < item.Proportion)
                    return item.Value;
                rnd -= item.Proportion;
            }
            throw new InvalidOperationException(
                "The proportions in the collection do not add up to 1.");
        }
    }
}
