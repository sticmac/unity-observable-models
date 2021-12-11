using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModel
{
    public class StringObservableModelTest
    {
        private StringObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = ScriptableObject.CreateInstance<StringObservableModel>();
        }

        [Test]
        public void StringObservableModelValueCanBeModified()
        {
            _model.Value = "hello";
            Assert.That(_model.Value, Is.EqualTo("hello"));
        }

        private class Subscriber : IObserver<string> {
            public void OnNext(string value) {
                Assert.Pass();
            }

            public void OnError(Exception e) {

            }
            
            public void OnCompleted() {

            }
        }

        [Test]
        public void StringObservableModelCanBeSubscribedTo() {
            _model.Value = "hello";

            _model.Subscribe(new Subscriber());
        }
    }
}