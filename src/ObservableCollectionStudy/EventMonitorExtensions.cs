using System;
using System.Collections.Specialized;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Events;

namespace ObservableCollectionStudy
{
    internal static class EventMonitorExtensions
    {
        public static IEventRecording RaiseCollectionChanged<T>(this EventAssertions<T> collectionShould, 
            params Expression<Func<NotifyCollectionChangedEventArgs, bool>>[] predicates)
        {
            return collectionShould
                .Raise(nameof(INotifyCollectionChanged.CollectionChanged))
                .WithArgs(predicates);
        }

        public static NotifyCollectionChangedEventArgs CollectionChangedEventArgs(this OccurredEvent eventRecording)
        {
            return (NotifyCollectionChangedEventArgs) eventRecording.Parameters[1];
        }
    }
}