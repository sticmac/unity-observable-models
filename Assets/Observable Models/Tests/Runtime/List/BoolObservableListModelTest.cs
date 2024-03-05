using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels.Collections
{
    public class BoolObservableListModelTest
    {
        private BoolObservableListModel _model;

        private static IList<bool> defaultValue = Enumerable.Empty<bool>().ToList();

        [SetUp]
        public void Setup()
        {
            _model = BoolObservableListModel.Create(true, true, false);
        }

        [Test]
        public void BoolObservableListModelValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void BoolObservableListModelValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void BoolObservableListModelValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void BoolObservableListModelValueCanBeModified()
        {
            _model.Value = new List<bool> { false, true, false };
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { false, true, false }));
        }

        [Test]
        public void BoolObservableListModelCanBeIteratedOn()
        {
            var result = new List<bool>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<bool> { true, true, false }));
        }

        [Test]
        public void BoolObservableListModelCanBeSubscribedTo()
        {
            IList<bool> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<bool> { false, true, false };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelCallbackNotCalledIfValueNotChanged()
        {
            IList<bool> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void BoolObservableListModelCanBeUnsubscribedFrom()
        {
            IList<bool> result = defaultValue;
            void Callback(IList<bool> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<bool> { false, true, false };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void BoolObservableListModelCanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void BoolObservableListModelCanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableListModelCanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableListModelCanBeSetAtIndex()
        {
            _model[1] = false;
            Assert.That(_model[1], Is.EqualTo(false));
        }

        [Test]
        public void BoolObservableListModelCanBeSetAtIndexThroughValue()
        {
            _model[1] = false;
            Assert.That(_model.Value[1], Is.EqualTo(false));
        }

        [Test]
        public void BoolObservableListModelCanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void BoolObservableListModelCanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void BoolObservableListModelCanBeInserted()
        {
            _model.Insert(1, false);
            Assert.That(_model.Value[1], Is.EqualTo(false));
        }

        [Test]
        public void BoolObservableListModelCanBeInsertedThroughValue()
        {
            _model.Value.Insert(1, false);
            Assert.That(_model.Value[1], Is.EqualTo(false));
        }

        [Test]
        public void BoolObservableListModelCanBeRemoved()
        {
            _model.Remove(true);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, false }));
        }

        [Test]
        public void BoolObservableListModelCanBeRemovedThroughValue()
        {
            _model.Value.Remove(true);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, false }));
        }

        [Test]
        public void BoolObservableListModelCanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, false }));
        }

        [Test]
        public void BoolObservableListModelCanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, false }));
        }

        [Test]
        public void BoolObservableListModelCanBeAdded()
        {
            _model.Add(false);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, true, false, false }));
        }

        [Test]
        public void BoolObservableListModelCanBeAddedThroughValue()
        {
            _model.Value.Add(false);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, true, false, false }));
        }

        [Test]
        public void BoolObservableListModelContains()
        {
            Assert.That(_model.Contains(true), Is.True);
        }

        [Test]
        public void BoolObservableListModelContainsThroughValue()
        {
            Assert.That(_model.Value.Contains(true), Is.True);
        }

        [Test]
        public void BoolObservableListModelSetAtIndexTriggersChangeEvent()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = false;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelSetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = false;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelChangeEventIsTriggeredOnAdd()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add(false);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add(false);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelChangeEventIsTriggeredOnRemove()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove(true);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove(true);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelChangeEventIsTriggeredOnClear()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelChangeEventIsTriggeredOnInsert()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, false);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, false);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelChangeEventIsTriggeredOnRemoveAt()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModelAll()
        {
            Assert.That(_model.All(), Is.False);

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.All(), Is.True);

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.All(), Is.False);

            _model.Value = new List<bool> {};
            Assert.That(_model.All(), Is.False);
        }

        [Test]
        public void BoolObservableListModelAny()
        {
            Assert.That(_model.Any(), Is.True);

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.Any(), Is.True);

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.Any(), Is.False);

            _model.Value = new List<bool> {};
            Assert.That(_model.Any(), Is.False);
        }

        [Test]
        public void BoolObservableListModelAnd()
        {
            Assert.That(_model.And(), Is.False);

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.And(), Is.True);

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.And(), Is.False);

            _model.Value = new List<bool> {};
            Assert.That(_model.And(), Is.False);
        }

        [Test]
        public void BoolObservableListModelOr()
        {
            Assert.That(_model.Or(), Is.True);

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.Or(), Is.True);

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.Or(), Is.False);

            _model.Value = new List<bool> {};
            Assert.That(_model.Or(), Is.False);
        }

        [Test]
        public void BoolObservableListModelNotAll()
        {
            Assert.That(_model.NotAll(), Is.True);

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.NotAll(), Is.False);

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.NotAll(), Is.True);

            _model.Value = new List<bool> {};
            Assert.That(_model.NotAll(), Is.True);
        }

        [Test]
        public void BoolObservableListModelNone()
        {
            Assert.That(_model.None(), Is.False);

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.None(), Is.False);

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.None(), Is.True);

            _model.Value = new List<bool> {};
            Assert.That(_model.None(), Is.True);
        }

        [Test]
        public void BoolObservableListModelNand()
        {
            Assert.That(_model.Nand(), Is.True);

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.Nand(), Is.False);

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.Nand(), Is.True);

            _model.Value = new List<bool> {};
            Assert.That(_model.Nand(), Is.True);
        }

        [Test]
        public void BoolObservableListModelNor()
        {
            Assert.That(_model.Nor(), Is.False);

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.Nor(), Is.False);

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.Nor(), Is.True);

            _model.Value = new List<bool> {};
            Assert.That(_model.Nor(), Is.True);
        }

        [Test]
        public void BoolObservableListModelAllEqual()
        {
            Assert.That(_model.AllEqual(), Is.False);

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.AllEqual(), Is.True);

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.AllEqual(), Is.True);

            _model.Value = new List<bool> {};
            Assert.That(_model.AllEqual(), Is.True);
        }

        [Test]
        public void BoolObservableListModelCountTrue()
        {
            Assert.That(_model.CountTrue(), Is.EqualTo(2));

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.CountTrue(), Is.EqualTo(3));

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.CountTrue(), Is.EqualTo(0));

            _model.Value = new List<bool> {};
            Assert.That(_model.CountTrue(), Is.EqualTo(0));
        }

        [Test]
        public void BoolObservableListModelCountFalse()
        {
            Assert.That(_model.CountFalse(), Is.EqualTo(1));

            _model.Value = new List<bool> { true, true, true };
            Assert.That(_model.CountFalse(), Is.EqualTo(0));

            _model.Value = new List<bool> { false, false, false };
            Assert.That(_model.CountFalse(), Is.EqualTo(3));

            _model.Value = new List<bool> {};
            Assert.That(_model.CountFalse(), Is.EqualTo(0));
        }
    }
}
