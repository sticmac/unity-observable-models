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
        public void StringObservableListModelValueIsNotNullAfterCreation()
        {
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void StringObservableListModelValueIsNotNullAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.Not.Null);
        }

        [Test]
        public void StringObservableListModelValueIsDefaultValueAfterSetToNull()
        {
            _model.Value = null;
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void StringObservableListModelValueCanBeModified()
        {
            _model.Value = new List<string> { "four", "five", "six" };
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "four", "five", "six" }));
        }

        [Test]
        public void StringObservableListModelCanBeIteratedOn()
        {
            var result = new List<string>();
            foreach (var i in _model)
            {
                result.Add(i);
            }
            Assert.That(result, Is.EqualTo(new List<string> { "one", "two", "three" }));
        }

        [Test]
        public void StringObservableListModelCanBeSubscribedTo()
        {
            IList<string> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            _model.Value = new List<string> { "four", "five", "six" };
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelCallbackNotCalledIfValueNotChanged()
        {
            IList<string> result = defaultValue;
            _model.OnValueChanged += v => result = v;
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void StringObservableListModelCanBeUnsubscribedFrom()
        {
            IList<string> result = defaultValue;
            void Callback(IList<string> v) => result = v;
            _model.OnValueChanged += Callback;
            _model.OnValueChanged -= Callback;
            _model.Value = new List<string> { "four", "five", "six" };
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void StringObservableListModelCanBeReset()
        {
            _model.ResetValue();
            Assert.That(_model.Value, Is.Not.Null); // Resetting the list model shouldn't set it to null
            Assert.That(_model.Value, Is.EqualTo(defaultValue));
        }

        [Test]
        public void StringObservableListModelCanBeReadAtIndex()
        {
            Assert.That(_model[1], Is.EqualTo("two"));
        }

        [Test]
        public void StringObservableListModelCanBeReadAtIndexThroughValue()
        {
            Assert.That(_model.Value[1], Is.EqualTo("two"));
        }

        [Test]
        public void StringObservableListModelCanBeSetAtIndex()
        {
            _model[1] = "four";
            Assert.That(_model[1], Is.EqualTo("four"));
        }

        [Test]
        public void StringObservableListModelCanBeSetAtIndexThroughValue()
        {
            _model[1] = "four";
            Assert.That(_model.Value[1], Is.EqualTo("four"));
        }

        [Test]
        public void StringObservableListModelCanBeCleared()
        {
            _model.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void StringObservableListModelCanBeClearedThroughValue()
        {
            _model.Value.Clear();
            Assert.That(_model.Value, Is.Empty);
        }

        [Test]
        public void StringObservableListModelCanBeInserted()
        {
            _model.Insert(1, "four");
            Assert.That(_model.Value[1], Is.EqualTo("four"));
        }

        [Test]
        public void StringObservableListModelCanBeInsertedThroughValue()
        {
            _model.Value.Insert(1, "four");
            Assert.That(_model.Value[1], Is.EqualTo("four"));
        }

        [Test]
        public void StringObservableListModelCanBeRemoved()
        {
            _model.Remove("two");
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "three" }));
        }

        [Test]
        public void StringObservableListModelCanBeRemovedThroughValue()
        {
            _model.Value.Remove("two");
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "three" }));
        }

        [Test]
        public void StringObservableListModelCanBeRemovedAt()
        {
            _model.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "three" }));
        }

        [Test]
        public void StringObservableListModelCanBeRemovedAtThroughValue()
        {
            _model.Value.RemoveAt(1);
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "three" }));
        }

        [Test]
        public void StringObservableListModelCanBeAdded()
        {
            _model.Add("four");
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "two", "three", "four" }));
        }

        [Test]
        public void StringObservableListModelCanBeAddedThroughValue()
        {
            _model.Value.Add("four");
            Assert.That(_model.Value, Is.EqualTo(new List<string> { "one", "two", "three", "four" }));
        }

        [Test]
        public void StringObservableListModelContains()
        {
            Assert.That(_model.Contains("two"), Is.True);
        }

        [Test]
        public void StringObservableListModelContainsThroughValue()
        {
            Assert.That(_model.Value.Contains("two"), Is.True);
        }

        [Test]
        public void StringObservableListModelSetAtIndexTriggersChangeEvent()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model[1] = "four";
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelSetAtIndexThroughValueTriggersChangeEvent()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value[1] = "four";
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelChangeEventIsTriggeredOnAdd()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Add("four");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelChangeEventIsTriggeredOnAddThroughValue()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Add("four");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelChangeEventIsTriggeredOnRemove()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Remove("two");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelChangeEventIsTriggeredOnRemoveThroughValue()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Remove("two");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelChangeEventIsTriggeredOnClear()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelChangeEventIsTriggeredOnClearThroughValue()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Clear();
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelChangeEventIsTriggeredOnInsert()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Insert(1, "four");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelChangeEventIsTriggeredOnInsertThroughValue()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.Insert(1, "four");
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelChangeEventIsTriggeredOnRemoveAt()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelChangeEventIsTriggeredOnRemoveAtThroughValue()
        {
            IList<string> result = default;
            _model.OnValueChanged += v => result = v;
            _model.Value.RemoveAt(1);
            Assert.That(result, Is.EqualTo(_model.Value));
        }

        [Test]
        public void StringObservableListModelConcatenatedIsCorrect()
        {
            Assert.That(_model.Concatenated, Is.EqualTo("one, two, three"));

            _model.Value = new List<string> { };
            Assert.That(_model.Concatenated, Is.EqualTo(""));
        }
    }
}
