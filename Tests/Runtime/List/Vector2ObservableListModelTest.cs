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
        public void Vector2ObservableListModel_ValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void Vector2ObservableListModel_ValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void Vector2ObservableListModel_ValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector2ObservableListModel_ValueCanBeModified()
        {
            _model.Value = new List<Vector2> { new(0,2), Vector2.up, Vector2.right };
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { new(0,2), Vector2.up, Vector2.right }));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeIteratedOn()
        {
            var result = new List<Vector2>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<Vector2> { Vector2.zero, Vector2.one, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeSubscribedTo()
        {
            IList<Vector2> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<Vector2> { new(0,2), Vector2.up, Vector2.right };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_CallbackNotCalledIfValueNotChanged()
        {
            IList<Vector2> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeUnsubscribedFrom()
        {
            IList<Vector2> result = defaultValue;
            void Callback(IList<Vector2> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<Vector2> { new(0,2), Vector2.up, Vector2.right };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo(Vector2.one));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeSetAtIndex()
        {
            Vector2 value = new(0,2);
            _model[1] = value;
            Assert.That(_model[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeSetAtIndexThroughValue()
        {
            Vector2 value = new(0,2);
            _model[1] = value;
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void Vector2ObservableListModel_CanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void Vector2ObservableListModel_CanBeInserted()
        {
            Vector2 value = new(0,2);
            _model.Insert(1, value);
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeInsertedThroughValue()
        {
            Vector2 value = new(0,2);
            _model.Value.Insert(1, value);
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeRemoved()
        {
            _model.Remove(Vector2.one);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeRemovedThroughValue()
        {
            _model.Value.Remove(Vector2.one);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeAdded()
        {
            _model.Add(new(0,2));
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, Vector2.one, new(0.5f, 0f), new(0,2) }));
        }

        [Test]
        public void Vector2ObservableListModel_CanBeAddedThroughValue()
        {
            _model.Value.Add(new(0,2));
            Assert.That(_model.Value, Is.EqualTo(new List<Vector2> { Vector2.zero, Vector2.one, new(0.5f, 0f), new(0,2) }));
        }

        [Test]
        public void Vector2ObservableListModel_Contains()
        {
            Assert.That(_model.Contains(Vector2.one), Is.True);
        }

        [Test]
        public void Vector2ObservableListModel_ContainsThroughValue()
        {
            Assert.That(_model.Value.Contains(Vector2.one), Is.True);
        }

        [Test]
        public void Vector2ObservableListModel_ChangeValueTriggersChangeEventOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value = new List<Vector2> { new(0,2), Vector2.up, Vector2.right };
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_SetAtIndexTriggersChangeEvent()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = new(0,2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_SetAtIndexTriggersChangeEventOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model[1] = new(0,2);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_SetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = new(0,2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_SetAtIndexThroughValueTriggersChangeEventOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value[1] = new(0,2);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnAdd()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add(new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnAddOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Add(new(0,2));
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add(new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnAddThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Add(new(0,2));
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnRemove()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove(Vector2.one);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnRemoveOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Remove(Vector2.one);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove(Vector2.one);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Remove(Vector2.one);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnClear()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnClearOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Clear();
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnClearThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Clear();
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnInsert()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }
        
        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnInsertOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Insert(1, new(0,2));
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnInsertThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Insert(1, new(0,2));
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnRemoveAt()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnRemoveAtOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.RemoveAt(1);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<Vector2> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector2ObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.RemoveAt(1);
            Assert.That(count, Is.EqualTo(1));
        }
    }
}
