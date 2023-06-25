---
title: Migrate V8 to V9
nav_order: 5
---

## Migrate from version V8.x.x to V9.x.x

There are some breaking changes between V8 and V9, most are detailed in the [CryptoExchange.Net migration guide](https://jkorf.github.io/CryptoExchange.Net/Migration%20Guide.html).

## Credentials
`BinanceApiCredentials` have been changed back to `ApiCredentials` when specifying the credentials in the client options. The base class from CryptoExchange.Net now supports RSA/certificate signing which removes the need to override the `ApiCredentials` class




