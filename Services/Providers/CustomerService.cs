using CustomerPlatform.Dtos;
using CustomerPlatform.Models;
using CustomerPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerPlatform.Services.Providers
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _dbContext;
        private readonly AuditLogContext _auditLogDbContext;

        public CustomerService(CustomerContext dbContext, AuditLogContext auditLogDbContext)
        {
            _dbContext = dbContext;
            _auditLogDbContext = auditLogDbContext;
        }

        public async Task<PaginatedResult<Customer>> GetCustomersAsync(int pageNumber, int pageSize)
        {
            if (_dbContext.Customers == null)
                return new PaginatedResult<Customer>(new List<Customer>(), 0, pageNumber, pageSize);

            var totalRecords = await _dbContext.Customers.CountAsync();
            var customers = await _dbContext.Customers
                .OrderByDescending(c => c.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Customer>(customers, totalRecords, pageNumber, pageSize);
        }

        public async Task<Customer?> GetCustomerByIdAsync(string id)
        {
            return _dbContext.Customers == null ? null : await _dbContext.Customers.FindAsync(id);
        }

        public async Task<Customer?> AddCustomerAsync(SaveCustomerRequest request)
        {
            if (_dbContext.Customers == null) return null;

            var customer = new Customer
            {
                Name = request.Name,
                Description = request.Description,
                ContactInformation = new ContactInformation
                {
                    Email = request.ContactInformation.Email,
                    PrimaryMobileNumber = request.ContactInformation.PrimaryMobileNumber,
                    SecondaryMobileNumber = request.ContactInformation.SecondaryMobileNumber,
                    Address = request.ContactInformation.Address
                },
                CurrentBalance = request.CurrentBalance,
                CreatedAt = DateTime.UtcNow
            };

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            var auditLog = new AuditLog
            {
                Action = $"New customer {customer.Name} with mobile number {customer.ContactInformation.PrimaryMobileNumber} added at {customer.CreatedAt}",
                LogDate = DateTime.UtcNow
            };

            await _auditLogDbContext.AuditLogs!.AddAsync(auditLog);
            await _auditLogDbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> UpdateCustomerAsync(string id, UpdateCustomerRequest updatedCustomer)
        {
            var customer = await _dbContext.Customers!.FindAsync(id);
            if (customer == null) return null;

            //for audit log
            var previousCustomerState = new
            {
                customer.Name,
                customer.Description,
                customer.ContactInformation.Email,
                customer.ContactInformation.PrimaryMobileNumber,
                customer.ContactInformation.SecondaryMobileNumber,
                customer.ContactInformation.Address,
                customer.CurrentBalance
            };

            //update customer details
            customer.Name = updatedCustomer.Name;
            customer.Description = updatedCustomer.Description;
            customer.ContactInformation.Email = updatedCustomer.ContactInformation.Email;
            customer.ContactInformation.PrimaryMobileNumber = updatedCustomer.ContactInformation.PrimaryMobileNumber;
            customer.ContactInformation.SecondaryMobileNumber = updatedCustomer.ContactInformation.SecondaryMobileNumber;
            customer.ContactInformation.Address = updatedCustomer.ContactInformation.Address;
            customer.CurrentBalance = updatedCustomer.CurrentBalance;
            customer.LastUpdatedAt = updatedCustomer.LastUpdatedAt;

            await _dbContext.SaveChangesAsync();

            var auditLog = new AuditLog
            {
                Action = $"Previous State: {previousCustomerState}; Updated State: " +
                  $"{{ Name: {updatedCustomer.Name}, Description: {updatedCustomer.Description}, " +
                  $"Email: {updatedCustomer.ContactInformation.Email}, PrimaryMobileNumber: {updatedCustomer.ContactInformation.PrimaryMobileNumber}, " +
                  $"SecondaryMobileNumber: {updatedCustomer.ContactInformation.SecondaryMobileNumber}, " +
                  $"Address: {updatedCustomer.ContactInformation.Address}, CurrentBalance: {updatedCustomer.CurrentBalance} }}",
                LogDate = DateTime.UtcNow
            };

            await _auditLogDbContext.AuditLogs!.AddAsync(auditLog);
            await _auditLogDbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var customer = await _dbContext.Customers!.FindAsync(id);
            if (customer == null) return false;

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();

            var auditLog = new AuditLog
            {
                Action = $"Customer {customer.Name} with mobile number {customer.ContactInformation.PrimaryMobileNumber} removed from our database at {DateTime.UtcNow}",
                LogDate = DateTime.UtcNow
            };

            await _auditLogDbContext.AuditLogs!.AddAsync(auditLog);
            await _auditLogDbContext.SaveChangesAsync();

            return true;
        }
    }
}
    

