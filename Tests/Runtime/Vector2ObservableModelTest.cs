using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModel
{
    public class Vector2ObservableModelTest
    {
        private Vector2ObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = ScriptableObject.CreateInstance<Vector2ObservableModel>();
        }

        [Test]
        public void Vector2ObservableModelValueCanBeModified()
        {
            _model.Value = Vector2.zero;
            Assert.That(_model.Value, Is.EqualTo(Vector2.zero));
        }

        private class Subscriber : IObserver<Vector2> {
            public void OnNext(Vector2 value) {
                Assert.Pass();
            }

            public void OnError(Exception e) {

            }
            
            public void OnCompleted() {

            }
        }

        [Test]
        public void Vector2ObservableModelCanBeSubscribedTo() {
            _model.Value = Vector2.zero;

            _model.Subscribe(new Subscriber());
        }
    }
}