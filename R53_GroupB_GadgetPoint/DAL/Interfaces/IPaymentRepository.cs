﻿using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Interface
{
    public interface IPaymentRepository
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);

    }
}
