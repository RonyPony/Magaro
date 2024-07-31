import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { FoodItem } from '../model/test';
import { RealTimeServiceService } from './realTimeService.service';



@Injectable({
  providedIn: 'root'
})
export class ChatService {
  // availableFood = signal<Array<FoodItem>>([]);
  // activeOrders = signal<Array<Order>>([]);
  // activeOrdersSubscription?: Subscription;


constructor(private realtime: RealTimeServiceService) {

}
connect(username:string){

  this.realtime.connect(username);
}
async sendMessage(user: string, message: string) {

  this.realtime.sendMessage(user,message)
}
}
