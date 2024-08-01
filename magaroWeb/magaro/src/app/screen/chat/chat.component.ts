import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ChatService } from 'src/app/service/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  @ViewChild('messagecontent') messageContent:ElementRef;
  onlineUsers:any;
  messages:string[]=[];
  constructor(
    private readonly _chatService:ChatService
  ) { }

  async ngOnInit() {
    this._chatService.connect("juan martinez")
    await this._chatService.getOnlineUsers().then(users=>{

      console.log(users)
      this.onlineUsers = users
    })


  }

  sendMesage(){
    const msj = this.messageContent.nativeElement.value
    this.messageContent.nativeElement.value = ''
    this.messages.push(msj)
    this._chatService.sendMessage("juan martinez",msj);
  }

}
