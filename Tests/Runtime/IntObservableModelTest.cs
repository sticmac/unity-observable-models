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

        [Test]
        public void IntObservableModelCanBeSubscribedTo() {
            int result = default(int);
            _model.OnValueChanged += v => result = v;
            _model.Value = 42;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableModelCallbackNotCalledIfValueNotChanged() {
            int result = default(int);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(int)));
        }
    }
}