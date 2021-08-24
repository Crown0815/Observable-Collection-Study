using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Events;
using Xunit;

namespace ObservableCollectionStudy.Observable_collection_study
{
    public class An_empty_observable_collection_when : IDisposable
    {
        private readonly ObservableCollection<Item> _collection;
        private readonly IMonitor<ObservableCollection<Item>> _monitor;

        public An_empty_observable_collection_when()
        {
            _collection = new ObservableCollection<Item>();
            _monitor = _collection.Monitor();
        }
        
        [Fact]
        public void an_item_is_added_raises_event_args_with_the_add_action()
        {
            var item = new Item();
            _collection.Add(item);
            var eventArgs = _monitor.OccurredEvents.Single().CollectionChangedEventArgs();
                
            eventArgs.Action.Should().Be(NotifyCollectionChangedAction.Add);
            eventArgs.NewItems!.Should<Item>().Equal(item);
            eventArgs.OldItems.Should().BeNull();
            eventArgs.OldStartingIndex.Should().Be(-1);
            eventArgs.NewStartingIndex.Should().Be(0);
        }

        [Fact]
        public void an_item_is_removed_raises_no_event()
        {
            _collection.Remove(new Item());
            _monitor.OccurredEvents.Should().BeEmpty();
        }
        
        [Fact]
        public void it_is_cleared_raises_event_args_with_the_reset_action()
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
        public void an_item_is_moved_throws_an_argument_out_of_range_exception()
        {
            _collection.Invoking(x => x.Move(0, 0))
                .Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Fact]
        public void an_item_is_replaced_throws_an_argument_out_of_range_exception()
        {
            _collection.Invoking(x => x[0] = new Item())
                .Should().Throw<ArgumentOutOfRangeException>();
        }

        public void Dispose() => _monitor.Dispose();
    }
}