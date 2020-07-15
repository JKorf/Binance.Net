## Serverless Example

![Screen Recording](https://github.com/mehmettahameral/Binance.Net/blob/serverless/Examples/recording.gif?raw=true)

To get started with serverless example, you need to do following steps:

- Grab a Binance API public and secret key.
- Set BinanceApiKey and BinanceSecretKey as environment variable on Serverless.FunctionApp. (Right Click on Serverless.FunctionApp project > Properties > Debug > Add BinanceApiKey and BinanceSecretKey with value you just grabbed from binance)
- You can also get variables from local.settings.json file if you'd prefer. In that case, don'f forget the update Serverless.FunctionApp.Startup class.
- Install Azure Functions Emulator (if you don't have it) and run in on your machine.
- Run Serverless.FunctionApp and Serverless.BlazorWASM project at the same time. (Right click on solution > Set Startup projects > Select Multiple startup projects > Set Action to Serverless.FunctionApp and Serverless.BlazorWASM. It would be better if Serverless.FunctionApp project is above to Serverless.BlazorWASM.)

![Screenshot](https://github.com/mehmettahameral/Binance.Net/blob/serverless/Examples/screenshot.png?raw=true)

- Navigate to /fetchdata by clicking BTC/USD link on the nav menu
- Wait 5 seconds for Azure Functions to initialize Binance api connection. Then the price refresh itself every 3 seconds. (You can change it )
