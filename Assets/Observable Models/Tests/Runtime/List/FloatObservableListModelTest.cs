using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels.Collections
{
    public class FloatObservableListModelTest
    {
        private FloatObservableListModel _model;

        private static IList<float> defaultValue = Enumerable.Empty<float>().ToList();

        [SetUp]
        public void Setup()
        {
            _model = FloatObservableListModel.Create(1, 2, 3);
        }

        [Test]
        public void FloatObservableListModel_ValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void FloatObservableListModel_ValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void FloatObservableListModel_ValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void FloatObservableListModel_ValueCanBeModified()
        {
            _model.Value = new List<float> { 4, 5, 6 };
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 4, 5, 6 }));
        }

        [Test]
        public void FloatObservableListModel_CanBeIteratedOn()
        {
            var result = new List<float>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<float> { 1, 2, 3 }));
        }

        [Test]
        public void FloatObservableListModel_CanBeSubscribedTo()
        {
            IList<float> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<float> { 4, 5, 6 };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_CallbackNotCalledIfValueNotChanged()
        {
            IList<float> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void FloatObservableListModel_CanBeUnsubscribedFrom()
        {
            IList<float> result = defaultValue;
            void Callback(IList<float> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<float> { 4, 5, 6 };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void FloatObservableListModel_CanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void FloatObservableListModel_CanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo(2));
        }

        [Test]
        public void FloatObservableListModel_CanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo(2));
        }

        [Test]
        public void FloatObservableListModel_CanBeSetAtIndex()
        {
            _model[1] = 4;
            Assert.That(_model[1], Is.EqualTo(4));
        }

        [Test]
        public void FloatObservableListModel_CanBeSetAtIndexThroughValue()
        {
            _model[1] = 4;
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void FloatObservableListModel_CanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void FloatObservableListModel_CanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void FloatObservableListModel_CanBeInserted()
        {
            _model.Insert(1, 4);
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void FloatObservableListModel_CanBeInsertedThroughValue()
        {
            _model.Value.Insert(1, 4);
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void FloatObservableListModel_CanBeRemoved()
        {
            _model.Remove(2);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 3 }));
        }

        [Test]
        public void FloatObservableListModel_CanBeRemovedThroughValue()
        {
            _model.Value.Remove(2);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 3 }));
        }

        [Test]
        public void FloatObservableListModel_CanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 3 }));
        }

        [Test]
        public void FloatObservableListModel_CanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 3 }));
        }

        [Test]
        public void FloatObservableListModel_CanBeAdded()
        {
            _model.Add(4);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 2, 3, 4 }));
        }

        [Test]
        public void FloatObservableListModel_CanBeAddedThroughValue()
        {
            _model.Value.Add(4);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 2, 3, 4 }));
        }

        [Test]
        public void FloatObservableListModel_Contains()
        {
            Assert.That(_model.Contains(2), Is.True);
        }

        [Test]
        public void FloatObservableListModel_ContainsThroughValue()
        {
            Assert.That(_model.Value.Contains(2), Is.True);
        }

        [Test]
        public void FloatObservableListModel_ChangeValueTriggersChangeEventOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value = new List<float> { 4, 5, 6 };
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_SetAtIndexTriggersChangeEvent()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = 4;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_SetAtIndexTriggersChangeEventOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model[1] = 4;
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_SetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = 4;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_SetAtIndexThroughValueTriggersChangeEventOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value[1] = 4;
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnAdd()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add(4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnAddOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Add(4);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add(4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnAddThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Add(4);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnRemove()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove(2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnRemoveOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Remove(2);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove(2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Remove(2);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnClear()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnClearOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Clear();
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnClearThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Clear();
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnInsert()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, 4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnInsertOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Insert(1, 4);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, 4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnInsertThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Insert(1, 4);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnRemoveAt()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnRemoveAtOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.RemoveAt(1);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.RemoveAt(1);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void FloatObservableListModel_Sum()
        {
            Assert.That(_model.Sum, Is.EqualTo(6));

            _model.Value = new List<float> {};
            Assert.That(_model.Sum, Is.EqualTo(0));
        }

        [Test]
        public void FloatObservableListModel_Min()
        {
            Assert.That(_model.Min, Is.EqualTo(1));

            _model.Value = new List<float> {};
            Assert.That(() => _model.Min, Throws.InvalidOperationException);
        }

        [Test]
        public void FloatObservableListModel_Max()
        {
            Assert.That(_model.Max, Is.EqualTo(3));

            _model.Value = new List<float> {};
            Assert.That(() => _model.Max, Throws.InvalidOperationException);
        }

        [Test]
        public void FloatObservableListModel_Average()
        {
            Assert.That(_model.Average, Is.EqualTo(2));

            _model.Value = new List<float> {};
            Assert.That(() => _model.Average, Throws.InvalidOperationException);
        }
    }
}
