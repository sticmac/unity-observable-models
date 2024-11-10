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
        public void Vector3ObservableListModel_ValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void Vector3ObservableListModel_ValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void Vector3ObservableListModel_ValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector3ObservableListModel_ValueCanBeModified()
        {
            _model.Value = new List<Vector3> { new(0,2), Vector3.up, Vector3.right };
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { new(0,2), Vector3.up, Vector3.right }));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeIteratedOn()
        {
            var result = new List<Vector3>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<Vector3> { Vector3.zero, Vector3.one, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeSubscribedTo()
        {
            IList<Vector3> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<Vector3> { new(0,2), Vector3.up, Vector3.right };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_CallbackNotCalledIfValueNotChanged()
        {
            IList<Vector3> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeUnsubscribedFrom()
        {
            IList<Vector3> result = defaultValue;
            void Callback(IList<Vector3> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<Vector3> { new(0,2), Vector3.up, Vector3.right };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeSetAtIndex()
        {
            Vector3 value = new(0,2);
            _model[1] = value;
            Assert.That(_model[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeSetAtIndexThroughValue()
        {
            Vector3 value = new(0,2);
            _model[1] = value;
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void Vector3ObservableListModel_CanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void Vector3ObservableListModel_CanBeInserted()
        {
            Vector3 value = new(0,2);
            _model.Insert(1, value);
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeInsertedThroughValue()
        {
            Vector3 value = new(0,2);
            _model.Value.Insert(1, value);
            Assert.That(_model.Value[1], Is.EqualTo(value));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeRemoved()
        {
            _model.Remove(Vector3.one);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeRemovedThroughValue()
        {
            _model.Value.Remove(Vector3.one);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, new(0.5f, 0f) }));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeAdded()
        {
            _model.Add(new(0,2));
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, Vector3.one, new(0.5f, 0f), new(0,2) }));
        }

        [Test]
        public void Vector3ObservableListModel_CanBeAddedThroughValue()
        {
            _model.Value.Add(new(0,2));
            Assert.That(_model.Value, Is.EqualTo(new List<Vector3> { Vector3.zero, Vector3.one, new(0.5f, 0f), new(0,2) }));
        }

        [Test]
        public void Vector3ObservableListModel_Contains()
        {
            Assert.That(_model.Contains(Vector3.one), Is.True);
        }

        [Test]
        public void Vector3ObservableListModel_ContainsThroughValue()
        {
            Assert.That(_model.Value.Contains(Vector3.one), Is.True);
        }

        [Test]
        public void Vector3ObservableListModel_ChangeValueTriggersChangeEventOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value = new List<Vector3> { Vector3.zero, Vector3.one, new(0.5f, 0f), new(0,2) };
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_SetAtIndexTriggersChangeEvent()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = new(0,2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_SetAtIndexTriggersChangeEventOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model[1] = new(0,2);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_SetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = new(0,2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_SetAtIndexThroughValueTriggersChangeEventOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value[1] = new(0,2);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnAdd()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add(new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnAddOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Add(new(0,2));
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add(new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }
        
        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnAddThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Add(new(0,2));
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnRemove()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove(Vector3.one);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnRemoveOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Remove(Vector3.one);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove(Vector3.one);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Remove(Vector3.one);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnClear()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnClearOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Clear();
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnClearThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Clear();
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnInsert()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnInsertOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Insert(1, new(0,2));
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, new(0,2));
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnInsertThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Insert(1, new(0,2));
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnRemoveAt()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnRemoveAtOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.RemoveAt(1);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<Vector3> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void Vector3ObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.RemoveAt(1);
            Assert.That(count, Is.EqualTo(1));
        }
    }
}
