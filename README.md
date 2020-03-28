# Advantage.API
 
Backend framework of an API server utilizing Kestrel and ASP/MVC systems. Configured using postgres database server, and using microsoft secrets as the connections string.

On first activation, dataSeed will populate the server with generated items; which was utilized in the development.

API strings:

    Orders:
        Get:
            /order/[page]/[limit per page]
            http://localhost:5000/api/order/1/100

            http://localhost:5000/api/order/ByState

            /order/ByCustomer/[Customer Id]
            http://localhost:5000/api/order/ByCustomer/1
        
    Server:
        Get:
            http://localhost:5000/api/server

            server/[server Id]
            http://localhost:5000/api/server/1

        Put:
            http://localhost:5000/api/server/1
      json  {
                "id": 1, //Server Id
                "payloadload": "deactivate" or "activate
            }

    Customer:
        Get:
            http://localhost:5000/api/customer

            customer/[Customer Id]
            http://localhost:5000/api/customer/1



    