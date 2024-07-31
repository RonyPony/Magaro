import { Injectable } from '@angular/core';
import { MessageRequest, Order, OrderState } from '../model/test';
import * as realtime from '@microsoft/signalr';
@Injectable({
  providedIn: 'root'
})
export class RealTimeServiceService {
  private hubConnection: realtime.HubConnection;
  private receiveMessageSubject = new realtime.Subject<Order[]>();
  private userConnectedSubject = new realtime.Subject<Order[]>();
  private userDisconnectedSubject = new realtime.Subject<Order[]>();
constructor() {
  const username = "ronel"
  this.hubConnection = new realtime.HubConnectionBuilder()
    .withUrl('https://localhost:7080/chathub?username='+username) // Replace with your SignalR hub URL
    .build();

  this.hubConnection
    .start()
    .then(() => console.log('Connected to SignalR hub'))
    .catch(err => console.error('Error connecting to SignalR hub:', err));

  this.hubConnection.on('ReceiveMessage', (orders: Order[]) => {
    this.receiveMessageSubject.next(orders);
  });
  this.hubConnection.on('UserConnected', (orders: Order[]) => {
    this.userConnectedSubject.next(orders);
  });
  this.hubConnection.on('UserDisconnected', (orders: Order[]) => {
    this.userDisconnectedSubject.next(orders);
  });
}

async connect(username:string){

}


async sendMessage(user: string, message: string) {
  console.log("sending message");
  await this.hubConnection.invoke('SendMessage', {
    user,
    message
  });
}

async updateFoodItem(orderId: number, state: OrderState) {
  await this.hubConnection.invoke('UpdateFoodItem', orderId, state);
}


}
