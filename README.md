# SignalR Basic Flow
These steps form the basic flow of SignalR, allowing real-time communication and updates between the server and connected clients:

## 1. Create SignalR Hub
The first step is to create a SignalR Hub, which acts as a central communication point in the SignalR application. The Hub is responsible for handling incoming and outgoing messages between the server and connected clients.

![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/b318cf66-c7b6-4255-ab5c-235e7e67dbd5)

![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/20fee0ec-61f9-4d85-92a6-ca36afb329b0)


## 2. Add Methods to Hub
In this step, you add methods to the SignalR Hub. These methods define the actions that can be invoked by clients or the server. Each method represents a specific functionality or operation that can be performed through SignalR.

![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/74be0422-c309-48b6-afef-fc3a7cd443d1)

## 3. Add Client side SignalR
To enable SignalR functionality on the client side, you need to include the SignalR JavaScript library in your web application. This library provides the necessary functions and utilities to establish a connection with the SignalR Hub and interact with it.

![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/cfdeafe2-4ca8-43ea-8c7b-7e453b907ecb)

## 4. Connect to SignalR Hub from Client JS
Using the SignalR library on the client side, you establish a connection to the SignalR Hub. This connection enables real-time communication between the server and the connected clients. The client-side code typically includes a method to establish the connection, passing the necessary configuration options.

![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/f3e939b2-8ec1-4b24-a1a9-2719ab234ec7)

## 5. Call SignalR Hub method
Once the client is connected to the SignalR Hub, it can invoke methods defined in the Hub. This allows the client to send requests or trigger specific actions on the server-side. The client-side code can call these methods using the SignalR library's provided functions.

![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/6f75c2d7-b124-4fbb-8b2f-e95faad01e20)

## 6. SignalR Hub invokes method in Client JS to notify clients
When the SignalR Hub receives a method invocation from a client, it can perform any necessary processing and invoke other methods in the connected clients. This enables the server to send real-time updates or notifications to the clients. The SignalR Hub uses the client's connection to invoke the corresponding client-side method.

![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/7a3c90ac-e5f4-4150-9e48-e9133a8cbe7b)

## 7. Client receives update from SignalR hub and performs action
Finally, the client receives the update or notification sent by the SignalR Hub. The client-side method invoked by the Hub can handle the received data and perform the necessary actions, such as updating the user interface, displaying notifications, or triggering further operations based on the received update.

![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/ebfd30e1-a171-4f30-b1c4-adf1cbc97c03)
