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
        public void BoolObservableModel_ValueCanBeModified()
        {
            _model.Value = true;
            Assert.That(_model.Value, Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableModel_CanBeSubscribedTo() {
            bool result = default(bool);
            _model.OnValueChanged += v => result = v;
            _model.Value = true;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableModel_CallbackNotCalledIfValueNotChanged() {
            bool result = default(bool);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(bool)));
        }

        [Test]
        public void BoolObservableModel_CanBeUnsubscribedFrom() {
            bool result = default(bool);
            void Callback(bool v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = true;
            Assert.That(result, Is.EqualTo(default(bool)));
        }

        [Test]
        public void BoolObservableModel_CanBeReset() {
            _model.Value = true;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(bool)));
        }

        [Test]
        public void BoolObservableModel_CanBeResetToNonDefaultValue() {
            _model = BoolObservableModel.Create(true);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableModel_Equality()
        {
            var model1 = BoolObservableModel.Create();
            var model2 = BoolObservableModel.Create();

            Assert.That(model1.Equals(model2), Is.True);
            Assert.That(model1 == model2, Is.True);
            Assert.That(model1 != model2, Is.False);

            model1.Value = true;
            Assert.That(model1.Equals(model2), Is.False);
            Assert.That(model1 == model2, Is.False);
            Assert.That(model1 != model2, Is.True);
        }

        [Test]
        public void BoolObservableModel_EqualityWithNull()
        {
            _model = BoolObservableModel.Create();
            Assert.That(_model.Equals(null), Is.False);
            Assert.That(_model == null, Is.False);
            Assert.That(_model != null, Is.True);

            _model = null;
            Assert.That(_model == null, Is.True);
            Assert.That(_model != null, Is.False);
        }

        [Test]
        public void BoolObservableModel_ObjectCanBeSet() {
            _model.ObjectValue = true;
            Assert.That(_model.Value, Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableModel_ObjectCanBeGet() {
            _model.Value = true;
            Assert.That(_model.ObjectValue, Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableModel_StringCanBeSet() {
            _model.StringValue = "true";
            Assert.That(_model.Value, Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableModel_StringCanBeGet() {
            _model.Value = true;
            Assert.That(_model.StringValue, Is.EqualTo("True"));
        }
    }
}