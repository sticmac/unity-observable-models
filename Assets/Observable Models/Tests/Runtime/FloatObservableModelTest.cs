using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels
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
            _model.Value = 42.5f;
            Assert.That(_model.Value, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModelCanBeSubscribedTo() {
            float result = default(float);
            _model.OnValueChanged += v => result = v;
            _model.Value = 42.5f;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableModelCallbackNotCalledIfValueNotChanged() {
            float result = default(float);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(float)));
        }
    }
}