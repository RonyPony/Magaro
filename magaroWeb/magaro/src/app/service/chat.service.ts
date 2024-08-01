import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { FoodItem } from '../model/test';
import { RealTimeServiceService } from './realTimeService.service';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ChatService {
  // availableFood = signal<Array<FoodItem>>([]);
  // activeOrders = signal<Array<Order>>([]);
  // activeOrdersSubscription?: Subscription;


constructor(private realtime: RealTimeServiceService,private http:HttpClient) {

}
connect(username:string){

  this.realtime.connect(username);
}
async getOnlineUsers():Promise<any>{
  return this.http.get("https://localhost:7080/getOnlineUsers").toPromise()
  .then(response => {
    console.log(response);
    return response; // Devuelve la respuesta
  })
  .catch(error => {
    console.error('Error al obtener los usuarios en línea:', error);
    throw error; // Lanza el error para manejarlo en el lugar donde se llame a este método
  });

}
async sendMessage(user: string, message: string) {

  this.realtime.sendMessage(user,message)
}
}
