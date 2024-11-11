using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels
{
    public class Vector3ObservableModelTest
    {
        private Vector3ObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = Vector3ObservableModel.Create();
        }

        [Test]
        public void Vector3ObservableModel_ValueCanBeModified()
        {
            _model.Value = Vector3.one;
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_CanBeSubscribedTo() {
            Vector3 result = default(Vector3);
            _model.OnValueChanged += v => result = v;
            _model.Value = Vector3.one;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableModel_CallbackNotCalledIfValueNotChanged() {
            Vector3 result = default(Vector3);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(Vector3)));
        }

        [Test]
        public void Vector3ObservableModel_CanBeUnsubscribedFrom() {
            Vector3 result = default(Vector3);
            void Callback(Vector3 v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = Vector3.one;
            Assert.That(result, Is.EqualTo(default(Vector3)));
        }

        [Test]
        public void Vector3ObservableModel_CanBeReset() {
            _model.Value = Vector3.one;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(Vector3)));
        }

        [Test]
        public void Vector3ObservableModel_CanBeResetToNonDefaultValue() {
            _model = Vector3ObservableModel.Create(Vector3.one);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_Equality()
        {
            var model1 = Vector3ObservableModel.Create();
            var model2 = Vector3ObservableModel.Create();
            Assert.That(model1.Equals(model2), Is.True);
            Assert.That(model1 == model2, Is.True);
            Assert.That(model1 != model2, Is.False);

            model2.Value = Vector3.one;
            Assert.That(model1.Equals(model2), Is.False);
            Assert.That(model1 == model2, Is.False);
            Assert.That(model1 != model2, Is.True);
        }

        [Test]
        public void Vector3ObservableModel_EqualityWithNull()
        {
            _model = Vector3ObservableModel.Create(Vector3.one);
            Assert.That(_model.Equals(null), Is.False);
            Assert.That(_model == null, Is.False);
            Assert.That(_model != null, Is.True);

            _model = null;
            Assert.That(_model == null, Is.True);
            Assert.That(_model != null, Is.False);
        }

        [Test]
        public void Vector3ObservableModel_StringCanBeSet() {
            _model.Value = Vector3.one;
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_ObjectCanBeSet()
        {
            _model.ObjectValue = Vector3.one;
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_ObjectCanBeGet()
        {
            _model.Value = Vector3.one;
            Assert.That(_model.ObjectValue, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_StringCanBeSetFromString() {
            _model.StringValue = "1,1,1";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_StringCanBeSetWithParenthesis() {
            _model.StringValue = "(1,1,1)";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_StringCanBeSetWithSpaces() {
            _model.StringValue = "1 1 1";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_StringCanBeSetWithParenthesisAndSpaces() {
            _model.StringValue = "(1 1 1)";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_StringCanBeSetWithCommaAndSpaces() {
            _model.StringValue = "1, 1, 1";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_StringCanBeSetWithCommaAndSpacesAndParenthesis() {
            _model.StringValue = "(1, 1, 1)";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModel_StringCanBeGet()
        {
            _model.Value = Vector3.one;
            Assert.That(_model.StringValue, Is.EqualTo(Vector3.one.ToString()));
        }
    }
}