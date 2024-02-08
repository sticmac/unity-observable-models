using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels
{
    public class IntObservableModelTest
    {
        private IntObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = IntObservableModel.Create();
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

        [Test]
        public void IntObservableModelCanBeUnsubscribedFrom() {
            int result = default(int);
            void Callback(int v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = 42;
            Assert.That(result, Is.EqualTo(default(int)));
        }

        [Test]
        public void IntObservableModelCanBeReset() {
            _model.Value = 42;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(int)));
        }

        [Test]
        public void IntObservableModelCanBeResetToNonDefaultValue() {
            _model = IntObservableModel.Create(42);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModelObjectCanBeSet() {
            _model.ObjectValue = 42;
            Assert.That(_model.Value, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModelObjectCanBeGet()
        {
            _model.Value = 42;
            Assert.That(_model.ObjectValue, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModelStringCanBeSet() {
            _model.StringValue = "42";
            Assert.That(_model.Value, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModelStringCanBeSetToNonDefaultValue() {
            _model = IntObservableModel.Create(42);
            _model.StringValue = "42";
            Assert.That(_model.Value, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModelStringCanBeGet() {
            _model.Value = 42;
            Assert.That(_model.StringValue, Is.EqualTo("42"));
        }

        [Test]
        public void IntObservableModelStringCanBeGetWithComma() {
            _model.Value = 42;
            Assert.That(_model.StringValue, Is.EqualTo("42"));
        }
    }
}