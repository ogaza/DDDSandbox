@REM if one of the apps does not want to run due to the port already having been taken
@REM run  
@REM netstat -ano
@REM or even sth like the following which finds a process occupying the 5014 port
@REM netstat -aof | findstr :5014 
@REM to find the proces id

@ECHO off

SET location="C:\repos\_backend\DDDSandbox\DDDSandbox"

wt.exe ^
  new-tab --title "API" ^
    -d "%location%\DDDSandbox.API" ^
    powershell -noexit "dotnet run --no-build" ; ^
  new-tab --title "PaymentAccepted" ^
    -d "%location%\DDDSandbox.Billing.Payments.PaymentAccepted" ^
    powershell -noexit "dotnet run --no-build" ; ^
  new-tab --title "Endpoint" ^
    -d "%location%\DDDSandbox.Endpoint" ^
    powershell -noexit "dotnet run --no-build" ; ^
  new-tab --title "ShippingArranged" ^
    -d "%location%\DDDSandbox.Shipping.BusinessCustomers.ShippingArranged" ^
    powershell -noexit "dotnet run --no-build" ; ^
  new-tab --title "Subscriber" ^
    -d "%location%\DDDSandbox.Subscriber" ^
    powershell -noexit "dotnet run --no-build" ; ^
  new-tab --title "Web" ^
    -d "%location%\DDDSandbox.Web" ^
    powershell -noexit "dotnet run --no-build"