using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModel
{
    public class FloatObservableModelTest
    {
        private FloatObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = ScriptableObject.CreateInstance<FloatObservableModel>();
        }

        [Test]
        public void FloatObservableModelValueCanBeModified()
        {
            _model.Value = 33.5f;
            Assert.That(_model.Value, Is.EqualTo(33.5f));
        }

        private class Subscriber : IObserver<float> {
            public void OnNext(float value) {
                Assert.Pass();
            }

            public void OnError(Exception e) {

            }
            
            public void OnCompleted() {

            }
        }

        [Test]
        public void FloatObservableModelCanBeSubscribedTo() {
            _model.Value = 33.5f;

            _model.Subscribe(new Subscriber());
        }
    }
}