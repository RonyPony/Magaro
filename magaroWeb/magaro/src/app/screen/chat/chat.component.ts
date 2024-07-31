import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ChatService } from 'src/app/service/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  @ViewChild('messagecontent') messageContent:ElementRef;
  constructor(
    private readonly _chatService:ChatService
  ) { }

  ngOnInit() {

    this._chatService.connect("juan martinez")
  }

  sendMesage(){
    debugger
    const msj = this.messageContent.nativeElement.value
    this._chatService.sendMessage("juan martinez",msj);
  }

}
