using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModel
{
    public class <%= Type %>ObservableModelTest
    {
        private <%= Type %>ObservableModel _model;

        [SetUp]
        public void Setup() {
            _model = ScriptableObject.CreateInstance<<%= Type %>ObservableModel>();
        }

        [Test]
        public void <%= Type %>ObservableModelValueCanBeModified()
        {
            _model.Value = <%= TestValue %>;
            Assert.That(_model.Value, Is.EqualTo(<%= TestValue %>));
        }

        [Test]
        public void <%= Type %>ObservableModelCanBeSubscribedTo() {
            <%= TypeGeneric %> result = default(<%= TypeGeneric %>);
            _model.OnValueChanged += v => result = v;
            _model.Value = <%= TestValue %>;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void <%= Type %>ObservableModelCallbackNotCalledIfValueNotChanged() {
            <%= TypeGeneric %> result = default(<%= TypeGeneric %>);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(<%= TypeGeneric %>)));
        }
    }
}