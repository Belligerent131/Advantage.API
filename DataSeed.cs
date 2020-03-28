using Advantage.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advantage.API
{
  public class DataSeed
  {
    private readonly ApiContext _ctx;

    public DataSeed(ApiContext ctx)
    {
      _ctx = ctx;
    }

    public void SeedData(int nCustomers, int nOrders)
    {
      if (!_ctx.Customers.Any())
      {
        SeedCustomers(nCustomers);
        _ctx.SaveChanges();
      }

      if (!_ctx.Orders.Any())
      {
        SeedOrders(nOrders);
        _ctx.SaveChanges();
      }

      if (!_ctx.Servers.Any())
      {
        SeedServers();
        _ctx.SaveChanges();
      }

      _ctx.SaveChanges();
    }

    private void SeedOrders(int n)
    {
      List<Order> orders = BuildOrderList(n);

      foreach (var order in orders)
      {
        _ctx.Orders.Add(order);
      }
    }

    private void SeedServers()
    {
      List<Server> servers = BuildServerList();
      foreach (var server in servers)
      {
        _ctx.Add(server);
      }
    }

    private void SeedCustomers(int n)
    {
      List<Customer> customers = BuildCustomerList(n);

      foreach (var customer in customers)
      {
        _ctx.Customers.Add(customer);
      }
    }

    private List<Customer> BuildCustomerList(int nCustomers)
    {
      var customers = new List<Customer>();
      var names = new List<string>();

      for (var i = 1; i <= nCustomers; i++)
      {
        var name = Helpers.MakeUniqueCustomerName(names);
        names.Add(name);

        customers.Add(new Customer
        {
          Id = i,
          Name = name,
          Email = Helpers.MakeCustomerEmail(name),
          State = Helpers.GetRandomState()
        });
      }
      return customers;
    }

    private List<Server> BuildServerList()
    {
      return new List<Server>()
      {
        new Server
        {
          Id = 1,
          Name = "Dev-Web",
          IsOnline = true
        },
        new Server
        {
          Id = 2,
          Name = "Dev-Mail",
          IsOnline = true
        },
        new Server
        {
          Id = 3,
          Name = "Prod-Web",
          IsOnline = false
        },
        new Server
        {
          Id = 4,
          Name = "Prod-Mail",
          IsOnline = true
        },
        new Server
        {
          Id = 5,
          Name = "Dev-Sql",
          IsOnline = true
        },
        new Server
        {
          Id = 6,
          Name = "Prod-Sql",
          IsOnline = false
        },
        new Server
        {
          Id = 7,
          Name = "QA-Web",
          IsOnline = true
        },
        new Server
        {
          Id = 8,
          Name = "QA-Mail",
          IsOnline = true
        },
        new Server
        {
          Id = 9,
          Name = "API-Services",
          IsOnline = false
        },
        new Server
        {
          Id = 10,
          Name = "Backup-Services",
          IsOnline = true
        },
      };
    }

    private List<Order> BuildOrderList(int nOrders)
    {
      var orders = new List<Order>();
      var rand = new Random();

      for (var i = 1; i <= nOrders; i++)
      {
        var randCustomerId = rand.Next(1, _ctx.Customers.Count());
        var placed = Helpers.GetRandomOrderPlaced();
        var completed = Helpers.GetRandomOrderCompleted(placed);
        var customers = _ctx.Customers.ToList();

        orders.Add(new Order
        {
          Id = i,
          Customer = customers.First(c => c.Id == randCustomerId),
          OrderTotal = Helpers.GetRandomOrderTotal(),
          Placed = placed,
          Completed = completed

        });
      }
      return orders;
    }
  }
}
