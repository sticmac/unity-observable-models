using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels.Collections
{
    public class Vector2ObservableListModelTest
    {
        private Vector2ObservableListModel _model;

        private static IList<Vector2> defaultValue = Enumerable.Empty<Vector2>().ToList();

        [SetUp]
        public void Setup()
        {
            _model = Vector2ObservableListModel.Create(Vector2.zero, Vector2.one, new(0.5f, 0f));
        }

        [Test]
        public void Vector2ObservableListModelValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void Vector2ObservableListModelValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void Vector2ObservableListModelValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector2ObservableListModelValueCanBeModified()
        {
            _model.Value = new List<Vector2> { new(0,2), Vector2.up, Vector2.right };
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { new(0,2), Vector2.up, Vector2.right }));
        }

        [Test]
        public void Vector2ObservableListModelCanBeIteratedOn()
        {
            var result = new List<Vector2>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<Vector2> { Vector2.zero, Vector2.one, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector2ObservableListModelCanBeSubscribedTo()
        {
            IList<Vector2> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<Vector2> { new(0,2), Vector2.up, Vector2.right };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelCallbackNotCalledIfValueNotChanged()
        {
            IList<Vector2> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector2ObservableListModelCanBeUnsubscribedFrom()
        {
            IList<Vector2> result = defaultValue;
            void Callback(IList<Vector2> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<Vector2> { new(0,2), Vector2.up, Vector2.right };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector2ObservableListModelCanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector2ObservableListModelCanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableListModelCanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableListModelCanBeSetAtIndex()
        {
            Vector2 value = new(0,2);
            _model[1] = value;
            Assert.That(_model[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector2ObservableListModelCanBeSetAtIndexThroughValue()
        {
            Vector2 value = new(0,2);
            _model[1] = value;
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector2ObservableListModelCanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void Vector2ObservableListModelCanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void Vector2ObservableListModelCanBeInserted()
        {
            Vector2 value = new(0,2);
            _model.Insert(1, value);
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector2ObservableListModelCanBeInsertedThroughValue()
        {
            Vector2 value = new(0,2);
            _model.Value.Insert(1, value);
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector2ObservableListModelCanBeRemoved()
        {
            _model.Remove(Vector2.one);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector2ObservableListModelCanBeRemovedThroughValue()
        {
            _model.Value.Remove(Vector2.one);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector2ObservableListModelCanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector2ObservableListModelCanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector2ObservableListModelCanBeAdded()
        {
            _model.Add(new(0,2));
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, Vector2.one, new(0.5f, 0f), new(0,2) }));
        }

        [Test]
        public void Vector2ObservableListModelCanBeAddedThroughValue()
        {
            _model.Value.Add(new(0,2));
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, Vector2.one, new(0.5f, 0f), new(0,2) }));
        }

        [Test]
        public void Vector2ObservableListModelContains()
        {
            Assert.That(_model.Contains(Vector2.one), Is.True);
        }

        [Test]
        public void Vector2ObservableListModelContainsThroughValue()
        {
            Assert.That(_model.Value.Contains(Vector2.one), Is.True);
        }

        [Test]
        public void Vector2ObservableListModelSetAtIndexTriggersChangeEvent()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = new(0,2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelSetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = new(0,2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelChangeEventIsTriggeredOnAdd()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add(new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add(new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelChangeEventIsTriggeredOnRemove()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove(Vector2.one);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove(Vector2.one);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelChangeEventIsTriggeredOnClear()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelChangeEventIsTriggeredOnInsert()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelChangeEventIsTriggeredOnRemoveAt()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModelChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }
    }
}
