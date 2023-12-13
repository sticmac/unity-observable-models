using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels
{
    public class Vector2ObservableModelTest
    {
        private Vector2ObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = Vector2ObservableModel.Create();
        }

        [Test]
        public void Vector2ObservableModelValueCanBeModified()
        {
            _model.Value = Vector2.one;
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModelCanBeSubscribedTo() {
            Vector2 result = default(Vector2);
            _model.OnValueChanged += v => result = v;
            _model.Value = Vector2.one;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableModelCallbackNotCalledIfValueNotChanged() {
            Vector2 result = default(Vector2);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(Vector2)));
        }

        [Test]
        public void Vector2ObservableModelCanBeUnsubscribedFrom() {
            Vector2 result = default(Vector2);
            void Callback(Vector2 v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = Vector2.one;
            Assert.That(result, Is.EqualTo(default(Vector2)));
        }

        [Test]
        public void Vector2ObservableModelCanBeReset() {
            _model.Value = Vector2.one;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(Vector2)));
        }

        [Test]
        public void Vector2ObservableModelCanBeResetToNonDefaultValue() {
            _model = Vector2ObservableModel.Create(Vector2.one);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }
    }
}