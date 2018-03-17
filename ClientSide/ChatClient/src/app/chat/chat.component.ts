import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { HubConnection } from '@aspnet/signalr-client';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
  moduleId: module.id
})
export class ChatComponent implements OnInit {
  private _hubConnection: HubConnection;
  nick = '';
  message = '';
  messages: string[] = [];
  currentUser: User;
  constructor(private userService: UserService) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }
  ngOnInit() {
    this.nick = this.currentUser.username;

    this._hubConnection = new HubConnection('http://localhost:5035/chat');
    console.log('Connecting');
    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
        const text = `${nick}: ${receivedMessage}`;
        this.messages.push(text);
      });
    }

    public sendMessage(): void {
      this._hubConnection
        .invoke('sendToAll', this.nick, this.message)
        .catch(err => console.error(err));
  }
}
