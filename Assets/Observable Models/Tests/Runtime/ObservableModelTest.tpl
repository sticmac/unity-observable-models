using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels
{
    public class <%= Type %>ObservableModelTest
    {
        private <%= Type %>ObservableModel _model;

        [SetUp]
        public void Setup()
        {
            _model = <%= Type %>ObservableModel.Create();
        }

        [Test]
        public void <%= Type %>_ObservableModelValueCanBeModified()
        {
            _model.Value = <%= TestValue %>;
            Assert.That(_model.Value, Is.EqualTo(<%= TestValue %>));
        }

        [Test]
        public void <%= Type %>_ObservableModelCanBeSubscribedTo()
        {
            <%= TypeGeneric %> result = default(<%= TypeGeneric %>);
            _model.OnValueChanged += v => result = v;
            _model.Value = <%= TestValue %>;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void <%= Type %>_ObservableModelCallbackNotCalledIfValueNotChanged()
        {
            <%= TypeGeneric %> result = default(<%= TypeGeneric %>);
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(default(<%= TypeGeneric %>)));
        }

        [Test]
        public void <%= Type %>_ObservableModelCanBeUnsubscribedFrom()
        {
            <%= TypeGeneric %> result = default(<%= TypeGeneric %>);
            void Callback(<%= TypeGeneric %> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = <%= TestValue %>;
            Assert.That(result, Is.EqualTo(default(<%= TypeGeneric %>)));
        }

        [Test]
        public void <%= Type %>_ObservableModelCanBeReset()
        {
            _model.Value = <%= TestValue %>;
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(default(<%= TypeGeneric %>)));
        }

        [Test]
        public void <%= Type %>_ObservableModelCanBeResetToNonDefaultValue()
        {
            _model = <%= Type %>ObservableModel.Create(<%= TestValue %>);
            _model.ResetValue();
            Assert.That(_model.Value, Is.EqualTo(<%= TestValue %>));
        }

        [Test]
        public void <%= Type %>_ObservableModelObjectCanBeSet()
        {
            _model.ObjectValue = <%= TestValue %>;
            Assert.That(_model.Value, Is.EqualTo(<%= TestValue %>));
        }

        [Test]
        public void <%= Type %>_ObservableModelObjectCanBeGet()
        {
            _model.Value = <%= TestValue %>;
            Assert.That(_model.ObjectValue, Is.EqualTo(<%= TestValue %>));
        }
    }
}