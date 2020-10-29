﻿using NikamoozStore.Core.Domain.Payments;

namespace NikamoozStore.Core.Contracts.Payments
{
    public interface PaymentService
    {
        RequestPaymentResult RequestPayment(string amount, string mobile, string factorNumber, string description);

        VerifyPayemtnResult VerifyPayment(string token);
    }
}
