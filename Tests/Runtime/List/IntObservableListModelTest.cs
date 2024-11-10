using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels.Collections
{
    public class IntObservableListModelTest
    {
        private IntObservableListModel _model;

        private static IList<int> defaultValue = Enumerable.Empty<int>().ToList();

        [SetUp]
        public void Setup()
        {
            _model = IntObservableListModel.Create(1, 2, 3);
        }

        [Test]
        public void IntObservableListModel_ValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void IntObservableListModel_ValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void IntObservableListModel_ValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void IntObservableListModel_ValueCanBeModified()
        {
            _model.Value = new List<int> { 4, 5, 6 };
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 4, 5, 6 }));
        }

        [Test]
        public void IntObservableListModel_CanBeIteratedOn()
        {
            var result = new List<int>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<int> { 1, 2, 3 }));
        }

        [Test]
        public void IntObservableListModel_CanBeSubscribedTo()
        {
            IList<int> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<int> { 4, 5, 6 };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_CallbackNotCalledIfValueNotChanged()
        {
            IList<int> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void IntObservableListModel_CanBeUnsubscribedFrom()
        {
            IList<int> result = defaultValue;
            void Callback(IList<int> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<int> { 4, 5, 6 };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void IntObservableListModel_CanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void IntObservableListModel_CanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo(2));
        }

        [Test]
        public void IntObservableListModel_CanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo(2));
        }

        [Test]
        public void IntObservableListModel_CanBeSetAtIndex()
        {
            _model[1] = 4;
            Assert.That(_model[1], Is.EqualTo(4));
        }

        [Test]
        public void IntObservableListModel_CanBeSetAtIndexThroughValue()
        {
            _model[1] = 4;
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void IntObservableListModel_CanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void IntObservableListModel_CanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void IntObservableListModel_CanBeInserted()
        {
            _model.Insert(1, 4);
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void IntObservableListModel_CanBeInsertedThroughValue()
        {
            _model.Value.Insert(1, 4);
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void IntObservableListModel_CanBeRemoved()
        {
            _model.Remove(2);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 3 }));
        }

        [Test]
        public void IntObservableListModel_CanBeRemovedThroughValue()
        {
            _model.Value.Remove(2);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 3 }));
        }

        [Test]
        public void IntObservableListModel_CanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 3 }));
        }

        [Test]
        public void IntObservableListModel_CanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 3 }));
        }

        [Test]
        public void IntObservableListModel_CanBeAdded()
        {
            _model.Add(4);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 2, 3, 4 }));
        }

        [Test]
        public void IntObservableListModel_CanBeAddedThroughValue()
        {
            _model.Value.Add(4);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 2, 3, 4 }));
        }

        [Test]
        public void IntObservableListModel_Contains()
        {
            Assert.That(_model.Contains(2), Is.True);
        }

        [Test]
        public void IntObservableListModel_ContainsThroughValue()
        {
            Assert.That(_model.Value.Contains(2), Is.True);
        }

        [Test]
        public void IntObservableListModel_ChangeValueTriggersChangeEventOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value = new List<int> { 4, 5, 6 };
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_SetAtIndexTriggersChangeEvent()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = 4;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_SetAtIndexTriggersChangeEventOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model[1] = 4;
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_SetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = 4;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_SetAtIndexThroughValueTriggersChangeEventOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value[1] = 4;
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnAdd()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add(4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnAddOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Add(4);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add(4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }
        
        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnAddThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Add(4);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnRemove()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove(2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnRemoveOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Remove(2);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove(2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Remove(2);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnClear()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnClearOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Clear();
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnClearThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Clear();
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnInsert()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, 4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnInsertOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Insert(1, 4);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, 4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnInsertThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Insert(1, 4);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnRemoveAt()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnRemoveAtOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.RemoveAt(1);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.RemoveAt(1);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void IntObservableListModel_Sum()
        {
            Assert.That(_model.Sum, Is.EqualTo(6));

            _model.Value = new List<int> {};
            Assert.That(_model.Sum, Is.EqualTo(0));
        }

        [Test]
        public void IntObservableListModel_Min()
        {
            Assert.That(_model.Min, Is.EqualTo(1));

            _model.Value = new List<int> {};
            Assert.That(() => _model.Min, Throws.InvalidOperationException);
        }

        [Test]
        public void IntObservableListModel_Max()
        {
            Assert.That(_model.Max, Is.EqualTo(3));

            _model.Value = new List<int> {};
            Assert.That(() => _model.Max, Throws.InvalidOperationException);
        }

        [Test]
        public void IntObservableListModel_Average()
        {
            Assert.That(_model.Average, Is.EqualTo(2));

            _model.Value = new List<int> {};
            Assert.That(() => _model.Average, Throws.InvalidOperationException);
        }
    }
}
