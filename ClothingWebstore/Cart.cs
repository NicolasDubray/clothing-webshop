namespace ClothingWebstore
{
    internal class Cart
    {
        private readonly List<CartItem> _items = new();

        public IReadOnlyList<CartItem> Items => _items;

        public void AddItem(int productId, int quantity = 1)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity of product added to cart must be at least 1.");

            var sameProductInCart = _items.FirstOrDefault(x => x.ProductId == productId);
            if (sameProductInCart != null)
                sameProductInCart.Quantity += quantity;
            else
                _items.Add(new CartItem { ProductId = productId, Quantity = quantity });
        }

        public void RemoveItemByIndex(int index) =>
            _items.RemoveAt(index);

        public void RemoveItemByProductId(int productId)
        {
            var item = _items.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
                _items.Remove(item);
        }

        public void EmptyOut() =>
            _items.Clear();
    }
}
