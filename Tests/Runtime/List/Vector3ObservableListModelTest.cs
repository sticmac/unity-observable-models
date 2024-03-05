using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels.Collections
{
    public class Vector3ObservableListModelTest
    {
        private Vector3ObservableListModel _model;

        private static IList<Vector3> defaultValue = Enumerable.Empty<Vector3>().ToList();

        [SetUp]
        public void Setup()
        {
            _model = Vector3ObservableListModel.Create(Vector3.zero, Vector3.one, new(0.5f, 0f));
        }

        [Test]
        public void Vector3ObservableListModelValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void Vector3ObservableListModelValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void Vector3ObservableListModelValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector3ObservableListModelValueCanBeModified()
        {
            _model.Value = new List<Vector3> { new(0,2), Vector3.up, Vector3.right };
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { new(0,2), Vector3.up, Vector3.right }));
        }

        [Test]
        public void Vector3ObservableListModelCanBeIteratedOn()
        {
            var result = new List<Vector3>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<Vector3> { Vector3.zero, Vector3.one, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector3ObservableListModelCanBeSubscribedTo()
        {
            IList<Vector3> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<Vector3> { new(0,2), Vector3.up, Vector3.right };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelCallbackNotCalledIfValueNotChanged()
        {
            IList<Vector3> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector3ObservableListModelCanBeUnsubscribedFrom()
        {
            IList<Vector3> result = defaultValue;
            void Callback(IList<Vector3> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<Vector3> { new(0,2), Vector3.up, Vector3.right };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector3ObservableListModelCanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector3ObservableListModelCanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableListModelCanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableListModelCanBeSetAtIndex()
        {
            Vector3 value = new(0,2);
            _model[1] = value;
            Assert.That(_model[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector3ObservableListModelCanBeSetAtIndexThroughValue()
        {
            Vector3 value = new(0,2);
            _model[1] = value;
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector3ObservableListModelCanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void Vector3ObservableListModelCanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void Vector3ObservableListModelCanBeInserted()
        {
            Vector3 value = new(0,2);
            _model.Insert(1, value);
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector3ObservableListModelCanBeInsertedThroughValue()
        {
            Vector3 value = new(0,2);
            _model.Value.Insert(1, value);
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector3ObservableListModelCanBeRemoved()
        {
            _model.Remove(Vector3.one);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector3ObservableListModelCanBeRemovedThroughValue()
        {
            _model.Value.Remove(Vector3.one);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector3ObservableListModelCanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector3ObservableListModelCanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector3ObservableListModelCanBeAdded()
        {
            _model.Add(new(0,2));
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, Vector3.one, new(0.5f, 0f), new(0,2) }));
        }

        [Test]
        public void Vector3ObservableListModelCanBeAddedThroughValue()
        {
            _model.Value.Add(new(0,2));
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, Vector3.one, new(0.5f, 0f), new(0,2) }));
        }

        [Test]
        public void Vector3ObservableListModelContains()
        {
            Assert.That(_model.Contains(Vector3.one), Is.True);
        }

        [Test]
        public void Vector3ObservableListModelContainsThroughValue()
        {
            Assert.That(_model.Value.Contains(Vector3.one), Is.True);
        }

        [Test]
        public void Vector3ObservableListModelSetAtIndexTriggersChangeEvent()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = new(0,2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelSetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = new(0,2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelChangeEventIsTriggeredOnAdd()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add(new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add(new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelChangeEventIsTriggeredOnRemove()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove(Vector3.one);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove(Vector3.one);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelChangeEventIsTriggeredOnClear()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelChangeEventIsTriggeredOnInsert()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelChangeEventIsTriggeredOnRemoveAt()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModelChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }
    }
}
