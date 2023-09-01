using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels
{
    public class BoolObservableModelTest
    {
        private BoolObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = ScriptableObject.CreateInstance<BoolObservableModel>();
        }

        [Test]
        public void BoolObservableModelValueCanBeModified()
        {
            _model.Value = true;
            Assert.That(_model.Value, Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableModelCanBeSubscribedTo() {
            bool result = default(bool);
            _model.OnValueChanged += v => result = v;
            _model.Value = true;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableModelCallbackNotCalledIfValueNotChanged() {
            bool result = default(bool);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(bool)));
        }
    }
}