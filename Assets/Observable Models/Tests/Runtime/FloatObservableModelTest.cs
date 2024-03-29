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
            _model = FloatObservableModel.Create();
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

        [Test]
        public void FloatObservableModelCanBeUnsubscribedFrom() {
            float result = default(float);
            void Callback(float v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = 42.5f;
            Assert.That(result, Is.EqualTo(default(float)));
        }

        [Test]
        public void FloatObservableModelCanBeReset() {
            _model.Value = 42.5f;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(float)));
        }

        [Test]
        public void FloatObservableModelCanBeResetToNonDefaultValue() {
            _model = FloatObservableModel.Create(42.5f);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModelObjectCanBeSet() {
            _model.ObjectValue = 42.5f;
            Assert.That(_model.Value, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModelObjectCanBeGet() {
            _model.Value = 42.5f;
            Assert.That(_model.ObjectValue, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModelStringCanBeSet() {
            _model.StringValue = "42.5";
            Assert.That(_model.Value, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModelStringCanBeSetWithComma() {
            _model.StringValue = "42,5";
            Assert.That(_model.Value, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModelStringCanBeGet() {
            _model.Value = 42.5f;
            Assert.That(_model.StringValue, Is.EqualTo("42.5"));
        }
    }
}