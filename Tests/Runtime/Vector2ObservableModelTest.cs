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
        public void Vector2ObservableModel_ValueCanBeModified()
        {
            _model.Value = Vector2.one;
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_CanBeSubscribedTo() {
            Vector2 result = default(Vector2);
            _model.OnValueChanged += v => result = v;
            _model.Value = Vector2.one;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableModel_CallbackNotCalledIfValueNotChanged() {
            Vector2 result = default(Vector2);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(Vector2)));
        }

        [Test]
        public void Vector2ObservableModel_CanBeUnsubscribedFrom() {
            Vector2 result = default(Vector2);
            void Callback(Vector2 v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = Vector2.one;
            Assert.That(result, Is.EqualTo(default(Vector2)));
        }

        [Test]
        public void Vector2ObservableModel_CanBeReset() {
            _model.Value = Vector2.one;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(Vector2)));
        }

        [Test]
        public void Vector2ObservableModel_CanBeResetToNonDefaultValue() {
            _model = Vector2ObservableModel.Create(Vector2.one);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_Equality()
        {
            var model1 = Vector2ObservableModel.Create();
            var model2 = Vector2ObservableModel.Create();
            Assert.That(model1.Equals(model2), Is.True);
            Assert.That(model1 == model2, Is.True);
            Assert.That(model1 != model2, Is.False);

            model2.Value = Vector2.one;
            Assert.That(model1.Equals(model2), Is.False);
            Assert.That(model1 == model2, Is.False);
            Assert.That(model1 != model2, Is.True);
        }

        [Test]
        public void Vector2ObservableModel_EqualityWithNull()
        {
            _model = Vector2ObservableModel.Create();
            Assert.That(_model.Equals(null), Is.False);
            Assert.That(_model == null, Is.False);
            Assert.That(_model != null, Is.True);

            _model = null;
            Assert.That(_model == null, Is.True);
            Assert.That(_model != null, Is.False);
        }

        [Test]
        public void Vector2ObservableModel_ObjectCanBeSet()
        {
            _model.ObjectValue = Vector2.one;
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_ObjectCanBeGet()
        {
            _model.Value = Vector2.one;
            Assert.That(_model.ObjectValue, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_StringCanBeSet() {
            _model.StringValue = "1,1";
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_StringCanBeSetWithParenthesis() {
            _model.StringValue = "(1,1)";
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_StringCanBeSetWithSpaces() {
            _model.StringValue = "1 1";
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_StringCanBeSetWithParenthesisAndSpaces() {
            _model.StringValue = "(1 1)";
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_StringCanBeSetWithComma() {
            _model.StringValue = "1,1";
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_StringCanBeSetWithCommaAndSpaces() {
            _model.StringValue = "1, 1";
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_StringCanBeSetWithCommaAndParenthesisAndSpaces() {
            _model.StringValue = "(1, 1)";
            Assert.That(_model.Value, Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableModel_StringCanBeGet() {
            _model.Value = Vector2.one;
            Assert.That(_model.StringValue, Is.EqualTo(Vector2.one.ToString()));
        }
    }
}