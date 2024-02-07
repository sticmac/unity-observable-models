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
            _model = BoolObservableModel.Create();
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

        [Test]
        public void BoolObservableModelCanBeUnsubscribedFrom() {
            bool result = default(bool);
            void Callback(bool v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = true;
            Assert.That(result, Is.EqualTo(default(bool)));
        }

        [Test]
        public void BoolObservableModelCanBeReset() {
            _model.Value = true;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(bool)));
        }

        [Test]
        public void BoolObservableModelCanBeResetToNonDefaultValue() {
            _model = BoolObservableModel.Create(true);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableModelStringCanBeSet() {
            _model.StringValue = "true";
            Assert.That(_model.Value, Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableModelStringCanBeGet() {
            _model.Value = true;
            Assert.That(_model.StringValue, Is.EqualTo("True"));
        }
    }
}