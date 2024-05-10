@ECHO off

taskkill /f /IM DDDSandbox.Endpoint.exe
taskkill /f /IM DDDSandbox.Subscriber.exe
taskkill /f /IM DDDSandbox.Shipping.BusinessCustomers.ShippingArranged.exe
taskkill /f /IM DDDSandbox.Sales.Orders.OrderCreated.exe
taskkill /f /IM DDDSandbox.Billing.Payments.PaymentAccepted.exe
taskkill /f /IM DDDSandbox.Web.exe
taskkill /f /IM DDDSandbox.API.exe
  