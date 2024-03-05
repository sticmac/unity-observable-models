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
        public void IntObservableListModelValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void IntObservableListModelValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void IntObservableListModelValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void IntObservableListModelValueCanBeModified()
        {
            _model.Value = new List<int> { 4, 5, 6 };
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 4, 5, 6 }));
        }

        [Test]
        public void IntObservableListModelCanBeIteratedOn()
        {
            var result = new List<int>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<int> { 1, 2, 3 }));
        }

        [Test]
        public void IntObservableListModelCanBeSubscribedTo()
        {
            IList<int> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<int> { 4, 5, 6 };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelCallbackNotCalledIfValueNotChanged()
        {
            IList<int> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void IntObservableListModelCanBeUnsubscribedFrom()
        {
            IList<int> result = defaultValue;
            void Callback(IList<int> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<int> { 4, 5, 6 };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void IntObservableListModelCanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void IntObservableListModelCanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo(2));
        }

        [Test]
        public void IntObservableListModelCanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo(2));
        }

        [Test]
        public void IntObservableListModelCanBeSetAtIndex()
        {
            _model[1] = 4;
            Assert.That(_model[1], Is.EqualTo(4));
        }

        [Test]
        public void IntObservableListModelCanBeSetAtIndexThroughValue()
        {
            _model[1] = 4;
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void IntObservableListModelCanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void IntObservableListModelCanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void IntObservableListModelCanBeInserted()
        {
            _model.Insert(1, 4);
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void IntObservableListModelCanBeInsertedThroughValue()
        {
            _model.Value.Insert(1, 4);
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void IntObservableListModelCanBeRemoved()
        {
            _model.Remove(2);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 3 }));
        }

        [Test]
        public void IntObservableListModelCanBeRemovedThroughValue()
        {
            _model.Value.Remove(2);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 3 }));
        }

        [Test]
        public void IntObservableListModelCanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 3 }));
        }

        [Test]
        public void IntObservableListModelCanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 3 }));
        }

        [Test]
        public void IntObservableListModelCanBeAdded()
        {
            _model.Add(4);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 2, 3, 4 }));
        }

        [Test]
        public void IntObservableListModelCanBeAddedThroughValue()
        {
            _model.Value.Add(4);
            Assert.That(_model.Value, Is.EqualTo(new List<int> { 1, 2, 3, 4 }));
        }

        [Test]
        public void IntObservableListModelContains()
        {
            Assert.That(_model.Contains(2), Is.True);
        }

        [Test]
        public void IntObservableListModelContainsThroughValue()
        {
            Assert.That(_model.Value.Contains(2), Is.True);
        }

        [Test]
        public void IntObservableListModelSetAtIndexTriggersChangeEvent()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = 4;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelSetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = 4;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelChangeEventIsTriggeredOnAdd()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add(4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add(4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelChangeEventIsTriggeredOnRemove()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove(2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove(2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelChangeEventIsTriggeredOnClear()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelChangeEventIsTriggeredOnInsert()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, 4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, 4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelChangeEventIsTriggeredOnRemoveAt()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<int> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void IntObservableListModelSum()
        {
            Assert.That(_model.Sum, Is.EqualTo(6));

            _model.Value = new List<int> {};
            Assert.That(_model.Sum, Is.EqualTo(0));
        }

        [Test]
        public void IntObservableListModelMin()
        {
            Assert.That(_model.Min, Is.EqualTo(1));

            _model.Value = new List<int> {};
            Assert.That(() => _model.Min, Throws.InvalidOperationException);
        }

        [Test]
        public void IntObservableListModelMax()
        {
            Assert.That(_model.Max, Is.EqualTo(3));

            _model.Value = new List<int> {};
            Assert.That(() => _model.Max, Throws.InvalidOperationException);
        }

        [Test]
        public void IntObservableListModelAverage()
        {
            Assert.That(_model.Average, Is.EqualTo(2));

            _model.Value = new List<int> {};
            Assert.That(() => _model.Average, Throws.InvalidOperationException);
        }
    }
}
