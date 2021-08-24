namespace ObservableCollectionStudy
{
    internal class Item
    {
        private static int _counter;
        private readonly int _id = _counter++;

        public override string ToString() => $"Item{_id}";
    }
}