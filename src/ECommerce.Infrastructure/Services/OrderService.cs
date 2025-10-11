

using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            // ✅ Business rule: order must have at least 1 item
            if (order.OrderItems == null || !order.OrderItems.Any())
                throw new Exception("Order must contain at least one item.");

            decimal total = 0;

            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception($"Product with ID {item.ProductId} not found.");

                total += product.Price * item.Quantity;
            }

            order.TotalAmount = total;
            order.Status = "Pending";

            await _orderRepository.AddAsync(order);
            return order;
        }

        public async Task<Order> UpdateAsync(Guid id, Order updatedOrder)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
                throw new Exception("Order not found.");

            // ✅ Business logic: only allow status update or items modification
            existingOrder.Status = updatedOrder.Status ?? existingOrder.Status;

            if (updatedOrder.OrderItems != null && updatedOrder.OrderItems.Any())
            {
                decimal total = 0;
                foreach (var item in updatedOrder.OrderItems)
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId);
                    if (product == null)
                        throw new Exception($"Product with ID {item.ProductId} not found.");

                    total += product.Price * item.Quantity;
                }
                existingOrder.OrderItems = updatedOrder.OrderItems;
                existingOrder.TotalAmount = total;
            }

            await _orderRepository.UpdateAsync(existingOrder);
            return existingOrder;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
                return false;

            await _orderRepository.DeleteAsync(existingOrder);
            return true;
        }
    }
}




// using ECommerce.Application.Interfaces.Repositories;
// using ECommerce.Application.Interfaces.Services;
// using ECommerce.Domain.Entities;

// namespace ECommerce.Infrastructure.Services
// {
//     public class OrderService : IOrderService
//     {
//         private readonly IOrderRepository _orderRepository;
//         private readonly IProductRepository _productRepository;

//         public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
//         {
//             _orderRepository = orderRepository;
//             _productRepository = productRepository;
//         }

//         public async Task<Order> CreateAsync(Order order)
//         {
//             // ✅ Business rule: order must have at least 1 item
//             if (order.OrderItems == null || !order.OrderItems.Any())
//                 throw new Exception("Order must contain at least one item.");

//             decimal total = 0;

//             foreach (var item in order.OrderItems)
//             {
//                 var product = await _productRepository.GetByIdAsync(item.ProductId);
//                 if (product == null)
//                     throw new Exception($"Product {item.ProductId} not found.");

//                 // Add up total
//                 total += product.Price * item.Quantity;
//             }

//             order.TotalAmount = total;
//             order.Status = "Pending";

//             await _orderRepository.AddAsync(order);
//             return order;
//         }

//         public async Task<IEnumerable<Order>> GetAllAsync() => await _orderRepository.GetAllAsync();
//         public async Task<Order?> GetByIdAsync(Guid id) => await _orderRepository.GetByIdAsync(id);
//     }
// }
