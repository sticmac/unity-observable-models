using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModel
{
    public class <%= Type %>ObservableModelTest
    {
        private <%= Type %>ObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = ScriptableObject.CreateInstance<<%= Type %>ObservableModel>();
        }

        [Test]
        public void <%= Type %>ObservableModelValueCanBeModified()
        {
            _model.Value = <%= TestValue %>;
            Assert.That(_model.Value, Is.EqualTo(<%= TestValue %>));
        }

        private class Subscriber : IObserver<<%= TypeGeneric %>> {
            public void OnNext(<%= TypeGeneric %> value) {
                Assert.Pass();
            }

            public void OnError(Exception e) {

            }
            
            public void OnCompleted() {

            }
        }

        [Test]
        public void <%= Type %>ObservableModelCanBeSubscribedTo() {
            _model.Value = <%= TestValue %>;

            _model.Subscribe(new Subscriber());
        }
    }
}