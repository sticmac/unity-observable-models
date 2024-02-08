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
        public void Vector3ObservableModelValueCanBeModified()
        {
            _model.Value = Vector3.one;
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelCanBeSubscribedTo() {
            Vector3 result = default(Vector3);
            _model.OnValueChanged += v => result = v;
            _model.Value = Vector3.one;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableModelCallbackNotCalledIfValueNotChanged() {
            Vector3 result = default(Vector3);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(Vector3)));
        }

        [Test]
        public void Vector3ObservableModelCanBeUnsubscribedFrom() {
            Vector3 result = default(Vector3);
            void Callback(Vector3 v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = Vector3.one;
            Assert.That(result, Is.EqualTo(default(Vector3)));
        }

        [Test]
        public void Vector3ObservableModelCanBeReset() {
            _model.Value = Vector3.one;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(Vector3)));
        }

        [Test]
        public void Vector3ObservableModelCanBeResetToNonDefaultValue() {
            _model = Vector3ObservableModel.Create(Vector3.one);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelStringCanBeSet() {
            _model.Value = Vector3.one;
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelObjectCanBeSet()
        {
            _model.ObjectValue = Vector3.one;
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelObjectCanBeGet()
        {
            _model.Value = Vector3.one;
            Assert.That(_model.ObjectValue, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelStringCanBeSetFromString() {
            _model.StringValue = "1,1,1";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelStringCanBeSetWithParenthesis() {
            _model.StringValue = "(1,1,1)";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelStringCanBeSetWithSpaces() {
            _model.StringValue = "1 1 1";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelStringCanBeSetWithParenthesisAndSpaces() {
            _model.StringValue = "(1 1 1)";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelStringCanBeSetWithCommaAndSpaces() {
            _model.StringValue = "1, 1, 1";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelStringCanBeSetWithCommaAndSpacesAndParenthesis() {
            _model.StringValue = "(1, 1, 1)";
            Assert.That(_model.Value, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableModelStringCanBeGet()
        {
            _model.Value = Vector3.one;
            Assert.That(_model.StringValue, Is.EqualTo(Vector3.one.ToString()));
        }
    }
}