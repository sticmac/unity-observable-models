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
            _model = ScriptableObject.CreateInstance<StringObservableModel>();
        }

        [Test]
        public void StringObservableModelValueCanBeModified()
        {
            _model.Value = "Hello world!";
            Assert.That(_model.Value, Is.EqualTo("Hello world!"));
        }

        [Test]
        public void StringObservableModelCanBeSubscribedTo() {
            string result = default(string);
            _model.OnValueChanged += v => result = v;
            _model.Value = "Hello world!";
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableModelCallbackNotCalledIfValueNotChanged() {
            string result = default(string);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(string)));
        }
    }
}