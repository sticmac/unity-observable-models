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
        public void FloatObservableListModelValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void FloatObservableListModelValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void FloatObservableListModelValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void FloatObservableListModelValueCanBeModified()
        {
            _model.Value = new List<float> { 4, 5, 6 };
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 4, 5, 6 }));
        }

        [Test]
        public void FloatObservableListModelCanBeIteratedOn()
        {
            var result = new List<float>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<float> { 1, 2, 3 }));
        }

        [Test]
        public void FloatObservableListModelCanBeSubscribedTo()
        {
            IList<float> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<float> { 4, 5, 6 };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelCallbackNotCalledIfValueNotChanged()
        {
            IList<float> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void FloatObservableListModelCanBeUnsubscribedFrom()
        {
            IList<float> result = defaultValue;
            void Callback(IList<float> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<float> { 4, 5, 6 };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void FloatObservableListModelCanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void FloatObservableListModelCanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo(2));
        }

        [Test]
        public void FloatObservableListModelCanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo(2));
        }

        [Test]
        public void FloatObservableListModelCanBeSetAtIndex()
        {
            _model[1] = 4;
            Assert.That(_model[1], Is.EqualTo(4));
        }

        [Test]
        public void FloatObservableListModelCanBeSetAtIndexThroughValue()
        {
            _model[1] = 4;
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void FloatObservableListModelCanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void FloatObservableListModelCanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void FloatObservableListModelCanBeInserted()
        {
            _model.Insert(1, 4);
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void FloatObservableListModelCanBeInsertedThroughValue()
        {
            _model.Value.Insert(1, 4);
            Assert.That(_model.Value[1], Is.EqualTo(4));
        }

        [Test]
        public void FloatObservableListModelCanBeRemoved()
        {
            _model.Remove(2);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 3 }));
        }

        [Test]
        public void FloatObservableListModelCanBeRemovedThroughValue()
        {
            _model.Value.Remove(2);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 3 }));
        }

        [Test]
        public void FloatObservableListModelCanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 3 }));
        }

        [Test]
        public void FloatObservableListModelCanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 3 }));
        }

        [Test]
        public void FloatObservableListModelCanBeAdded()
        {
            _model.Add(4);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 2, 3, 4 }));
        }

        [Test]
        public void FloatObservableListModelCanBeAddedThroughValue()
        {
            _model.Value.Add(4);
            Assert.That(_model.Value, Is.EqualTo(new List<float> { 1, 2, 3, 4 }));
        }

        [Test]
        public void FloatObservableListModelContains()
        {
            Assert.That(_model.Contains(2), Is.True);
        }

        [Test]
        public void FloatObservableListModelContainsThroughValue()
        {
            Assert.That(_model.Value.Contains(2), Is.True);
        }

        [Test]
        public void FloatObservableListModelSetAtIndexTriggersChangeEvent()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = 4;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelSetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = 4;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelChangeEventIsTriggeredOnAdd()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add(4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add(4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelChangeEventIsTriggeredOnRemove()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove(2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove(2);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelChangeEventIsTriggeredOnClear()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelChangeEventIsTriggeredOnInsert()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, 4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, 4);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelChangeEventIsTriggeredOnRemoveAt()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<float> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void FloatObservableListModelSum()
        {
            Assert.That(_model.Sum, Is.EqualTo(6));

            _model.Value = new List<float> {};
            Assert.That(_model.Sum, Is.EqualTo(0));
        }

        [Test]
        public void FloatObservableListModelMin()
        {
            Assert.That(_model.Min, Is.EqualTo(1));

            _model.Value = new List<float> {};
            Assert.That(() => _model.Min, Throws.InvalidOperationException);
        }

        [Test]
        public void FloatObservableListModelMax()
        {
            Assert.That(_model.Max, Is.EqualTo(3));

            _model.Value = new List<float> {};
            Assert.That(() => _model.Max, Throws.InvalidOperationException);
        }

        [Test]
        public void FloatObservableListModelAverage()
        {
            Assert.That(_model.Average, Is.EqualTo(2));

            _model.Value = new List<float> {};
            Assert.That(() => _model.Average, Throws.InvalidOperationException);
        }
    }
}
