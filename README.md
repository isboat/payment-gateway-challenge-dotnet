# Instructions for candidates

This is the .NET version of the Payment Gateway challenge. If you haven't already read this [README.md](https://github.com/cko-recruitment/) on the details of this exercise, please do so now. 

## Template structure
```
src/
    PaymentGateway.Api - a skeleton ASP.NET Core Web API
test/
    PaymentGateway.Api.Tests - an empty xUnit test project
imposters/ - contains the bank simulator configuration. Don't change this

.editorconfig - don't change this. It ensures a consistent set of rules for submissions when reformatting code
docker-compose.yml - configures the bank simulator
PaymentGateway.sln
```

Feel free to change the structure of the solution, use a different test library etc.

# Potential API security
## Using API key
An API key is like a unique password used to identify and authenticate a user or application making requests to an API. 
It ensures that only authorized users can access the API’s services and data/resources.

## Behind API Management service
API Management services provide a way for organizations to manage, secure, and analyze APIs, 
ensuring that APIs are easily accessible, scalable, and reliable for traffic management, analytic and security.


# How to run it locally
## Via docker
### Payment Gateway
To locally run this payment gateway project, run the docker-compose.yml on the command line using `docker-compose up -d`.
It will generate the image and run the docker container, alongside the dependencies. 

### Banking simulator
It will also run the simulator

The service will run on `http://localhost:8081`. To test the APIs, You can choose your preferred API Client (Postman, cURL, etc), 
or try out the file `requests.http`. Just remember to replace the payment-id placeholder with the one you created.

Currently, it supports both get and post for this endpoint:
- GET `api/Payments/{id}`
- POST `api/Payments`:

Example of curl request:
```curl
curl -X POST http://localhost:8081/api/Payments \
-H "Content-Type: application/json" \
-d '{
  "card_number": "111111111111111111",
  "expiry_month": "12",
  "expiry_year": "2028",
  "currency": "GBP",
  "amount": 100,
  "cvv": "123"
}'
```

## FluentValidation
FluentValidation is a .NET library for building strongly-typed validation rules.
The reason for not using it, is that there's a note on the fluentvalidation.net  that states the following
```
If you use FluentValidation in a commercial project, 
please sponsor the project financially. 
FluentValidation is developed for free by @JeremySkinner in his spare time and financial sponsorship helps keep the project going. 
Please sponsor the project via either GitHub sponsors or OpenCollective.
```
