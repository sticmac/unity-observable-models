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
        public void StringObservableModelValueCanBeModified()
        {
            _model.Value = "hello world";
            Assert.That(_model.Value, Is.EqualTo("hello world"));
        }

        [Test]
        public void StringObservableModelCanBeSubscribedTo() {
            string result = default(string);
            _model.OnValueChanged += v => result = v;
            _model.Value = "hello world";
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableModelCallbackNotCalledIfValueNotChanged() {
            string result = default(string);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(string)));
        }

        [Test]
        public void StringObservableModelCanBeUnsubscribedFrom() {
            string result = default(string);
            void Callback(string v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = "hello world";
            Assert.That(result, Is.EqualTo(default(string)));
        }

        [Test]
        public void StringObservableModelCanBeReset() {
            _model.Value = "hello world";
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(string)));
        }

        [Test]
        public void StringObservableModelCanBeResetToNonDefaultValue() {
            _model = StringObservableModel.Create("hello world");
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo("hello world"));
        }
    }
}