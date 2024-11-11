using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels
{
    public class StringObservableModelTest
    {
        private StringObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = StringObservableModel.Create();
        }

        [Test]
        public void StringObservableModel_ValueCanBeModified()
        {
            _model.Value = "hello world";
            Assert.That(_model.Value, Is.EqualTo("hello world"));
        }

        [Test]
        public void StringObservableModel_CanBeSubscribedTo() {
            string result = default(string);
            _model.OnValueChanged += v => result = v;
            _model.Value = "hello world";
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableModel_CallbackNotCalledIfValueNotChanged() {
            string result = default(string);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(string)));
        }

        [Test]
        public void StringObservableModel_CanBeUnsubscribedFrom() {
            string result = default(string);
            void Callback(string v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = "hello world";
            Assert.That(result, Is.EqualTo(default(string)));
        }

        [Test]
        public void StringObservableModel_CanBeReset() {
            _model.Value = "hello world";
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(string)));
        }

        [Test]
        public void StringObservableModel_CanBeResetToNonDefaultValue() {
            _model = StringObservableModel.Create("hello world");
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo("hello world"));
        }

        [Test]
        public void StringObservableModel_Equality()
        {
            var model1 = StringObservableModel.Create("hello world");
            var model2 = StringObservableModel.Create("hello world");
            Assert.That(model1.Equals(model2), Is.True);
            Assert.That(model1 == model2, Is.True);
            Assert.That(model1 != model2, Is.False);

            model2.Value = "hello";
            Assert.That(model1.Equals(model2), Is.False);
            Assert.That(model1 == model2, Is.False);
            Assert.That(model1 != model2, Is.True);
        }

        [Test]
        public void StringObservableModel_EqualityWithNull()
        {
            _model = StringObservableModel.Create("hello world");
            Assert.That(_model.Equals(null), Is.False);
            Assert.That(_model == null, Is.False);
            Assert.That(_model != null, Is.True);

            _model = null;
            Assert.That(_model == null, Is.True);
            Assert.That(_model != null, Is.False);
        }

        [Test]
        public void StringObservableModel_ObjectCanBeSet()
        {
            _model.ObjectValue = "hello world";
            Assert.That(_model.Value, Is.EqualTo("hello world"));
        }

        [Test]
        public void StringObservableModel_ObjectCanBeGet()
        {
            _model.Value = "hello world";
            Assert.That(_model.ObjectValue, Is.EqualTo("hello world"));
        }

        [Test]
        public void StringObservableModel_StringCanBeSet() {
            _model.StringValue = "hello world";
            Assert.That(_model.Value, Is.EqualTo("hello world"));
        }

        [Test]
        public void StringObservableModel_StringCanBeSetToNull() {
            _model.StringValue = null;
            Assert.That(_model.Value, Is.EqualTo(null));
        }

        [Test]
        public void StringObservableModel_StringCanBeGet() {
            _model.Value = "hello world";
            Assert.That(_model.StringValue, Is.EqualTo("hello world"));
        }
    }
}