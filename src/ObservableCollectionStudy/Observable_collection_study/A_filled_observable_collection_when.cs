using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Events;
using Xunit;

namespace ObservableCollectionStudy.Observable_collection_study
{
    public class A_filled_observable_collection_when
    {
        private readonly Item _item1 = new();
        private readonly Item _item2 = new();
        private readonly ObservableCollection<Item> _collection;
        private readonly IMonitor<ObservableCollection<Item>> _monitor;

        public A_filled_observable_collection_when()
        {
            _collection = new ObservableCollection<Item>{_item1, _item2};
            _monitor = _collection.Monitor();
        }

        [Fact]
        public void an_item_is_added_raises_event_args_for_add_action()
        {
            var anotherItem = new Item();
            _collection.Add(anotherItem);
            var eventArgs = _monitor.OccurredEvents.Single().CollectionChangedEventArgs();

            eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Add);
            eventArgs.NewItems!.Should<Item>().Equal(anotherItem);
            eventArgs.OldItems.Should().BeNull();
            eventArgs.OldStartingIndex.Should().Be(-1);
            eventArgs.NewStartingIndex.Should().Be(2);
        }

        [Fact]
        public void an_item_is_inserted_raises_event_args_for_add_action()
        {
            var anotherItem = new Item();
            _collection.Insert(1, anotherItem);
            var eventArgs = _monitor.OccurredEvents.Single().CollectionChangedEventArgs();

            eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Add);
            eventArgs.NewItems!.Should<Item>().Equal(anotherItem);
            eventArgs.OldItems.Should().BeNull();
            eventArgs.OldStartingIndex.Should().Be(-1);
            eventArgs.NewStartingIndex.Should().Be(1);
        }
        
        [Fact]
        public void an_item_is_removed_raises_event_args_for_remove_action()
        {
            _collection.Remove(_item1);
            
            var eventArgs = _monitor.OccurredEvents.Single().CollectionChangedEventArgs();
            eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Remove);
            eventArgs.NewItems!.Should().BeNull();
            eventArgs.OldItems!.Should<Item>().Equal(_item1);
            eventArgs.OldStartingIndex.Should().Be(0);
            eventArgs.NewStartingIndex.Should().Be(-1);
        }
        
        [Fact]
        public void it_is_cleared_raises_event_args_for_reset()
        {
            _collection.Clear();
            
            var eventArgs = _monitor.OccurredEvents.Single().CollectionChangedEventArgs();
            eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Reset);
            eventArgs.NewItems.Should().BeNull();
            eventArgs.OldItems.Should().BeNull();
            eventArgs.OldStartingIndex.Should().Be(-1);
            eventArgs.NewStartingIndex.Should().Be(-1);
        }
        
        [Fact]
        public void an_item_is_moved_raises_event_args_for_move_action()
        {
            _collection.Move(1, 0);

            var eventArgs = _monitor.OccurredEvents.Single().CollectionChangedEventArgs();
            eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Move);
            eventArgs.NewItems!.Should<Item>().Equal(_item2);
            eventArgs.OldItems!.Should<Item>().Equal(_item2);
            eventArgs.OldStartingIndex.Should().Be(1);
            eventArgs.NewStartingIndex.Should().Be(0);
        }
        
        [Fact]
        public void an_item_is_replaced_raises_event_args_for_replace_action()
        {
            var anotherItem = new Item();
            _collection[0] = anotherItem;

            var eventArgs = _monitor.OccurredEvents.Single().CollectionChangedEventArgs();
            eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Replace);
            eventArgs.NewItems!.Should<Item>().Equal(anotherItem);
            eventArgs.OldItems!.Should<Item>().Equal(_item1);
            eventArgs.OldStartingIndex.Should().Be(0);
            eventArgs.NewStartingIndex.Should().Be(0);
        }
    }
}