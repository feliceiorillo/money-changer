# money-changer

1. Reference the Library in Your Web API Project
2. Configure Dependency Injection
```cs
     // Add HttpClient
    services.AddHttpClient();

    // Register your library services
    services.AddTransient<IMoneyChangerService, MoneyChangerService>();
    services.AddTransient<IDataParserService, DataParserService>();
```

3. Use the Library in Controllers
 ```cs
 // retrieves the dictionary 
 var currencyRates = await _dataParserService.GetCurrencyRateByDate(request.Date);

 // load the currency rates
 _moneyChangerService.LoadCurrencyRates(currencyRates);

 // exchange the currency
 var result = _moneyChangerService.ExchangeCurrency(request.SourceCurrency, request.DestinationCurrency, request.Amount, request.Date);
                
 ```
# Todo 
1. Add a caching system
2. Read this value from appsettings stored online with IOptionsSnapshot feature
