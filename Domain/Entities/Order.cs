using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int? EmployedId { get; set; }
        public User? Employed { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderProduct> Products { get; set; } = new List<OrderProduct>();
        public ICollection<OrderHistory> History { get; set; } = new List<OrderHistory>();
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public BuyMethod BuyMethod { get; set; }
        public ICollection<OrderAppliedPromotion> AppliedPromotions { get; set; } = new List<OrderAppliedPromotion>();

        public void ChangeStatus(OrderStatus newStatus, int? employeeId = null)
        {
            Status = newStatus;
            if (newStatus == OrderStatus.asignada && employeeId.HasValue)
            {
                EmployedId = employeeId.Value;
            }
            // Si ya estaba asignado y pasamos a otro estado (Paga, Completada),
            // mantenemos el mismo empleado
            if (newStatus != OrderStatus.pendiente && newStatus != OrderStatus.cancelada && EmployedId.HasValue)
            {
                // no tocar EmployedId, se mantiene
            }
        }
        public void ApplyPromotionResult(decimal subtotal, IEnumerable<(int PromotionId, decimal DiscountAmount, string Details)> appliedPromotions)
        {
            // Asignar subtotal y calcular total
            Subtotal = Math.Round(subtotal, 2);
            var totalDiscount = Math.Round(appliedPromotions.Sum(p => p.DiscountAmount), 2);
            Total = Math.Max(0m, Subtotal - totalDiscount);
            // Actualizar colección de AppliedPromotions para persistencia/auditoría
            AppliedPromotions.Clear();
            foreach (var ap in appliedPromotions)
            {
                AppliedPromotions.Add(new OrderAppliedPromotion
                {
                    PromotionId = ap.PromotionId,
                    DiscountAmount = Math.Round(ap.DiscountAmount, 2),
                    Details = ap.Details,
                    AppliedAt = DateTimeOffset.UtcNow
                });
            }
        }

    }
}

