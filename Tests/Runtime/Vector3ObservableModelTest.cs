using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModel
{
    public class Vector3ObservableModelTest
    {
        private Vector3ObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = ScriptableObject.CreateInstance<Vector3ObservableModel>();
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
    }
}