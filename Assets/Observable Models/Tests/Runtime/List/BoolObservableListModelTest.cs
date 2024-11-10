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
        public void BoolObservableListModel_ValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void BoolObservableListModel_ValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void BoolObservableListModel_ValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void BoolObservableListModel_ValueCanBeModified()
        {
            _model.Value = new List<bool> { false, true, false };
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { false, true, false }));
        }

        [Test]
        public void BoolObservableListModel_CanBeIteratedOn()
        {
            var result = new List<bool>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<bool> { true, true, false }));
        }

        [Test]
        public void BoolObservableListModel_CanBeSubscribedTo()
        {
            IList<bool> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<bool> { false, true, false };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_CallbackNotCalledIfValueNotChanged()
        {
            IList<bool> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void BoolObservableListModel_CanBeUnsubscribedFrom()
        {
            IList<bool> result = defaultValue;
            void Callback(IList<bool> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<bool> { false, true, false };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void BoolObservableListModel_CanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void BoolObservableListModel_CanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableListModel_CanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo(true));
        }

        [Test]
        public void BoolObservableListModel_CanBeSetAtIndex()
        {
            _model[1] = false;
            Assert.That(_model[1], Is.EqualTo(false));
        }

        [Test]
        public void BoolObservableListModel_CanBeSetAtIndexThroughValue()
        {
            _model[1] = false;
            Assert.That(_model.Value[1], Is.EqualTo(false));
        }

        [Test]
        public void BoolObservableListModel_CanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void BoolObservableListModel_CanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void BoolObservableListModel_CanBeInserted()
        {
            _model.Insert(1, false);
            Assert.That(_model.Value[1], Is.EqualTo(false));
        }

        [Test]
        public void BoolObservableListModel_CanBeInsertedThroughValue()
        {
            _model.Value.Insert(1, false);
            Assert.That(_model.Value[1], Is.EqualTo(false));
        }

        [Test]
        public void BoolObservableListModel_CanBeRemoved()
        {
            _model.Remove(true);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, false }));
        }

        [Test]
        public void BoolObservableListModel_CanBeRemovedThroughValue()
        {
            _model.Value.Remove(true);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, false }));
        }

        [Test]
        public void BoolObservableListModel_CanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, false }));
        }

        [Test]
        public void BoolObservableListModel_CanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, false }));
        }

        [Test]
        public void BoolObservableListModel_CanBeAdded()
        {
            _model.Add(false);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, true, false, false }));
        }

        [Test]
        public void BoolObservableListModel_CanBeAddedThroughValue()
        {
            _model.Value.Add(false);
            Assert.That(_model.Value, Is.EqualTo(new List<bool> { true, true, false, false }));
        }

        [Test]
        public void BoolObservableListModel_Contains()
        {
            Assert.That(_model.Contains(true), Is.True);
        }

        [Test]
        public void BoolObservableListModel_ContainsThroughValue()
        {
            Assert.That(_model.Value.Contains(true), Is.True);
        }

        [Test]
        public void BoolObservableListModel_ChangeValueTriggersChangeEventOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value = new List<bool> { false, true, false };
            Assert.That(callCount, Is.EqualTo(1));
        }


        [Test]
        public void BoolObservableListModel_SetAtIndexTriggersChangeEvent()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = false;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_SetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = false;
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_SetAtIndexThroughValueTriggersChangeEventOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value[1] = false;
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnAdd()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add(false);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnAddOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Add(false);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add(false);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnAddThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Add(false);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnRemove()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove(true);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnRemoveOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Remove(true);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove(true);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Remove(true);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnClear()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnClearOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Clear();
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnClearThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Clear();
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnInsert()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, false);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnInsertOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Insert(1, false);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, false);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnInsertThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.Insert(1, false);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnRemoveAt()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnRemoveAtOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.RemoveAt(1);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<bool> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void BoolObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValueOnce()
        {
            int callCount = 0;
            _model.OnValueChanged += v => callCount++;
            _model.Value.RemoveAt(1);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void BoolObservableListModel_All()
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
        public void BoolObservableListModel_Any()
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
        public void BoolObservableListModel_And()
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
        public void BoolObservableListModel_Or()
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
        public void BoolObservableListModel_NotAll()
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
        public void BoolObservableListModel_None()
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
        public void BoolObservableListModel_Nand()
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
        public void BoolObservableListModel_Nor()
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
        public void BoolObservableListModel_AllEqual()
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
        public void BoolObservableListModel_CountTrue()
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
        public void BoolObservableListModel_CountFalse()
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
