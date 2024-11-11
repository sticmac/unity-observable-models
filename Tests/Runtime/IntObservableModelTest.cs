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
        public void IntObservableModel_ValueCanBeModified()
        {
            _model.Value = 42;
            Assert.That(_model.Value, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModel_CanBeSubscribedTo() {
            int result = default(int);
            _model.OnValueChanged += v => result = v;
            _model.Value = 42;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableModel_CallbackNotCalledIfValueNotChanged() {
            int result = default(int);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(int)));
        }

        [Test]
        public void IntObservableModel_CanBeUnsubscribedFrom() {
            int result = default(int);
            void Callback(int v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = 42;
            Assert.That(result, Is.EqualTo(default(int)));
        }

        [Test]
        public void IntObservableModel_CanBeReset() {
            _model.Value = 42;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(int)));
        }

        [Test]
        public void IntObservableModel_CanBeResetToNonDefaultValue() {
            _model = IntObservableModel.Create(42);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModel_Equality()
        {
            var model1 = IntObservableModel.Create(42);
            var model2 = IntObservableModel.Create(42);
            Assert.That(model1.Equals(model2), Is.True);
            Assert.That(model1 == model2, Is.True);
        }

        [Test]
        public void IntObservableModel_EqualityWithNull()
        {
            _model = IntObservableModel.Create(42);
            Assert.That(_model.Equals(null), Is.False);
            Assert.That(_model == null, Is.False);
            Assert.That(_model != null, Is.True);

            _model = null;
            Assert.That(_model == null, Is.True);
            Assert.That(_model != null, Is.False);
        }

        [Test]
        public void IntObservableModel_ObjectCanBeSet() {
            _model.ObjectValue = 42;
            Assert.That(_model.Value, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModel_ObjectCanBeGet()
        {
            _model.Value = 42;
            Assert.That(_model.ObjectValue, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModel_StringCanBeSet() {
            _model.StringValue = "42";
            Assert.That(_model.Value, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModel_StringCanBeSetToNonDefaultValue() {
            _model = IntObservableModel.Create(42);
            _model.StringValue = "42";
            Assert.That(_model.Value, Is.EqualTo(42));
        }

        [Test]
        public void IntObservableModel_StringCanBeGet() {
            _model.Value = 42;
            Assert.That(_model.StringValue, Is.EqualTo("42"));
        }
    }
}