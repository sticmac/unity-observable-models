using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModel
{
    public class IntObservableModelTest
    {
        private IntObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = ScriptableObject.CreateInstance<IntObservableModel>();
        }

        [Test]
        public void IntObservableModelValueCanBeModified()
        {
            _model.Value = 42;
            Assert.That(_model.Value, Is.EqualTo(42));
        }

        private class Subscriber : IObserver<int> {
            public void OnNext(int value) {
                Assert.Pass();
            }

            public void OnError(Exception e) {

            }
            
            public void OnCompleted() {

            }
        }

        [Test]
        public void IntObservableModelCanBeSubscribedTo() {
            _model.Value = 42;

            _model.Subscribe(new Subscriber());
        }
    }
}