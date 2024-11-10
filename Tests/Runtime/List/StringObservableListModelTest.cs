using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sticmac.ObservableModels.Collections
{
    public class StringObservableListModelTest
    {
        private StringObservableListModel _model;

        private static IList<string> defaultValue = Enumerable.Empty<string>().ToList();

        [SetUp]
        public void Setup()
        {
            _model = StringObservableListModel.Create("one", "two", "three");
        }

        [Test]
        public void StringObservableListModel_ValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void StringObservableListModel_ValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void StringObservableListModel_ValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void StringObservableListModel_ValueCanBeModified()
        {
            _model.Value = new List<string> { "four", "five", "six" };
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "four", "five", "six" }));
        }

        [Test]
        public void StringObservableListModel_CanBeIteratedOn()
        {
            var result = new List<string>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<string> { "one", "two", "three" }));
        }

        [Test]
        public void StringObservableListModel_CanBeSubscribedTo()
        {
            IList<string> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<string> { "four", "five", "six" };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_CallbackNotCalledIfValueNotChanged()
        {
            IList<string> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void StringObservableListModel_CanBeUnsubscribedFrom()
        {
            IList<string> result = defaultValue;
            void Callback(IList<string> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<string> { "four", "five", "six" };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void StringObservableListModel_CanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void StringObservableListModel_CanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo("two"));
        }

        [Test]
        public void StringObservableListModel_CanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo("two"));
        }

        [Test]
        public void StringObservableListModel_CanBeSetAtIndex()
        {
            _model[1] = "four";
            Assert.That(_model[1], Is.EqualTo("four"));
        }

        [Test]
        public void StringObservableListModel_CanBeSetAtIndexThroughValue()
        {
            _model[1] = "four";
            Assert.That(_model.Value[1], Is.EqualTo("four"));
        }

        [Test]
        public void StringObservableListModel_CanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void StringObservableListModel_CanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void StringObservableListModel_CanBeInserted()
        {
            _model.Insert(1, "four");
            Assert.That(_model.Value[1], Is.EqualTo("four"));
        }

        [Test]
        public void StringObservableListModel_CanBeInsertedThroughValue()
        {
            _model.Value.Insert(1, "four");
            Assert.That(_model.Value[1], Is.EqualTo("four"));
        }

        [Test]
        public void StringObservableListModel_CanBeRemoved()
        {
            _model.Remove("two");
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "three" }));
        }

        [Test]
        public void StringObservableListModel_CanBeRemovedThroughValue()
        {
            _model.Value.Remove("two");
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "three" }));
        }

        [Test]
        public void StringObservableListModel_CanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "three" }));
        }

        [Test]
        public void StringObservableListModel_CanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "three" }));
        }

        [Test]
        public void StringObservableListModel_CanBeAdded()
        {
            _model.Add("four");
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "two", "three", "four" }));
        }

        [Test]
        public void StringObservableListModel_CanBeAddedThroughValue()
        {
            _model.Value.Add("four");
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "two", "three", "four" }));
        }


        [Test]
        public void StringObservableListModel_Contains()
        {
            Assert.That(_model.Contains("two"), Is.True);
        }

        [Test]
        public void StringObservableListModel_ContainsThroughValue()
        {
            Assert.That(_model.Value.Contains("two"), Is.True);
        }

        [Test]
        public void StringObservableListModel_ChangeValueTriggersChangeEventOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value = new List<string> { "four", "five", "six" };
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_SetAtIndexTriggersChangeEvent()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = "four";
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_SetAtIndexTriggersChangeEventOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model[1] = "four";
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_SetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = "four";
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_SetAtIndexThroughValueTriggersChangeEventOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value[1] = "four";
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnAdd()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add("four");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnAddOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Add("four");
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add("four");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnAddThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Add("four");
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnRemove()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove("two");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnRemoveOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Remove("two");
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove("two");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnRemoveThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Remove("two");
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnClear()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnClearOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Clear();
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnClearThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Clear();
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnInsert()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, "four");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnInsertOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Insert(1, "four");
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, "four");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnInsertThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.Insert(1, "four");
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnRemoveAt()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnRemoveAtOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.RemoveAt(1);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModel_ChangeEventIsTriggeredOnRemoveAtThroughValueOnce()
        {
            int count = 0;
            _model.OnValueChanged += v => count++;
            _model.Value.RemoveAt(1);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void StringObservableListModel_ConcatenatedIsCorrect()
        {
            Assert.That(_model.Concatenated, Is.EqualTo("one, two, three"));

            _model.Value = new List<string> { };
            Assert.That(_model.Concatenated, Is.EqualTo(""));
        }
    }
}
