﻿// using Customers.Api.Domain;
using FinBud_Backend.Mapping;
using FinBud_Backend.Models;
using FinBud_Backend.Repositories;
// using FluentValidation;
// using FluentValidation.Results;

namespace FinBud_Backend.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<bool> CreateAsync(Client client)
    {
        // var existingUser = await _customerRepository.GetAsync(customer.Id.Value);
        // if (existingUser is not null)
        // {
        //     var message = $"A user with id {customer.Id} already exists";
        //     throw new ValidationException(message, new []
        //     {
        //         new ValidationFailure(nameof(Customer), message)
        //     });
        // }

        var clientDto = client.ToClientDto();
        return await _clientRepository.CreateAsync(clientDto);
    }

    // public async Task<Client?> GetAsync(Guid id)
    // {
    //     var customerDto = await _clientRepository.GetAsync(id);
    //     return customerDto?.ToCustomer();
    // }

    // public async Task<bool> UpdateAsync(Customer customer)
    // {
    //     var customerDto = customer.ToCustomerDto();
    //     return await _customerRepository.UpdateAsync(customerDto);
    // }

    // public async Task<bool> DeleteAsync(Guid id)
    // {
    //     return await _customerRepository.DeleteAsync(id);
    // }
}
