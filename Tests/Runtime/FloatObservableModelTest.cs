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
        public void FloatObservableModel_ValueCanBeModified()
        {
            _model.Value = 42.5f;
            Assert.That(_model.Value, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModel_CanBeSubscribedTo() {
            float result = default(float);
            _model.OnValueChanged += v => result = v;
            _model.Value = 42.5f;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableModel_CallbackNotCalledIfValueNotChanged() {
            float result = default(float);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(float)));
        }

        [Test]
        public void FloatObservableModel_CanBeUnsubscribedFrom() {
            float result = default(float);
            void Callback(float v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = 42.5f;
            Assert.That(result, Is.EqualTo(default(float)));
        }

        [Test]
        public void FloatObservableModel_CanBeReset() {
            _model.Value = 42.5f;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(float)));
        }

        [Test]
        public void FloatObservableModel_CanBeResetToNonDefaultValue() {
            _model = FloatObservableModel.Create(42.5f);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModel_Equality()
        {
            var model1 = FloatObservableModel.Create(42.5f);
            var model2 = FloatObservableModel.Create(42.5f);
            
            Assert.That(model1.Equals(model2), Is.True);
            Assert.That(model1 == model2, Is.True);
            Assert.That(model1 != model2, Is.False);

            model2.Value = 42.6f;
            Assert.That(model1.Equals(model2), Is.False);
            Assert.That(model1 == model2, Is.False);
            Assert.That(model1 != model2, Is.True);
        }

        [Test]
        public void FloatObservableModel_EqualityWithNull()
        {
            _model = FloatObservableModel.Create(42.5f);
            Assert.That(_model.Equals(null), Is.False);
            Assert.That(_model == null, Is.False);
            Assert.That(_model != null, Is.True);

            _model = null;
            Assert.That(_model == null, Is.True);
            Assert.That(_model != null, Is.False);
        }

        [Test]
        public void FloatObservableModel_ObjectCanBeSet() {
            _model.ObjectValue = 42.5f;
            Assert.That(_model.Value, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModel_ObjectCanBeGet() {
            _model.Value = 42.5f;
            Assert.That(_model.ObjectValue, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModel_StringCanBeSet() {
            _model.StringValue = "42.5";
            Assert.That(_model.Value, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModel_StringCanBeSetWithComma() {
            _model.StringValue = "42,5";
            Assert.That(_model.Value, Is.EqualTo(42.5f));
        }

        [Test]
        public void FloatObservableModel_StringCanBeGet() {
            _model.Value = 42.5f;
            Assert.That(_model.StringValue, Is.EqualTo("42.5"));
        }
    }
}