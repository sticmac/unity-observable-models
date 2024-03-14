using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels.Catalogs
{
    public class Vector3ObservableModelsCatalogTest
    {
        private Vector3ObservableModelsCatalog _catalog;

        private (string, Vector3)[] _models = new (string, Vector3)[]
        {
            ("one", Vector3.zero),
            ("two", Vector3.one),
            ("three", Vector3.right)
        };

        [SetUp]
        public void SetUp()
        {
            _catalog = Vector3ObservableModelsCatalog.Create(_models);
        }

        [Test]
        public void CountTest()
        {
            Assert.That(_catalog.Count, Is.EqualTo(_models.Length));
        }

        [Test]
        public void CreationContainsAllModelsTest()
        {
            foreach (var (name, value) in _models)
            {
                Assert.That(_catalog.ContainsKey(name), Is.True);
                Assert.That(_catalog[name].Value, Is.EqualTo(value));
            }
        }

        [Test]
        public void CatalogDoesNotContainNonExistingModelTest()
        {
            Assert.That(_catalog.ContainsKey("non-existing"), Is.False);
        }

        [Test]
        public void TryGetValueTest()
        {
            foreach (var (name, value) in _models)
            {
                Assert.That(_catalog.TryGetValue(name, out var model), Is.True);
                Assert.That(model.Value, Is.EqualTo(value));
            }
        }

        [Test]
        public void TryGetValueNonExistingTest()
        {
            Assert.That(_catalog.TryGetValue("non-existing", out var model), Is.False);
            Assert.That(model, Is.Null);
        }

        [Test]
        public void SubscribeToOneModelTest()
        {
            var model = _catalog["one"];
            var received = 0;
            model.OnValueChanged += _ => received++;

            model.Value = Vector3.zero;
            Assert.That(received, Is.EqualTo(1));

            model.Value = Vector3.zero;
            Assert.That(received, Is.EqualTo(2));
        }

        [Test]
        public void SubscribeToOneModelDoesntTriggerOtherModelChangeEvents()
        {
            var model = _catalog["one"];
            var received = 0;
            _catalog["two"].OnValueChanged += _ => received++;

            model.Value = Vector3.zero;
            Assert.That(received, Is.EqualTo(0));

            model.Value = Vector3.one;
            Assert.That(received, Is.EqualTo(0));
        }
    }
}
