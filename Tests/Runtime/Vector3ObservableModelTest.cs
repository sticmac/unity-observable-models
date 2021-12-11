using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModel
{
    public class Vector3ObservableModelTest
    {
        private Vector3ObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = ScriptableObject.CreateInstance<Vector3ObservableModel>();
        }

        [Test]
        public void Vector3ObservableModelValueCanBeModified()
        {
            _model.Value = Vector3.zero;
            Assert.That(_model.Value, Is.EqualTo(Vector3.zero));
        }

        private class Subscriber : IObserver<Vector3> {
            public void OnNext(Vector3 value) {
                Assert.Pass();
            }

            public void OnError(Exception e) {

            }
            
            public void OnCompleted() {

            }
        }

        [Test]
        public void Vector3ObservableModelCanBeSubscribedTo() {
            _model.Value = Vector3.zero;

            _model.Subscribe(new Subscriber());
        }
    }
}